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
    public class DirectedGraph : AbstractGraph<Point, Edge>
    {
        public DirectedGraph(string GraphName) : base(GraphName, new DirectedEdgeEqualityComparer())
        {

        }

        public DirectedGraph(string GraphName, IEqualityComparer<Edge> EdgeComparer) : base(GraphName, EdgeComparer)
        {

        }

        public override bool AddEdge(Edge GraphEdge)
        {
            if (PointCollection.Contains(GraphEdge.StartPoint) && PointCollection.Contains(GraphEdge.EndPoint))
            {
                // Переназначаем вершины графа на те, что точно содержаться в списке графа.
                GraphEdge.StartPoint = PointCollection.First(i1 => i1.Name == GraphEdge.StartPoint.Name);
                GraphEdge.EndPoint = PointCollection.First(i1 => i1.Name == GraphEdge.EndPoint.Name);

                if (base.HashSetEdges.Add(GraphEdge))
                {
                    (PointCollection.First(i1 => i1.Name == GraphEdge.StartPoint.Name)).AddEdge(GraphEdge);
                    return true;
                }
                else return false;
            }
            else throw new Exception("Graph not contains this combination of points: " + GraphEdge.StartPoint.ToString() + ", " + GraphEdge.EndPoint.ToString());
        }

        public override bool AddPoint(Point GraphPoint)
        {
            return HashSetPoints.Add(GraphPoint);
        }
    }
}