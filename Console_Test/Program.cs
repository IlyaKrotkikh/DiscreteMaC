using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.GraphNotations;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Paths;

namespace Console_Test
{
    class Program
    {
        static bool stop;
        static TextAdjacencyMatrixNotation notation;
        static AbstractGraph<Point,Edge> TestGraph1;

        static void Main(string[] args)
        {
            stop = false;
            notation = new TextAdjacencyMatrixNotation();
            while (!stop)
            {
                //PrintTestDirectedGraph();
                //PrintTestDirectedGraphWithPointID();
                //PrintTransitiveClosureForAllPoints(TestGrapch1);
                //TestPrintIncludedSubGraphes();
                //TestSearchPathInGraph();
                //BuildGrapgAndTestIt();
                //PrintTestOneSideStrongComponents();
                //PrintTestRandomDirectedGraphWithValuedEdgeAndPointID();
                PrintTestFindingMinValuedPaths();

                if (Console.ReadLine() == "stop")
                    stop = true;

            }
        }

        public static void BuildGrapgAndTestIt()
        {
            while (!stop)
            {
                TestGraph1 = GraphUtils.GenerateRandomDirectedGraph("g1", 4, 3);

                Console.WriteLine(notation.ConvertFromGrapch(TestGraph1));

                PrintTransitiveClosureForAllPoints(TestGraph1);
                PrintOneSidedComp(TestGraph1);

                if (Console.ReadLine() == "stop")
                    stop = true;

            }
        }

        public static void PrintTestDirectedGraph()
        {
            TestGraph1 = new DirectedGraph("g1");
            Point x1 = new Point("x1");
            TestGraph1.AddPoint(x1);
            Point x2 = new Point("x2");
            TestGraph1.AddPoint(x2);
            Point x3 = new Point("x3");
            TestGraph1.AddPoint(x3);
            Point x4 = new Point("x4");
            TestGraph1.AddPoint(x4);
            //Point x5 = new Point("x5");
            //TestGrapch1.AddPoint(x5);

            TestGraph1.AddEdge(new Edge("a1", x1, x2));
            TestGraph1.AddEdge(new Edge("a2", x2, x3));
            TestGraph1.AddEdge(new Edge("a3", x2, x3));
            //TestGraph1.AddEdge(new Edge("a4", x2, x3));
            //TestGraph1.AddEdge(new Edge("a5", x3, x4));
            //TestGraph1.AddEdge(new Edge("a6", x3, x5));
            //TestGraph1.AddEdge(new Edge("a7", x5, x3));

            Console.WriteLine(notation.ConvertFromGrapch(TestGraph1));

        }

        public static void PrintTestDirectedGraphWithPointID()
        {
            DirectedGraphWithPointID tempGraph = new DirectedGraphWithPointID("g1","x", new AbstractEdgeEqualityComparer<PointWithID>());
            PointWithID x1 = new PointWithID(1,"x");
            PointWithID x2 = new PointWithID(2,"x");
            PointWithID x3 = new PointWithID(3,"x");
            PointWithID x4 = new PointWithID(4,"x");
            PointWithID x5 = new PointWithID(5,"x");
            PointWithID x6 = new PointWithID(6,"x");

            tempGraph.AddPoint(x1);
            tempGraph.AddPoint(x2);
            tempGraph.AddPoint(x3);
            tempGraph.AddPoint(x4);
            //tempGraph.AddPoint(x5);
            //tempGraph.AddPoint(x6);

            tempGraph.AddEdge(new EdgePointID("a1", x1, x2));
            tempGraph.AddEdge(new EdgePointID("a2", x2, x3));
            tempGraph.AddEdge(new EdgePointID("a3", x2, x3));
            tempGraph.AddEdge(new EdgePointID("a4", x4, x3));
            tempGraph.AddEdge(new EdgePointID("a5", x4, x3));
            //tempGraph.AddEdge(new EdgePointID("a6", x3, x5));
            //tempGraph.AddEdge(new EdgePointID("a7", x5, x3));

            Console.WriteLine(notation.ConvertFromGrapch(tempGraph));

        }

        public static void PrintResultONDirectedTree()
        {
            Console.WriteLine("\nIs directed tree?: {0} \n", GraphUtils.IsDirectedTree(TestGraph1));
        }

        public static void PrintTransitiveClosureForAllPoints(IGraphBasics<Point,IEdgeBasics<Point>> CurrentGraph)
        {
            foreach (KeyValuePair<Point, IEnumerable<Point>> kvp in GraphUtils.GetOutTransitiveClosureForAllPoints(CurrentGraph))
                Console.WriteLine("Point {0}, Out transitive closure = {{{1}}}", kvp.Key, String.Join(",", kvp.Value));
            Console.WriteLine();
            foreach (KeyValuePair<Point, IEnumerable<Point>> kvp in GraphUtils.GetInTransitiveClosureForAllPoints(CurrentGraph))
                Console.WriteLine("Point {0}, In transitive closure = {{{1}}}", kvp.Key, String.Join(",", kvp.Value));
            Console.WriteLine("\nOneSides Graphs:\n");
        }

        public static void PrintOneSidedComp(IGraphBasics<Point,IEdgeBasics<Point>> CurrentGraph)
        {
            foreach (IEnumerable<Point> collP in GraphUtils.GetCollectionPointsOneSidedCompOfGraph(CurrentGraph))
                Console.WriteLine("Graph: {{{0}}}", String.Join(",", collP));
        }

        public static void TestPrintIncludedSubGraphes()
        {
            while (!stop)
            {
                DirectedGraphWithPointID tempGraph = GraphUtils.GenerateRandomDirectedGraphWithPointID("g1", "x", 4, 3);



                Console.WriteLine(notation.ConvertFromGrapch(tempGraph));
                PrintTransitiveClosureForAllPoints(tempGraph);
                foreach (IEnumerable<Point> collP in GraphUtils.GetCollectionPointsOneSidedCompOfGraph(tempGraph))
                {
                    Console.WriteLine("Graph: {{{0}}}", String.Join(",", collP));
                    Console.WriteLine(notation.ConvertFromGrapch(GraphUtils.GetInducedSubgraph(tempGraph, collP)));
                }

                if (Console.ReadLine() == "stop")
                    stop = true;

            }
        }

        public static void TestSearchPathInGraph()
        {

            TestGraph1 = GraphUtils.GenerateRandomDirectedGraph("g1", 4, 10);

            Console.WriteLine(notation.ConvertFromGrapch(TestGraph1));
            Console.WriteLine("===");
            PrintAllEdgesForGraph(TestGraph1);
        }

        public static void PrintAllEdgesForGraph(IGraphBasics<Point, IEdgeBasics<Point>> CurrentGraph)
        {
            foreach (KeyValuePair<Point,IEnumerable<AbstractPath<IEdgeBasics<Point>,Point>>> kvp in GraphUtils.GetAllPathsForGraph(CurrentGraph))
                Console.WriteLine("Point: {0}\n    {1}", kvp.Key,String.Join("\n    ",kvp.Value.Select(i1 => String.Join("->",i1.ListPathPoints))));
        }

        public static void PrintTestOneSideStrongComponents()
        {
            DirectedGraph testGraph = GraphUtils.GenerateRandomDirectedGraph("G1", 5, 7);

            IEnumerable<IGraphBasics<Point, IEdgeBasics<Point>>> lstOneSideStrongGraphs = GraphUtils.GetCollectionOneSidedStrongComponentsOfGraph(testGraph, testGraph.PointCollection.First());

            Console.WriteLine(notation.ConvertFromGrapch(testGraph));

            foreach (IGraphBasics<Point, IEdgeBasics<Point>> g in lstOneSideStrongGraphs)
            {
                Console.WriteLine();
                Console.WriteLine(notation.ConvertFromGrapch(g));
            }
        }

        public static void PrintTestRandomDirectedGraphWithValuedEdgeAndPointID()
        {
            DirectedGraphWithValuedEdgeAndPointID testGraph = GraphUtils.GenerateRandomDirectedGraphWithValuedEdgeAndPointID("G1", "x", 5);
            TextAdjacencyValueedMatrixNotation testNotation = new TextAdjacencyValueedMatrixNotation();

            Console.WriteLine(testNotation.ConvertFromGrapch(testGraph));
        }

        public static void PrintTestFindingMinValuedPaths()
        {
            DirectedGraphWithValuedEdgeAndPointID testGraph = GraphUtils.GenerateRandomDirectedGraphWithValuedEdgeAndPointID("G1", "x", 5);
            TextAdjacencyValueedMatrixNotation testNotation = new TextAdjacencyValueedMatrixNotation();

            Console.WriteLine(testNotation.ConvertFromGrapch(testGraph));

            foreach (KeyValuePair<Point, ValuedPath> kvp in GraphUtils.GetMinimalPathFromPointToAllPointsInGraph(testGraph, testGraph.PointCollection.First()))
            {
                Console.WriteLine("{0}: {{{1}}} = {2}", 
                    kvp.Key,
                    String.Join("->",kvp.Value.ListPathPoints),
                    kvp.Value.PathLengrh);
            }
        }
    }
}