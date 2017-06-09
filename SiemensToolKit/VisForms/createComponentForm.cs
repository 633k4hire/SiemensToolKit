using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SiemensToolKit
{
    
    
    public partial class createComponentForm : Form
    {
        public Image image;
        public VisualComponent visualComponent = VisualComponent.Create();
        public  createComponentForm(string name="Component",Image img = null, int serial =0)
        {
            InitializeComponent();                   
            visualComponent.SerialNumber =description.Text = serial.ToString();
            outputName.Text = visualComponent.Name = name;
            image = visualComponent.Image = img;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg|BMP|*.bmp|PNG|*.png|All Files|*.*";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.ShowDialog();
            if (!ofd.FileName.Equals(""))
            {
                imgbox.Image = image= Image.FromFile(ofd.FileName);
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (image==null)
            {
                MessageBox.Show("Please Select Image");
            }
            else
            {                
                visualComponent.Name = outputName.Text;
                visualComponent.SerialNumber = description.Text;
                visualComponent.Image = image;
                this.Hide();
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
