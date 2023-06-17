using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.Elements.HTMLElements
{
    [HTML(NodeName = "p", PureContainer = true, Margin = "16,0")]
    public class HTMLParagraphElement : HTMLElement
    {
        public HTMLParagraphElement(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
