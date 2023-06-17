using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.CharacterDatas
{
    [HTML(NodeName = "!--")]
    public class Comment : CharacterData
    {
        public Comment(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }

        public override string Stringify(int indent = 0)
        {
            string tabs = IStringifier.GetTabs(indent);
            if (NodeName.StartsWith('#'))
            {
                return $"{tabs}{NodeName}\n{GetChilds(indent)}";
            }

            return $"{tabs}<!--\n{GetChilds(indent + 2)}{tabs}-->";
        }
    }
}
