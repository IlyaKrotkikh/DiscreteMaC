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
    [ValueConversion(typeof(IGraphBasics<Point, AbstractEdge<Point>>), typeof(string))]
    public class TextAdjacencyValueedMatrixNotation : TypedGraphNotation<string>
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

        public override string ConvertFromGrapch(IGraphBasics<Point, IEdgeBasics<Point>> InitialGraph)
        {
            StringBuilder content = new StringBuilder(String.Format("{0} \n", InitialGraph.GraphName));

            List<Point> points = (InitialGraph.PointCollection.ToList());
            points.Sort((i1, i2) => { return i1.Name.CompareTo(i2.Name); });

            string[,] matrix = new string[points.Count + 1, points.Count + 1];
            for (int i = 0; i < points.Count; i++)
            {
                matrix[i + 1, 0] = points[i].ToString();
                matrix[0, i + 1] = points[i].ToString();
            }

            foreach (IEdgeBasics<Point> e in InitialGraph.EdgeCollection)
            {
                if (e is IValuedEdgeBasics<Point>)
                    if (String.IsNullOrEmpty(matrix[points.IndexOf(e.StartPoint) + 1, points.IndexOf(e.EndPoint) + 1]))
                        matrix[points.IndexOf(e.StartPoint) + 1, points.IndexOf(e.EndPoint) + 1] = (e as IValuedEdgeBasics<Point>).EdgeValue.ToString("0.00");
                    else matrix[points.IndexOf(e.StartPoint) + 1, points.IndexOf(e.EndPoint) + 1] += (String.Format(", {0}", (e as IValuedEdgeBasics<Point>).EdgeValue.ToString("0.00")));
                else if (String.IsNullOrEmpty(matrix[points.IndexOf(e.StartPoint) + 1, points.IndexOf(e.EndPoint) + 1]))
                    matrix[points.IndexOf(e.StartPoint) + 1, points.IndexOf(e.EndPoint) + 1] = "1";
                else matrix[points.IndexOf(e.StartPoint) + 1, points.IndexOf(e.EndPoint) + 1] += ", 1";
            }

            int[] maxColumnLength = new int[matrix.GetLength(1)];

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                int maxLength = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (!String.IsNullOrEmpty(matrix[i, j]))
                        if (matrix[i, j].Length > maxLength)
                            maxLength = matrix[i, j].Length;
                }
                maxColumnLength[j] = maxLength;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!String.IsNullOrEmpty(matrix[i, j]))
                        content.AppendFormat("{0}{1} | ", matrix[i, j], new String(' ', maxColumnLength[j] - matrix[i, j].Length));
                    else content.AppendFormat("{0}{1} | ", 0, new String(' ', maxColumnLength[j] - 1));
                }
                content.Append("\n");
            }

            return content.ToString();
        }

        public override object ConvertToGrapch(string InitialGraph)
        {
            throw new NotImplementedException();
        }
    }
}
