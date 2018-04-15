using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DiscreteMaC_Lib.Graphes
{
    class BaseMinimalGraph<PointType, EdgeType> : IGraphBasics<PointType, EdgeType>
        where PointType : Point
        where EdgeType : IEdgeBasics<PointType>
    {
        public virtual string GraphName { get; set; }
        public virtual IEnumerable<PointType> PointCollection { get; set; }
        public virtual IEnumerable<EdgeType> EdgeCollection { get; set; }

        public BaseMinimalGraph()
        {

        }
    }
}