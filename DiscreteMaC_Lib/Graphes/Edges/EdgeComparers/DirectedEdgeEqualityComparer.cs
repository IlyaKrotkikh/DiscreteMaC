using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges.EdgeComparers
{
    public class DirectedEdgeEqualityComparer : IEqualityComparer<Edge>
    {
        public DirectedEdgeEqualityComparer()
        { }

        public bool Equals(Edge edge1, Edge edge2)
        {
            if (edge2 == null && edge1 == null)
                return true;
            else if (edge1 == null | edge2 == null)
                return false;
            else if (edge1.Name == edge2.Name)
                return true;
            else if (edge1.StartPoint == edge2.StartPoint && edge1.EndPoint == edge2.EndPoint)
                return true;
            else
                return false;
        }

        public int GetHashCode(Edge obj)
        {
            return 0;
            //return (String.Concat(obj.StartPoint.ID, obj.EndPoint.ID)).GetHashCode();
        }
    }
}
