﻿namespace Lead.Detect.ThermoAOI2.MachineA
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lGCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报警ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.更改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllToolStripMenuItem,
            this.数据文件夹ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAllToolStripMenuItem.Text = "保存";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // 数据文件夹ToolStripMenuItem
            // 
            this.数据文件夹ToolStripMenuItem.Name = "数据文件夹ToolStripMenuItem";
            this.数据文件夹ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.数据文件夹ToolStripMenuItem.Text = "数据文件夹";
            this.数据文件夹ToolStripMenuItem.Click += new System.EventHandler(this.数据文件夹ToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lGCToolStripMenuItem,
            this.日志ToolStripMenuItem,
            this.报警ToolStripMenuItem,
            this.显示全部ToolStripMenuItem,
            this.更改密码ToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this.windowToolStripMenuItem.Text = "Machine";
            // 
            // lGCToolStripMenuItem
            // 
            this.lGCToolStripMenuItem.Name = "lGCToolStripMenuItem";
            this.lGCToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lGCToolStripMenuItem.Text = "主界面";
            this.lGCToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // 日志ToolStripMenuItem
            // 
            this.日志ToolStripMenuItem.Name = "日志ToolStripMenuItem";
            this.日志ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.日志ToolStripMenuItem.Text = "日志";
            this.日志ToolStripMenuItem.Click += new System.EventHandler(this.日志ToolStripMenuItem_Click);
            // 
            // 报警ToolStripMenuItem
            // 
            this.报警ToolStripMenuItem.Name = "报警ToolStripMenuItem";
            this.报警ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.报警ToolStripMenuItem.Text = "报警";
            this.报警ToolStripMenuItem.Click += new System.EventHandler(this.报警ToolStripMenuItem_Click);
            // 
            // 显示全部ToolStripMenuItem
            // 
            this.显示全部ToolStripMenuItem.Name = "显示全部ToolStripMenuItem";
            this.显示全部ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.显示全部ToolStripMenuItem.Text = "显示全部";
            this.显示全部ToolStripMenuItem.Click += new System.EventHandler(this.显示全部ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionToolStripMenuItem1});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(55, 21);
            this.helpToolStripMenuItem1.Text = "About";
            // 
            // versionToolStripMenuItem1
            // 
            this.versionToolStripMenuItem1.Name = "versionToolStripMenuItem1";
            this.versionToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.versionToolStripMenuItem1.Text = "Version";
            this.versionToolStripMenuItem1.Click += new System.EventHandler(this.versionToolStripMenuItem1_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.ActiveAutoHideContent = null;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.mainPanel.Location = new System.Drawing.Point(0, 25);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1008, 704);
            this.mainPanel.TabIndex = 6;
            // 
            // 更改密码ToolStripMenuItem
            // 
            this.更改密码ToolStripMenuItem.Name = "更改密码ToolStripMenuItem";
            this.更改密码ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.更改密码ToolStripMenuItem.Text = "更改密码";
            this.更改密码ToolStripMenuItem.Click += new System.EventHandler(this.更改密码ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private WeifenLuo.WinFormsUI.Docking.DockPanel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lGCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报警ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示全部ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改密码ToolStripMenuItem;
    }
}