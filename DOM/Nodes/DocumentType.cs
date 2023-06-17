using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    [HTML(NodeName = "!DOCTYPE")]
    public class DocumentType : Node
    {
        public DocumentType(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
