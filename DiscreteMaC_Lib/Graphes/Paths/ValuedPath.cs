using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Paths
{
    public class ValuedPath : AbstractPath<IValuedEdgeBasics<Point>, Point>
    {
        private List<IValuedEdgeBasics<Point>> _ListPathEdges { get; set; }
        private List<Point> _ListPathPoints { get; set; }

        public double PathLengrh
        {
            get
            {
                if (ListPathEdges.Count() != 0)
                    return ListPathEdges.Sum(i1 => i1.EdgeValue);
                else return 0.00;
            }
        }

        protected ValuedPath(List<IValuedEdgeBasics<Point>> ListPathEdges, List<Point> ListPathPoints) : base(ListPathEdges, ListPathPoints)
        {
            _ListPathEdges = ListPathEdges;
            _ListPathPoints = ListPathPoints;
        }

        public static ValuedPath InitPath()
        {
            List<IValuedEdgeBasics<Point>> ListPathEdges = new List<IValuedEdgeBasics<Point>>();
            List<Point> ListPathPoints = new List<Point>();
            return new ValuedPath(ListPathEdges, ListPathPoints);
        }

        public static ValuedPath InitPath(ValuedPath PathToCopy)
        {
            List<IValuedEdgeBasics<Point>> ListPathEdges = new List<IValuedEdgeBasics<Point>>(PathToCopy.ListPathEdges);
            List<Point> ListPathPoints = new List<Point>(PathToCopy.ListPathPoints);
            return new ValuedPath(ListPathEdges, ListPathPoints);
        }

        public override void AddEdge(IValuedEdgeBasics<Point> EdgeToPath)
        {
            if (_ListPathEdges.Count() == 0)
            {
                _ListPathEdges.Add(EdgeToPath);
                _ListPathPoints.Add(EdgeToPath.StartPoint);
                _ListPathPoints.Add(EdgeToPath.EndPoint);
            }
            else if (_ListPathEdges.Last().EndPoint == EdgeToPath.StartPoint)
            {
                _ListPathEdges.Add(EdgeToPath);
                _ListPathPoints.Add(EdgeToPath.EndPoint);
            }
            else throw new Exception("Edge does not continue the path");
        }

        public override void RemoveRange(int StartIndex, int Count)
        {
            _ListPathEdges.RemoveRange(StartIndex, Count);
        }
    }
}
