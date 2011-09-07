using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawingExtensions;

namespace TestFunctionality
{
    public partial class DisplayForm : Form
    {
        Graphics graphicsObject;

        public Graphics GraphicsObject
        {
            get { return graphicsObject; }
            set { graphicsObject = value; }
        }

        public DisplayForm()
        {
            InitializeComponent();
            graphicsObject = this.CreateGraphics();

            this.Paint += new PaintEventHandler(DisplayForm_Paint);
            int expresion =Convert.ToInt32( 400 / 24.53);
            textBoxOut.Text = expresion.ToString();
           
        }

        void DisplayForm_Paint(object sender, PaintEventArgs e)
        {
          // GraphicsObject.SmoothFillRectangle(new SolidBrush(Color.AliceBlue),new Point(40,40),new Size(50,40), Direction.Right, 1000);
          // GraphicsObject.SmoothFillRectangle(new SolidBrush(Color.Black), new Point(90, 40), new Size(50, 40), Direction.Right, 1000);
           /* GraphicsObject.DrawLine(new Pen(Color.Black,1.0F),50,40,50,80);
            GraphicsObject.DrawLine(new Pen(Color.Black, 1.0F), 51, 40, 51, 80);
            GraphicsObject.DrawLine(new Pen(Color.Black, 1.8F), 50, 40, 50, 80);*/

           GraphicsObject.SmoothFillRectangle
              (new SolidBrush(Color.AliceBlue), new Point(10, 10), new Size(50, 40), Direction.Right, 8000);
           GraphicsObject.SmoothFillRectangle
              (new SolidBrush(Color.AliceBlue), new Point(60, 90), new Size(50, 40), Direction.Down, 20000);
           GraphicsObject.SmoothFillRectangle
              (new SolidBrush(Color.Black), new Point(100, 100), new Size(50, 40), Direction.Left, 10000);
           GraphicsObject.SmoothFillRectangle
              (new SolidBrush(Color.AliceBlue), new Point(20, 50), new Size(50, 40), Direction.Up, 1000);
           GraphicsObject.SmoothFillRectangle
              (new SolidBrush(Color.AliceBlue), new Point(90, 70), new Size(50, 40), Direction.Right, 5000);
         
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            //pictureBoxGraphic.BackColor = Color.White;
           // pictureBoxGraphic.Paint += new PaintEventHandler(pictureBoxGraphic_Paint);
          
        }

        void pictureBoxGraphic_Paint(object sender, PaintEventArgs e)
        {
            GraphicsObject.SmoothFillRectangle
                (new SolidBrush(Color.AliceBlue), new Point(10, 10), new Size(50, 40), Direction.Right, 8000);
            GraphicsObject.SmoothFillRectangle
               (new SolidBrush(Color.AliceBlue), new Point(60, 90), new Size(50, 40), Direction.Right, 20000);
            GraphicsObject.SmoothFillRectangle
               (new SolidBrush(Color.Black), new Point(100, 100), new Size(50, 40), Direction.Left, 10000);
            GraphicsObject.SmoothFillRectangle
               (new SolidBrush(Color.AliceBlue), new Point(20, 50), new Size(50, 40), Direction.Right, 1000);
            GraphicsObject.SmoothFillRectangle
               (new SolidBrush(Color.AliceBlue), new Point(90, 70), new Size(50, 40), Direction.Right, 5000);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            lock (GraphicsObject)
            {
                GraphicsObject.FillRectangle(new SolidBrush(Color.YellowGreen), 70, 120, 40, 40);
            }
        }

    }
}
