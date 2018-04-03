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
        static AbstractGraph<Point,Edge> TestGrapch1;

        static void Main(string[] args)
        {
            stop = false;
            notation = new TextAdjacencyMatrixNotation();

            TestGrapch1 = new DirectedGraph("g1");
            Point x1 = new Point("x1");
            TestGrapch1.AddPoint(x1);
            Point x2 = new Point("x2");
            TestGrapch1.AddPoint(x2);
            Point x3 = new Point("x3");
            TestGrapch1.AddPoint(x3);
            Point x4 = new Point("x4");
            TestGrapch1.AddPoint(x4);


            TestGrapch1.AddEdge(new Edge("a1", x1, x2));
            TestGrapch1.AddEdge(new Edge("a2", x1, x3));
            TestGrapch1.AddEdge(new Edge("a1", x3, x4));
            TestGrapch1.AddEdge(new Edge("a1", x4, x2));

            Console.WriteLine(notation.ConvertFromGrapch(TestGrapch1));
            Console.WriteLine(GraphUtils.IsDirectedTree(TestGrapch1));

            foreach (KeyValuePair<Point, IEnumerable<Point>> kvp in GraphUtils.GetOutTransitiveClosureForAllPoints(TestGrapch1))
                Console.WriteLine("Point {0}, Out transitive closure = {{{1}}}", kvp.Key, String.Join(",", kvp.Value));
            Console.WriteLine();
            foreach (KeyValuePair<Point, IEnumerable<Point>> kvp in GraphUtils.GetInTransitiveClosureForAllPoints(TestGrapch1))
                Console.WriteLine("Point {0}, In transitive closure = {{{1}}}", kvp.Key, String.Join(",", kvp.Value));

            Console.ReadLine();

            Task task = new Task(BuildGrapgAndTestIt);
            task.Start();

            Console.ReadLine();
            stop = true;
            Console.ReadLine();

            
        }

        public static void BuildGrapgAndTestIt()
        {
            while (!stop)
            {
                TestGrapch1 = GraphUtils.GenerateRandomDirectedGraph("g1", 4, 3);

                Console.WriteLine(notation.ConvertFromGrapch(TestGrapch1));
                Console.WriteLine(GraphUtils.IsDirectedTree(TestGrapch1));

            }
        }
    }
}