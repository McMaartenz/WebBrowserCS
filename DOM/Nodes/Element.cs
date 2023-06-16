using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    [HTML]
    public class Element : Node
    {
        protected Element(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
