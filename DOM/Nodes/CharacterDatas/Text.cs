using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WebBrowser.DOM.Nodes.CharacterDatas
{
    [HTML(NodeName = "#text", Renderable = typeof(TextBlock))]
    public class Text : CharacterData
    {
        public Text(Node? parentNode, string nodeName = "#text") : base(parentNode, nodeName) { }

        public override string Stringify(int indent = 0)
        {
            string tabs = IStringifier.GetTabs(indent);
            return $"{tabs}{NodeValue}";
        }

        public override UIElement? AsRendered()
        {
            if (base.AsRendered() is not TextBlock element)
            {
                return null;
            }

            element.Text = NodeValue;
            return element;
        }
    }
}
