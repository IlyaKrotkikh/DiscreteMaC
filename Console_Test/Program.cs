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

namespace Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<Edge> TestGrapch = new OrientedGraph("Test");
            TestGrapch.AddPoint(new Point("x1"));
            TestGrapch.AddPoint(new Point("x2"));
            TestGrapch.AddPoint(new Point("x3"));
            TestGrapch.AddPoint(new Point("x4"));


            List<Point> TestGrapchPoints = TestGrapch.ListPoint.Keys.ToList();

            Edge a1 = new Edge("a1", TestGrapchPoints[0], TestGrapchPoints[0]);
            Edge a2 = new Edge("a2", TestGrapchPoints[0], TestGrapchPoints[3]);

            TestGrapch.AddEdge(a1);
            TestGrapch.AddEdge(a2);
            TestGrapch.AddEdge(new Edge("a3", TestGrapchPoints[0], TestGrapchPoints[1]));
            TestGrapch.AddEdge(new Edge("a4", TestGrapchPoints[1], TestGrapchPoints[2]));
            TestGrapch.AddEdge(new Edge("a5", TestGrapchPoints[1], TestGrapchPoints[3]));

            HtmlAdjacencyMatrixNotation notation = new HtmlAdjacencyMatrixNotation();
            Console.WriteLine(notation.ConvertFromGrapch(TestGrapch));

            Console.ReadLine();

            //int i = 8;
            //while (i > 0)
            //{
            //    TestGrapch = GraphUtils.GenerateRandomDirectedGraph("g1", i, Convert.ToInt32(Math.Pow(i, 2)-1));
            //    Console.WriteLine(notation.ConvertFromGrapch(TestGrapch));
            //    i--;
            //}

            Graph<Edge> TestGrapch1 = GraphUtils.GenerateRandomDirectedGraph("g1",4,15);
            Graph<Edge> TestGrapch2 = GraphUtils.GenerateRandomDirectedGraph("g2",6,10);

            Console.WriteLine(notation.ConvertFromGrapch(TestGrapch1));
            Console.WriteLine(notation.ConvertFromGrapch(TestGrapch2));

            Graph<Edge> TestResultGraph = GraphUtils.DirectedGraphIntersection(TestGrapch1,TestGrapch2);

            Console.WriteLine(notation.ConvertFromGrapch(TestResultGraph));

            Console.ReadLine();
        }
    }
}
