using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes
{
    public class DirectedGraphWithValuedEdgeAndPointID : AbstractGraph<PointWithID, ValuedEdge<PointWithID>>
    {
        protected int PointIncrementalCounter { get; set; }

        public string PointNamePrefix { get; set; }

        public DirectedGraphWithValuedEdgeAndPointID(string GraphName, string PointNamePrefix, IEqualityComparer<IValuedEdgeBasics<PointWithID>> EdgeComparer) :
            base(GraphName, EdgeComparer)
        {
            this.PointNamePrefix = PointNamePrefix;
            PointIncrementalCounter = 1;
        }

        public override bool AddEdge(ValuedEdge<PointWithID> GraphEdge)
        {
            if (PointCollection.Contains(GraphEdge.StartPoint) && PointCollection.Contains(GraphEdge.EndPoint))
            {
                // Переназначаем вершины графа на те, что точно содержаться в списке графа.
                GraphEdge.StartPoint = PointCollection.First(i1 => i1.Name == GraphEdge.StartPoint.Name);
                GraphEdge.EndPoint = PointCollection.First(i1 => i1.Name == GraphEdge.EndPoint.Name);

                return base.HashSetEdges.Add(GraphEdge);
            }
            else throw new Exception("Graph not contains this combination of points: " + GraphEdge.StartPoint.ToString() + ", " + GraphEdge.EndPoint.ToString());
        }

        public bool AddPoint()
        {
            if (HashSetPoints.Add(new PointWithID(PointIncrementalCounter, PointNamePrefix)))
            {
                PointIncrementalCounter++;
                return true;
            }
            else return false;
        }

        public override bool AddPoint(PointWithID GraphPoint)
        {
            if (GraphPoint.NamePrefix != PointNamePrefix || PointCollection.Count(i1 => i1.ID == GraphPoint.ID) > 0)
                throw new Exception(String.Format("Point {0} not compatible from this graph or already contains in collection points.", GraphPoint));
            else
            {
                if (HashSetPoints.Add(GraphPoint))
                {
                    PointIncrementalCounter = PointCollection.Count();
                    return true;
                }
                else return false;
            }
        }
    }
}
