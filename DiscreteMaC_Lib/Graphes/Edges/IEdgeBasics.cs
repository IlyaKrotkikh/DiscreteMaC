using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public interface IEdgeBasics<out PointType> where PointType:Point
    {
        string Name { get; set; }
        PointType StartPoint { get;}
        PointType EndPoint { get;}
    }
}
