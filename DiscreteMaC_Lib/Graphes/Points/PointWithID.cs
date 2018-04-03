using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteMaC_Lib.Graphes.Points
{
    public class PointWithID : Point, IEquatable<PointWithID>
    {
        public string NamePrefix { get; set; }
        public int ID { get; set; }
        public override string Name
        {
            get
            {
                return this.NamePrefix + ID.ToString();
            }
        }

        public PointWithID(int ID, string NamePrefix)
        {
            this.ID = ID;
            this.NamePrefix = NamePrefix;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is PointWithID)
                return Equals(obj as PointWithID);
            else return base.Equals(obj);
        }

        public bool Equals(PointWithID other)
        {
            PointWithIDEqualityComparer defComparer = new PointWithIDEqualityComparer();
            return defComparer.Equals(this, other);
        }
    }
}
