﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimSktClient
{
    public class PrimSocketClient : IPrim, ISktClient
    {
        public ClientConfig _config;
        public event PrimDataRefresh OnPrimDataRefresh;
        public event PrimOpLog OnPrimOpLog;
        public event PrimStateChanged OnPrimStateChanged;
        protected virtual int OnOnPrimDataRefresh(string devname, object context)
        {
            OnPrimDataRefresh?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log);
            return 0;
        }

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }
        public event MsgReved OnMsgReved;

        #region Private Feilds

        private Socket _socket;
        private IPAddress _ip;
        private IPEndPoint _port;

        private Task _sendTask;
        private Task _revTask;
        private Task _heartTask;

        private Queue<object> _sendQueue;
        private readonly object _sendQueueMutex = new object();
        private bool _sendFlag;
        private bool NetConnetedSuccess;
        private Queue<object> _revQueue;
        private readonly object _revQueueMutex = new object();
        private bool _revFlag;

        #endregion

        #region Override IPrim Property

        public Type ChildType
        {
            get { return _config.ChildType; }

            set { _config.ChildType = value; }
        }

        public string ConnInfo
        {
            get { return _config.ConnInfo; }

            set { _config.ConnInfo = value; }
        }

        public PrimConnType ConnType
        {
            get { return _config.ConnType; }

            set { _config.ConnType = value; }
        }

        public IDataArea DataArea { set; get; }

        public PrimType HSType
        {
            get { return _config.HSType; }

            set { _config.HSType = value; }
        }

        public Guid PrimId
        {
            get { return _config.Id; }

            set { _config.Id = value; }
        }

        public PrimManufacture Manu
        {
            get { return _config.Manu; }

            set { _config.Manu = value; }
        }

        public string Name
        {
            get { return _config.Name; }

            set { _config.Name = value; }
        }

        public Control PrimConfigUI { get; }

        public PrimConnState PrimConnStat
        {
            get { return _config.PrimConnStat; }

            set { _config.PrimConnStat = value; }
        }

        public bool PrimDebug
        {
            get { return _config.PrimDebug; }

            set { _config.PrimDebug = value; }
        }

        public Control PrimDebugUI { get; }

        public bool PrimEnable
        {
            get { return _config.PrimEnable; }

            set { _config.PrimEnable = value; }
        }

        public Control PrimOutputUI { get; }

        public PrimRunState PrimRunStat
        {
            get { return _config.PrimRunStat; }

            set { _config.PrimRunStat = value; }
        }

        public bool PrimSimulator
        {
            get { return _config.PrimSimulator; }

            set { _config.PrimSimulator = value; }
        }

        public string PrimTypeName
        {
            get { return _config.PrimTypeName; }

            set { _config.PrimTypeName = value; }
        }

        public PrimVer Ver
        {
            get { return _config.Ver; }

            set { _config.Ver = value; }
        }

        #endregion

        #region Override IPrim Function

        public int IPrimInit()
        {
            if (_socket != null) return -1;

            var iRet = 0;

            if (string.IsNullOrEmpty(_config.DstIp)) return -1;

            _ip = IPAddress.Parse(_config.DstIp);
            //IPAddress ip = IPAddress.Any;

            if (string.IsNullOrEmpty(_config.Port)) return -1;
            _port = new IPEndPoint(_ip, int.Parse(_config.Port));

            if (SendQueueCnt <= 0)
                _sendQueue = new Queue<object>();
            else
                _sendQueue = new Queue<object>(SendQueueCnt);


            if (_config.NotifyMode == SktNotifyMode.DataQueue)
            {
                if (RevQueueCnt <= 0)
                    _revQueue = new Queue<object>();
                else
                    _revQueue = new Queue<object>(RevQueueCnt);
            }

            if (_socket == null)
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                PrimRunStat = PrimRunState.Stop;
                PrimConnStat = PrimConnState.UnConnected;
            }


            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            //if (_socket != null)
            //{
            //    return -1;
            //}
            var iRet = 0;
            if (PrimConnStat == PrimConnState.Connected)
            {
                result = Name + "Connected";
                return 0;
            }

            while (true)
            {
                var reconnectCnt = 0;
                try
                {
                    _socket.Connect(_port);
                    result = Name + "Connect Success";
                    PrimConnStat = PrimConnState.Connected;
                    NetConnetedSuccess = true;
                    break;
                }
                catch (Exception )
                {
                    PrimConnStat = PrimConnState.UnConnected;
                    NetConnetedSuccess = false;
                    result = Name + "Connect Fail";
                    reconnectCnt++;
                    if (reconnectCnt >= ReconnectCnt) return -1;
                }
            }

            PrimConnStat = PrimConnState.Connected;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;


            //if (_socket != null)
            //{
            //    return -1;
            //}

            if (PrimConnStat != PrimConnState.Connected) return -1;

            if (PrimRunStat != PrimRunState.Stop) return 0;

            if (_config.HeartInfoCycleTime < 1) _config.HeartInfoCycleTime = 1000;

            if (_sendTask == null)
            {
                _sendTask = new Task(() => CycleSend());
                _sendFlag = true;
                _sendTask.Start();
            }

            if (_revTask == null)
            {
                _revTask = new Task(() => CycleRev());
                _revFlag = true;
                _revTask.Start();
            }

            if (_heartTask == null)
            {
                _heartTask = new Task(() => CycleHeart());
                _heartTask.Start();
            }

            _sendQueue.Enqueue(_config.NetName + ":" + _config.HeartInfo);

            PrimRunStat = PrimRunState.Running;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimDisConnect(ref string result)
        {
            throw new NotImplementedException();
        }

        public int IPrimDispose()
        {
            throw new NotImplementedException();
        }

        public XmlNode ExportConfig()
        {
            //config turn to xmlNode
            if (_config == null) return null;

            var node = XMLHelper.ObjectToXML(_config);

            return node;
        }

        public XmlNode ExportDataConfig()
        {
            throw new NotImplementedException();
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var iRet = 0;
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(ClientConfig)) as ClientConfig;
            else
                return -1;
            return iRet;
        }

        public int ImportDataConfig(XmlNode xmlNode)
        {
            throw new NotImplementedException();
        }

        public int IPrimResume()
        {
            throw new NotImplementedException();
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            throw new NotImplementedException();
        }

        public int IPrimStop()
        {
            if (_socket != null) _socket.Close();

            _socket = null;
            PrimConnStat = PrimConnState.UnConnected;
            PrimRunStat = PrimRunState.Stop;
            NetConnetedSuccess = false;
            //if (_sendTask != null) _sendTask.Dispos;
            //_sendTask = null;
            //if (_revTask != null) _revTask.Dispose();
            //_revTask = null;
            //if (_heartTask != null) _heartTask.Dispose();
            //_heartTask = null;
            //_sendFlag = false;
            //_sendTask.Wait();
            //_sendTask.Dispose();

            return 0;
        }

        public int IPrimSuspend()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overide ISkt Property

        public bool NetConneted
        {
            get
            {
                if (_socket != null)
                    return NetConnetedSuccess;
                return false;
            }
        }

        public string DstIp
        {
            get { return _config.DstIp; }

            set
            {
                _config.DstIp = value;
                ;
            }
        }

        public string Port
        {
            get { return _config.Port; }

            set { _config.Port = value; }
        }

        public int ReconnectCnt
        {
            get { return _config.ReconnectCnt; }

            set { _config.ReconnectCnt = value; }
        }

        public SktNotifyMode NotifyMode
        {
            get { return _config.NotifyMode; }
            set { _config.NotifyMode = value; }
        }

        public string HeartInfo
        {
            set { _config.HeartInfo = value; }
            get { return _config.HeartInfo; }
        }

        public int HeartInfoCycleTime
        {
            set { _config.HeartInfoCycleTime = value; }
            get { return _config.HeartInfoCycleTime; }
        }

        public bool HeartFlag
        {
            set { _config.HeartFlag = value; }
            get { return _config.HeartFlag; }
        }

        public int SendQueueCnt
        {
            set { _config.SendQueueCnt = value; }
            get { return _config.SendQueueCnt; }
        }

        public int RevQueueCnt
        {
            set { _config.RevQueueCnt = value; }
            get { return _config.RevQueueCnt; }
        }

        public string NetName
        {
            set { _config.NetName = value; }
            get { return _config.NetName; }
        }

        #endregion

        #region Overide ISkt Function

        public int SetSendQueueCnt(int cnt)
        {
            var iRet = 0;

            SendQueueCnt = cnt;

            return iRet;
        }

        public int RegNotifyInfo(string regInfo)
        {
            throw new NotImplementedException();
        }

        public int SendInfo(string rev, object context)
        {
            var iRet = 0;

            lock (_sendQueueMutex)
            {
                _sendQueue.Enqueue(context);
            }

            return iRet;
        }

        public int SetFilterInfo(string filterInfo)
        {
            throw new NotImplementedException();
        }

        public int SetHeartInfo(string heart, int cycleTime)
        {
            var iRet = 0;

            HeartInfo = heart;
            HeartInfoCycleTime = cycleTime;

            return iRet;
        }

        public int SetNetName(string name)
        {
            throw new NotImplementedException();
        }

        public int SetNotifyMode(SktNotifyMode mode, int cnt)
        {
            var iRet = 0;

            NotifyMode = mode;
            RevQueueCnt = cnt;
            return iRet;
        }

        #endregion

        #region Constructor

        public PrimSocketClient()
        {
            _config = new ClientConfig();
            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        public PrimSocketClient(XmlNode xmlNode)
        {
            //xmlNode turn to _config
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(ClientConfig)) as ClientConfig;
            else
                return;

            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        #endregion

        #region Private Function

        private void CycleSend()
        {
            while (true)
            {
                //if (PrimConnStat == PrimConnState.UnConnected)
                //{
                //    try
                //    {
                //        _socket.Connect(_port);
                //        break;
                //    }
                //    catch (Exception ex)
                //    {
                //        PrimConnStat = PrimConnState.UnConnected;
                //        Thread.Sleep(100);
                //        continue;
                //    }
                //}

                if (!_sendFlag)
                {
                    Thread.Sleep(2);
                    continue;
                }

                if (_sendQueue.Count < 1)
                {
                    Thread.Sleep(2);
                    continue;
                }

                object sendObj = null;

                lock (_sendQueueMutex)
                {
                    sendObj = _sendQueue.Dequeue();
                }

                if (sendObj == null)
                {
                    Thread.Sleep(2);
                    continue;
                }

                var sendStr = (string) sendObj;

                try
                {
                    var buffer = Encoding.UTF8.GetBytes(sendStr);
                    _socket.Send(buffer);
                }
                catch (Exception )
                {
                    NetConnetedSuccess = false;
                    PrimConnStat = PrimConnState.UnConnected;
                }

                Thread.Sleep(2);
            }
        }

        private void CycleRev()
        {
            while (true)
            {
                if (!_revFlag)
                {
                    Thread.Sleep(2);
                    continue;
                }

                try
                {
                    var buffer = new byte[1024];

                    var n = _socket.Receive(buffer);

                    var revMsg = Encoding.UTF8.GetString(buffer, 0, n);

                    if (PrimDebug) ((PrimConfigControl) PrimConfigUI).ShowRevMsg(revMsg);

                    if (NotifyMode == SktNotifyMode.DataEvent && OnMsgReved != null)
                    {
                        OnMsgReved("", revMsg);
                        PrimConnStat = PrimConnState.Connected;
                        NetConnetedSuccess = true;
                    }

                    if (NotifyMode == SktNotifyMode.DataQueue)
                        lock (_revQueueMutex)
                        {
                            _revQueue.Enqueue(revMsg);
                        }
                }
                catch (Exception )
                {
                    NetConnetedSuccess = false;
                }

                Thread.Sleep(2);
            }
        }

        private void CycleHeart()
        {
            while (true)
            {
                Thread.Sleep(_config.HeartInfoCycleTime);

                if (!_config.HeartFlag) continue;

                lock (_sendQueueMutex)
                {
                    _sendQueue.Enqueue(_config.NetName + ":" + _config.HeartInfo);
                }
            }
        }

        #endregion

        #region Public Function

        #endregion
    }
}