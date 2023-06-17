using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public class DOMRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        
        public double Width { get; set; }
        public double Height { get; set; }

        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }

        public static DOMRect FromRect(RectangleF rectangle)
        {
            DOMRect dRect = new()
            {
                X = rectangle.X,
                Y = rectangle.Y,
                Width = rectangle.Width,
                Height = rectangle.Height,
                Top = rectangle.Y + Math.Min(0, rectangle.Height),
                Right = rectangle.X + Math.Max(0, rectangle.Width),
                Bottom = rectangle.Y + Math.Max(0, rectangle.Height),
                Left = rectangle.X + Math.Min(0, rectangle.Width),
            };

            return dRect;
        }

        public static DOMRect FromRect(float x, float y, float width, float height)
        {
            return FromRect(new(x, y, width, height));
        }
    }
}
