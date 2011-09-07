using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawingExtensions
{
    public enum Direction
    {
        Left,Right,Up,Down
    }
    public static class SmoothFill
    {
        public static void SmoothFillRectangle
            (this System.Drawing.Graphics graphicsObject, Brush brush, Rectangle rectangle, Direction direction, int time)
        {

        }
    }
}
