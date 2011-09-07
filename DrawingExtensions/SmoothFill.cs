using System;
using System.Drawing;
using System.Threading;

namespace DrawingExtensions
{
    public enum Direction
    {
        Left,Right,Up,Down
    }
    public static class SmoothFill
    {
        static object locker = new Object();
        public static void SmoothFillRectangle
            (this System.Drawing.Graphics graphicsObject, Brush brush, Point location,Size size, Direction direction, int time)
        {
            Pen pen = new Pen(brush, 1.0F);
            switch (direction)
            {
                case Direction.Left:
                    GivenRectangle rect1 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread1 = new Thread(SmoothFill.DrawRightToLeft);
                    newThread1.Start(rect1);
                    break;
                case Direction.Right:
                    GivenRectangle rect2 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread2 = new Thread(SmoothFill.DrawLeftToRight);
                    newThread2.Start(rect2);
                    break;
                case Direction.Up:
                    GivenRectangle rect3 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread3 = new Thread(SmoothFill.DrawDownToUp);
                    newThread3.Start(rect3);
                    break;
                case Direction.Down:
                    GivenRectangle rect4 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread4 = new Thread(SmoothFill.DrawUpToDown);
                    newThread4.Start(rect4);
                    break;
                default:
                    break;
            }
        }

        public static void DrawLeftToRight(object objRect)
        {
            GivenRectangle rect = (GivenRectangle)objRect;
            double msPerPixel = rect.Time / rect.Size.Width;
            int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
            int drawPointUpperY = rect.Location.Y;
            int drawPointDownY = rect.Location.Y + rect.Size.Height - 1;
            
            for (int i = 0; i <= rect.Size.Width - 1; i++)
            {
                int drawPointX = rect.Location.X + i;
                lock (rect.GraphicsObject)
                {
                    rect.GraphicsObject.DrawLine(rect.Pen, drawPointX, drawPointUpperY, drawPointX, drawPointDownY);
                }
                Thread.Sleep(timeForPixel);
            }
        }
        public static void DrawRightToLeft(object objRect)
        {
            GivenRectangle rect = (GivenRectangle)objRect;
            double msPerPixel = rect.Time / rect.Size.Width;
            int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
            int drawPointUpperY = rect.Location.Y;
            int drawPointDownY = rect.Location.Y + rect.Size.Height - 1;
            for (int i = 0; i <= rect.Size.Width - 1; i++)
            {
                int drawPointX = rect.Location.X + rect.Size.Width-1-i;
                lock (rect.GraphicsObject)
                {
                    rect.GraphicsObject.DrawLine(rect.Pen, drawPointX, drawPointUpperY, drawPointX, drawPointDownY);
                }
                Thread.Sleep(timeForPixel);
            }
        }
        public static void DrawDownToUp(object objRect)
        {
            GivenRectangle rect = (GivenRectangle)objRect;
            double msPerPixel = rect.Time / rect.Size.Height;
            int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
            int drawPointLeftX = rect.Location.X;
            int drawPointRightX = rect.Location.X + rect.Size.Width - 1;
            for (int i = 0; i <= rect.Size.Height-1; i++)
            {
                int drawPointY = rect.Location.Y + rect.Size.Height - 1 - i;
                lock (rect.GraphicsObject)
                {
                    rect.GraphicsObject.DrawLine(rect.Pen, drawPointLeftX, drawPointY, drawPointRightX, drawPointY);
                }
                Thread.Sleep(timeForPixel);
            }
        }
        public static void DrawUpToDown(object objRect)
        {
            GivenRectangle rect = (GivenRectangle)objRect;
            double msPerPixel = rect.Time / rect.Size.Height;
            int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
            int drawPointLeftX = rect.Location.X;
            int drawPointRightX = rect.Location.X + rect.Size.Width - 1;
            for (int i = 0; i <= rect.Size.Height - 1; i++)
            {
                int drawPointY = rect.Location.Y + i;
                lock (rect.GraphicsObject)
                {
                    rect.GraphicsObject.DrawLine(rect.Pen, drawPointLeftX, drawPointY, drawPointRightX, drawPointY);
                }
                Thread.Sleep(timeForPixel);
            }
        }

        private class GivenRectangle
        {
            Graphics graphicsObject;
            Pen pen;
            Point location;
            Size size;
            int time;

            public int Time
            {
                get { return time; }
                set { time = value; }
            }

            public Size Size
            {
                get { return size; }
                set { size = value; }
            }

            public Point Location
            {
                get { return location; }
                set { location = value; }
            }

            public Pen Pen
            {
                get { return pen; }
                set { pen = value; }
            }
            public Graphics GraphicsObject
            {
                get { return graphicsObject; }
                set { graphicsObject = value; }
            }
            public GivenRectangle(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
            {
                this.graphicsObject = graphicsObject;
                this.pen = pen;
                this.location = location;
                this.size = size;
                this.time = time;
            }

        }
        
    }

    
}
