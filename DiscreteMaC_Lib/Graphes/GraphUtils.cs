﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;

namespace DiscreteMaC_Lib.Graphes
{
    public static class GraphUtils
    {
        private static Random GlobalRandom = new Random();

        public static Graph<Edge> DirectedGraphIntersection(Graph<Edge> g1, Graph<Edge> g2)
        {
            string graphName = g1.GraphName + "∩" + g2.GraphName;
            Graph<Edge> OutGraph = new OrientedGraph(graphName);

            List<Point> ListTempPoints = g1.ListPoint.Keys.ToList();
            List<Edge> ListTempEdges = g1.ListEdges.Keys.ToList();

            ListTempPoints.AddRange((g2.ListPoint.Keys.ToList()).Except(g1.ListPoint.Keys, new PointEqualityComparer()));
            ListTempEdges = ListTempEdges.Intersect(g2.ListEdges.Keys, new DirectedEdgeEqualityComparer()).ToList();

            foreach (Point p in ListTempPoints)
            {
                OutGraph.AddPoint(new Point(p.ID));
            }
            int i = 0;
            foreach (Edge e in ListTempEdges)
            {
                OutGraph.AddEdge(new Edge(graphName + "_" + i.ToString(), e));
                i++;
            }

            return OutGraph;
        }
        public static Graph<Edge> DirectedGraphUnion(Graph<Edge> g1, Graph<Edge> g2)
        {
            string graphName = g1.GraphName + "∪" + g2.GraphName;
            Graph<Edge> OutGraph = new OrientedGraph(graphName);

            IEnumerable<Point> ListTempPoints = g1.ListPoint.Keys.Union(g2.ListPoint.Keys, new PointEqualityComparer());
            IEnumerable<Edge> ListTempEdges = g1.ListEdges.Keys.Union(g2.ListEdges.Keys, new DirectedEdgeEqualityComparer());

            foreach (Point p in ListTempPoints)
            {
                OutGraph.AddPoint(new Point(p.ID));
            }
            int i = 0;
            foreach (Edge e in ListTempEdges)
            {
                OutGraph.AddEdge(new Edge(graphName + "_" + i.ToString(), e));
                i++;
            }

            return OutGraph;
        }

        public static Graph<Edge> GenerateRandomDirectedGraph(string GraphName)
        {
            return GenerateRandomDirectedGraph(GraphName, GlobalRandom.Next(0, 101));
        }
        public static Graph<Edge> GenerateRandomDirectedGraph(string GraphName, int PointCount)
        {
            return GenerateRandomDirectedGraph(GraphName, PointCount, GlobalRandom.Next(0, Convert.ToInt32(Math.Pow(PointCount, 2))));
        }
        public static Graph<Edge> GenerateRandomDirectedGraph(string GraphName, int PointCount, int EdgeCount)
        {
            if (EdgeCount > Math.Pow(PointCount, 2))
                throw new Exception("Count of > PointCount^2");

            Graph<Edge> OutGraph = GenerateEmptyDirectedGrapch(GraphName, PointCount);

            List<Point> ListPoints = OutGraph.ListPoint.Keys.ToList();
            for (int i = 1; i <= EdgeCount;)
            {
                try
                {
                    OutGraph.AddEdge(new Edge(GraphName + "_" + i.ToString(), ListPoints[GlobalRandom.Next(0, ListPoints.Count)], ListPoints[GlobalRandom.Next(0, ListPoints.Count)]));
                    i++;
                }
                catch (ArgumentException)
                {

                }
            }

            return OutGraph;
        }
        public static Graph<Edge> GenerateEmptyDirectedGrapch(string GraphName, int PointCount)
        {
            Graph<Edge> OutGraph = new OrientedGraph(GraphName);

            string format = "D" + ((PointCount.ToString()).Length).ToString();
            for (int i = 1; i <= PointCount; i++)
            {

                OutGraph.AddPoint(new Point("x" + i.ToString(format)));
            }

            return OutGraph;
        }

        public static int CountInDegreeForPoint(Graph<Edge> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.ListPoint.Contains(new KeyValuePair<Point, System.Collections.ObjectModel.ReadOnlyDictionary<Edge, string>>(CurrentPoint, CurrentPoint.ListEdges)))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            return CurrentGraph.ListEdges.Count(i1 => i1.Key.EndPoint == CurrentPoint);
        }
        public static int CountOutDegreeForPoint(Graph<Edge> CurrentGraph, Point CurrentPoint)
        {
            if (!CurrentGraph.ListPoint.Contains(new KeyValuePair<Point, System.Collections.ObjectModel.ReadOnlyDictionary<Edge, string>>(CurrentPoint, CurrentPoint.ListEdges)))
                throw new Exception("Point " + CurrentPoint.ToString() + " not contains in graph " + CurrentGraph);
            return CurrentGraph.ListEdges.Count(i1 => i1.Key.StartPoint == CurrentPoint);
        }

        public static IEnumerable<KeyValuePair<Point, int>> CountInDegreeForAllPoint(Graph<Edge> CurrentGraph)
        {
            return CurrentGraph.ListPoint.
                Select(i1 => { return new KeyValuePair<Point, int>(i1.Key, CountInDegreeForPoint(CurrentGraph, i1.Key)); });
        }
        public static IEnumerable<KeyValuePair<Point, int>> CountOutDegreeForAllPoint(Graph<Edge> CurrentGraph)
        {
            return CurrentGraph.ListPoint.
                Select(i1 => { return new KeyValuePair<Point, int>(i1.Key, CountOutDegreeForPoint(CurrentGraph, i1.Key)); });
        }

        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMaxInDegree(Graph<Edge> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountInDegreeForAllPoint(CurrentGraph);
            int maxDegree = listDegrees.Max(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == maxDegree);
        }
        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMaxOutDegree(Graph<Edge> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountOutDegreeForAllPoint(CurrentGraph);
            int maxDegree = listDegrees.Max(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == maxDegree);
        }
        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMinInDegree(Graph<Edge> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountInDegreeForAllPoint(CurrentGraph);
            int minDegree = listDegrees.Min(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == minDegree);
        }
        public static IEnumerable<KeyValuePair<Point, int>> GetPointsWithMinOutDegree(Graph<Edge> CurrentGraph)
        {
            IEnumerable<KeyValuePair<Point, int>> listDegrees = CountOutDegreeForAllPoint(CurrentGraph);
            int minDegree = listDegrees.Min(i1 => i1.Value);
            return listDegrees.Where(i1 => i1.Value == minDegree);
        }
    }
}
