using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.Elements
{
    public class HTMLElement : Element
    {
        protected HTMLElement(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
