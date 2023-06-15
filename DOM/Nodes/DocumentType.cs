using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    public class DocumentType : Node
    {
        public DocumentType(Node? parentNode, string nodeName = "!DOCTYPE") : base(parentNode, nodeName) { }
    }
}
