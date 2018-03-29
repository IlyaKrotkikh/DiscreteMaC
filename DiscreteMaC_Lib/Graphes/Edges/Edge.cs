using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public class  Edge : AbstractEdge<Point>, IEquatable<Edge>
    {
        public Edge(string Name, Point StartPoint, Point EndPoint) : base(Name, StartPoint, EndPoint) { }
        public Edge(string Name, Edge EdgeToCopy) : base(Name, EdgeToCopy) { }

        public bool Equals(Edge other)
        {
            DirectedEdgeEqualityComparer defComperer = new DirectedEdgeEqualityComparer();
            return defComperer.Equals(this, other);
        }
    }
}
