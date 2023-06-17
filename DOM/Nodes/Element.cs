using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Shell;

namespace WebBrowser.DOM.Nodes
{
    [HTML]
    public class Element : Node
    {
        protected Element(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }

        public HTMLAttribute GetHTMLAttribute()
        {
            return (GetType().GetCustomAttribute(typeof(HTMLAttribute)) is HTMLAttribute HTMLAttr)
                ? HTMLAttr
                : throw new Exception($"Type {GetType()} does not have an HTMLAttribute");
        }

        // todo fix
        public static DOMRect ApplyMargin(DOMRect original, DOMRect margin)
        {
            original.X += margin.X;
            original.Y += margin.Y;

            original.Width += margin.Width;
            original.Height += margin.Height;

            return original;
        }

        public virtual DOMRect GetDesiredSize()
        {
            DOMRect rect = new();
            foreach (Element element in ChildNodes.Where(child => child is Element).Cast<Element>())
            {
                DOMRect desired = element.GetDesiredSize();

                rect.Height += desired.Height; // Append below previous
                rect.Width = Math.Max(rect.Width, desired.Width);
            }
            
            return rect;
        }

        public virtual DOMRect GetBoundingClientRect()
        {
            HTMLAttribute attr = GetHTMLAttribute();
            DOMRect margin = DOMRect.FromArea(attr.MarginArea);

            DOMRect rect = ApplyMargin(GetDesiredSize(), margin);
            if (ParentNode is not Element element)
            {
                return rect;
            }

            DOMRect parent = element.GetBoundingClientRect();
            rect.X += parent.X;
            rect.Y += parent.Y;

            double fitWidth = Math.Max(rect.Width, (rect.X - parent.X) + rect.Width - parent.Width);
            double fitHeight = Math.Max(rect.Height, (rect.Y - parent.Y) + rect.Height - parent.Height);
            rect.Width = fitWidth;
            rect.Height = fitHeight;

            Node? nodeWalker = this;
            while (nodeWalker is not null)
            {
                nodeWalker = nodeWalker.PreviousSibling;
                if (nodeWalker is Element)
                {
                    break;
                }
            }

            if (nodeWalker is Element sibling)
            {
                DOMRect siblingRect = sibling.GetBoundingClientRect();
                rect.Y += siblingRect.Bottom;
            }

            return rect;
        }
    }
}
