using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WitnessTest
{
    public partial class MenuScreen : UserControl
    {
        List<Wall> walls = new List<Wall>();
        List<Wall> outerWalls = new List<Wall>();
        
        Brush redBrush = new SolidBrush(Color.Maroon);
        Brush fireBrush = new SolidBrush(Color.Firebrick);
        Brush coralBrush = new SolidBrush(Color.LightCoral);
        Brush blackBrush = new SolidBrush(Color.Black);

        Rectangle cursorRectangle = new Rectangle(0, 0, 1, 1);
        Rectangle bottomCircle = new Rectangle(14, 586, 50, 50);

        PointF mousePosition;
        public MenuScreen()
        {
            InitializeComponent();
            gameTimer.Enabled = true;

            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Wall wallSegment = new Wall(24 * (i + 2) + (i * 120), 24 * (k + 2) + (k * 120), 120, 120);
                    walls.Add(wallSegment);
                }
            }

            Wall outerLayer = new Wall(0, 0, this.Height, 24);
            outerWalls.Add(outerLayer);

            outerLayer = new Wall(0, 0, 24, this.Width);
            outerWalls.Add(outerLayer);

            outerLayer = new Wall(this.Width - 24, 0, this.Height, 24);
            outerWalls.Add(outerLayer);

            outerLayer = new Wall(0, this.Height - 24, 24, this.Width);
            outerWalls.Add(outerLayer);


        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void MenuScreen_Paint(object sender, PaintEventArgs e)
        {

            foreach (Wall wall in walls)
            {
                e.Graphics.FillRectangle(redBrush, new Rectangle(wall.x, wall.y, wall.width, wall.height));
            }

            foreach (Wall wall in outerWalls)
            {
                e.Graphics.FillRectangle(blackBrush, new Rectangle(wall.x, wall.y, wall.width, wall.height));
            }

            if (cursorRectangle.IntersectsWith(bottomCircle)) {
                e.Graphics.FillEllipse(coralBrush, bottomCircle);
            }
            else
            {
                e.Graphics.FillEllipse(fireBrush, bottomCircle);
            }

            //e.Graphics.FillEllipse(fireBrush, new Rectangle(14, this.Height - 61, 50, 50));

        }

        private void MenuScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = this.PointToClient(Cursor.Position);

            cursorRectangle = new Rectangle(Convert.ToInt16(mousePosition.X), Convert.ToInt16(mousePosition.Y), 2, 2);

            xInput.Text = mousePosition.X.ToString();
            yInput.Text = mousePosition.Y.ToString();

            rectXPos.Text = cursorRectangle.X.ToString();
            rectYPos.Text = cursorRectangle.Y.ToString();
        }
    }
}
