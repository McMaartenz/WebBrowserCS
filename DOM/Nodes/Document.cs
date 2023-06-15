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

        public Document(Window parentWindow) : base(null, "DOCUMENT")
        {
            _parentWindow = parentWindow;
        }

        public Node CreateElement(Node? parentNode, string name)
        {
            // more logic
            return new(parentNode, name);
        }

        public void LoadHTML(string HTML)
        {
            _ = new HTMLParser(this, HTML);
        }
    }
}
