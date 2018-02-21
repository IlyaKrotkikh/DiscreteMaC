using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib
{
    public class Edge
    {
        public string Name;
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Edge(string Name, Point StartPoint, Point EndPoint)
        {
            this.Name = Name;
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }
    }
}
