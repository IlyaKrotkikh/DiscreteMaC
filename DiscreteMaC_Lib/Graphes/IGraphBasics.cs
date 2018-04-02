using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes
{
    public interface IGraphBasics<out PointType, out EdgeType>
        where PointType:Point
        where EdgeType : IEdgeBasics<PointType>
    {
        string GraphName { get; set; }
        IEnumerable<PointType> PointCollection { get; }
        IEnumerable<EdgeType> EdgeCollection { get; }
    }
}