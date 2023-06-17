﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.CharacterDatas
{
    [HTML(NodeName = "#text")]
    public class Text : CharacterData
    {
        public Text(Node? parentNode, string nodeName = "#text") : base(parentNode, nodeName) { }

        public override string Stringify(int indent = 0)
        {
            string tabs = IStringifier.GetTabs(indent);
            return $"{tabs}{NodeValue}";
        }
    }
}
