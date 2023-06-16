using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{

    [AttributeUsage(AttributeTargets.Class)]
    public class HTMLAttribute : Attribute
    {
        public string? NodeName { get; set; }
    }
}
