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
    public struct Area
    {
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }

        public static Area Empty => new() { Top = 0f, Right = 0f, Bottom = 0f, Left = 0f };
        
        public static Area From(float top, float right, float bottom, float left)
        {
            return new() { Top = top, Right = right, Bottom = bottom, Left = left };
        }

        public static Area From(float vertical, float horizontal)
        {
            return From(vertical, horizontal, vertical, horizontal);
        }

        public static Area From(float value)
        {
            return From(value, value);
        }

        public override string ToString()
        {
            return $"{Top},{Right},{Bottom},{Left}";
        }

        public static Area FromString(string str)
        {
            string[] values = str.Split(',');

            float top = 0;
            float right = 0;
            float bottom = 0;
            float left = 0;

            if (values.Length < 1 || values.Length == 3 || values.Length > 4)
            {
                throw new ArgumentException("Unexpected value count. Expected 4, 2, or 1.");
            }

            if (values.Length >= 1)
            {
                top = float.Parse(values[0]);
                right = top;
                bottom = top;
                left = top;
            }

            if (values.Length >= 2)
            {
                right = float.Parse(values[1]);
                left = right;
            }
            
            if (values.Length == 4)
            {
                bottom = float.Parse(values[2]);
                left = float.Parse(values[3]);
            }

            return new()
            {
                Top = top,
                Right = right,
                Bottom = bottom,
                Left = left,
            };
        }
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

        public string Margin
        {
            get
            {
                return MarginArea.ToString();
            }

            set
            {
                MarginArea = Area.FromString(value);
            }
        }

        public Area MarginArea { get; set; } = Area.Empty;
    }
}
