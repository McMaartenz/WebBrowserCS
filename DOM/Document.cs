using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public class Document
    {
        private Window _parentWindow;

        public Document(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }
    }
}
