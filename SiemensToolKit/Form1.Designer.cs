namespace SiemensToolKit
{
    partial class Loader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loader));
            this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.cloudToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.FLOW = new System.Windows.Forms.FlowLayoutPanel();
            this.output = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripProgressBar();
            this.ServerConnection = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ethernetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtIP = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtSlot = new System.Windows.Forms.ToolStripTextBox();
            this.TxtRack = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ConnectBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ModuleList = new System.Windows.Forms.ToolStripMenuItem();
            this.addModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.connectToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tray
            // 
            this.Tray.BalloonTipText = "Siemens ToolKit";
            this.Tray.ContextMenuStrip = this.iconMenu;
            this.Tray.Icon = ((System.Drawing.Icon)(resources.GetObject("Tray.Icon")));
            this.Tray.Text = "Siemens ToolKit";
            this.Tray.Visible = true;
            this.Tray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskBar_MouseDoubleClick);
            // 
            // iconMenu
            // 
            this.iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLauncherToolStripMenuItem,
            this.toolStripSeparator7,
            this.cloudToolStripMenuItem1,
            this.toolStripSeparator8,
            this.exitToolStripMenuItem1});
            this.iconMenu.Name = "iconMenu";
            this.iconMenu.Size = new System.Drawing.Size(156, 82);
            // 
            // openLauncherToolStripMenuItem
            // 
            this.openLauncherToolStripMenuItem.Name = "openLauncherToolStripMenuItem";
            this.openLauncherToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openLauncherToolStripMenuItem.Text = "Open Launcher";
            this.openLauncherToolStripMenuItem.Click += new System.EventHandler(this.openLauncherToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(152, 6);
            // 
            // cloudToolStripMenuItem1
            // 
            this.cloudToolStripMenuItem1.Enabled = false;
            this.cloudToolStripMenuItem1.Name = "cloudToolStripMenuItem1";
            this.cloudToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.cloudToolStripMenuItem1.Text = "Cloud";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // FLOW
            // 
            this.FLOW.AutoScroll = true;
            this.FLOW.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FLOW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FLOW.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FLOW.Location = new System.Drawing.Point(0, 24);
            this.FLOW.Name = "FLOW";
            this.FLOW.Size = new System.Drawing.Size(285, 327);
            this.FLOW.TabIndex = 19;
            this.FLOW.WrapContents = false;
            this.FLOW.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FLOW_ControlAdded);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.output.Location = new System.Drawing.Point(0, 351);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(285, 44);
            this.output.TabIndex = 18;
            this.output.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.ServerConnection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 395);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(285, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Status
            // 
            this.Status.AutoToolTip = true;
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(125, 16);
            this.Status.ToolTipText = "PLC Connection Status";
            // 
            // ServerConnection
            // 
            this.ServerConnection.Name = "ServerConnection";
            this.ServerConnection.Size = new System.Drawing.Size(125, 16);
            this.ServerConnection.ToolTipText = "Server Connection";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(285, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.ethernetToolStripMenuItem,
            this.DisconnectBtn,
            this.toolStripSeparator1,
            this.ModuleList,
            this.toolStripSeparator2,
            this.cloudToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuToolStripMenuItem.Image = global::SiemensToolKit.Properties.Resources._45_Menu_128;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPIToolStripMenuItem,
            this.dPToolStripMenuItem,
            this.oPIToolStripMenuItem});
            this.connectToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // mPIToolStripMenuItem
            // 
            this.mPIToolStripMenuItem.Enabled = false;
            this.mPIToolStripMenuItem.Name = "mPIToolStripMenuItem";
            this.mPIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mPIToolStripMenuItem.Text = "MPI";
            // 
            // dPToolStripMenuItem
            // 
            this.dPToolStripMenuItem.Enabled = false;
            this.dPToolStripMenuItem.Name = "dPToolStripMenuItem";
            this.dPToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dPToolStripMenuItem.Text = "DP";
            // 
            // oPIToolStripMenuItem
            // 
            this.oPIToolStripMenuItem.Enabled = false;
            this.oPIToolStripMenuItem.Name = "oPIToolStripMenuItem";
            this.oPIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.oPIToolStripMenuItem.Text = "OPI";
            // 
            // ethernetToolStripMenuItem
            // 
            this.ethernetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TxtIP,
            this.TxtSlot,
            this.TxtRack,
            this.toolStripSeparator3,
            this.ConnectBtn});
            this.ethernetToolStripMenuItem.Name = "ethernetToolStripMenuItem";
            this.ethernetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ethernetToolStripMenuItem.Text = "Ethernet";
            this.ethernetToolStripMenuItem.ToolTipText = "Rack";
            // 
            // TxtIP
            // 
            this.TxtIP.Name = "TxtIP";
            this.TxtIP.Size = new System.Drawing.Size(160, 22);
            this.TxtIP.Text = "127.0.0.1";
            this.TxtIP.ToolTipText = "IP";
            // 
            // TxtSlot
            // 
            this.TxtSlot.Name = "TxtSlot";
            this.TxtSlot.Size = new System.Drawing.Size(100, 23);
            this.TxtSlot.Text = "0";
            this.TxtSlot.ToolTipText = "Slot";
            // 
            // TxtRack
            // 
            this.TxtRack.Name = "TxtRack";
            this.TxtRack.Size = new System.Drawing.Size(100, 23);
            this.TxtRack.Text = "2";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(160, 22);
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(152, 22);
            this.DisconnectBtn.Text = "Disconnect";
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // ModuleList
            // 
            this.ModuleList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addModuleToolStripMenuItem,
            this.toolStripSeparator4});
            this.ModuleList.Enabled = false;
            this.ModuleList.Name = "ModuleList";
            this.ModuleList.Size = new System.Drawing.Size(152, 22);
            this.ModuleList.Text = "Modules";
            // 
            // addModuleToolStripMenuItem
            // 
            this.addModuleToolStripMenuItem.Name = "addModuleToolStripMenuItem";
            this.addModuleToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addModuleToolStripMenuItem.Text = "Add Module";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(138, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // cloudToolStripMenuItem
            // 
            this.cloudToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator6,
            this.connectToolStripMenuItem2});
            this.cloudToolStripMenuItem.Enabled = false;
            this.cloudToolStripMenuItem.Name = "cloudToolStripMenuItem";
            this.cloudToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cloudToolStripMenuItem.Text = "Cloud";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(117, 6);
            // 
            // connectToolStripMenuItem2
            // 
            this.connectToolStripMenuItem2.Name = "connectToolStripMenuItem2";
            this.connectToolStripMenuItem2.Size = new System.Drawing.Size(120, 22);
            this.connectToolStripMenuItem2.Text = "Connect";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 417);
            this.Controls.Add(this.FLOW);
            this.Controls.Add(this.output);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Loader";
            this.Text = "Siemens Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._FormClosing);
            this.iconMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NotifyIcon Tray;
        private System.Windows.Forms.FlowLayoutPanel FLOW;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar ServerConnection;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ethernetToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem TxtIP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ModuleList;
        private System.Windows.Forms.ToolStripMenuItem addModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mPIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPIToolStripMenuItem;
        public System.Windows.Forms.ToolStripTextBox TxtSlot;
        public System.Windows.Forms.ToolStripTextBox TxtRack;
        private System.Windows.Forms.ContextMenuStrip iconMenu;
        private System.Windows.Forms.ToolStripMenuItem openLauncherToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem cloudToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        public System.Windows.Forms.ToolStripProgressBar Status;
        public System.Windows.Forms.RichTextBox output;
        public System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem DisconnectBtn;
        public System.Windows.Forms.ToolStripMenuItem ConnectBtn;
    }
}

