﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    [HTML(NodeName = "#document-fragment")]
    public class DocumentFragment : Node
    {
        public DocumentFragment(Node? parentNode, string nodeName = "#document-fragment") : base(parentNode, nodeName) { }
    }
}
