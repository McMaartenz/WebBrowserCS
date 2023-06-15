using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.CharacterDatas
{
    public class Comment : CharacterData
    {
        public Comment(Node? parentNode, string nodeName = "!--") : base(parentNode, nodeName) { }
    }
}
