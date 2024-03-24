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
    public partial class TitleScreen : UserControl
    {
        public TitleScreen()
        {
            InitializeComponent();


        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new GameOverScreen());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TitleScreen_Paint(object sender, PaintEventArgs e)
        {
            Rectangle bottomLeft = new Rectangle(0, 550, 60, 24);
            Rectangle bottomRight = new Rectangle(36, 574, 24, 74);

            Brush brownBrush = new SolidBrush(Color.SandyBrown);

           // e.Graphics.FillRectangle(brownBrush, bottomLeft);
            //e.Graphics.FillRectangle(brownBrush, bottomRight);
        }
    }
}
