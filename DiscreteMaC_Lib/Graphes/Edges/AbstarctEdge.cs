using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public class AbstractEdge<PointType> : IEquatable<AbstractEdge<PointType>> where PointType : Point
    {
        public PointType StartPoint { get; set; }
        public PointType EndPoint { get; set; }
        public virtual string Name { get; set; }

        public AbstractEdge(string Name, PointType StartPoint, PointType EndPoint)
        {
            this.Name = Name;
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }

        public AbstractEdge(string Name, AbstractEdge<PointType> EdgeToCopy)
        {
            this.Name = Name;
            this.StartPoint = EdgeToCopy.StartPoint;
            this.EndPoint = EdgeToCopy.EndPoint;
        }

        public bool Equals(AbstractEdge<PointType> other)
        {
            AbstractEdgeEqualityComparer<PointType> defComperer = new AbstractEdgeEqualityComparer<PointType>();
            return defComperer.Equals(this, other);
        }
    }
}
