using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Paths.PathComparers
{
    public class PathEqualityComparer<EdgeType, PointType> : IEqualityComparer<AbstractPath<EdgeType, PointType>> where EdgeType: Edge where PointType : Point
    {
        public bool Equals(AbstractPath<EdgeType, PointType> path1, AbstractPath<EdgeType, PointType> path2)
        {
            if (path1 == null && path2 == null)
                return true;
            else if (path1 == null | path2 == null)
                return false;
            else if (path1.ListPathEdges == null || path2.ListPathEdges == null)
                return false;
            else if (path1.ListPathEdges.Count != path2.ListPathEdges.Count)
                return false;
            else return path1.ListPathEdges.SequenceEqual(path2.ListPathEdges);
        }

        public int GetHashCode(AbstractPath<EdgeType, PointType> obj)
        {
            return 0; //TODO Костыль GetHashCode
        }
    }
}
