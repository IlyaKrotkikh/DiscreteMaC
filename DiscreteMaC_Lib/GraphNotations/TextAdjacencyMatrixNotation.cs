using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;

namespace DiscreteMaC_Lib.GraphNotations
{
    public class TextAdjacencyMatrixNotation : TypedGraphNotation<string>
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
            string content = InitialGraph.GraphName + " | ";

            List<Point> points = (InitialGraph.ListPoint.Keys.ToList());
            points.Sort((i1, i2) => { return i1.ID.CompareTo(i2.ID); });

            content = String.Concat(content, String.Join(" | ", points.Select(i1 => i1.ID)));
            content = String.Concat(content, "\n");

            byte[,] matrix = new byte[points.Count, points.Count];
            foreach (Edge e in InitialGraph.ListEdges.Keys)
            {
                matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] += 1;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                content = String.Concat(content, points[i].ToString());
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    content = String.Concat(content, " | ");
                    content = String.Concat(content, matrix[i,j].ToString());
                }
                content = String.Concat(content, "\n");
            }

            return content;
        }

        public override Graph<Edge> ConvertToGrapch(string InitialGraph)
        {
            throw new NotImplementedException();
        }
    }
}
