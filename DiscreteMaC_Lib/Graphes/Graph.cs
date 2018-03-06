using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Edges;

namespace DiscreteMaC_Lib.Graphes
{
    public abstract class Graph<EdgeType> where EdgeType: Edge
    {
        protected Dictionary<Point, ReadOnlyDictionary<Edge, string>> _ListPoint { get; set; }
        protected Dictionary<EdgeType, string> _ListEdges { get; set; }

        public ReadOnlyDictionary<Point, ReadOnlyDictionary<Edge, string>> ListPoint { get; private set; }
        public ReadOnlyDictionary<EdgeType, string> ListEdges { get; private set; }
        public string GraphName { get; set; }

        public Graph(string GraphName, IEqualityComparer<Edge> EdgeComparer)
        {
            this.GraphName = GraphName;

            _ListPoint = new Dictionary<Point, ReadOnlyDictionary<Edge, string>>(new PointEqualityComparer());
            _ListEdges = new Dictionary<EdgeType, string>(EdgeComparer);

            ListPoint = new ReadOnlyDictionary<Point, ReadOnlyDictionary<Edge, string>>(_ListPoint);
            ListEdges = new ReadOnlyDictionary<EdgeType, string>(_ListEdges);
        }

        public abstract void AddPoint(Point GraphPoint);    
        public abstract void AddEdge(Edge GraphEdge);
    }
}
