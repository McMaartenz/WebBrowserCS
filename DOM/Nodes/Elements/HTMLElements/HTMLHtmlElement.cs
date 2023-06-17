using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.Elements.HTMLElements
{
    [HTML(NodeName = "html", PureContainer = true)]
    public class HTMLHtmlElement : HTMLElement
    {
        public HTMLHtmlElement(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
