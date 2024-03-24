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
            runTimer.Start();

            int totalTime = (int)GameScreen.playTime.ElapsedMilliseconds / 1000;
            scoreLabel.Text = $"You Beat The Game In {totalTime} Seconds!";

            Cursor.Show();
            AdjustMouseSpeed.SetMouseSpeed(18);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new TitleScreen());
        }

        private void runTimer_Tick(object sender, EventArgs e) //necessary to stay outside of the initalization
        {
            Cursor.Position = this.PointToScreen(new Point(324, 225));
            runTimer.Stop();
        }
    }
}
