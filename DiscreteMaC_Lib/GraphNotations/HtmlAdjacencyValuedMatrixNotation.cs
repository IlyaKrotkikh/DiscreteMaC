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
    [ValueConversion(typeof(IGraphBasics<Point, IValuedEdgeBasics<Point>>), typeof(string))]
    public class HtmlValuedAdjacencyMatrixNotation : TypedGraphNotation<string>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("Converted value is null");
            return ConvertFromGrapch((IGraphBasics<Point, IEdgeBasics<Point>>)value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("Converted value is null");
            return ConvertToGrapch(value as string);
        }

        public override string ConvertFromGrapch(IGraphBasics<Point, IEdgeBasics<Point>> InitialGraph)
        {
            List<Point> points = (InitialGraph.PointCollection.ToList());
            points.Sort();
            StringBuilder HtmlStringBuilder = new StringBuilder();
            
            HtmlStringBuilder.AppendFormat("<!DOCTYPE HTML><html><head><meta charset =\"utf-8\"></head><body><table border = \"1\"><caption>{0}</caption>", InitialGraph.GraphName);
            HtmlStringBuilder.AppendFormat("<tr><th></th><th>{0}</th></tr>",String.Join("</th><th>", points.Select(i1 => i1.Name)));

            string[,] matrix = new string[points.Count, points.Count];
            foreach (IEdgeBasics<Point> e in InitialGraph.EdgeCollection)
            {
                if (e is IValuedEdgeBasics<Point>)
                    if (String.IsNullOrEmpty(matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)]))
                        matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] = (e as IValuedEdgeBasics<Point>).EdgeValue.ToString("0");
                    else matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] += String.Format(", {0}", (e as IValuedEdgeBasics<Point>).EdgeValue.ToString("0"));
                else if (String.IsNullOrEmpty(matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)]))
                    matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] = "1";
                else matrix[points.IndexOf(e.StartPoint), points.IndexOf(e.EndPoint)] += ", 1";
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                HtmlStringBuilder.AppendFormat("<tr><th>{0}</th>", points[i].ToString());

                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (String.IsNullOrEmpty(matrix[i, j]))
                        HtmlStringBuilder.AppendFormat("<td>{0}</td>", 0);
                    else HtmlStringBuilder.AppendFormat("<td>{0}</td>", matrix[i, j].ToString());

                HtmlStringBuilder.Append("</tr>");
            }
            HtmlStringBuilder.Append("</table></body></html>");

            return HtmlStringBuilder.ToString();
        }

        public override object ConvertToGrapch(string InitialGraph)
        {
            throw new NotImplementedException();
        }
    }
}
