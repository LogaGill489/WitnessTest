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
    public partial class Form1 : Form
    {
        //Rectangle blackLayer = new Rectangle(40, 40, 620, 620);
        //Brush blackBrush = new SolidBrush(Color.Black);

        PointF mousePosition = new PointF();
        public Form1()
        {
            InitializeComponent();
            ChangeScreen(this, new MenuScreen());
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.FillRectangle(blackBrush, blackLayer);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = Cursor.Position;

            xInputF1.Text = mousePosition.X.ToString();
            yInputF1.Text = mousePosition.Y.ToString();
        }
    }
}
