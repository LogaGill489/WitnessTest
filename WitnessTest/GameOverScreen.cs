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
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {
            InitializeComponent();

            int totalTime = (int)GameScreen.playTime.ElapsedMilliseconds / 1000;
            scoreLabel.Text = $"You Beat The Game In {totalTime} Seconds!";

            Cursor.Show();
            AdjustMouseSpeed.SetMouseSpeed(18);
            Cursor.Position = new Point(this.Width / 2, this.Height / 2);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new TitleScreen());
        }
    }
}
