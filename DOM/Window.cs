using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public class Window
    {
        public Document Document { get; set; }

        public Window()
        {
            Url = "about:blank";
            Document = new(this);
        }

        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
