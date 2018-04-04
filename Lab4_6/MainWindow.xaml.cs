using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DirectedGraphWithPointID _MainGraph;
        private string _MainGraphName;
        private int _MainGraphPointsCount;
        private DiscreteMaC_Lib.Graphes.Points.Point _SelectedPoint;
        private string _InTransitiveClosure, _OutTransitiveClosure;
        private IEnumerable<OneSideItem> _CollectionOneSideItems;
        private int CurrentVariant;
        public DirectedGraphWithPointID MainGraph
        {
            get
            {
                return this._MainGraph;
            }
            set
            {
                if (value != null)
                {
                this._MainGraph = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("SelectedPoint");
                    NotifyPropertyChanged("InTransitiveClosure");
                    NotifyPropertyChanged("OutTransitiveClosure");
                    NotifyPropertyChanged("CollectionOneSideItems");
                }
            }
        }
        public string MainGraphName
        {
            get
            {
                return this._MainGraphName;
            }
            set
            {
                if (value != null)
                {
                    this._MainGraphName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int MainGraphPointsCount
        {
            get
            {
                return this._MainGraphPointsCount;
            }
            set
            {
                if (value != 0)
                {
                    this._MainGraphPointsCount = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public DiscreteMaC_Lib.Graphes.Points.Point SelectedPoint
        {
            get
            {
                if (MainGraph.PointCollection.Count() < CurrentVariant)
                    return this.MainGraph.PointCollection.Max();
                else return this.MainGraph.PointCollection.SingleOrDefault(i1 => i1.ID == CurrentVariant);

            }
        }
        public string InTransitiveClosure
        {
            get
            {
                if (MainGraph != null && SelectedPoint != null)
                    return String.Format("{{{0}}}", String.Join(",", GraphUtils.GetInTransitiveClosureForPoint(MainGraph, SelectedPoint).OrderBy(i1 => i1.Name)));
                else return String.Empty;
            }
        }
        public string OutTransitiveClosure
        {
            get
            {
                if (MainGraph != null && SelectedPoint != null)
                    return String.Format("{{{0}}}", String.Join(",", GraphUtils.GetOutTransitiveClosureForPoint(MainGraph, SelectedPoint).OrderBy(i1 => i1.Name)));
                else return String.Empty;
            }
        }

        public IEnumerable<OneSideItem> CollectionOneSideItems
        {
            get
            {
                return GraphUtils.GetCollectionPointsOneSidedCompOfGraph(MainGraph).Select(i1 => new OneSideItem(MainGraph, i1));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            CurrentVariant = 6;

            MainGraphName = "g1";
            MainGraphPointsCount = 6;
            MainGraph = GraphUtils.GenerateRandomDirectedGraphWithPointID(MainGraphName, "x", MainGraphPointsCount, MainGraphPointsCount);

        }

        private void BtnGenMainGraph_Click(object sender, RoutedEventArgs e)
        {
            MainGraph = GraphUtils.GenerateRandomDirectedGraphWithPointID(MainGraphName, "x", MainGraphPointsCount, MainGraphPointsCount);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class OneSideItem
    {
        private DirectedGraphWithPointID OriginalGraph { get; set; }
        public IEnumerable<DiscreteMaC_Lib.Graphes.Points.Point> PointCollection { get; set; }
        public string Name
        {
            get
            {
                if (PointCollection == null || PointCollection.Count() == 0)
                    return String.Empty;
                return String.Format("{{{0}}}", String.Join(", ", this.PointCollection));
            }
        }
        public DirectedGraphWithPointID IncludedGraph
        {
            get
            {
                return GraphUtils.GetInducedSubgraph(OriginalGraph, PointCollection);
            }
        }

        public OneSideItem(DirectedGraphWithPointID OriginalGraph, IEnumerable<DiscreteMaC_Lib.Graphes.Points.Point> PointCollection)
        {
            if (OriginalGraph == null)
                throw new Exception("OriginalGraph is null");
            else this.OriginalGraph = OriginalGraph;
            if (PointCollection == null)
                throw new Exception("PointCollection is null");
            else this.PointCollection = PointCollection;
        }
    }
}