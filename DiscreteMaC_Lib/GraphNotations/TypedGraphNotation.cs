using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.GraphNotations
{
    public abstract class TypedGraphNotation<CustomType>: GraphNotation
    {
        public abstract CustomType ConvertFromGrapch(Graph InitialGraph) ;
        public abstract Graph ConvertToGrapch(CustomType InitialGraph);
    }
}
