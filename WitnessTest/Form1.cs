using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * The Witness
 * Logan Gillett
 * Mr. T
 * 25.3.24
 * ICS4U
 */

namespace WitnessTest
{ 
    public partial class Form1 : Form
    {
        //Rectangle blackLayer = new Rectangle(40, 40, 620, 620);
        //Brush blackBrush = new SolidBrush(Color.Black);

        PointF mousePosition = new PointF();
        public Form1()
        {
            InitializeComponent();

            ChangeScreen(this, new TitleScreen());
        }

        public static void ChangeScreen(object sender, UserControl next)
        {
            Form f; // will either be the sender or parent of sender

            if (sender is Form)
            {
                f = (Form)sender;                          //f is sender
            }
            else
            {
                UserControl current = (UserControl)sender;  //create UserControl from sender
                f = current.FindForm();                     //find Form UserControl is on
                f.Controls.Remove(current);                 //remove current UserControl
            }

            // add the new UserControl to the middle of the screen and focus on it
            next.Location = new Point((f.ClientSize.Width - next.Width) / 2,
                (f.ClientSize.Height - next.Height) / 2);
            f.Controls.Add(next);
            next.Focus();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = Cursor.Position;

            xInputF1.Text = mousePosition.X.ToString();
            yInputF1.Text = mousePosition.Y.ToString();
        }

        private void updateScreen_Tick(object sender, EventArgs e)
        {
            levelLabel.Text = $"Level: {GameScreen.level}";
            if (GameScreen.playTime.ElapsedMilliseconds >= 1000 && GameScreen.playTime.ElapsedMilliseconds <= 2000)
            {
                timeLabel.Text = "1 Second Elapsed";
            }
            else
            {
                int time = (int)(GameScreen.playTime.ElapsedMilliseconds / 1000);
                timeLabel.Text = $"{time} Seconds Elapsed";
            }

            if (GameScreen.level == 13 || GameScreen.level == 0)
            {
                levelLabel.Visible = false;
                timeLabel.Visible = false;
            }
            else
            {
                levelLabel.Visible = true;
                timeLabel.Visible = true;
            }
            levelLabel.Refresh();
        }
    }
}
