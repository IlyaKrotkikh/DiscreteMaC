using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Points.PointComparers;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.Graphes.Edges;

namespace DiscreteMaC_Lib.Graphes
{
    //public abstract class Graph<PointType, EdgeType>: IGraphBasics<PointType,EdgeType>
    //    where PointType: Point
    //    where EdgeType: AbstractEdge<PointType>
    //{
    //    protected Dictionary<PointType, ReadOnlyDictionary<EdgeType, string>> _ListPoint { get; set; }
    //    protected Dictionary<EdgeType, string> _ListEdges { get; set; }

    //    public ReadOnlyDictionary<PointType, ReadOnlyDictionary<EdgeType, string>> ListPoint { get; private set; }
    //    public ReadOnlyDictionary<EdgeType, string> ListEdges { get; private set; }
    //    public string GraphName { get; set; }

    //    public IEnumerable<PointType> PointCollection
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public IEnumerable<EdgeType> EdgeCollection
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public Graph(string GraphName, IEqualityComparer<EdgeType> EdgeComparer)
    //    {
    //        this.GraphName = GraphName;

    //        _ListPoint = new Dictionary<PointType, ReadOnlyDictionary<EdgeType, string>>(new PointEqualityComparer());
    //        _ListEdges = new Dictionary<EdgeType, string>(EdgeComparer);

    //        ListPoint = new ReadOnlyDictionary<PointType, ReadOnlyDictionary<EdgeType, string>>(_ListPoint);
    //        ListEdges = new ReadOnlyDictionary<EdgeType, string>(_ListEdges);
    //    }

    //    public abstract void AddPoint(Point GraphPoint);    
    //    public abstract void AddEdge(EdgeType GraphEdge);
    //}
}
