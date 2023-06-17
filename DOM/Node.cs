using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

    [HTML]
    public class Node : EventTarget, IStringifier
    {
        public string NodeName { get; init; }
        public string NodeValue { get; set; } = "";
        public Document? OwnerDocument => ParentNode is null
            ? null
            : ParentNode is Document document
                ? document
                : ParentNode.OwnerDocument;

        public NodeType NodeType => GetNodeType();

        private string? _baseURI;
        public string BaseURI
        {
            get
            {
                return _baseURI ?? ParentNode?.BaseURI ?? "about:blank";
            }

            init
            {
                if (ParentNode is not null)
                {
                    throw new InvalidOperationException();
                }

                _baseURI = value;
            }
        }

        public Node? ParentNode { get; init; }
        public Element? ParentElement => ParentNode is not null && ParentNode is Element element
            ? element
            : null;

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

            if (this is ProcessingInstruction)
                return NodeType.PROCESSING_INSTRUCTION_NODE;

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

        public override string ToString()
        {
            return Stringify();
        }

        public virtual string Stringify(int indent = 0)
        {
            string tabs = IStringifier.GetTabs(indent);
            if (NodeName.StartsWith('#'))
            {
                return $"{tabs}{NodeName}\n{GetChilds(indent)}";
            }

            return $"{tabs}<{NodeName}>\n{GetChilds(indent + 2)}{tabs}</{NodeName}>";
        }

        public string GetChilds(int indent = 0)
        {
            string[] childStrings = ChildNodes.Select(node => node.Stringify(indent)).ToArray();

            string innerData = string.Join('\n', childStrings);
            if (innerData.Length > 0)
            {
                innerData += '\n';
            }

            return innerData;
        }

        public virtual UIElement? AsRendered()
        {
            Type me = GetType();

            IConsole console = this is Document document ? document.DefaultView.Console : OwnerDocument!.DefaultView.Console;

            if (me.GetCustomAttribute(typeof(HTMLAttribute)) is not HTMLAttribute attr)
            {
                console.Warn($"Component {me} {NodeName} not rendered: no HTMLAttribute");
                return null;
            }

            if (!attr.CanRender)
            {
                console.Warn($"Component {me} {NodeName} not rendered: cannot render");
                return null;
            }

            if (attr.Renderable!.GetConstructor(Array.Empty<Type>())?.Invoke(null) is not UIElement element)
            {
                console.Warn($"Component {me} {NodeName} not rendered: no suitable constructor");
                return null;
            }

            if (element is Canvas canvas)
            {
                var renderables = ChildNodes
                    .Select(child => child.AsRendered())
                    .Where(child => child is not null);

                foreach (UIElement? renderable in renderables)
                {
                    canvas.Children.Add(renderable!);
                }
            }

            if (this is Element DOMElement)
            {
                DOMRect rect = DOMElement.GetBoundingClientRect();
                element.RenderSize = new(rect.Width, rect.Height);

                // rect left/top not updating. probably calculation issue
                Canvas.SetLeft(element, rect.Left);
                Canvas.SetTop(element, rect.Top);
            }

            console.Log($"Rendered component {me} {NodeName}");
            return element;
        }
    }
}
