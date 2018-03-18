using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Points;

namespace DiscreteMaC_Lib.Graphes.Edges.EdgeComparers
{
    public class PointEdgeEqualityComparer : IEqualityComparer<Edge>
    {
        private Point StartPoint { get; set; }

        public PointEdgeEqualityComparer(Point StartPoint)
        {
            this.StartPoint = StartPoint;
        }

        public bool Equals(Edge edge1, Edge edge2)
        {
            if ((edge1.StartPoint != StartPoint) && (edge2.StartPoint != StartPoint))
                return false;
            if (edge2 == null && edge1 == null)
                return true;
            else if (edge1 == null | edge2 == null)
                return false;
            //else if (edge1.Name == edge2.Name)
            //    return true;
            else if (edge1.StartPoint == edge2.StartPoint && edge1.EndPoint == edge2.EndPoint)
                return true;
            else
                return false;
        }

        public int GetHashCode(Edge obj)
        {
            return 0;
            //return obj.GetHashCode();
        }
    }
}
