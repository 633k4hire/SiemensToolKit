using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiemensToolKit.Controls;

namespace SiemensToolKit
{
    public partial class Loader : Form
    {
        public static Sharp7.S7Client Client;
        private static Loader m_instance;
        public static Loader _Instance
        {
            get { return m_instance; }
        }
        public Loader()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            m_instance = this;
            Client = new Sharp7.S7Client();
            createDM("Visualizer", Client, new Visualizer(), Properties.Resources.data_access_bannerv2);
        }
        public bool Connected = false;
        public void createDM(string Name, Sharp7.S7Client client, SiemensForm Form, Image Image = null, object obj = null)
        {
            Controls.DmData dmd = new DmData();
            dmd.Name = Name;
            dmd.Client = client;
            dmd.Icon = Image;
            dmd.Form = Form;
            Form.Client = client;
            //dmd.Form.sendPacket = cloud.sendPacket;
            //dmd.Form.getConnection = cloud.getConnection;
            DiagnosticModule dm = new DiagnosticModule(dmd);
            if (obj is Module)
            {
                dm.AssemblyModule = (Module)obj;
            }
            FLOW.Controls.Add(dm);
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            int Result;
            int Rack = System.Convert.ToInt32(TxtRack.Text);
            int Slot = System.Convert.ToInt32(TxtSlot.Text);

            Result = Client.ConnectTo(TxtIP.Text, Rack, Slot);
            Client.ShowResult(Result);
            if (Result == 0)
            {
                output.Text = output.Text + " PDU Negotiated : " + Client.PduSizeNegotiated.ToString();
                TxtIP.Enabled = false;
                TxtRack.Enabled = false;
                TxtSlot.Enabled = false;
                ConnectBtn.Enabled = false;
                DisconnectBtn.Enabled = true;
                Status.Value = 100;
                Connected = true;
            }
        }

        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            Client.Disconnect();
            output.Text = "Disconnected";
            TxtIP.Enabled = true;
            TxtRack.Enabled = true;
            TxtSlot.Enabled = true;
            ConnectBtn.Enabled = true;
            DisconnectBtn.Enabled = false;
            Status.Value = 0;
            Connected = false;
        }
        private bool exit = false;
        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exit)
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.Invalidate();
                this.Hide();
            }

        }
        private void FLOW_ControlAdded(object sender, ControlEventArgs e)
        {
            if (FLOW.Controls.Count > 2)
            {
                Width = Width + 19;
            }
            else
            {
                if (Width > 315)
                {
                    Width = Width - 19;
                }
            }
        }

        private void taskBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.BringToFront();
            this.ShowInTaskbar = true;
            this.Focus();
        }
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exit = true;
            this.Close();
        }

        private void openLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.BringToFront();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit = true;
            this.Close();
        }
    }
}
