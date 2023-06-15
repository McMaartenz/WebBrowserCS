using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.CharacterDatas
{
    public class ProcessingInstruction : Node
    {
        public ProcessingInstruction(Node? parentNode, string nodeName = "?xml") : base(parentNode, nodeName) { }
    }
}
