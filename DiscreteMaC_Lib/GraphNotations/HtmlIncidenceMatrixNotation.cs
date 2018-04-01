using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System.Windows.Data;

namespace DiscreteMaC_Lib.GraphNotations
{
    [ValueConversion(typeof(IGraphBasics<Point,AbstractEdge<Point>>), typeof(string))]
    public class HtmlIncidenceMatrixNotation : TypedGraphNotation<string>, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("Converted value is null");
            return ConvertFromGrapch(value as IGraphBasics<Point, AbstractEdge<Point>>);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("Converted value is null");
            return ConvertToGrapch(value as string);
        }

        public override string ConvertFromGrapch(IGraphBasics<Point, AbstractEdge<Point>> InitialGraph)
        {
            List<Point> points = (InitialGraph.PointCollection.ToList());
            points.Sort((i1, i2) => { return i1.Name.CompareTo(i2.Name); });
            List<AbstractEdge<Point>> edges = (InitialGraph.EdgeCollection.ToList());
            edges.Sort((i1, i2) => { return i1.Name.CompareTo(i2.Name); });
            StringBuilder HtmlStringBuilder;

            HtmlStringBuilder = new StringBuilder();
            HtmlStringBuilder.AppendFormat("<!DOCTYPE HTML><html><head><meta charset =\"utf-8\"></head><body><table border = \"1\"><caption>{0}</caption>", InitialGraph.GraphName);
            HtmlStringBuilder.AppendFormat("<tr><th></th><th>{0}</th></tr>", String.Join("</th><th>", edges.Select(i1 => i1.Name)));

            short[,] matrix = new short[points.Count, edges.Count];
            foreach (AbstractEdge<Point> e in InitialGraph.EdgeCollection)
            {
                matrix[points.IndexOf(e.StartPoint), edges.IndexOf(e)] += 1;
                matrix[points.IndexOf(e.EndPoint), edges.IndexOf(e)] -= 1;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                HtmlStringBuilder.AppendFormat("<tr><th>{0}</th>", points[i].ToString());

                for (int j = 0; j < matrix.GetLength(1); j++)
                    HtmlStringBuilder.AppendFormat("<td id = >{0}</td>", matrix[i, j].ToString());

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
