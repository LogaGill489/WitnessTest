using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WitnessTest
{
    internal class Wall
    {
        //acts as a rectangle
        //used to generate the walls on each side of the player
        public int x, y, height, width;

        public Wall(int x, int y, int height, int width)
        {
            this.x = x; 
            this.y = y;
            this.height = height;
            this.width = width;
        }
    }
}
