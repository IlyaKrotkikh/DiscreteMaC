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

        public static DirectedGraphWithValuedEdgeAndPointID GenerateRandomDirectedGraphWithValuedEdgeAndPointID(string GraphName, string PointNamePrefix)
        {
            return GenerateRandomDirectedGraphWithValuedEdgeAndPointID(GraphName, PointNamePrefix, GlobalRandom.Next(1, 101));
        }
        public static DirectedGraphWithValuedEdgeAndPointID GenerateRandomDirectedGraphWithValuedEdgeAndPointID(string GraphName, string PointNamePrefix, int PointCount)
        {
            return GenerateRandomDirectedGraphWithValuedEdgeAndPointID(GraphName, PointNamePrefix, PointCount, GlobalRandom.Next(0, Convert.ToInt32(Math.Pow(PointCount, 2))));
        }
        public static DirectedGraphWithValuedEdgeAndPointID GenerateRandomDirectedGraphWithValuedEdgeAndPointID(string GraphName, string PointNamePrefix, int PointCount, int EdgeCount)
        {
            if (EdgeCount > Math.Pow(PointCount, 2))
                throw new Exception("Count of > PointCount^2");

            DirectedGraphWithValuedEdgeAndPointID OutGraph = GenerateEmptyDirectedGraphWithValuedEdgeAndPointID(GraphName, PointNamePrefix, PointCount);

            List<PointWithID> ListPoints = OutGraph.PointCollection.ToList();
            for (int i = 1; i <= EdgeCount;)
            {
                if (OutGraph.AddEdge(new ValuedEdge<PointWithID>(GraphName + "_" + i.ToString(), ListPoints[GlobalRandom.Next(0, ListPoints.Count)], ListPoints[GlobalRandom.Next(0, ListPoints.Count)], GlobalRandom.NextDouble()+GlobalRandom.Next(1,10))))
                {
                    i++;
                }
            }

            return OutGraph;
        }
        public static DirectedGraphWithValuedEdgeAndPointID GenerateEmptyDirectedGraphWithValuedEdgeAndPointID(string GraphName, string PointNamePrefix, int PointCount)
        {
            DirectedGraphWithValuedEdgeAndPointID OutGraph = new DirectedGraphWithValuedEdgeAndPointID(GraphName, PointNamePrefix,new DirectedValuedEdgeEqualityComparer());
            for (int i = 1; i <= PointCount; i++)
            {
                OutGraph.AddPoint();
            }
            return OutGraph;
        }

        public static DirectedGraphWithPointID GetInducedSubgraph(DirectedGraphWithPointID CurrentGraph, SelectionCondition<PointWithID> PointSelectionCondition)
        {
            IEnumerable<PointWithID> pointCollection = CurrentGraph.PointCollection.Where(i1 => PointSelectionCondition(i1));

            return GetInducedSubgraph(CurrentGraph, pointCollection);
        }
        public static DirectedGraphWithPointID GetInducedSubgraph(DirectedGraphWithPointID CurrentGraph, IEnumerable<Point> PointCollection)
        {
            DirectedGraphWithPointID OutGraph = new DirectedGraphWithPointID(String.Format("Induced \"{0}\"", CurrentGraph.GraphName), CurrentGraph.PointNamePrefix, new AbstractEdgeEqualityComparer<PointWithID>());

            IEnumerable<EdgePointID> edgeCollection = CurrentGraph.EdgeCollection.Where(i1 => PointCollection.Contains(i1.StartPoint) && PointCollection.Contains(i1.EndPoint));

            foreach (PointWithID p in PointCollection)
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
            return CurrentGraph.EdgeCollection.Count(i1 => i1.EndPoint.Equals(CurrentPoint));
        }
        public static int CountOutDegreeForPoint(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            return CurrentGraph.EdgeCollection.Count(i1 => i1.StartPoint.Equals(CurrentPoint));
        }

        public static IEnumerable<Point> GetInDegreeForPoint(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            HashSet<Point> inPoints = new HashSet<Point>(CurrentGraph.EdgeCollection.Where(i1 => i1.EndPoint.Equals(CurrentPoint)).Select(i1 => i1.StartPoint));
            
            return inPoints;
        }
        public static IEnumerable<Point> GetOutDegreeForPoint(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(CurrentPoint))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            HashSet<Point> inPoints = new HashSet<Point>(CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint.Equals(CurrentPoint)).Select(i1 => i1.EndPoint));

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

        public static IEnumerable<IEnumerable<Point>> GetCollectionPointsOneSidedCompOfGraph(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph)
        {
            List<KeyValuePair<List<Point>, List<Point>>> listKvpPoints = new List<KeyValuePair<List<Point>, List<Point>>>(CurrentGraph.EdgeCollection
                .Select(i1 =>
                 {
                     KeyValuePair<List<Point>, List<Point>> kvp = new KeyValuePair<List<Point>, List<Point>>(new List<Point>(), new List<Point>());
                     kvp.Key.Add(i1.StartPoint);
                     if (!i1.StartPoint.Equals(i1.EndPoint))
                         kvp.Key.Add(i1.EndPoint);
                     kvp.Value.Add(i1.EndPoint);
                     return kvp;
                 }
            ));

            bool calcEnd = false;
            while (!calcEnd)
            {
                foreach (KeyValuePair<List<Point>, List<Point>> kvp in listKvpPoints)
                {
                    IEnumerable<Point> pointCondidates = CurrentGraph.EdgeCollection
                            .Where(i1 => kvp.Value.Contains(i1.StartPoint))
                            .Select(i1 => i1.EndPoint)
                            .Except(kvp.Key)
                            .ToList();
                    kvp.Key.AddRange(pointCondidates);
                    kvp.Value.Clear();
                    kvp.Value.AddRange(pointCondidates);

                    
                }

                for (int listID = 0; listID < listKvpPoints.Count;)
                {
                    KeyValuePair<List<Point>,List<Point>> currentList = listKvpPoints[listID];
                    listKvpPoints
                        .RemoveAll(i1 => i1.Value.Count() == 0 && !currentList.Equals(i1) && (i1.Key.Except(currentList.Key).Count() == 0));
                    listID = listKvpPoints.IndexOf(currentList) +1 ;
                }

                if (listKvpPoints.Count(i1 => i1.Value.Count() == 0) == listKvpPoints.Count())
                    calcEnd = true;
            }

            return listKvpPoints.Select( i1 => i1.Key);
        }

        public static IEnumerable<IGraphBasics<Point, IEdgeBasics<Point>>> GetCollectionOneSidedStrongComponentsOfGraph(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point StartPoint)
        {
            if (!CurrentGraph.PointCollection.Contains(StartPoint))
                throw new Exception(String.Format("Start point {1} not contained in graph {0}.", CurrentGraph.GraphName, StartPoint));

            BaseMinimalGraph<Point, IEdgeBasics<Point>> originalGraph = new BaseMinimalGraph<Point, IEdgeBasics<Point>>()
            {
                GraphName = CurrentGraph.GraphName,
                EdgeCollection = CurrentGraph.EdgeCollection.ToList(),
                PointCollection = CurrentGraph.PointCollection.ToList()
            };

            List<IGraphBasics<Point,IEdgeBasics<Point>>> listOneSidedGraphs = new List<IGraphBasics<Point, IEdgeBasics<Point>>>();

            Point tmpPoint = StartPoint;

            bool algEnd = false;
            while (!algEnd)
            {
                IEnumerable<Point> inTrClosure = GetInTransitiveClosureForPoint(originalGraph, tmpPoint);
                IEnumerable<Point> outTrClosure = GetOutTransitiveClosureForPoint(originalGraph, tmpPoint);

                IEnumerable<Point> trClosure = inTrClosure.Intersect(outTrClosure);
                if (trClosure.Count() != 0)
                {
                    IGraphBasics<Point, IEdgeBasics<Point>> OneSidedStrongComponent = new BaseMinimalGraph<Point, IEdgeBasics<Point>>()
                    {
                        GraphName = String.Format("One-side_strong_comp\n {0}", String.Join(",", trClosure)),
                        PointCollection = trClosure.ToList(),
                        EdgeCollection = originalGraph.EdgeCollection.Where(i1 => trClosure.Contains(i1.EndPoint) && trClosure.Contains(i1.StartPoint)).ToList()
                    };

                    listOneSidedGraphs.Add(OneSidedStrongComponent);

                    originalGraph.EdgeCollection = originalGraph.EdgeCollection.Where(i1 => !trClosure.Contains(i1.EndPoint) && !trClosure.Contains(i1.StartPoint)).ToList();
                    originalGraph.PointCollection = originalGraph.PointCollection.Except(OneSidedStrongComponent.PointCollection).ToList();
                }
                else
                {
                    List<Point> tmpListPoints = originalGraph.PointCollection.ToList();
                    tmpListPoints.Remove(tmpPoint);

                    originalGraph.PointCollection = tmpListPoints;
                    originalGraph.EdgeCollection = originalGraph.EdgeCollection.Where(i1 => !i1.EndPoint.Equals(tmpPoint) && !i1.StartPoint.Equals(tmpPoint)).ToList();
                }

                if (originalGraph.PointCollection.Count() == 0)
                {
                    algEnd = true;
                }
                else
                {
                    tmpPoint = originalGraph.PointCollection.First();
                }
            }

            return listOneSidedGraphs;
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

        public static bool CheckSimpleCycles(IGraphBasics<Point, AbstractEdge<Point>> CurrentGraph)
        {
            if (CurrentGraph.EdgeCollection.Count(i1 => i1.StartPoint.Equals(i1.EndPoint)) != 0)
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
                IEnumerable<AbstractEdge<Point>> candidatesToPath = CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint.Equals(currentPath.ListPathEdges.Last().EndPoint));
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

        public static IEnumerable<KeyValuePair<Point, IEnumerable<AbstractPath<IEdgeBasics<Point>, Point>>>> GetAllPathsForGraph(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph)
        {
            List<KeyValuePair<Point, IEnumerable<AbstractPath<IEdgeBasics<Point>, Point>>>> listPathsByPoints = new List<KeyValuePair<Point, IEnumerable<AbstractPath<IEdgeBasics<Point>, Point>>>>();
            foreach (Point p in CurrentGraph.PointCollection)
            {
                listPathsByPoints.Add(new KeyValuePair<Point, IEnumerable<AbstractPath<IEdgeBasics<Point>, Point>>>(p, GetPathsForPointInGraph(CurrentGraph, p)));
            }
            return listPathsByPoints;
        }

        public static List<Path> GetPathsForPointInGraph(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
        {
            List<Path> currentListPaths = new List<Path>
                    (
                        CurrentGraph.EdgeCollection
                        .Where(i2 => i2.StartPoint.Equals(CurrentPoint))
                        .Select(i2 =>
                        {
                            Path currentPath = Path.InitPath();
                            currentPath.AddEdge(i2);
                            return currentPath;
                        }));

            for (int i = 0; i < currentListPaths.Count(); i++)
            {
                Path currentPath = currentListPaths[i];
                IEnumerable<IEdgeBasics<Point>> candidatesToPath = CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint.Equals(currentPath.ListPathEdges.Last().EndPoint));
                foreach (IEdgeBasics<Point> e in candidatesToPath)
                {
                    if (!currentPath.ListPathPoints.Contains(e.EndPoint))
                    {
                        Path newPath = Path.InitPath(currentPath);
                        newPath.AddEdge(e);
                        currentListPaths.Add(newPath);
                    }
                }
            }

            return currentListPaths;
        }

        public static IEnumerable<KeyValuePair<Point, ValuedPath>> GetMinimalPathFromPointToAllPointsInGraph(IGraphBasics<Point, IValuedEdgeBasics<Point>> CurrentGraph, Point CurrentPoint)
         {
            Dictionary<Point, ValuedPath> dictionaryPermMinValuedPaths = new Dictionary<Point, ValuedPath>();
            Dictionary<Point, ValuedPath> dictionaryValuedPaths = new Dictionary<Point, ValuedPath>();

            if (CurrentGraph.EdgeCollection.Count(i1 => i1.StartPoint.Equals(CurrentPoint)) == 0)
                return dictionaryPermMinValuedPaths;

            foreach (IValuedEdgeBasics<Point> ve in CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint.Equals(CurrentPoint)))
            {
                ValuedPath tempValPath = ValuedPath.InitPath();
                tempValPath.AddEdge(ve);
                if (dictionaryValuedPaths.ContainsKey(tempValPath.ListPathPoints.Last()))
                {
                    if (dictionaryValuedPaths[tempValPath.ListPathPoints.Last()].PathLengrh > tempValPath.PathLengrh)
                        dictionaryValuedPaths[tempValPath.ListPathPoints.Last()] = tempValPath;
                }
                else dictionaryValuedPaths.Add(tempValPath.ListPathPoints.Last(), tempValPath);
            }

            ValuedPath selectedTempPath = dictionaryValuedPaths.Values.OrderBy(i1 => i1.PathLengrh).First();
            dictionaryPermMinValuedPaths.Add(selectedTempPath.ListPathPoints.Last(),selectedTempPath);
            

            bool algEnd = false;
            while (!algEnd)
            {
                foreach (IValuedEdgeBasics<Point> ve in CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint.Equals(selectedTempPath.ListPathPoints.Last()) 
                && !selectedTempPath.ListPathPoints.Contains(i1.EndPoint)
                && !dictionaryPermMinValuedPaths.ContainsKey(i1.EndPoint)))
                {
                    ValuedPath tempValPath = ValuedPath.InitPath(selectedTempPath);
                    tempValPath.AddEdge(ve);
                    if (dictionaryValuedPaths.ContainsKey(ve.EndPoint))
                    {
                        if (dictionaryValuedPaths[tempValPath.ListPathPoints.Last()].PathLengrh > tempValPath.PathLengrh)
                            dictionaryValuedPaths[tempValPath.ListPathPoints.Last()] = tempValPath;
                    }
                    else dictionaryValuedPaths.Add(tempValPath.ListPathPoints.Last(), tempValPath);
                }

                dictionaryValuedPaths.Remove(selectedTempPath.ListPathPoints.Last());

                
                //dictionaryValuedPaths.Remove(selectedTempPath.ListPathPoints.Last());

                if (dictionaryValuedPaths.Count == 0)
                    algEnd = true;
                else
                {
                    selectedTempPath = dictionaryValuedPaths.Values.OrderBy(i1 => i1.PathLengrh).First();
                    dictionaryPermMinValuedPaths.Add(selectedTempPath.ListPathPoints.Last(), selectedTempPath);
                }
            }

            return dictionaryPermMinValuedPaths;
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
                IEnumerable<string> listEndPointsIDs = CurrentGraph.EdgeCollection.Where(i1 => i1.StartPoint.Equals(p)).Select(i1 => i1.EndPoint.Name);
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