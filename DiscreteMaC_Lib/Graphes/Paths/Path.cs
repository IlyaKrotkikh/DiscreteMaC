using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Paths
{
    public class Path : AbstractPath<IEdgeBasics<Point>, Point>
    {
        private List<IEdgeBasics<Point>> _ListPathEdges { get; set; }
        private List<Point> _ListPathPoints { get; set; }

        protected Path(List<IEdgeBasics<Point>> ListPathEdges, List<Point> ListPathPoints) : base(ListPathEdges, ListPathPoints)
        {
            _ListPathEdges = ListPathEdges;
            _ListPathPoints = ListPathPoints;
        }

        public static Path InitPath()
        {
            List<IEdgeBasics<Point>> ListPathEdges = new List<IEdgeBasics<Point>>();
            List<Point> ListPathPoints = new List<Point>();
            return new Path(ListPathEdges, ListPathPoints);
        }

        public static Path InitPath(Path PathToCopy)
        {
            List<IEdgeBasics<Point>> ListPathEdges = new List<IEdgeBasics<Point>>(PathToCopy.ListPathEdges);
            List<Point> ListPathPoints = new List<Point>(PathToCopy.ListPathPoints);
            return new Path(ListPathEdges,ListPathPoints);
        }

        public override void AddEdge(IEdgeBasics<Point> EdgeToPath)
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
