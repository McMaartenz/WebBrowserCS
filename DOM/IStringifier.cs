using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public interface IStringifier
    {
        public abstract string Stringify(int indent = 0);
        public abstract string GetChilds(int indent = 0);
        
        public static string GetTabs(int indent)
        {
            return new(' ', indent);
        }
    }
}
