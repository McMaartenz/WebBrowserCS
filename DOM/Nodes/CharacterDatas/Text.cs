﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes.CharacterDatas
{
    public class Text : CharacterData
    {
        public Text(Node? parentNode, string nodeName = "#text") : base(parentNode, nodeName) { }
    }
}
