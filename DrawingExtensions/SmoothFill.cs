using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DrawingExtensions
{
    public enum Direction
    {
        Left,Right,Up,Down
    }
    public static class SmoothFillRectMultithreading
    {
       
        public static void SmoothFillRectangleMultithreading
            (this System.Drawing.Graphics graphicsObject, Brush brush, Point location,Size size, Direction direction, int time)
        {
            Pen pen = new Pen(brush, 1.0F);
            switch (direction)
            {
                case Direction.Left:
                    GivenRectangle rect1 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread1 = new Thread(SmoothFillRectMultithreading.DrawRightToLeft);
                    newThread1.Start(rect1);
                    break;
                case Direction.Right:
                    GivenRectangle rect2 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread2 = new Thread(SmoothFillRectMultithreading.DrawLeftToRight);
                    newThread2.Start(rect2);
                    break;
                case Direction.Up:
                    GivenRectangle rect3 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread3 = new Thread(SmoothFillRectMultithreading.DrawDownToUp);
                    newThread3.Start(rect3);
                    break;
                case Direction.Down:
                    GivenRectangle rect4 = new GivenRectangle(graphicsObject, pen, location, size, time);
                    Thread newThread4 = new Thread(SmoothFillRectMultithreading.DrawUpToDown);
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


    public static class SmoothFillRectNoThreading
    {
        public static void SmoothFillRectangleNoThreading
           (this System.Drawing.Graphics graphicsObject, Brush brush, Point location, Size size, Direction direction, int time)
        {
            Pen pen = new Pen(brush, 1.0F);
            switch (direction)
            {
                case Direction.Left:
                    RectangleToLeft rect1 = new RectangleToLeft(graphicsObject, pen, location, size, time);
                    break;
                case Direction.Right:
                    RectangleToRight rect2 = new RectangleToRight(graphicsObject, pen, location, size, time);                    
                    break;
                case Direction.Up:
                    RectangleToUp rect3 = new RectangleToUp(graphicsObject, pen, location, size, time);
                    break;
                case Direction.Down:
                    RectangleToDown rect4 = new RectangleToDown(graphicsObject, pen, location, size, time);
                    break;
                default:
                    break;
            }
        }

        
        private class RectangleToRight
        {
            private Graphics graphicsObject;
            private Pen pen;
            private Point location;
            private Size size;
            private System.Windows.Forms.Timer drawTimer;
            private int count;
            
            public System.Windows.Forms.Timer DrawTimer
            {
                get { return drawTimer; }
                set { drawTimer = value; }
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
            public RectangleToRight(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
            {
                double msPerPixel = time / size.Width;
                int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
                this.graphicsObject = graphicsObject;
                this.pen = pen;
                this.location = location;
                this.size = size;
                this.drawTimer = new System.Windows.Forms.Timer();
                this.drawTimer.Interval = timeForPixel;
                this.drawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.drawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                int drawPointUpperY = this.Location.Y;
                int drawPointDownY = this.Location.Y + this.Size.Height - 1;
                int drawPointX = this.Location.X + count;
                
                lock (GraphicsObject)
                {
                    graphicsObject.DrawLine(pen, drawPointX, drawPointUpperY, drawPointX, drawPointDownY);
                }
                count++;
                if (count==this.size.Width)
                {
                    this.DrawTimer.Stop();
                }
            }
        }

        private class RectangleToLeft
        {
            Graphics graphicsObject;
            Pen pen;
            Point location;
            Size size;
            System.Windows.Forms.Timer drawTimer;
            int count;

            public System.Windows.Forms.Timer DrawTimer
            {
                get { return drawTimer; }
                set { drawTimer = value; }
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
            public RectangleToLeft(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
            {
                double msPerPixel = time / size.Width;
                int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
                this.graphicsObject = graphicsObject;
                this.pen = pen;
                this.location = location;
                this.size = size;
                this.drawTimer = new System.Windows.Forms.Timer();
                this.drawTimer.Interval = timeForPixel;
                this.drawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.drawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                int drawPointUpperY = this.Location.Y;
                int drawPointDownY = this.Location.Y + this.Size.Height - 1;
                int drawPointX = this.Location.X + this.size.Width-1-count;                
                lock (GraphicsObject)
                {
                    graphicsObject.DrawLine(pen, drawPointX, drawPointUpperY, drawPointX, drawPointDownY);
                }
                count++;
                if (count == this.size.Width)
                {
                    this.DrawTimer.Stop();
                }
            }
        }

        private class RectangleToDown
        {
            private Graphics graphicsObject;
            private Pen pen;
            private Point location;
            private Size size;
            private System.Windows.Forms.Timer drawTimer;
            private int count;

            public System.Windows.Forms.Timer DrawTimer
            {
                get { return drawTimer; }
                set { drawTimer = value; }
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
            public RectangleToDown(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
            {
                double msPerPixel = time / size.Width;
                int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
                this.graphicsObject = graphicsObject;
                this.pen = pen;
                this.location = location;
                this.size = size;
                this.drawTimer = new System.Windows.Forms.Timer();
                this.drawTimer.Interval = timeForPixel;
                this.drawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.drawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                int drawPointLeftX = this.Location.X;
                int drawPointRightX = this.Location.X + this.Size.Width - 1;
                int drawPointY = this.Location.Y + count;

                lock (GraphicsObject)
                {
                    graphicsObject.DrawLine(pen, drawPointLeftX,drawPointY,drawPointRightX,drawPointY);
                }
                count++;
                if (count == this.size.Height)
                {
                    this.DrawTimer.Stop();
                }
            }
        }
        private class RectangleToUp
        {
            private Graphics graphicsObject;
            private Pen pen;
            private Point location;
            private Size size;
            private System.Windows.Forms.Timer drawTimer;
            private int count;

            public System.Windows.Forms.Timer DrawTimer
            {
                get { return drawTimer; }
                set { drawTimer = value; }
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
            public RectangleToUp(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
            {
                double msPerPixel = time / size.Width;
                int timeForPixel = Convert.ToInt32(Math.Floor(msPerPixel));
                this.graphicsObject = graphicsObject;
                this.pen = pen;
                this.location = location;
                this.size = size;
                this.drawTimer = new System.Windows.Forms.Timer();
                this.drawTimer.Interval = timeForPixel;
                this.drawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.drawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                int drawPointLeftX = this.Location.X;
                int drawPointRightX = this.Location.X + this.Size.Width - 1;
                int drawPointY = this.Location.Y + this.size.Height-1- count;

                lock (GraphicsObject)
                {
                    graphicsObject.DrawLine(pen, drawPointLeftX, drawPointY, drawPointRightX, drawPointY);
                }
                count++;
                if (count == this.size.Height)
                {
                    this.DrawTimer.Stop();
                }
            }
        }
    }    
}
