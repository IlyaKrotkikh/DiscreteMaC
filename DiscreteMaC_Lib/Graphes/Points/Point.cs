using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using System.Collections.ObjectModel;

namespace DiscreteMaC_Lib.Graphes.Points
{
    public class Point : IEquatable<Point>
    {
        private Dictionary<Edge, string> _ListEdges { get; set; }

        public string Name { get; set; }
        public ReadOnlyDictionary<Edge, string> ListEdges { get; set; }

        public Point(string Name)
        {
            this.Name = Name;
            _ListEdges = new Dictionary<Edge, string>(new PointEdgeEqualityComparer(this));
            ListEdges = new ReadOnlyDictionary<Edge, string>(_ListEdges);
        }

        public void AddEdge(Edge EdgeToPoint)
        {
            _ListEdges.Add(EdgeToPoint, EdgeToPoint.EndPoint.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Point other)
        {
            PointEqualityComparer defComparer = new PointEqualityComparer();
            return defComparer.Equals(this, other);
        }
    }
}
