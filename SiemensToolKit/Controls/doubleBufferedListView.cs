using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanucVisualizer
{
    class DoubleBufferedListView : System.Windows.Forms.ListView
    {
        public DoubleBufferedListView()
            : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
