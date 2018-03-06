using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;

namespace DiscreteMaC_Lib.GraphNotations
{
    public abstract class TypedGraphNotation<CustomType>: GraphNotation
    {
        public abstract CustomType ConvertFromGrapch(Graph<Edge> InitialGraph) ;
        public abstract Graph<Edge> ConvertToGrapch(CustomType InitialGraph);
    }
}
