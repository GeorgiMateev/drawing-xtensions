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

        private abstract class GivenRectangle
        {
            private Graphics graphicsObject;
            private Pen pen;
            private Point location;
            private Size size;
            private System.Windows.Forms.Timer drawTimer;
            private int count;

            public int Count
            {
                get { return count; }
                set { count = value; }
            }

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
            public GivenRectangle(Graphics graphic,Pen pen,Point location,Size size,int time)
            {
                               
            }
            public virtual void DrawRectangle()
            {
            }
        }

        private class RectangleToRight : GivenRectangle
        {
            
            public RectangleToRight(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
                :base(graphicsObject, pen,location,size,time)
            {
                double msPerPixel = (double)time / size.Width;
                int timeForPixel = Convert.ToInt32(Math.Round(msPerPixel));
                if (timeForPixel == 0) timeForPixel = 1;
                this.GraphicsObject = graphicsObject;
                this.Pen = pen;
                this.Location = location;
                this.Size = size;
                this.DrawTimer = new System.Windows.Forms.Timer();
                this.DrawTimer.Interval = timeForPixel;
                this.DrawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.DrawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
               this.DrawRectangle();
            }

            public override void DrawRectangle()
            {
                int drawPointUpperY = this.Location.Y;
                int drawPointDownY = this.Location.Y + this.Size.Height - 1;
                int drawPointX = this.Location.X + this.Count;

                lock (this.GraphicsObject)
                {
                    this.GraphicsObject.DrawLine(Pen, drawPointX, drawPointUpperY, drawPointX, drawPointDownY);
                }
                Count++;
                if (Count == this.Size.Width)
                {
                    this.DrawTimer.Stop();
                }
            }
        }

        private class RectangleToLeft : GivenRectangle
        {
           
            public RectangleToLeft(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
                : base(graphicsObject, pen, location, size, time)
            {
                double msPerPixel =(double)time/size.Width;
                int timeForPixel = Convert.ToInt32(Math.Round(msPerPixel));
                if (timeForPixel == 0) timeForPixel = 1;
                this.GraphicsObject = graphicsObject;
                this.Pen = pen;
                this.Location = location;
                this.Size = size;
                this.DrawTimer = new System.Windows.Forms.Timer();
                this.DrawTimer.Interval = timeForPixel;
                this.DrawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.DrawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                this.DrawRectangle();
            }

            public  override void DrawRectangle()
            {
                int drawPointUpperY = this.Location.Y;
                int drawPointDownY = this.Location.Y + this.Size.Height - 1;
                int drawPointX = this.Location.X + this.Size.Width - 1 - Count;
                
                    GraphicsObject.DrawLine(Pen, drawPointX, drawPointUpperY, drawPointX, drawPointDownY);
                
                Count++;
                if (Count == this.Size.Width)
                {
                    this.DrawTimer.Stop();
                }
            }
        }

        private class RectangleToDown : GivenRectangle
        {
            
            public RectangleToDown(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
                : base(graphicsObject, pen, location, size, time)
            {
                double msPerPixel = (double)time / size.Height;
                int timeForPixel = Convert.ToInt32(Math.Round(msPerPixel));
                if (timeForPixel == 0) timeForPixel = 1;
                this.GraphicsObject = graphicsObject;
                this.Pen = pen;
                this.Location = location;
                this.Size = size;
                this.DrawTimer = new System.Windows.Forms.Timer();
                this.DrawTimer.Interval = timeForPixel;
                this.DrawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.DrawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                this.DrawRectangle();
            }

            public override void DrawRectangle()
            {
                int drawPointLeftX = this.Location.X;
                int drawPointRightX = this.Location.X + this.Size.Width - 1;
                int drawPointY = this.Location.Y + Count;

                lock (this.GraphicsObject)
                {
                    GraphicsObject.DrawLine(Pen, drawPointLeftX, drawPointY, drawPointRightX, drawPointY);
                }
                Count++;
                if (Count == this.Size.Height)
                {
                    this.DrawTimer.Stop();
                }
            }
        }
        private class RectangleToUp : GivenRectangle
        {
           
            public RectangleToUp(Graphics graphicsObject, Pen pen, Point location, Size size, int time)
                : base(graphicsObject, pen, location, size, time)
            {
                double msPerPixel = (double)time / size.Height;
                int timeForPixel = Convert.ToInt32(Math.Round(msPerPixel));
                if (timeForPixel == 0) timeForPixel = 1;
                this.GraphicsObject = graphicsObject;
                this.Pen = pen;
                this.Location = location;
                this.Size = size;
                this.DrawTimer = new System.Windows.Forms.Timer();
                this.DrawTimer.Interval = timeForPixel;
                this.DrawTimer.Tick += new EventHandler(drawTimer_Tick);
                this.DrawTimer.Start();
            }

            void drawTimer_Tick(object sender, EventArgs e)
            {
                this.DrawRectangle();
            }

            public override  void DrawRectangle()
            {
                int drawPointLeftX = this.Location.X;
                int drawPointRightX = this.Location.X + this.Size.Width - 1;
                int drawPointY = this.Location.Y + this.Size.Height - 1 - Count;

                lock (this.GraphicsObject)
                {
                    GraphicsObject.DrawLine(Pen, drawPointLeftX, drawPointY, drawPointRightX, drawPointY);
                }
                Count++;
                if (Count == this.Size.Height)
                {
                    this.DrawTimer.Stop();
                }
            }
        }
    }    
}
