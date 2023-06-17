using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.Elements.HTMLElements
{
    [HTML(NodeName = "body", PureContainer = true)]
    public class HTMLBodyElement : HTMLElement
    {
        public HTMLBodyElement(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
