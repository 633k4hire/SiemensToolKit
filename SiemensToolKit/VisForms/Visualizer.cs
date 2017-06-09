
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using SharpFocas;

namespace SiemensToolKit
{
    public partial class Visualizer : SiemensToolKit.Controls.SiemensForm
    {
        public static Visualizer _Instance
        {
            get { return m_Instance; }
        }
        public static Visualizer m_Instance;
        public IOBuffer Buffer = new IOBuffer().Initialize();
        public Sharp7.S7Client Client;
        public static readonly object _vcLock = new object();
        //public ushort HANDLE;
        public delegate void US(string result);
        //public US updateDelegate;
        //public Focas focas = new Focas();

        public List<VcPanel> VcpList = new List<VcPanel>();
        public Library CLibrary = Library.Create();
        public bool placingInput = false;
        public bool placingOutput = false;
        public VcTree TreeData = VcTree.Create();
        public VcView ViewData = VcView.Create();
        public string CurrentVcName = "";
        //public bool Connected = false;

        public Visualizer(Sharp7.S7Client client = null)
        {
            this.DoubleBuffered = true;
            m_Instance = this;
            if (client == null) Client = Loader.Client;

            InitializeComponent();           
            if (File.Exists("default.clf"))
            {
                try
                {
                    CLibrary = Library.Read("default.clf");
                    CLibrary = assignEventstoVcps(CLibrary);
                    updateVcpFlow(CLibrary);
                    updateLibrary();
                    updateWatchList();
                }
                catch { }
            }
        }
        public void updateWatchList()
        {
            if (CurrentVCP != null)
                updateView(CurrentVCP.VisualComponent);
        }
        public void updateLibrary()
        {
            if (CLibrary.SymbolList == null) return;
            if (CLibrary.SymbolList.Symbols == null) return;
            LibraryTree.Nodes.Clear();
            LibraryTree.BeginUpdate();
            foreach (var symbol in CLibrary.SymbolList.Symbols)
            {
                TreeNode node = new TreeNode();
                node.Text = symbol.Name;
                node.ToolTipText = symbol.Address;
                node.Tag = symbol;
                LibraryTree.Nodes.Add(node);
            }
            LibraryTree.EndUpdate();
        }
        public Visualizer(Sharp7.S7Client client, VisualComponent component)
        {
            this.DoubleBuffered = true;
            Client = client;
            AddVcPanel(component);
            InitializeComponent();
            m_Instance = this;

        }

        public void AddVcPanel(VisualComponent vc) //ADD Panel;
        {
            VcPanel vcp = new VcPanel(vc);
            CurrentVcName = vcp.VisualComponent.Name;
            vcp.ViewClick += Vcp_ButtonClick;
            vcp.RemoveClick += Vcp_RemoveClick;
            vcp.SaveClick += Vcp_SaveClick;
            CLibrary.Add(vcp);
            updateVcpFlow(CLibrary);
        }
        public void displayVCP(VcPanel vcp)
        {
            DisplayVC(vcp.VisualComponent);
            CurrentVcName = vcp.VisualComponent.Name;
            CurrentVCP = vcp;
        }
        private void updateVcpFlow(Library lib)
        {
            VcpFlow.Controls.Clear();
            VcpFlow.Controls.AddRange(lib.Components.ToArray());
            if (lib.Components.Count >= 1)
            {
                displayVCP(lib.Components[0]);
            }
        }
        public VcPanel CurrentVCP = null;
        private void Vcp_ButtonClick(object sender, EventArgs e) //VCP View Click
        {
            editToolStrip.Visible = true;
            var vcp = (VcPanel)sender;
            DisplayVC(vcp.VisualComponent);
            CurrentVcName = vcp.VisualComponent.Name;
            CurrentVCP = vcp;
        }
        private void DisplayVC(VisualComponent vc)
        {
            updateTree(vc);
            updateView(vc);
            createImageBox(vc);
        }
        public void createImageBox(VisualComponent component)
        {
            PictureBox box = new PictureBox();
            box.BorderStyle = BorderStyle.FixedSingle;
            box.Width = component.Image.Width;
            box.Height = component.Image.Height;
            box.Image = component.Image;
            box.Name = component.Name;
            box.Tag = component;
            box.MouseMove += imgbox_MouseMove;
            box.MouseLeave += imgbox_MouseLeave;
            box.DragDrop += Box_DragDrop;
            box.MouseEnter += imgbox_MouseEnter;
            box.Resize += imgbox_Resize;
            box.MouseClick += imgbox_MouseClick;
            box.SizeMode = PictureBoxSizeMode.AutoSize;
            foreach (var panel in component.IO)
            {
                panel.ContextMenuStrip = redSquareMenu;
                panel.MouseEnter += Control_MouseEnter;
                panel.MouseClick += Panel_MouseClick;
                panel.MouseHover += Panel_MouseHover;
                panel.MouseDown += Panel_MouseDown;
                panel.MouseMove += Panel_MouseMove;
                panel.MouseUp += Panel_MouseUp;
            }
            box.Controls.AddRange(component.IO.ToArray());
            FLOW.Controls.Clear();
            FLOW.Controls.Add(box);
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMovingObject)
            {
                var panel = ((Panel)sender);
                var a = e.Location;
                Point panelPoint = ((Panel)sender).Location;
                panelPoint.X = (panelPoint.X + e.Location.X) - 6;
                panelPoint.Y = (panelPoint.Y + e.Location.Y) - 6;

                ((Panel)sender).Location = panelPoint;
                //((Panel)sender).Parent.Refresh();
            }
            mouseDown = false;
            isMovingObject = false;
            ((Panel)sender).Cursor = Cursors.Default;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                isMovingObject = true;
                ((Panel)sender).Cursor = Cursors.NoMove2D;
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            MovingObject = (Panel)sender;
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                ((Panel)sender).Cursor = Cursors.NoMove2D;
            }
        }

        private void Panel_MouseHover(object sender, EventArgs e)
        {

            var panel = (Panel)sender;
            ToolTipHelper.SetToolTip(panel, panel.Name);
            //ToolTip tt = new ToolTip();
            //tt.IsBalloon = true;
            //tt.UseFading = tt.UseAnimation = false;
            //tt.ReshowDelay = 1;
            //tt.SetToolTip(panel, panel.Name);
        }
        private bool mouseDown = false;
        private bool isMovingObject = false;
        private object MovingObject = null;
        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            //start move
            MovingObject = (Panel)sender;

        }

        public static Control FindControlAtPoint(Control container, Point pos)
        {
            Control child;
            foreach (Control c in container.Controls)
            {
                if (c.Visible && c.Bounds.Contains(pos))
                {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    if (child == null) return c;
                    else return child;
                }
            }
            return null;
        }
        public static Control FindControlAtCursor(Form form)
        {
            Point pos = Cursor.Position;
            if (form.Bounds.Contains(pos))
                return FindControlAtPoint(form, form.PointToClient(pos));
            return null;
        }
        private void Box_DragDrop(object sender, DragEventArgs e)
        {

        }
        public void createViewAndTree(VisualComponent component)
        {
            updateTree(component);
            updateView(component);
        }
        public void fillInSensorView(VisualComponent component)
        {
            foreach(var io in component.ios)
            {
                var symbol = io.IO as Symbol;
                var b = new ListViewItem.ListViewSubItem[2] { new ListViewItem.ListViewSubItem(), new ListViewItem.ListViewSubItem() };
                b[1].Text = symbol.Name;
                ListViewItem item = new ListViewItem(b, 0);
                item.Name = symbol.Name;
                item.Text = "X" + symbol.Address;
                item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                item.UseItemStyleForSubItems = true;
                item.StateImageIndex = Convert.ToInt16(symbol.State);
                item.ImageIndex = 2;
                item.Tag = symbol;
                component.IOview.Items.Add(item);
                
            }
            updateView(component);
        }
        public void updateView(VisualComponent component)
        {
            try
            {
                if (component.IOview.Items.Count == 0 && component.ios.Count > 0)
                {
                    fillInSensorView(component);
                }
            }
            catch { }
            SensorView.Items.Clear();
            SensorView.BeginUpdate();
            List<ListViewItem> I = new List<ListViewItem>();
            foreach (var i in component.IOview.Items)
            {
                if (i.Tag is Symbol)
                {
                    Symbol input = (Symbol)i.Tag;
                    if (Connected)
                    { }//READ STATE  
                    var b = new ListViewItem.ListViewSubItem[2] { new ListViewItem.ListViewSubItem(), new ListViewItem.ListViewSubItem() };
                    b[1].Text = input.Name;
                    ListViewItem item = new ListViewItem(b, 0);
                    item.Name = input.Name;
                    item.Text = input.Address;
                    item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    item.UseItemStyleForSubItems = true;
                    item.StateImageIndex = Convert.ToInt16(input.State);
                    item.ImageIndex = 2;
                    item.Tag = input;
                    I.Add(item);
                }

            }
            
            component.IOview.Items = I;
            SensorView.Items.AddRange(I.ToArray());
            SensorView.EndUpdate();
        }
        private void updateTree(VisualComponent component)
        {
            SensorTree.Nodes.Clear();
            // SensorTree.BeginUpdate();
            List<TreeNode> I = new List<TreeNode>();


            foreach (var symbol in component.IOtree.Symbols)
            {


                TreeNode symbolNode = new TreeNode();
                Input tempState = Input.Create();
                if (Connected)
                { } //UPDATE STATE
                symbolNode.Text = symbolNode.Name = symbol.Name;
                symbolNode.Tag = symbol;
                symbolNode.ToolTipText = symbol.Address;
                symbolNode.StateImageIndex = Convert.ToInt16(tempState.State);
                symbolNode.ImageIndex = 2;

                I.Add(symbolNode);
            }

            component.IOtree.Items = I;
            SensorTree.Nodes.AddRange(component.IOtree.Items.ToArray());
            // SensorTree.EndUpdate();
        }
        public void AddToWatchList(TreeNode node)
        {
            if (node.Tag is Symbol)
            {
                Symbol symbol = (Symbol)node.Tag;
                if (Connected)
                { } //READ STATE   
                var b = new ListViewItem.ListViewSubItem[2] { new ListViewItem.ListViewSubItem(), new ListViewItem.ListViewSubItem() };
                b[1].Text = symbol.Name;
                ListViewItem item = new ListViewItem(b, 0);
                item.Name = symbol.Name;
                item.Text = "X" + symbol.Address;
                item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                item.UseItemStyleForSubItems = true;
                item.StateImageIndex = Convert.ToInt16(symbol.State);
                item.ImageIndex = 2;
                item.Tag = symbol;
                ViewData.Items.Add(item);
                SensorView.Items.Add(item);
            }


        }
        private Panel selectedIO = null;
        private void Control_MouseEnter(object sender, EventArgs e)
        {
            selectedIO = (Panel)sender;

        }

        private VisualComponent placeDragIO(Point location, VisualComponent vc, object IO)
        {
            if (IO is Symbol)
            {
                Symbol input = (Symbol)IO;
                Panel control = new Panel();
                string name = input.Name + ": " + input.Address;
                control.Size = new Size(12, 12);
                control.Tag = input;
                control.Name = name;
                control.BackColor = Color.Red;
                control.BorderStyle = BorderStyle.FixedSingle;
                control.BringToFront();
                control.Location = location;
                control.ContextMenuStrip = redSquareMenu;
                control.MouseEnter += Control_MouseEnter;
                control.MouseClick += Panel_MouseClick;
                control.MouseHover += Panel_MouseHover;
                control.MouseDown += Panel_MouseDown;
                control.MouseMove += Panel_MouseMove;
                control.MouseUp += Panel_MouseUp;
                //ToolTip tt = new ToolTip();
                //tt.IsBalloon = true;
                //tt.UseFading = true;
                //tt.SetToolTip(control,name);
                vc.IO.Add(control);
            }

            return vc;
        }
        private void addIoToVcp(string vcName, List<Symbol> IO)
        {
            if (IO == null) { return; }

            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IOtree.Add(IO);
                    updateTree(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);
                }
            }
        }
        private bool removeIoFromImgbox(string vcName, Panel IO)
        {
            if (IO == null) { return false; }
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    var bb = ((VcPanel)(VcpFlow.Controls[i])).VisualComponent;
                    List<Panel> panrem = new List<Panel>(); 
                    foreach (var io in ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IO)
                    {
                        if (IO.Tag.Equals(io.Tag))
                        {
                            panrem.Add(io);
                            
                        }
                    }
                    List<ListViewItem> r = new List<ListViewItem>();
                    foreach (var item in bb.IOview.Items)
                    {
                        if (item.Tag as Symbol == IO.Tag as Symbol)
                        {
                            r.Add(item);
                        }
                    }
                    foreach (var rem in r)
                    {
                        bb.IOview.Items.Remove(rem);
                    }
                    List<VisualComponent.iostruct> rems = new List<VisualComponent.iostruct>();
                    foreach (var ios in bb.ios)
                    {
                        if (ios.IO as Symbol == IO.Tag as Symbol)
                        {
                            rems.Add(ios);
                        }
                    }
                    foreach(var rem in rems)
                    {
                        bb.ios.Remove(rem);
                    }
                    foreach (var rem in panrem)
                    {
                        bb.IO.Remove(rem);
                    }
                    refreshImgboxControls(bb.IO);
                    updateView(bb);
                }

            }
            return true;
        }
        private void removeIoFromVcp(string vcName, object IO)
        {
            if (IO == null) { return; }
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IOtree.Remove(IO);

                    updateTree(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);
                }
            }
        }
        private void replaceIoFromVcp(string vcName, object IO)
        {
            if (IO == null) { return; }
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IOtree.Replace(IO);

                    updateTree(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);
                }
            }
        }
        private void removeIoFromIOView(string vcName, ListViewItem item)
        {
            if (item == null) { return; }
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    List<ListViewItem> rems = new List<ListViewItem>();
                    var vc = ((VcPanel)(VcpFlow.Controls[i])).VisualComponent;
                    foreach (var it in ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IOview.Items)
                    {
                        if (item.Equals(it))
                        {
                            rems.Add(it);
                        }else
                        if (item.SubItems[0].Text.Equals(it.SubItems[0].Text) && item.SubItems[1].Text.Equals(it.SubItems[1].Text))
                        {
                            rems.Add(it);
                        }else
                        if (item.Tag.Equals(it.Tag))
                        {
                            rems.Add(it);
                        }
                    }
                    foreach(var rem in rems)
                    {
                        vc.IOview.Items.Remove(rem);
                    }
                    updateView(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);

                    return;
                }
            }
        }

        private void ChangeVcImage(string vcName, Image image)
        {
            if (image == null) { return; }
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    ((VcPanel)(VcpFlow.Controls[i])).VcImage = image;
                    ((VcPanel)(VcpFlow.Controls[i])).Icon = image;
                    ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IO.Clear();
                    createImageBox(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);
                }
            }
        }
        private void CreateImageBoxFromVCP(string vcName)
        {
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    createImageBox(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);
                }
            }
        }
        private void updateVcp(VisualComponent vc)
        {
            foreach (var control in VcpFlow.Controls)
            {
                var vcp = (VcPanel)control;
                if (vcp.VisualComponent.Name.Equals(vc.Name))
                {
                    vcp.VisualComponent = vc;
                    return;
                }
            }
        }
        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SensorView.View = System.Windows.Forms.View.List;
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SensorView.View = System.Windows.Forms.View.Tile;
        }

        private void iconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SensorView.View = System.Windows.Forms.View.LargeIcon;
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SensorView.View = System.Windows.Forms.View.Details;
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SensorView.View = System.Windows.Forms.View.SmallIcon;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            try
            {

                if (SensorTree.SelectedNode != null)
                {
                    AddToWatchList(SensorTree.SelectedNode);
                    foreach (TreeNode node in SensorTree.SelectedNode.Nodes)
                    {
                        AddToWatchList(node);
                    }
                    SensorTree.SelectedNode = null;
                }
            }
            catch (Exception ex) { Exceptions.Add(ex); }
        }
        public List<Exception> Exceptions = new List<Exception>();
        private void Remove_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = SensorView.SelectedItems[0];
                ViewData.Items.Remove(selectedItem);
                //updateView();
            }
            catch { }
        }
        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void imgbox_MouseEnter(object sender, EventArgs e)
        {
            var imgbox = (PictureBox)sender;
            if (placingInput || placingOutput)
                imgbox.Cursor = Cursors.Cross;
        }
        private void imgbox_MouseLeave(object sender, EventArgs e)
        {
            var imgbox = (PictureBox)sender;
            if (placingInput || placingOutput)
                imgbox.Cursor = Cursors.Default;
        }
        private void imgbox_MouseMove(object sender, MouseEventArgs e)
        {
            var imgbox = (PictureBox)sender;
            if (placingInput || placingOutput)
                imgbox.Cursor = Cursors.Cross;
        }
        private void imgbox_MouseClick(object sender, MouseEventArgs e)
        {
            var imgbox = (PictureBox)sender;
            if (placingInput || placingOutput)
            {
                if (imgbox.Tag is VisualComponent)
                {
                    var pos = e.Location;
                    pos.X = pos.X - 6;
                    pos.Y = pos.Y - 6;
                    //var vc = placeIO(pos, (VisualComponent)imgbox.Tag,(PictureBox)sender, placingInput, placingOutput);


                }
            }
        }
        private void imgbox_Resize(object sender, EventArgs e)
        {
            /*foreach (var control in Sensors)
            {
                control.AdjustPositionToScale();
            }*/
        }
        private void addinput_click(object sender, EventArgs e)
        {
            if (!placingInput)
            {
                addinput.Enabled = false;
                addoutput.Enabled = true;
                placingOutput = false;
                placingInput = true;
            }
        }
        private void addoutput_Click(object sender, EventArgs e)
        {
            if (!placingOutput)
            {
                addinput.Enabled = true;
                addoutput.Enabled = false;
                placingOutput = true;
                placingInput = false;
            }
        }
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (editToolStrip.Visible)
                editToolStrip.Visible = false;
            else
                editToolStrip.Visible = true;
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void editToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            placingInput = placingOutput = false;
            addinput.Enabled = addoutput.Enabled = true;
        }
        private void saveLibraryBtn(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Component Library|*.clf|All Files|*.*";
            sfd.AddExtension = true;
            sfd.DefaultExt = "*.clf";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.ShowDialog();
            if (!sfd.FileName.Equals(""))
            {
                CLibrary.Write(sfd.FileName);
            }
        }
        private void OpenLibraryBtn(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Component Library|*.clf|All Files|*.*";
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.clf";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.ShowDialog();
            if (!ofd.FileName.Equals(""))
            {
                CLibrary = Library.Read(ofd.FileName);
                CLibrary = assignEventstoVcps(CLibrary);
                updateVcpFlow(CLibrary);
            }
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void SensorTree_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void FLOW_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void SensorTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.Copy);
        }
        private void FLOW_DragDrop(object sender, DragEventArgs e)
        {
            var flow = (FlowLayoutPanel)sender;
            var pos = flow.PointToClient(new Point(e.X, e.Y));
            var imgbox = flow.GetChildAtPoint(pos);
            var boxpos = imgbox.PointToClient(new Point(e.X, e.Y));
            var vc = (VisualComponent)imgbox.Tag;
            TreeNode node;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                Point p = new Point(boxpos.X - 6, boxpos.Y - 6);
                flow.GetChildAtPoint(pos).Tag = vc = placeDragIO(p, vc, node.Tag); //updates imgbox.tag with VC[VC.IO containing new control];
                imgbox.Controls.Add(vc.IO[vc.IO.Count - 1]);
                imgbox.Controls[imgbox.Controls.Count - 1].BringToFront();
                AddToVcIOView(CurrentVcName, node);

            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void SensorTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //e.Node.Tag
        }

        private void addInputToTreeClick(object sender, EventArgs e)
        {
            //Add Symbol

            Forms.createSymbolForm symbolform = new Forms.createSymbolForm();
            symbolform.StartPosition = FormStartPosition.CenterParent;
            var result = symbolform.ShowDialog();
            if (!symbolform.Cancelled)
            {
                var symbols = symbolform.Symbols;
                addIoToVcp(CurrentVcName, symbols);
            }
            symbolform.Close();
        }




        private void spindleGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolChangerToTree_click(object sender, EventArgs e)
        {

        }

        private void removeFromTree_click(object sender, EventArgs e)
        {
            if (SensorTree.SelectedNode != null)
            {
                removeIoFromVcp(CurrentVcName, SensorTree.SelectedNode.Tag);
            }
        }

        private void changeVcImageClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg|BMP|*.bmp|PNG|*.png|All Files|*.*";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.ShowDialog();
            if (!ofd.FileName.Equals(""))
            {
                Image image = Image.FromFile(ofd.FileName);
                ChangeVcImage(CurrentVcName, image);
                //CreateImageBoxFromVCP(CurrentVcName);
            }
        }
        private Library assignEventstoVcps(Library lib)
        {
            foreach (var vcp in lib.Components)
            {
                vcp.ViewClick += Vcp_ButtonClick;
                vcp.RemoveClick += Vcp_RemoveClick;
                vcp.SaveClick += Vcp_SaveClick;
            }
            return lib;
        }

        private void Vcp_SaveClick(object sender, EventArgs e)
        {
            var vcp = (VcPanel)sender;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Component|*.vc|All Files|*.*";
            sfd.AddExtension = true;
            sfd.DefaultExt = "*.vc";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.ShowDialog();
            if (!sfd.FileName.Equals(""))
            {
                vcp.VisualComponent.Write(sfd.FileName);


            }
        }

        private void Vcp_RemoveClick(object sender, EventArgs e)
        {
            CLibrary.Remove((VcPanel)sender);
            updateVcpFlow(CLibrary);
        }

        private void componentLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createComponentForm cf = new createComponentForm();
            cf.ShowDialog();
            var vc = cf.visualComponent;
            cf.Close();
            if (vc.Image != null)
            {
                var vcp = new VcPanel(vc);
                vcp.ViewClick += Vcp_ButtonClick;
                vcp.SaveClick += Vcp_SaveClick;
                vcp.RemoveClick += Vcp_RemoveClick;
                CLibrary.Add(vcp);
                updateVcpFlow(CLibrary);
            }
        }

        private void newLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CLibrary = Library.Create();
            updateVcpFlow(CLibrary);
        }

        private void openComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Visual Component|*.vc|All Files|*.*";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.ShowDialog();
            if (!ofd.FileName.Equals(""))
            {
                VisualComponent vc = VisualComponent.Read(ofd.FileName);

                CLibrary.Add(new VcPanel(vc));
                CLibrary = assignEventstoVcps(CLibrary);
                updateVcpFlow(CLibrary);
            }
        }

        private void saveComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Component|*.vc|All Files|*.*";
            sfd.AddExtension = true;
            sfd.DefaultExt = "*.vc";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.ShowDialog();
            if (!sfd.FileName.Equals(""))
            {

                ((VcPanel)sender).VisualComponent.Write(sfd.FileName);
            }
        }

        public void updateViewNoFlicker()
        {

            SensorView.BeginUpdate();
            for (int i = 0; i < SensorView.Items.Count; ++i)
            {
                if (SensorView.Items[i].Tag is Symbol)
                {
                    var symbol = (Symbol)SensorView.Items[i].Tag;
                    symbol.ReadState(Buffer);
                    SensorView.Items[i].Tag = symbol;
                    SensorView.Items[i].StateImageIndex = Convert.ToInt16(symbol.State);
                }

            }

            SensorView.EndUpdate();
        }
        private void refreshImgboxControls(List<Panel> IO)
        {
            if (FLOW.Controls.Count > 0)
            {
                FLOW.Controls[0].Controls.Clear();
                FLOW.Controls[0].Controls.AddRange(IO.ToArray());
            }
        }
        private void UpdateImgBoxControls() //REFRESH FROM IO
        {
            if (FLOW.Controls.Count > 0)
            {
                for (int i = 0; i < ((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO.Count; ++i)
                {

                    if (((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i].Tag is Symbol)
                    {
                        //if (Connected)
                        //{
                        var symbol = (Symbol)((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i].Tag;
                        symbol.ReadState(Buffer);
                        ((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i].Tag = symbol;
                        //}
                        //((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i].Tag = focas.readInput((Input)(((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i].Tag), HANDLE);
                        if (((Symbol)(((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i].Tag)).State)
                        {
                            (((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i]).BackColor = Color.Lime;
                        }
                        else
                        {
                            (((VisualComponent)(((PictureBox)FLOW.Controls[0]).Tag)).IO[i]).BackColor = Color.Red;
                        }
                    }




                }

            }
        }
        private void watch_Tick(object sender, EventArgs e)
        {
            if (Client==null)
            {
                Client = Loader.Client;
                if (Client == null)
                {
                    MessageBox.Show("An error has occured with the PLC connection, please restart Visualizer and Connection Launcher programs");
                    watch.Enabled = false;
                    return;
                }
            }
            
            Buffer = Client.ReadIO(Properties.Settings.Default.BufferSize); // MAYBE TOO MUCH DATA HERE AND NO GARBAGE COLLECT
            updateViewNoFlicker();
            UpdateImgBoxControls();
        }

        private void SensorView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        public void AddToVcIOView(string vcName, TreeNode node)
        {
            if (node.Tag == null) { return; }

            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {


                    if (node.Tag is Symbol)
                    {
                        Symbol symbol = (Symbol)node.Tag;
                        if (Connected)
                        { } //READ STATE   
                        var b = new ListViewItem.ListViewSubItem[2] { new ListViewItem.ListViewSubItem(), new ListViewItem.ListViewSubItem() };
                        b[1].Text = symbol.Name;
                        ListViewItem item = new ListViewItem(b, 0);
                        item.Name = symbol.Name;
                        item.Text = "X" + symbol.Address;
                        item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        item.UseItemStyleForSubItems = true;
                        item.StateImageIndex = Convert.ToInt16(symbol.State);
                        item.ImageIndex = 2;
                        item.Tag = symbol;
                        ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IOview.Items.Add(item);
                        //SensorView.Items.Add(item);
                    }

                    updateView(((VcPanel)(VcpFlow.Controls[i])).VisualComponent);
                }
            }
        }

        private void SensorView_DragDrop(object sender, DragEventArgs e)
        {

            var flow = (DoubleBufferedListView)sender;

            TreeNode node;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                AddToVcIOView(CurrentVcName, node);
            }
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove
            if (SensorView.SelectedItems.Count > 0)
            {
                removeIoFromIOView(CurrentVcName, SensorView.SelectedItems[0]);
            }
        }

        private void removeAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in SensorView.Items)
            {
                removeIoFromIOView(CurrentVcName, item);
            }
        }

        private void WatchTheWatcher_Tick(object sender, EventArgs e)
        {
            if (toolStripButton5.Checked)
            {
                if (FLOW.Controls.Count > 0 || SensorView.Items.Count > 0)
                {
                    if (SensorView.Items.Count > 0)
                    {
                        if (watch.Enabled) return;
                        watch.Enabled = true;
                    }
                    if (FLOW.Controls[0].Controls.Count > 0)
                    {
                        if (watch.Enabled) return;
                        watch.Enabled = true;
                    }
                }
                else
                {
                    //watch.Enabled = false;
                }
            }
            else
            {
                // watch.Enabled = false;
            }
        }
        private bool timerRunning = false;
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!Loader._Instance.Connected)
            {
                MessageBox.Show("Not Connected");
                toolStripButton5.Checked = false;
                return;
            }
            if (timerRunning == false)
            {
                WatchTimer.Enabled = true;
                timerRunning = true;
            }
            else
            {
                WatchTimer.Enabled = false;
                timerRunning = false;
            }
        }

        private void removeToolStripMenuItem1_MouseUp(object sender, MouseEventArgs e)
        {
            removeIoFromImgbox(CurrentVcName, selectedIO);
        }

        private void renameIoFromImgBox(string vcName, Panel item, string newCaption = "")
        {
            if (item == null) { return; }
            for (int i = 0; i < VcpFlow.Controls.Count; ++i)
            {
                if (((VcPanel)(VcpFlow.Controls[i])).VisualComponent.Name.Equals(vcName))
                {
                    foreach (var it in ((VcPanel)(VcpFlow.Controls[i])).VisualComponent.IO)
                    {
                        if (it.Equals(item))
                        {
                            ToolTipHelper.SetToolTip(it, newCaption);

                        }
                    }


                }
            }
        }
        private void renameImgBoxIo(object sender, EventArgs e)
        {
            renameIoFromImgBox(CurrentVcName, selectedIO, newPanelName.Text);
        }

        private void renameToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            int Result;
            int Rack = System.Convert.ToInt32(Loader._Instance.TxtRack.Text);
            int Slot = System.Convert.ToInt32(Loader._Instance.TxtSlot.Text);

            Result = Client.ConnectTo(Loader._Instance.TxtIP.Text, Rack, Slot);
            Client.ShowResult(Result);
            if (Result == 0)
            {
                Loader._Instance.output.Text = Loader._Instance.output.Text + " PDU Negotiated : " + Client.PduSizeNegotiated.ToString();
                Loader._Instance.TxtIP.Enabled = false;
                Loader._Instance.TxtRack.Enabled = false;
                Loader._Instance.TxtSlot.Enabled = false;
                Loader._Instance.ConnectBtn.Enabled = false;
                Loader._Instance.DisconnectBtn.Enabled = true;
                Loader._Instance.Status.Value = 100;
            }
        }

        private void editToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (SensorTree.SelectedNode != null)
            {
                if (SensorTree.SelectedNode.Tag is Input)
                {
                    Forms.createSymbolForm cif = new Forms.createSymbolForm();
                    cif.ShowDialog();
                    if (cif.Symbols.Count > 0)
                    {
                        SensorTree.SelectedNode.Tag = cif.Symbols[0];
                        replaceIoFromVcp(CurrentVcName, cif.Symbols[0]);
                        return;
                    }
                }
                //update IO
                //update tree

            }
        }

        private void SensorTree_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            SensorTree.SelectedNode = e.Node;
        }

        private void importSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (CLibrary.SymbolList == null)
            {
                CLibrary.SymbolList = new SymbolList();
            }
            if (CLibrary.SymbolList.Symbols == null)
            {
                CLibrary.SymbolList.Symbols = new List<Symbol>();
            }
            CLibrary.SymbolList.Import();
            CLibrary.SymbolList.Symbols = CLibrary.SymbolList.FilterByType(new DataType[] { DataType.I, DataType.Q, DataType.M });
            //
            try
            {
                foreach (var symbol in CLibrary.SymbolList.Symbols)
                {
                    TreeNode node = new TreeNode();
                    node.Text = symbol.Name;
                    node.ToolTipText = symbol.Address;
                    node.Tag = symbol;
                    LibraryTree.Nodes.Add(node);
                }
            }
            catch { }

        }

        private void componentSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComponentSymbolContainer.Visible = componentSymbolsToolStripMenuItem.Checked;

            if (componentSymbolsToolStripMenuItem.Checked && showHideLibraryToolStripMenuItem.Checked)
            {
                SPLIT.SplitterDistance = 340 + 250;
            }
            else
            {
                SPLIT.SplitterDistance = 340;
            }
        }

        private void showHideLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LibraryPanel.Visible = showHideLibraryToolStripMenuItem.Checked;
            if (showHideLibraryToolStripMenuItem.Checked)
            {

            }
            if (componentSymbolsToolStripMenuItem.Checked && showHideLibraryToolStripMenuItem.Checked)
            {
                SPLIT.SplitterDistance = 340 + 250;
            }
            else
            {
                SPLIT.SplitterDistance = 340;
            }
        }

        private void Visualizer_Load(object sender, EventArgs e)
        {

        }

        private void LibraryTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            
            this.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Settings s = new Forms.Settings();
            s.ShowDialog();
        }

        private void SensorTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void SensorTree_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                var node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                var symbol = node.Tag as Symbol;
                var view = node.TreeView;
                addIoToVcp(CurrentVcName, new List<Symbol> { symbol});
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Visualizer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
