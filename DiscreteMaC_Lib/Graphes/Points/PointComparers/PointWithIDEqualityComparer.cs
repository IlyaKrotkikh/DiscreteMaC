using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Points.PointComparers
{
    class PointWithIDEqualityComparer : IEqualityComparer<PointWithID>
    {
        public bool Equals(PointWithID point1, PointWithID point2)
        {
            if (point2 == null && point1 == null)
                return true;
            else if (point1 == null | point2 == null)
                return false;
            else if (point1.ID == point2.ID && point1.NamePrefix == point2.NamePrefix)
                return true;
            else
                return false;
        }

        public int GetHashCode(PointWithID obj)
        {
            return 0;
            //return obj.GetHashCode();
        }
    }
}
