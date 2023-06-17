using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.Elements.HTMLElements
{
    [HTML(NodeName = "head", PureContainer = true)]
    public class HTMLHeadElement : HTMLElement
    {
        public HTMLHeadElement(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
