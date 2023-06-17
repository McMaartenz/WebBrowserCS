using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebBrowser.DOM.Nodes
{
    [HTML(NodeName = "#document", PureContainer = true)]
    public class Document : Node
    {
        private readonly Window _parentWindow;
        public Window DefaultView => _parentWindow;

        public Document(Window parentWindow) : base(null, "#document")
        {
            _parentWindow = parentWindow;
            BaseURI = parentWindow.Location;
        }

        public Node CreateElement(Node? parentNode, string name)
        {
            if (!NodeConversion.Conversions.TryGetValue(name, out Type? type))
            {
                return new(parentNode, name);
            }

            ConstructorInfo? constructor = type.GetConstructor(new Type[] { typeof(Node), typeof(string) });
            if (constructor is null)
            {
                MessageBox.Show("No constructor(Node, string) found for type " + type.FullName, "Invalid HTML", MessageBoxButton.OK, MessageBoxImage.Error);
                return new(parentNode, name);
            }

            Node node = (Node)constructor.Invoke(new object?[] { parentNode, name });
            return node;
        }

        public void LoadHTML(string HTML)
        {
            _ = new HTMLParser(this, HTML);
            UIElement? element = AsRendered();
            DefaultView.Renderer.Render(element);
        }
    }
}
