using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    public class Pointer<T>
    {
        public T? Object { get; set; }

        public bool Null => Object is null;
    }

    public class VoidPointer : Pointer<object> { }
}
