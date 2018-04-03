using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Paths;

namespace DiscreteMaC_Lib.Graphes
{
    public delegate bool SelectionCondition<in Type>(Type ObjToCondition);

    public static class GraphUtils
    {
        private static Random GlobalRandom = new Random();

        public static DirectedGraph DirectedGraphIntersection(IGraphBasics<Point, Edge> g1, IGraphBasics<Point, Edge> g2)
        {
            string graphName = g1.GraphName + "∩" + g2.GraphName;
            DirectedGraph OutGraph = new DirectedGraph(graphName);

            List<Point> ListTempPoints = g1.PointCollection.ToList();
            List<Edge> ListTempEdges = g1.EdgeCollection.ToList();

            ListTempPoints.AddRange((g2.PointCollection).Except(g1.PointCollection));
            ListTempEdges = ListTempEdges.Intersect(g2.EdgeCollection, new DirectedEdgeEqualityComparer()).ToList();

            foreach (Point p in ListTempPoints)
            {
                OutGraph.AddPoint(new Point(p.Name));
            }
            int i = 0;
            foreach (Edge e in ListTempEdges)
            {
                OutGraph.AddEdge(new Edge(graphName + "_" + i.ToString(), e));
                i++;
            }

            return OutGraph;
        }
        public static DirectedGraph DirectedGraphUnion(IGraphBasics<Point, Edge> g1, IGraphBasics<Point, Edge> g2)
        {
            string graphName = g1.GraphName + "∪" + g2.GraphName;
            DirectedGraph OutGraph = new DirectedGraph(graphName);

            IEnumerable<Point> ListTempPoints = g1.PointCollection.Union(g2.PointCollection, new PointEqualityComparer());
            IEnumerable<Edge> ListTempEdges = g1.EdgeCollection.Union(g2.EdgeCollection, new DirectedEdgeEqualityComparer());

            foreach (Point p in ListTempPoints)
            {
                OutGraph.AddPoint(new Point(p.Name));
            }
            int i = 0;
            foreach (Edge e in ListTempEdges)
            {
                OutGraph.AddEdge(new Edge(graphName + "_" + i.ToString(), e));
                i++;
            }

            return OutGraph;
        }

        public static DirectedGraph GenerateRandomDirectedGraph(string GraphName)
        {
            return GenerateRandomDirectedGraph(GraphName, GlobalRandom.Next(1, 101));
        }
        public static DirectedGraph GenerateRandomDirectedGraph(string GraphName, int PointCount)
        {
            return GenerateRandomDirectedGraph(GraphName, PointCount, GlobalRandom.Next(0, Convert.ToInt32(Math.Pow(PointCount, 2))));
        }
        public static DirectedGraph GenerateRandomDirectedGraph(string GraphName, int PointCount, int EdgeCount)
        {
            if (EdgeCount > Math.Pow(PointCount, 2))
                throw new Exception("Count of > PointCount^2");

            DirectedGraph OutGraph = GenerateEmptyDirectedGraph(GraphName, PointCount);

            List<Point> ListPoints = OutGraph.PointCollection.ToList();
            for (int i = 1; i <= EdgeCount;)
            {
                if (OutGraph.AddEdge(new Edge(GraphName + "_" + i.ToString(), ListPoints[GlobalRandom.Next(0, ListPoints.Count)], ListPoints[GlobalRandom.Next(0, ListPoints.Count)])))
                {
                    i++;
                }
            }

            return OutGraph;
        }
        public static DirectedGraph GenerateEmptyDirectedGraph(string GraphName, int PointCount)
        {
            DirectedGraph OutGraph = new DirectedGraph(GraphName);

            string format = "D" + ((PointCount.ToString()).Length).ToString();
            for (int i = 1; i <= PointCount; i++)
            {

                OutGraph.AddPoint(new Point("x" + i.ToString(format)));
            }

            return OutGraph;
        }

        public static DirectedGraphWithPointID GenerateRandomDirectedGraphWithPointID(string GraphName, string PointNamePrefix)
        {
            return GenerateRandomDirectedGraphWithPointID(GraphName, PointNamePrefix, GlobalRandom.Next(1, 101));
        }
        public static DirectedGraphWithPointID GenerateRandomDirectedGraphWithPointID(string GraphName, string PointNamePrefix, int PointCount)
        {
            return GenerateRandomDirectedGraphWithPointID(GraphName, PointNamePrefix, PointCount, GlobalRandom.Next(0, Convert.ToInt32(Math.Pow(PointCount, 2))));
        }
        public static DirectedGraphWithPointID GenerateRandomDirectedGraphWithPointID(string GraphName, string PointNamePrefix, int PointCount, int EdgeCount)
        {
            if (EdgeCount > Math.Pow(PointCount, 2))
                throw new Exception("Count of > PointCount^2");

            DirectedGraphWithPointID OutGraph = GenerateEmptyDirectedGraphWithPointID(GraphName, PointNamePrefix, PointCount);

            List<PointWithID> ListPoints = OutGraph.PointCollection.ToList();
            for (int i = 1; i <= EdgeCount;)
            {
                if (OutGraph.AddEdge(new EdgePointID(GraphName + "_" + i.ToString(), ListPoints[GlobalRandom.Next(0, ListPoints.Count)], ListPoints[GlobalRandom.Next(0, ListPoints.Count)])))
                {
                    i++;
                }
            }

            return OutGraph;
        }
        public static DirectedGraphWithPointID GenerateEmptyDirectedGraphWithPointID(string GraphName, string PointNamePrefix, int PointCount)
        { 
            DirectedGraphWithPointID OutGraph = new DirectedGraphWithPointID(GraphName, PointNamePrefix, new AbstractEdgeEqualityComparer<PointWithID>());
            for (int i = 1; i <= PointCount; i++)
            {
                OutGraph.AddPoint();
            }
            return OutGraph;
        }

        public static DirectedGraphWithPointID GetInducedSubgraph(DirectedGraphWithPointID CurrentGraph, SelectionCondition<PointWithID> PointSelectionCondition)
        {
            DirectedGraphWithPointID OutGraph = new DirectedGraphWithPointID(String.Format("Induced \"{0}\"", CurrentGraph.GraphName), CurrentGraph.PointNamePrefix, new AbstractEdgeEqualityComparer<PointWithID>());

            IEnumerable<PointWithID> pointCollection = CurrentGraph.PointCollection.Where(i1 => PointSelectionCondition(i1));
            IEnumerable<EdgePointID> edgeCollection = CurrentGraph.EdgeCollection.Where(i1 => pointCollection.Contains(i1.StartPoint) && pointCollection.Contains(i1.EndPoint));

            foreach (PointWithID p in pointCollection)
                OutGraph.AddPoint(p);
            foreach (EdgePointID e in edgeCollection)
                OutGraph.AddEdge(e);

            return OutGraph;
        }
        public static DirectedGraphWithPointID GetSpanningSubgraph(DirectedGraphWithPointID CurrentGraph, SelectionCondition<EdgePointID> EdgeSelectionCondition)
        {
            DirectedGraphWithPointID OutGraph = new DirectedGraphWithPointID(String.Format("Spanning \"{0}\"", CurrentGraph.GraphName), CurrentGraph.PointNamePrefix, new AbstractEdgeEqualityComparer<PointWithID>());

            IEnumerable<EdgePointID> edgeCollection = CurrentGraph.EdgeCollection.Where(i1 => EdgeSelectionCondition(i1));

            foreach (PointWithID p in CurrentGraph.PointCollection)
                OutGraph.AddPoint(p);
            foreach (EdgePointID e in edgeCollection)
                OutGraph.AddEdge(e);

            return OutGraph;
        }

        public static int CountInDegreeForPoint(IGraphBasics<Point,AbstractEdge<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            return CurrentGraph.EdgeCollection.Count(i1 => i1.EndPoint == CurrentPoint);
        }
        public static int CountOutDegreeForPoint(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            return CurrentGraph.EdgeCollection.Count(i1 => i1.StartPoint == CurrentPoint);
        }

        public static IEnumerable<Point> GetInDegreeForPoint(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            HashSet<Point> inPoints = new HashSet<Point>(CurrentGraph.EdgeCollection.Where(i1 => i1.EndPoint == CurrentPoint).Select(i1 => i1.StartPoint));
            
            return inPoints;
        }
        public static IEnumerable<Point> GetOutDegreeForPoint(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            HashSet<Point> inPoints = new HashSet<Point>(CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint == CurrentPoint).Select(i1 => i1.EndPoint));

            return inPoints;
        }

        public static IEnumerable<Point> GetInTransitiveClosureForPoint(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            List<Point> listTransitivePoints = new List<Point>(GetInDegreeForPoint(CurrentGraph, CurrentPoint));
            for (int pointID = 0; pointID < listTransitivePoints.Count(); pointID++)
            {
                listTransitivePoints = listTransitivePoints.Union(GetInDegreeForPoint(CurrentGraph, listTransitivePoints[pointID])).ToList();
            }
            
            return listTransitivePoints;
        }

        public static IEnumerable<Point> GetOutTransitiveClosureForPoint(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            List<Point> listTransitivePoints = new List<Point>(GetOutDegreeForPoint(CurrentGraph, CurrentPoint));
            for (int pointID = 0; pointID < listTransitivePoints.Count(); pointID++)
            {
                listTransitivePoints = listTransitivePoints.Union(GetOutDegreeForPoint(CurrentGraph, listTransitivePoints[pointID])).ToList();
            }

            return listTransitivePoints;
        }

        public static IEnumerable<KeyValuePair<Point, IEnumerable<Point>>> GetInTransitiveClosureForAllPoints(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph)
        {
            return CurrentGraph.PointCollection.Select(i1 =>
                new KeyValuePair<Point, IEnumerable<Point>>(i1, GetInTransitiveClosureForPoint(CurrentGraph, i1))
            );
        }

        public static IEnumerable<KeyValuePair<Point,IEnumerable<Point>>> GetOutTransitiveClosureForAllPoints(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph)
        {
            return CurrentGraph.PointCollection.Select(i1 =>
                new KeyValuePair<Point, IEnumerable<Point>>(i1, GetOutTransitiveClosureForPoint(CurrentGraph, i1))
            );
        }

        public static IEnumerable<KeyValuePair<Point, int>> CountInDegreeForAllPoint(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            return CurrentGraph.PointCollection.
                Select(i1 => { return new KeyValuePair<Point, int>(i1, CountInDegreeForPoint(CurrentGraph, i1)); });
        }
        public static IEnumerable<KeyValuePair<Point, int>> CountOutDegreeForAllPoint(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            return CurrentGraph.PointCollection.
                Select(i1 => { return new KeyValuePair<Point, int>(i1, CountOutDegreeForPoint(CurrentGraph, i1)); });
        }

        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMaxInDegree(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountInDegreeForAllPoint(CurrentGraph);
            int maxDegree = listDegrees.Max(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == maxDegree);
        }
        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMaxOutDegree(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountOutDegreeForAllPoint(CurrentGraph);
            int maxDegree = listDegrees.Max(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == maxDegree);
        }
        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMinInDegree(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountInDegreeForAllPoint(CurrentGraph);
            int minDegree = listDegrees.Min(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == minDegree);
        }
        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMinOutDegree(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountOutDegreeForAllPoint(CurrentGraph);
            int minDegree = listDegrees.Min(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == minDegree);
        }

        //public static List<Path> GetAllGraphPaths(Graph<Edge> CurrentGraph)
        //{
        //    List<Path> listPaths = CurrentGraph.ListEdges.Keys.Select<Edge, Path>(i1 =>
        //     {
        //         Path path = Path.InitPath();
        //         path.AddEdge(i1);
        //         return path;
        //     }).ToList();

        //    for (int i = 0; i < listPaths.Count(); i++)
        //    {
        //        Path p = listPaths[i];
        //        Edge endEdge = p.ListPathEdges.Last();
        //        listPaths.InsertRange(listPaths.Count, CurrentGraph.ListEdges.Keys
        //            .Where(i1 => endEdge.EndPoint == i1.StartPoint
        //                && !i1.Equals(endEdge)
        //                && p.ListPathEdges.First().StartPoint != i1.EndPoint)
        //            .Select(i1 =>
        //            {
        //                Path path = Path.InitPath(p);
        //                path.AddEdge(i1);
        //                return path;
        //            }));
        //    }
        //    return listPaths;
        //}

        public static bool CheckSimpleCycles(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            if (CurrentGraph.EdgeCollection.Count(i1 => i1.StartPoint == i1.EndPoint) != 0)
                return true;
            else return false;
        }
        public static bool CheckCycles(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            if (CheckSimpleCycles(CurrentGraph))
                return true;

            List<Path> listPaths = CurrentGraph.EdgeCollection.Select<AbstractEdge<Point>, Path>(i1 =>
            {
                Path path = Path.InitPath();
                path.AddEdge(i1);
                return path;
            }).ToList();

            for (int i = 0; i < listPaths.Count(); i++)
            {
                Path currentPath = listPaths[i];
                IEnumerable<AbstractEdge<Point>> candidatesToPath = CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint == currentPath.ListPathEdges.Last().EndPoint);
                foreach (Edge e in candidatesToPath)
                {
                    if (currentPath.ListPathPoints.Contains(e.EndPoint))
                        return true;
                    else
                    {
                        Path newPath = Path.InitPath(currentPath);
                        newPath.AddEdge(e);
                        listPaths.Add(newPath);
                    }
                }
            }
            return false;
        }

        public static bool IsDirectedTree(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            if (CheckCycles(CurrentGraph))
                return false;

            IEnumerable<KeyValuePair<Point, int>> inDegrees = CountInDegreeForAllPoint(CurrentGraph);
            if (CurrentGraph.EdgeCollection.Count(i1 => i1.StartPoint == i1.EndPoint) == 0 &&
                inDegrees.Count(i1 => i1.Value == 0) == 1 &&
                inDegrees.Count(i1 => i1.Value > 1) == 0)
                return true;
            else return false;
        }

        public static string GenerateGraphDescription(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            StringBuilder descriptionBuilder = new StringBuilder(String.Format("{0} = ", CurrentGraph.GraphName));
            descriptionBuilder.AppendFormat("{{{0}}} – множество вершин", String.Join(", ", CurrentGraph.PointCollection.Select(i1 => i1.Name)));

            foreach (Point p in CurrentGraph.PointCollection)
            {
                IEnumerable<string> listEndPointsIDs = CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint == p).Select(i1 => i1.EndPoint.Name);
                if (listEndPointsIDs.Count() > 0)
                {
                    descriptionBuilder.Append(", ");
                    descriptionBuilder.AppendFormat("Г({0}) = {{ {1} }}", new string[] { p.Name, String.Join(", ", listEndPointsIDs) });
                }
            }

            descriptionBuilder.Append(" – отображения.");
            return descriptionBuilder.ToString();
        }

        public static bool Lab3_8PointSelectionCondition(PointWithID CurrentPoint)
        {
            if ((CurrentPoint.ID % 2) == 1)
                return true;
            else return false;
        }

        public static bool Lab3_8EdgeSelectionCondition(EdgePointID CurrentEdge)
        {
            if ((CurrentEdge.StartPoint.ID + CurrentEdge.EndPoint.ID) == 0)
                return false;
            else if (((CurrentEdge.StartPoint.ID + CurrentEdge.EndPoint.ID) % 2) == 0)
                return true;
            else return false;
        }
    }
}