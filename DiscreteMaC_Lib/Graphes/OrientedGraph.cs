using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;

namespace DiscreteMaC_Lib.Graphes
{
    public class OrientedGraph : Graph<Edge>
    {
        public OrientedGraph(string GraphName) : base(GraphName, new DirectedEdgeEqualityComparer())
        {

        }

        public OrientedGraph(string GraphName, IEqualityComparer<Edge> EdgeComparer) : base(GraphName, EdgeComparer)
        {

        }

        public override void AddEdge(Edge GraphEdge)
        {
            if (ListPoint.ContainsKey(GraphEdge.StartPoint) && ListPoint.ContainsKey(GraphEdge.EndPoint))
            {
                // Переназначаем вершины графа на те, что точно содержаться в списке графа.
                GraphEdge.StartPoint = ListPoint.Keys.First(i1 => i1.Name == GraphEdge.StartPoint.Name);
                GraphEdge.EndPoint = ListPoint.Keys.First(i1 => i1.Name == GraphEdge.EndPoint.Name);

                base._ListEdges.Add(GraphEdge, GraphEdge.Name);
                (ListPoint.Keys.First(i1 => i1.Name == GraphEdge.StartPoint.Name)).AddEdge(GraphEdge);
            }
            else throw new Exception("Graph not contains this combination of points: " + GraphEdge.StartPoint.ToString() + ", " + GraphEdge.EndPoint.ToString());
        }

        public override void AddPoint(Point GraphPoint)
        {
            _ListPoint.Add(GraphPoint,GraphPoint.ListEdges);
        }
    }
}
