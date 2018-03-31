using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using System.Windows.Data;
using DiscreteMaC_Lib.Graphes.Points;

namespace DiscreteMaC_Lib.GraphNotations
{
    [ValueConversion(typeof(Graph<AbstractEdge<Point>>), typeof(object))]
    public abstract class TypedGraphNotation<CustomType>: GraphNotation
    {
        public abstract CustomType ConvertFromGrapch(Graph<AbstractEdge<Point>> InitialGraph) ;
        public abstract Graph<Edge> ConvertToGrapch(CustomType InitialGraph);
    }
}
