using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Points;

namespace DiscreteMaC_Lib.Graphes.Edges.EdgeComparers
{
    public class AbstractEdgeEqualityComparer<PointType> : IEqualityComparer<AbstractEdge<PointType>> where PointType: Point
    {
        private Point StartPoint { get; set; }

        public virtual bool Equals(AbstractEdge<PointType> edge1, AbstractEdge<PointType> edge2)
        {
            if (edge2 == null && edge1 == null)
                return true;
            else if (edge1 == null | edge2 == null)
                return false;
            else if (edge1.StartPoint.Equals(edge2.StartPoint) && edge1.EndPoint.Equals(edge2.EndPoint))
                return true;
            else
                return false;
        }

        public virtual int GetHashCode(AbstractEdge<PointType> obj)
        {
            return 0;
            //return obj.GetHashCode();
        }
    }
}
