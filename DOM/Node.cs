using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBrowser.DOM.Nodes;
using WebBrowser.DOM.Nodes.CharacterDatas;
using WebBrowser.DOM.Nodes.CharacterDatas.Texts;

namespace WebBrowser.DOM
{
    public enum NodeType
    {
        ELEMENT_NODE = 1,
        ATTRIBUTE_NODE = 2,
        TEXT_NODE = 3,
        CDATA_SECTION_NODE = 4,
        PROCESSING_INSTRUCTION_NODE = 7,
        COMMENT_NODE = 8,
        DOCUMENT_NODE = 9,
        DOCUMENT_TYPE_NODE = 10,
        DOCUMENT_FRAGMENT_NODE = 11,
    }

    public class Node : EventTarget
    {
        public string NodeName { get; init; }

        public NodeType NodeType => GetNodeType();

        public Node? ParentNode { get; init; }
        public List<Node> ChildNodes { get; init; } = new();
        public Node? FirstChild => ChildNodes.FirstOrDefault();
        public Node? LastChild => ChildNodes.LastOrDefault();

        public Node(Node? parentNode, string nodeName)
        {
            ParentNode = parentNode;
            parentNode?.ChildNodes.Add(this);

            NodeName = nodeName;
        }

        private NodeType GetNodeType()
        {
            if (this is Element)
                return NodeType.ELEMENT_NODE;

            if (this is Attr)
                return NodeType.ATTRIBUTE_NODE;

            if (this is Text)
                return NodeType.TEXT_NODE;

            if (this is CDATASection)
                return NodeType.CDATA_SECTION_NODE;

            /* Processing Instruction */

            if (this is Comment)
                return NodeType.COMMENT_NODE;

            if (this is Document)
                return NodeType.DOCUMENT_NODE;

            if (this is DocumentType)
                return NodeType.DOCUMENT_TYPE_NODE;

            if (this is DocumentFragment)
                return NodeType.DOCUMENT_FRAGMENT_NODE;

            throw new NotImplementedException();
        }
    }
}
