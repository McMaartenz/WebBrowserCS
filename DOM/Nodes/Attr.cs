using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    [HTML]
    public class Attr : Node
    {
        protected Attr(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
