using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System.Globalization;

namespace DiscreteMaC_Lib.GraphNotations
{
    public class HtmlAdjacencyMatrixNotation : TypedGraphNotation<string>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override string ConvertFromGrapch(Graph<Edge> InitialGraph)
        {
            string content = "<!DOCTYPE HTML><html><head><meta charset =\"utf-8\" ></head>\n<body><table border = \"1\"><caption>";
            content = String.Concat(content, InitialGraph.GraphName);
            content = String.Concat(content, "</caption>\n<tr><th></th><th>");

            List <Point> points = (InitialGraph.ListPoint.Keys.ToList());
            points.Sort((i1, i2) => { return i1.ID.CompareTo(i2.ID); });

            content = String.Concat(content, String.Join("</th><th>", points.Select(i1 => i1.ID)));
            content = String.Concat(content, "</th></tr>\n");

            byte[,] matrix = new byte[points.Count, points.Count];
            foreach (Edge e in InitialGraph.ListEdges.Keys)
            {
                matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] += 1;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                content = String.Concat(content, "<tr><th>");
                content = String.Concat(content, points[i].ToString());
                content = String.Concat(content, "</th>");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    content = String.Concat(content, "<td>");
                    content = String.Concat(content, matrix[i, j].ToString());
                    content = String.Concat(content, "</td>");

                }
                content = String.Concat(content, "</tr>\n");
            }
            content = String.Concat(content, "</table> </body></html>");

            return content;
        }

        public override Graph<Edge> ConvertToGrapch(string InitialGraph)
        {
            throw new NotImplementedException();
        }
    }
}
