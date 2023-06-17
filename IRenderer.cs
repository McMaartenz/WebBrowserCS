using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebBrowser
{
    public interface IRenderer
    {
        public abstract void Render(UIElement? element);
    }
}
