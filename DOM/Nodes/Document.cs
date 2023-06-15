using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    public class Document : Node
    {
        private Window _parentWindow;

        public Document(Window parentWindow) : base(null, "#document")
        {
            _parentWindow = parentWindow;
            BaseURI = parentWindow.Location;
        }

        public Node CreateElement(Node? parentNode, string name)
        {
            // Activator.CreateInstance()
            // Dictionary of string -> class of type Node

            return new(parentNode, name);
        }

        public void LoadHTML(string HTML)
        {
            _ = new HTMLParser(this, HTML);
        }
    }
}
