namespace SiemensToolKit.Forms
{
    partial class Settings
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
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Communication");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Advanced");
            this.SettingsContainer = new System.Windows.Forms.SplitContainer();
            this.SettingsTree = new System.Windows.Forms.TreeView();
            this.CommunicationFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BufferSizeChooser = new System.Windows.Forms.NumericUpDown();
            this.AdvancedFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.GeneralFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.WatchIntervalChooser = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsContainer)).BeginInit();
            this.SettingsContainer.Panel1.SuspendLayout();
            this.SettingsContainer.Panel2.SuspendLayout();
            this.SettingsContainer.SuspendLayout();
            this.CommunicationFlow.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BufferSizeChooser)).BeginInit();
            this.AdvancedFlow.SuspendLayout();
            this.GeneralFlow.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WatchIntervalChooser)).BeginInit();
            this.SuspendLayout();
            // 
            // SettingsContainer
            // 
            this.SettingsContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SettingsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsContainer.Location = new System.Drawing.Point(0, 0);
            this.SettingsContainer.Name = "SettingsContainer";
            // 
            // SettingsContainer.Panel1
            // 
            this.SettingsContainer.Panel1.Controls.Add(this.SettingsTree);
            // 
            // SettingsContainer.Panel2
            // 
            this.SettingsContainer.Panel2.Controls.Add(this.CommunicationFlow);
            this.SettingsContainer.Panel2.Controls.Add(this.AdvancedFlow);
            this.SettingsContainer.Panel2.Controls.Add(this.GeneralFlow);
            this.SettingsContainer.Size = new System.Drawing.Size(607, 474);
            this.SettingsContainer.SplitterDistance = 201;
            this.SettingsContainer.TabIndex = 1;
            // 
            // SettingsTree
            // 
            this.SettingsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTree.Location = new System.Drawing.Point(0, 0);
            this.SettingsTree.Name = "SettingsTree";
            treeNode19.Name = "generalNode";
            treeNode19.Text = "General";
            treeNode20.Name = "CommunicationNode";
            treeNode20.Text = "Communication";
            treeNode21.Name = "AdvancedNode";
            treeNode21.Text = "Advanced";
            this.SettingsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20,
            treeNode21});
            this.SettingsTree.Size = new System.Drawing.Size(197, 470);
            this.SettingsTree.TabIndex = 0;
            this.SettingsTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SettingsTree_NodeMouseDoubleClick);
            // 
            // CommunicationFlow
            // 
            this.CommunicationFlow.Controls.Add(this.groupBox1);
            this.CommunicationFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommunicationFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.CommunicationFlow.Location = new System.Drawing.Point(0, 0);
            this.CommunicationFlow.Name = "CommunicationFlow";
            this.CommunicationFlow.Size = new System.Drawing.Size(398, 470);
            this.CommunicationFlow.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IO Buffer Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BufferSizeChooser);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 43);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buffer Size";
            // 
            // BufferSizeChooser
            // 
            this.BufferSizeChooser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BufferSizeChooser.Location = new System.Drawing.Point(3, 16);
            this.BufferSizeChooser.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.BufferSizeChooser.Name = "BufferSizeChooser";
            this.BufferSizeChooser.Size = new System.Drawing.Size(99, 20);
            this.BufferSizeChooser.TabIndex = 0;
            this.BufferSizeChooser.Value = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.BufferSizeChooser.ValueChanged += new System.EventHandler(this.BufferSizeChooser_Changed);
            // 
            // AdvancedFlow
            // 
            this.AdvancedFlow.Controls.Add(this.label5);
            this.AdvancedFlow.Controls.Add(this.textBox5);
            this.AdvancedFlow.Controls.Add(this.label6);
            this.AdvancedFlow.Controls.Add(this.textBox6);
            this.AdvancedFlow.Location = new System.Drawing.Point(41, 360);
            this.AdvancedFlow.Name = "AdvancedFlow";
            this.AdvancedFlow.Size = new System.Drawing.Size(200, 100);
            this.AdvancedFlow.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "label5";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(3, 16);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(341, 20);
            this.textBox5.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "label6";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(3, 55);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(341, 20);
            this.textBox6.TabIndex = 3;
            // 
            // GeneralFlow
            // 
            this.GeneralFlow.Controls.Add(this.label1);
            this.GeneralFlow.Controls.Add(this.textBox1);
            this.GeneralFlow.Controls.Add(this.label2);
            this.GeneralFlow.Controls.Add(this.textBox2);
            this.GeneralFlow.Location = new System.Drawing.Point(128, 269);
            this.GeneralFlow.Name = "GeneralFlow";
            this.GeneralFlow.Size = new System.Drawing.Size(200, 100);
            this.GeneralFlow.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(341, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 55);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(341, 20);
            this.textBox2.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.WatchIntervalChooser);
            this.groupBox3.Location = new System.Drawing.Point(117, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(142, 43);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Buffer Refresh Rate (ms)";
            // 
            // WatchIntervalChooser
            // 
            this.WatchIntervalChooser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WatchIntervalChooser.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.WatchIntervalChooser.Location = new System.Drawing.Point(3, 16);
            this.WatchIntervalChooser.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.WatchIntervalChooser.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.WatchIntervalChooser.Name = "WatchIntervalChooser";
            this.WatchIntervalChooser.Size = new System.Drawing.Size(136, 20);
            this.WatchIntervalChooser.TabIndex = 0;
            this.WatchIntervalChooser.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.WatchIntervalChooser.ValueChanged += new System.EventHandler(this.WatchIntervalChooser_ValueChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 474);
            this.Controls.Add(this.SettingsContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.SettingsContainer.Panel1.ResumeLayout(false);
            this.SettingsContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingsContainer)).EndInit();
            this.SettingsContainer.ResumeLayout(false);
            this.CommunicationFlow.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BufferSizeChooser)).EndInit();
            this.AdvancedFlow.ResumeLayout(false);
            this.AdvancedFlow.PerformLayout();
            this.GeneralFlow.ResumeLayout(false);
            this.GeneralFlow.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WatchIntervalChooser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer SettingsContainer;
        private System.Windows.Forms.TreeView SettingsTree;
        private System.Windows.Forms.FlowLayoutPanel GeneralFlow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.FlowLayoutPanel CommunicationFlow;
        private System.Windows.Forms.FlowLayoutPanel AdvancedFlow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown BufferSizeChooser;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown WatchIntervalChooser;
    }
}