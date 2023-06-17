using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebBrowser.DOM
{
    public class Area
    {
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }

        public static Area Empty => new() { Top = 0f, Right = 0f, Bottom = 0f, Left = 0f };
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class HTMLAttribute : Attribute
    {
        public string? NodeName { get; set; }
        public bool CanRender => Renderable is not null;
        public Type? Renderable { get; set; }

        private bool _pureContainer;
        public bool PureContainer
        {
            get => _pureContainer;
            set
            {
                _pureContainer = value;
                if (value)
                {
                    Renderable ??= typeof(Canvas);
                }
            }
        }

        public Area Margin { get; set; } = Area.Empty;
    }
}
