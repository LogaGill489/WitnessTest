using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitnessTest
{
    internal class TrailIntersection
    {
        public int x, y;
        public int width = 24, height = 24;

        public TrailIntersection (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        //overrides Equals & GetHasCode methods in the hashcode to prevent an issue with the hashcode not deleting equal TrailIntersections
        public override bool Equals(object obj)
        {
            TrailIntersection q = obj as TrailIntersection;
            return q != null && q.x == this.x && q.y == this.y;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode(); //returns values that stops each TrailIntersection from being unique
        }
    }
}
