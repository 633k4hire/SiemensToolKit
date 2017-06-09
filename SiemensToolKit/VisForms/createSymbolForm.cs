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
    public partial class createSymbolForm : Form
    {
        public List<Symbol> Symbols = new List<Symbol>();
        public createSymbolForm()
        {
            
            InitializeComponent();
            typeBox.Text = "I";
        }

        private void createSymbolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (address.Text=="" || inputName.Text=="")
            {
                MessageBox.Show("Fill out Form Please");
                return;
            }
            Symbol s = new Symbol();
            s.Name = inputName.Text;
            s.Description = description.Text;
            s.Address = address.Text;
            
            //parse out address
            char[] alpha = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };
            string t = s.Address;
            foreach (var a in alpha)
            {
                t = t.Replace(a.ToString(), "");
            }
            var tmp = t.Split('.');
            s.Index = Convert.ToInt32(tmp[0]);
            if (tmp.Count() > 1)
            {
                s.SubIndex = Convert.ToInt32(tmp[1]);
            }
            try
            {
                if (typeBox.Text.ToUpper() == "I")
                {
                    s.Type = DataType.I;
                }else
                if (typeBox.Text.ToUpper() == "Q")
                {
                    s.Type = DataType.Q;
                }
                else
                if (typeBox.Text.ToUpper() == "M")
                {
                    s.Type = DataType.M;
                }
            }
            catch { MessageBox.Show("Error Adding Type Information"); }
            Symbols.Add(s);
            inCount.Text = Symbols.Count.ToString();
            inputName.Text = "";
            description.Text = "";
            address.Text = "";
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Cancelled = false;
            this.Hide();
        }
        public bool Cancelled = true;
        private void createSymbolForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
