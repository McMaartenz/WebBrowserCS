using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.CharacterDatas.Texts
{
    [HTML(NodeName = "![CDATA[")]
    public class CDATASection : Text
    {
        public CDATASection(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }
    }
}
