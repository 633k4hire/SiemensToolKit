

namespace SiemensToolKit

{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Drawing;
    
    public class VcPanel : Panel
    {
        private VisualComponent m_vc = VisualComponent.Create();
        private Image m_icon = null;
        private Button m_button = new Button();
        private PictureBox m_imgbox = new PictureBox();
        private ToolTip m_tooltip = new ToolTip();
        private ContextMenuStrip VcpContextMenu = new System.Windows.Forms.ContextMenuStrip();

        public VcPanel(VisualComponent vc)
        {
            this.Size = new System.Drawing.Size(75, 100);
            this.BorderStyle = BorderStyle.FixedSingle;
            m_vc = vc;
            this.Name = vc.Name;
            //menuItems
            ToolStripMenuItem save = new ToolStripMenuItem();
            save.Name = "saveToolStripMenuItem";
            save.Size = new System.Drawing.Size(152, 22);
            save.Text = "Save";
            save.Click += Save_Click;

            ToolStripMenuItem remove = new ToolStripMenuItem();
            remove.Name = "removeToolStripMenuItem";
            remove.Size = new System.Drawing.Size(152, 22);
            remove.Text = "Remove";
            remove.Click += Remove_Click;

            ToolStripMenuItem rename = new ToolStripMenuItem();
            rename.Name = "renameToolStripMenuItem";
            rename.Size = new System.Drawing.Size(152, 22);
            rename.Text = "Rename";
            rename.Click += Rename_Click;
            //ContextMenu
            VcpContextMenu = new System.Windows.Forms.ContextMenuStrip();
            VcpContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { save, remove });
            VcpContextMenu.Name = "VcpContextMenu";
            VcpContextMenu.Size = new System.Drawing.Size(153, 120);
            this.ContextMenuStrip = VcpContextMenu;
            //toolTip
            m_tooltip.IsBalloon = true;
            m_tooltip.UseFading = true;
            m_tooltip.SetToolTip(this, vc.Name);
            m_tooltip.SetToolTip(m_button, vc.Name);
            m_tooltip.SetToolTip(m_imgbox, vc.Name);
            //imgbox
            m_imgbox.Image = vc.Image;            
            m_imgbox.SizeMode = PictureBoxSizeMode.Zoom;            
            m_imgbox.Size = new Size(67, 60);
            m_imgbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top)));
            m_imgbox.Location = new Point(3, 3);
            m_imgbox.ContextMenuStrip = VcpContextMenu;
            m_imgbox.DoubleClick += Button_Click;
            //button
            m_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom)));
            m_button.Size = new Size(67, 26);
            m_button.Location = new Point(3, 68);
            m_button.Text = "View";
            m_button.Click += Button_Click;
            
            this.Controls.Add(m_imgbox);
            this.Controls.Add(m_button);
            Invalidate();

        }

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

        public VcPanel()
        {
            m_vc = VisualComponent.Create();
            this.Size = new System.Drawing.Size(75, 100);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = "Default";
            m_tooltip.IsBalloon = true;
            m_tooltip.UseFading = true;
            m_tooltip.SetToolTip(this, "Default");
            m_tooltip.SetToolTip(m_button, "Default");
            m_tooltip.SetToolTip(m_imgbox, "Default");
            m_imgbox.Size = new Size(67, 60);
            m_imgbox.Location = new Point(3, 3);
            m_imgbox.BorderStyle = BorderStyle.Fixed3D;
            m_imgbox.SizeMode = PictureBoxSizeMode.Zoom;
            m_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom)));
            m_imgbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top)));
            m_button.Size = new Size(67, 26);
            m_button.Location = new Point(3, 68);
            m_button.Click += Button_Click;
            m_button.Text = "View";
            this.Controls.Add(m_imgbox);
            this.Controls.Add(m_button);
            Invalidate();
        }
        public Image Icon
        {
            get { return m_icon; }
            set { m_icon = value; m_imgbox.Image = value; Invalidate(); }
        }
        public Image VcImage
        {
            get { return m_vc.Image; }
            set { m_vc.Image = value; Invalidate(); }
        }
        public string ToolTip
        {
            get { return m_tooltip.GetToolTip(this); }
            set { m_tooltip.SetToolTip(this, value); Invalidate(); }
        }
        public VisualComponent VisualComponent
        {
            get { return m_vc; }
            set
            {
                m_vc = value;
                Icon = value.Image;
                ToolTip = value.Name;
                //Invalidate();
            }
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
            //bubble the event up to the parent
            if (this.ViewClick != null)
                this.ViewClick(this, e);
        }

    }

    [Serializable]
    public class VcTree
    {
       
        public void Add(List<Symbol> symbols)
        {
            
                Symbols.AddRange(symbols);
            
        }
        public void Remove(object obj)
        {
            if (obj is Symbol)
            {
                Symbols.Remove(obj as Symbol);
            }
        }
        public void Replace(object obj)
        {
            if (obj is Symbol)
            {
                //Symbols.Add(obj as Symbol);
            }
        }
        public static VcTree Create()
        {
            VcTree s = new VcTree();
            s.Symbols = new List<Symbol>();
            return s;
        }
        public static Predicate<Symbol> ByName(string name)
        {
            return delegate (Symbol input)
            {
                return input.Name == name;
            };
        }
        public Predicate<Symbol> ByAddress(string a)
        {
            return delegate (Symbol input)
            {
                return input.Address == a;
            };
        }
        public List<TreeNode> Items;
        public List<Symbol> Symbols;
    }
    [Serializable]
    public class VcView
    {
        public void saveTags()
        {
            foreach(var i in Items)
            {
                Symbols.Add(i.Tag as Symbol);
            }
        }
        public void restoreTags()
        {
            int c = 0;
            foreach (var i in Items)
            {
                Items[c].Tag = Symbols[c];
            }
        }
        public static VcView Create()
        {
            VcView v = new VcView();
            v.Items = new List<ListViewItem>();
            v.Symbols = new List<Symbol>();
            return v;
        }
        public List<ListViewItem> Items;
        public List<Symbol> Symbols;
    }
    [Serializable]
    public class VisualComponent
    {
        public static VisualComponent Read(string path)
        {
            VisualComponent obj = new VisualComponent();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                obj = (VisualComponent)formatter.Deserialize(stream);
                obj.IO = new List<Panel>();
                obj.IOview.restoreTags();
                foreach (var ios in obj.ios)
                {
                    Panel io = new Panel();
                    io.BackColor = Color.Red;
                    io.BorderStyle = BorderStyle.FixedSingle;
                    io.Size = ios.Size;
                    io.Location = ios.Location;
                    io.Name = ios.Name;
                    io.Tag = ios.IO;
                    
                    obj.IO.Add(io);
                }
                stream.Close();
            }
            catch (Exception ee){
                ee.ToString();
            }
            return obj;
        }
        public byte[] Write(string path)
        {
            ios = new List<iostruct>();
            foreach (var io in IO)
            {
                ios.Add(iostruct.Create(io.Name, io.Tag, io.Size, io.Location));
            }
            IOview.saveTags();
            var bytes = new byte[0];
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();
            }
            catch (Exception ee)
            {
                ee.ToString();
            }

            return bytes;
        }
        public static VisualComponent Create()
        {
            VisualComponent v = new VisualComponent();
            v.IO = new List<Panel>();
            v.IOview = VcView.Create();
            v.IOtree = VcTree.Create();            

            return v;
        }
        public static VisualComponent Create(Image image, VcView view, string name = "", List<Panel> io = null, string serial = "", object tag = null)
        {
            VisualComponent v = new VisualComponent();
            if (io != null)
                v.IO = new List<Panel>();
            else
                v.IO = io;
            v.IOview = view;
            if (v.IOview.Items == null)
                v.IOview.Items = new List<ListViewItem>();
            v.Name = name;
            v.Image = image;
            v.SerialNumber = serial;
            v.Tag = tag;
            return v;
        }
        public void IOStoIO()
        {
            IO = new List<Panel>();
            foreach (var ios in ios)
            {
                //ToolTip tt = new ToolTip();
                Panel control = new Panel();
                control.BackColor = Color.Red;
                control.Size = ios.Size;
                control.Location = ios.Location;
                control.Name = ios.Name;
                control.BorderStyle = BorderStyle.FixedSingle;
                //tt.IsBalloon = true;
                //tt.SetToolTip(control, ios.Name);
                control.Tag = ios.IO;
                IO.Add(control);
            }
        }
        public void IOtoIOS()
        {
            ios = new List<iostruct>();
            foreach (var io in IO)
            {
                ios.Add(iostruct.Create(io.Name, io.Tag, io.Size, io.Location));
            }
        }

        public Symbol Symbol;
        public Image Image;
        public string Name;
        public string SerialNumber;
        [NonSerialized]
        public List<Panel> IO;
        public List<iostruct> ios;
        public VcTree IOtree;
        public VcView IOview;
        public object Tag;
        [Serializable]
        public struct iostruct
        {
            public static iostruct Create(string name,object io, Size size, Point location)
            {
                iostruct ios = new iostruct();
                ios.Name = name;
                ios.Size = size;
                ios.IO = io;
                ios.Location = location;
                return ios;
            }
            public string Name;
            public object IO;
            public Size Size;
            public Point Location;
        }
    }
}
