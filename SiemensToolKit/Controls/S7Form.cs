using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiemensToolKit.Controls
{
    public class SiemensForm : Form
    {
        //public delegate Connection getCon();
        //public getCon getConnection;
        //public delegate string getStr();
        //public getStr getHostPort;
        //public delegate void connectionDelegate(Packet pack);
        //public connectionDelegate sendPacket;
        //public delegate ushort UpHandle();
        //public UpHandle updateHANDLE;
        public delegate void ConnectedAct(ushort h);
        public ConnectedAct ConnectionAction;
        public delegate void US(string result);
        public US updateDelegate;
        private bool m_connected = false;
        public bool Connected
        {
            get { return m_connected; }
            set { m_connected = value; }
        }
        private DmData m_dm;
        public DmData DmData
        {
            get { return m_dm; }
            set { m_dm = value; }
        }
        public Sharp7.S7Client Client;
        public SiemensForm()
        {
            
        }
        public SiemensForm(Sharp7.S7Client client)
        {
            
        }
    }

}
