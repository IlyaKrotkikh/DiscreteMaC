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
    public class TextAdjacencyMatrixNotation : TypedGraphNotation<string>
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
            string content = InitialGraph.GraphName + " | ";

            List<Point> points = (InitialGraph.PointCollection.ToList());
            points.Sort((i1, i2) => { return i1.Name.CompareTo(i2.Name); });

            content = String.Concat(content, String.Join(" | ", points.Select(i1 => i1.Name)));
            content = String.Concat(content, "\n");

            byte[,] matrix = new byte[points.Count, points.Count];
            foreach (AbstractEdge<Point> e in InitialGraph.EdgeCollection)
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

        public override object ConvertToGrapch(string InitialGraph)
        {
            throw new NotImplementedException();
        }
    }
}
