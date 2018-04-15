using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public class ValuedEdge<PointType> : AbstractEdge<PointType>, IValuedEdgeBasics<PointType>, IEquatable<IValuedEdgeBasics<PointType>>, IComparable<IValuedEdgeBasics<PointType>>
        where PointType : Point
    {
        public double EdgeValue { get; set; }

        public ValuedEdge(string Name, PointType StartPoint, PointType EndPoint, double EdgeValue) :
            base(Name, StartPoint, EndPoint)
        {
            this.EdgeValue = EdgeValue;
        }

        public bool Equals(IValuedEdgeBasics<PointType> other)
        {
            DirectedValuedEdgeEqualityComparer defaultComparer = new DirectedValuedEdgeEqualityComparer();
            return defaultComparer.Equals(this, other);
        }

        public override bool Equals(AbstractEdge<PointType> other)
        {
            if (other is IValuedEdgeBasics<PointType>)
                return this.Equals(other as IValuedEdgeBasics<PointType>);
            else return base.Equals(other);
        }

        public int CompareTo(IValuedEdgeBasics<PointType> other)
        {
            return this.EdgeValue.CompareTo(other.EdgeValue);
        }
    }
}