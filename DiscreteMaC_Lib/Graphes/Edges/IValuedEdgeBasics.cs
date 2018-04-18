using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public interface IValuedEdgeBasics<out PointType> : IEdgeBasics<PointType>
        where PointType: Point
    {
        double EdgeValue  { get; }
    }
}
