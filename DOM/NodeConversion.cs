using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public static class NodeConversion
    {
        public static readonly Dictionary<string, Type> Conversions;

        static NodeConversion()
        {
            Conversions = new();

            Assembly assembly = Assembly.GetAssembly(typeof(Node))!;

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(typeof(Node)))
                {
                    continue;
                }

                Attribute? attr = type.GetCustomAttribute(typeof(HTMLAttribute));
                if (attr is null)
                {
                    continue;
                }

                if (attr is HTMLAttribute htmlAttr)
                {
                    if (htmlAttr.NodeName is null)
                    {
                        continue;
                    }

                    Conversions.Add(htmlAttr.NodeName.ToLower(), type);
                }
            }
        }
    }
}
