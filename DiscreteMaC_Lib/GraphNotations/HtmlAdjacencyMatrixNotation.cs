using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System.Globalization;
using System.Windows.Data;

namespace DiscreteMaC_Lib.GraphNotations
{
    [ValueConversion(typeof(Graph<Edge>), typeof(string))]
    public class HtmlAdjacencyMatrixNotation : TypedGraphNotation<string>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("Converted value is null");
            return ConvertFromGrapch(value as Graph<Edge>);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("Converted value is null");
            return ConvertToGrapch(value as string);
        }

        public override string ConvertFromGrapch(Graph<Edge> InitialGraph)
        {
            List<Point> points = (InitialGraph.ListPoint.Keys.ToList());
            points.Sort((i1, i2) => { return i1.ID.CompareTo(i2.ID); });
            StringBuilder HtmlStringBuilder;
            try
            {
                int fileLength = points.Select(i1 => i1.ID.Length).Aggregate((i1, i2) => i1 + i2) * 2; // Double length (row and columns) names of points 
                fileLength += 126; // <!DOCTYPE HTML><html><head><meta charset =\"utf-8\"></head><body><table border = "1"><caption></caption></table></body></html>
                fileLength += InitialGraph.GraphName.Length; // Plus Graph name
                fileLength += ((InitialGraph.ListPoint.Count + 2) * (InitialGraph.ListPoint.Count + 1)) * 9;// Count <tr></tr>, <th></th>, <td></td>
                fileLength += (InitialGraph.ListPoint.Count * InitialGraph.ListPoint.Count); // Reserved for 0 or 1 in ref matrix
                HtmlStringBuilder = new StringBuilder(fileLength, fileLength);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                HtmlStringBuilder = new StringBuilder(Int32.MaxValue);
            }

            if (HtmlStringBuilder == null)
                HtmlStringBuilder = new StringBuilder();
            HtmlStringBuilder.AppendFormat("<!DOCTYPE HTML><html><head><meta charset =\"utf-8\"></head><body><table border = \"1\"><caption>{0}</caption>", InitialGraph.GraphName);
            HtmlStringBuilder.AppendFormat("<tr><th></th><th>{0}</th></tr>",String.Join("</th><th>", points.Select(i1 => i1.ID)));

            byte[,] matrix = new byte[points.Count, points.Count];
            foreach (Edge e in InitialGraph.ListEdges.Keys)
            {
                matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] += 1;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                HtmlStringBuilder.AppendFormat("<tr><th>{0}</th>", points[i].ToString());

                for (int j = 0; j < matrix.GetLength(1); j++)
                    HtmlStringBuilder.AppendFormat("<td>{0}</td>", matrix[i, j].ToString());

                HtmlStringBuilder.Append("</tr>");
            }
            HtmlStringBuilder.Append("</table></body></html>");

            return HtmlStringBuilder.ToString();
        }

        public override Graph<Edge> ConvertToGrapch(string InitialGraph)
        {
            throw new NotImplementedException();
        }
    }
}
