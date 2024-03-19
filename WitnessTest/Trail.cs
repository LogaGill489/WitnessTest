using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessTest
{
    internal class Trail
    {
        public int x, y, width, height;
        public string state;

        public Trail (int x, int y, int width, int height, string state)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.state = state;
        }
    }
}
