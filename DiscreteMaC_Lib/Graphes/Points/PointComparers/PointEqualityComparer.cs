using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Points.PointComparers
{
    class PointEqualityComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point point1, Point point2)
        {
            if (point2 == null && point1 == null)
                return true;
            else if (point1 == null | point2 == null)
                return false;
            else if (point1.Equals(point2))
                return true;
            else
                return false;
        }

        public int GetHashCode(Point obj)
        {
            return 0;
            //return obj.GetHashCode();
        }
    }
}
