using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public interface IConsole
    {
        public abstract void Log(object contents);
        public abstract void Warn(object contents);
        public abstract void Error(object contents);
    }
}
