using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiemensToolKit.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Pop(GeneralFlow);
        }
        public void Pop(Control control, bool visible=true)
        {
            control.Visible = visible;
            control.Dock = DockStyle.Fill;
            control.BringToFront();
           
        }
        public void LoadValues()
        {
            BufferSizeChooser.Value = Properties.Settings.Default.BufferSize;
            WatchIntervalChooser.Value = Visualizer._Instance.watch.Interval;
        }
        private void SettingsTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            if (e.Node.Text.ToUpper().Contains("GEN"))
            {
                Pop(GeneralFlow);
            }
            if (e.Node.Text.ToUpper().Contains("COM"))
            {
                Pop(CommunicationFlow);
            }
            if (e.Node.Text.ToUpper().Contains("ADV"))
            {
                Pop(AdvancedFlow);
            }
        }

        private void BufferSizeChooser_Changed(object sender, EventArgs e)
        {
            Properties.Settings.Default.BufferSize = Convert.ToInt32(BufferSizeChooser.Value);
        }

        private void WatchIntervalChooser_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WatchInterval = Convert.ToInt32(WatchIntervalChooser.Value);
            Visualizer._Instance.watch.Interval = Convert.ToInt32(WatchIntervalChooser.Value);
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
