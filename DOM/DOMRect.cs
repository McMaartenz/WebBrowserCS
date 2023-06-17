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

        public double Top => Y + Math.Min(0, Height);
        public double Right => X + Math.Max(0, Width);
        public double Bottom => Y + Math.Max(0, Height);
        public double Left => X + Math.Min(0, Width);

        public static DOMRect Clone(DOMRect rect)
        {
            return new()
            {
                X = rect.X,
                Y = rect.Y,
                Width = rect.Width,
                Height = rect.Height
            };
        }

        public static DOMRect FromArea(Area area)
        {
            return FromRect(area.Left, area.Top, area.Left + area.Right, area.Top + area.Bottom);
        }

        public static DOMRect FromRect(RectangleF rectangle)
        {
            DOMRect dRect = new()
            {
                X = rectangle.X,
                Y = rectangle.Y,
                Width = rectangle.Width,
                Height = rectangle.Height,
            };

            return dRect;
        }

        public static DOMRect FromRect(float x, float y, float width, float height)
        {
            return FromRect(new(x, y, width, height));
        }
    }
}
