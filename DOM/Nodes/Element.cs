using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM.Nodes
{
    [HTML]
    public class Element : Node
    {
        protected Element(Node? parentNode, string nodeName) : base(parentNode, nodeName) { }

        public virtual DOMRect GetBoundingClientRect()
        {
            if (ParentNode is not Element element)
            {
                float width = (float)OwnerDocument!.DefaultView.InnerWidth;
                float height = (float)OwnerDocument!.DefaultView.InnerHeight;
                return DOMRect.FromRect(0, 0, width, height);
            }

            DOMRect parentRect = element.GetBoundingClientRect();

            // not gonna play well when this element has siblings
            return DOMRect.FromRect(0, 0, (float)parentRect.Width, (float)parentRect.Height);
        }
    }
}
