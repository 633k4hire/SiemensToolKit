using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace SiemensToolKit
{
    class DoubleBufferedListView : System.Windows.Forms.ListView
    {
        public DoubleBufferedListView()
            : base()
        {
            this.DoubleBuffered = true;
        }
    }

    class DoubleBufferedPicturebox : System.Windows.Forms.PictureBox
    {
        public DoubleBufferedPicturebox()
            : base()
        {
            this.DoubleBuffered = true;
        }
    }
    class ToolTipHelper
    {
        private static readonly Dictionary<string, ToolTip> tooltips = new Dictionary<string, ToolTip>();
        public static ToolTip GetControlToolTip(string controlName)
        {
            if (tooltips.ContainsKey(controlName))
            {
                return tooltips[controlName];
            }
            else
            {
                ToolTip tt = new ToolTip();
                tooltips.Add(controlName, tt);
                return tt;
            }
        }
        public static ToolTip GetControlToolTip(Control control)
        {
            return GetControlToolTip(control.Name);
        }
        public static void SetToolTip(Control control, string text)
        {
            ToolTip tt = GetControlToolTip(control);
            tt.SetToolTip(control, text);
        }
    }
    [Serializable]
    public struct Library
    {
        public static Library Read(string path)
        {
            Library obj = new Library();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                obj = (Library)formatter.Deserialize(stream);
                stream.Close();
                obj.Components = new List<VcPanel>();
                foreach (var vc in obj.vcl)
                {
                    var vcc = vc;
                    vcc.IO = new List<Panel>();
                    if (vcc.ios == null)
                        vcc.ios = new List<VisualComponent.iostruct>();
                    vcc.IOStoIO();
                    obj.Components.Add(new VcPanel(vcc));
                }
            }
            catch { }
            return obj;
        }
        public byte[] Write(string path)
        {
            vcl = new List<VisualComponent>();
            foreach (var vcp in Components)
            {
                var vc = vcp.VisualComponent;
                vc.IOtoIOS();
                vcl.Add(vc);

            }
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
        public static Library Create()
        {
            Library cl = new Library();
            cl.Components = new List<VcPanel>();
            cl.vcl = new List<VisualComponent>();
            cl.SymbolList = new SymbolList();
            return cl;
        }
        [NonSerialized]
        public List<VcPanel> Components;
        private List<VisualComponent> vcl;
        public void Add(VcPanel vc)
        {
            Components.Add(vc);
        }
        public void Remove(VcPanel vc)
        {
            Components.Remove(vc);
        }
        public string Name;
        public object Tag;
        public SymbolList SymbolList;
    }
    [Serializable]
    public struct Module
    {
        public static Module Read(string path)
        {
            Module obj = new Module();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                obj = (Module)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception ee)
            {
                ee.ToString();
            }
            return obj;
        }
        public byte[] Write(string path)
        {

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
        public string assembly64;
        [NonSerialized]
        public Assembly assembly;
        public byte[] bytes;
        public string Name;
        public object Tag;
        public static Module Create()
        {
            Module m = new Module();
            return m;
        }
        public static Module Create(string path)
        {

            Module m = new Module();
            try
            {
                m.assembly = Assembly.LoadFrom(path);
                m.Name = path;
            }
            catch { }
            return m;
        }

    }
}
