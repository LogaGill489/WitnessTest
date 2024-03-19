using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessTest
{
    internal class barrierGen
    {
        /// x position graph
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

        /// y position graph
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

        public int width = 24, height = 24;

        public barrierGen (string type, int xLocation, int yLocation)
        {
            this.type = type;
            this.xLocation = xLocation;
            this.yLocation = yLocation;
        }
    }
}
