using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges.EdgeComparers
{
    public class DirectedValuedEdgeEqualityComparer : IEqualityComparer<IValuedEdgeBasics<Point>>
    {
        public DirectedValuedEdgeEqualityComparer()
        { }

        public bool Equals(IValuedEdgeBasics<Point> edge1, IValuedEdgeBasics<Point> edge2)
        {
            if (edge2 == null && edge1 == null)
                return true;
            else if (edge1 == null | edge2 == null)
                return false;
            //else if (edge1.Name == edge2.Name)
            //    return true;
            else if (edge1.StartPoint.Equals(edge2.StartPoint)
                && edge1.EndPoint.Equals(edge2.EndPoint)
                && edge1.EdgeValue == edge2.EdgeValue)
                return true;
            else
                return false;
        }

        public int GetHashCode(IValuedEdgeBasics<Point> obj)
        {
            return 0;
            //return (String.Concat(obj.StartPoint.ID, obj.EndPoint.ID)).GetHashCode();
        }
    }
}
