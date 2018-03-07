using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using System.Collections.ObjectModel;

namespace DiscreteMaC_Lib.Graphes.Points
{
    public class Point : IEquatable<Point>
    {
        private Dictionary<Edge, string> _ListEdges { get; set; }

        public string ID { get; set; }
        public ReadOnlyDictionary<Edge, string> ListEdges { get; set; }

        public Point(string ID)
        {
            this.ID = ID;
            _ListEdges = new Dictionary<Edge, string>(new PointEdgeEqualityComparer(this));
            ListEdges = new ReadOnlyDictionary<Edge, string>(_ListEdges);
        }

        public void AddEdge(Edge EdgeToPoint)
        {
            _ListEdges.Add(EdgeToPoint, EdgeToPoint.EndPoint.ID);
        }

        public override string ToString()
        {
            return ID;
        }

        public bool Equals(Point other)
        {

            if (other == null)
                return false;
            else if (this.ID == other.ID)
                return true;
            else return false;
        }
    }
}
