using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Edges
{
    public class EdgePointID : AbstractEdge<PointWithID>
    {
        public EdgePointID(string Name, PointWithID StartPoint, PointWithID EndPoint) : base(Name, StartPoint, EndPoint) { }
        public EdgePointID(string Name, EdgePointID EdgeToCopy) : base(Name, EdgeToCopy) { }
    }
}
