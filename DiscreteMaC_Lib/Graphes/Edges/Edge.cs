using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public class  Edge : IEquatable<Edge>
    {
        public string Name;
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Edge(string Name, Point StartPoint, Point EndPoint)
        {
            this.Name = Name;
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }

        public Edge(string Name, Edge EdgeToCopy)
        {
            this.Name = Name;
            StartPoint = EdgeToCopy.StartPoint;
            EndPoint = EdgeToCopy.EndPoint;
        }

        public bool Equals(Edge other)
        {
            DirectedEdgeEqualityComparer defComperer = new DirectedEdgeEqualityComparer();
            return defComperer.Equals(this, other);
        }
    }
}
