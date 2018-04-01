using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes
{
    public abstract class AbstractGraph<PointType, EdgeType> : IGraphBasics<PointType, EdgeType>
        where PointType : Point
        where EdgeType : AbstractEdge<PointType>
    {
        protected virtual HashSet<PointType> HashSetPoints { get; set; }
        protected virtual HashSet<EdgeType> HashSetEdges { get; set; }

        public string GraphName { get; set; }
        public virtual IEnumerable<EdgeType> EdgeCollection
        {
            get
            {
                return this.HashSetEdges;
            }
        }
        public virtual IEnumerable<PointType> PointCollection
        {
            get
            {
                return this.HashSetPoints;
            }
        }

        public AbstractGraph(string GraphName, IEqualityComparer<EdgeType> EdgeComparer)
        {
            this.GraphName = GraphName;

            HashSetPoints = new HashSet<PointType>(new PointEqualityComparer());
            HashSetEdges = new HashSet<EdgeType>(EdgeComparer);
        }

        public abstract void AddPoint(PointType GraphPoint);
        public abstract void AddEdge(EdgeType GraphEdge);
    }
}
