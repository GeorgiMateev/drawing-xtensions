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
            Graphics graphicsObject = this.CreateGraphics();
            this.Paint += new PaintEventHandler(DisplayForm_Paint);

            int expresion =Convert.ToInt32( 1000 / 100.5);
            textBoxOut.Text = expresion.ToString();
           
        }

        void DisplayForm_Paint(object sender, PaintEventArgs e)
        {
            GraphicsObject.SmoothFillRectangle(new SolidBrush(Color.AliceBlue), new Rectangle(3, 4, 5, 6), Direction.Down, 50);
        }

    }
}
