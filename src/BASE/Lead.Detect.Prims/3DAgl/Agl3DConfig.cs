﻿using System;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.Prim3DAgl
{
    public class Agl3DConfig
    {
        #region Override IPrim's Property

        public string Name { set; get; }

        public string PrimTypeName { set; get; }

        public Guid Id { set; get; }

        //Prim 子类的类型
        public Type ChildType { set; get; }

        //Prim的硬件、软件类型
        public PrimType HSType { set; get; }

        public PrimManufacture Manu { set; get; }

        public PrimVer Ver { set; get; }

        public PrimConnType ConnType { set; get; }

        public string ConnInfo { set; get; }

        public PrimConnState PrimConnStat { set; get; }

        public PrimRunState PrimRunStat { set; get; }

        public bool PrimSimulator { set; get; }

        public bool PrimDebug { set; get; }

        public bool PrimEnable { set; get; }

        #endregion
    }
}