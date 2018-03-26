using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Paths.PathComparers;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Paths
{
    public abstract class AbstractPath<EdgeType, PointType> : IEquatable<AbstractPath<EdgeType, PointType>> where EdgeType:Edge where PointType: Point
    {
        public ReadOnlyCollection<EdgeType> ListPathEdges { get; set; }
        public ReadOnlyCollection<PointType> ListPathPoints { get; set; }

        public AbstractPath(List<EdgeType> ListPathEdges, List<PointType> ListPathPoints)
        {
            this.ListPathEdges = new ReadOnlyCollection<EdgeType>(ListPathEdges);
            this.ListPathPoints = new ReadOnlyCollection<PointType>(ListPathPoints);
        }

        public abstract void AddEdge(EdgeType EdgeToPath);
        public abstract void RemoveRange(int StartIndex, int Count);

        public bool Equals(AbstractPath<EdgeType, PointType> other)
        {
            PathEqualityComparer<EdgeType, PointType> defComparer = new PathEqualityComparer<EdgeType, PointType>();
            return defComparer.Equals(this, other);
        }
    }
}
