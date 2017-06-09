using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SiemensToolKit.Controls
{
    public class DiagnosticModule : Panel
    {

        public Sharp7.S7Client Client;
        private Module m_module;
        public Module AssemblyModule
        {
            get { return m_module; }
            set
            {
                m_module = value;
            }
        }
        private SiemensForm m_ff;
        private SiemensForm m_tempff;
        public SiemensForm ChildForm
        {
            get { return m_ff; }
            set
            {
                m_ff = value;
                //Set Delegates here for when opening child
            }
        }
        //delegates for handle
        private DmData m_dmdata = new DmData();
        private Image m_icon = null;
        private Button m_button = new Button();
        private PictureBox m_imgbox = new PictureBox();
        private ToolTip m_tooltip = new ToolTip();
        public void OpenChildForm()
        {
            if (ChildForm != null)
            {
                try
                {
                    if (ChildForm.IsDisposed)
                    {
                        // get type information

                        var type = ChildForm.GetType();
                        // get all constructors
                        var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.CreateInstance);
                        //invoke second ctor that passes the handle, or call first one with no arguments and pass HANDLE after <Cast>
                        object obj = null;
                        if (ctors.Length > 1)
                        {
                            obj = ctors[1].Invoke(new object[] { Client });
                        }
                        else
                        {
                            obj = ctors[0].Invoke((new object[] { }));
                        }

                        ChildForm = (SiemensForm)obj;
                        //ChildForm = m_tempff;
                        //ChildForm.HANDLE = HANDLE;
                        ChildForm.StartPosition = FormStartPosition.CenterScreen;
                        ChildForm.Show();
                    }
                    else
                    {
                        ChildForm.Client = Client;
                        ChildForm.StartPosition = FormStartPosition.CenterScreen;
                        ChildForm.Show();
                    }
                }
                catch
                {

                }
            }
        }
        public void OpenChildForm_Click(object sender, EventArgs e)
        {
            OpenChildForm();
        }
        private ContextMenuStrip VcpContextMenu = new System.Windows.Forms.ContextMenuStrip();


        private void Rename_Click(object sender, EventArgs e)
        {
            if (this.RenameClick != null)
                this.RenameClick(this, e);
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (this.RemoveClick != null)
                this.RemoveClick(this, e);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (this.SaveClick != null)
                this.SaveClick(this, e);
        }
        public DiagnosticModule(DmData data)
        {
            m_dmdata = data;
            Client = m_dmdata.Client;
            ChildForm = data.Form;

            m_tempff = data.Form;
            Text = data.Name;
            //m_button.Text = data.Name;
            m_icon = data.Icon;
            m_imgbox.Image = data.Icon;

            this.Size = new System.Drawing.Size(280, 150);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = data.Name;
            m_tooltip.IsBalloon = true;
            m_tooltip.UseFading = true;
            m_tooltip.SetToolTip(this, data.Description);
            m_tooltip.SetToolTip(m_button, data.Description);
            m_tooltip.SetToolTip(m_imgbox, data.Description);
            m_imgbox.Size = new Size(274, 118);
            m_imgbox.Location = new Point(3, 3);
            m_imgbox.BorderStyle = BorderStyle.Fixed3D;
            m_imgbox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_imgbox.DoubleClick += Button_Click;
            m_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom)));
            m_imgbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top)));
            m_button.Size = new Size(274, 26);
            m_button.Location = new Point(3, 121);
            m_button.Click += Button_Click;
            m_button.Font = new System.Drawing.Font("BankGothic Md BT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(m_imgbox);
            this.Controls.Add(m_button);


            Invalidate();

        }
        public DiagnosticModule()
        {
            this.Size = new System.Drawing.Size(280, 150);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = "Default";

            m_tooltip.IsBalloon = true;
            m_tooltip.UseFading = true;
            m_tooltip.SetToolTip(this, "");
            m_tooltip.SetToolTip(m_button, "");
            m_tooltip.SetToolTip(m_imgbox, "");
            m_imgbox.Size = new Size(274, 118);
            m_imgbox.Location = new Point(3, 3);
            m_imgbox.BorderStyle = BorderStyle.Fixed3D;
            m_imgbox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom)));
            m_imgbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top)));
            m_button.Size = new Size(274, 26);
            m_button.Location = new Point(3, 121);
            m_button.Click += Button_Click;
            m_button.Text = "View";
            m_button.Font = new System.Drawing.Font("BankGothic Md BT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(m_imgbox);
            this.Controls.Add(m_button);
            Invalidate();
        }
        public DmData DmData
        {
            get { return m_dmdata; }
            set { m_dmdata = value; Icon = value.Icon; ToolTip = value.Description; Text = value.Name; }
        }
        public Image Icon
        {
            get { return m_icon; }
            set { m_icon = value; m_imgbox.Image = value; Invalidate(); }
        }
        public string ToolTip
        {
            get { return m_tooltip.GetToolTip(this); }
            set { m_tooltip.SetToolTip(this, value); Invalidate(); }
        }
        public override string Text
        {
            get { return m_button.Text; }
            set { m_button.Text = value; Invalidate(); }
        }
        public event EventHandler ViewClick;
        public event EventHandler SaveClick;
        public event EventHandler RemoveClick;
        public event EventHandler RenameClick;
        protected void Button_Click(object sender, EventArgs e)
        {
            
            if (ChildForm == null)
            {
                ChildForm = new SiemensForm(Client);
                ChildForm.StartPosition = FormStartPosition.CenterScreen;
                ChildForm.Show();
            }
            else
            {
                try
                {
                    if (ChildForm.IsDisposed)
                    {
                        // get type information

                        var type = ChildForm.GetType();
                        // get all constructors
                        var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.CreateInstance);
                        //invoke second ctor that passes the handle, or call first one with no arguments and pass HANDLE after <Cast>
                        var obj = ctors[1].Invoke(new object[] { Client });

                        ChildForm = (SiemensForm)obj;
                        //ChildForm = m_tempff;
                        //ChildForm.HANDLE = HANDLE;
                        ChildForm.StartPosition = FormStartPosition.CenterScreen;
                        ChildForm.Show();
                    }
                    else
                    {
                        ChildForm.Client = Client;
                        ChildForm.StartPosition = FormStartPosition.CenterScreen;
                        ChildForm.Show();
                    }
                }
                catch
                {

                }
            }

            //bubble the event up to the parent
            if (this.ViewClick != null)
                this.ViewClick(this, e);
        }

    }
    [Serializable]
    public class DmData
    {
        public SiemensForm Form;
        public Image Icon;
        public string Name;
        public string Description;
        public object Tag;
        public Sharp7.S7Client Client;
    }

}
