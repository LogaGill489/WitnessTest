using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WitnessTest
{
    internal class barrierGen
    {
        /// y position graph
        /// 24x ---------------------------|
        /// | 1--1------2------3------4---||
        /// | | 120x | 120x | 120x | 120x ||
        /// | 2---------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | 3---------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | 4---------------------------||
        /// | | 120x | 120x | 120x | 120x ||
        /// | 5---------------------------||
        /// |------------------------------| 
        /// </summary>
        /// 

        /// x position graph
        /// 24x ---------------------------|
        /// | 1------2------3------4------5|
        /// | 1 120x | 120x | 120x | 120x ||
        /// | ----------------------------||
        /// | 2 120x | 120x | 120x | 120x ||
        /// | ----------------------------||
        /// | 3 120x | 120x | 120x | 120x ||
        /// | ----------------------------||
        /// | 4 120x | 120x | 120x | 120x ||
        /// | ----------------------------||
        /// |------------------------------| 
        /// </summary>
        /// 

        public string type;
        public int xLocation, yLocation;

        public int width, height, x, y;

        public barrierGen (string type, int xLocation, int yLocation) //accepts x or y input as well as a location and draws it based of the grids above
        {
            this.type = type;
            this.xLocation = xLocation;
            this.yLocation = yLocation;

            if (type == "x") //generates along the x pattern
            {
                x = 80 + ((xLocation - 1) * 144);
                y = 24 + ((yLocation - 1) * 144);
                width = 56;
                height = 24;
            }
            else if (type == "y") //generates along the y pattern
            {
                x = 24 + ((xLocation - 1) * 144);
                y = 80 + ((yLocation - 1) * 144);
                width = 24;
                height = 56;
            }
        }
    }
}
