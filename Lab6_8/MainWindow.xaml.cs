using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Paths;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
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

namespace Lab6_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DiscreteMaC_Lib.Graphes.Points.Point _SelectedPoint;
        private DirectedGraphWithValuedEdgeAndPointID _MainGraph;

        public event PropertyChangedEventHandler PropertyChanged;
        public DirectedGraphWithValuedEdgeAndPointID MainGraph
        {
            get
            {
                if (_MainGraph != null)
                    return _MainGraph;
                else return GraphUtils.GenerateEmptyDirectedGraphWithValuedEdgeAndPointID("None", "x", 1);
            }
            set
            {
                if (value is DirectedGraphWithValuedEdgeAndPointID)
                {
                    this._MainGraph = value;
                    NotifyPropertyChanged();
                    if (MainGraph.PointCollection.Count(i1 => i1.ID == 8) == 1)
                        SelectedPoint = MainGraph.PointCollection.SingleOrDefault(i1 => i1.ID == 8);
                    else SelectedPoint = MainGraph.PointCollection.Last();
                }
            }
        }

        public DiscreteMaC_Lib.Graphes.Points.Point SelectedPoint
        {
            get
            {
                return this._SelectedPoint;
            }
            set
            {
                if (value is DiscreteMaC_Lib.Graphes.Points.Point)
                {
                    this._SelectedPoint = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListMinPaths");
                }
            }
        }

        public IEnumerable<DataGridPathItem> ListMinPaths
        {
            get
            {
                if (SelectedPoint != null && MainGraph.PointCollection.Contains(SelectedPoint))
                    return GraphUtils.GetMinimalPathFromPointToAllPointsInGraph(MainGraph, SelectedPoint).Select(i1 => new DataGridPathItem(i1));
                else return new List<DataGridPathItem>();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            MainGraph = GenDefGraph();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void BtnGetDefGraph_Click(object sender, RoutedEventArgs e)
        {
            MainGraph = GenDefGraph();
        }

        private void BtnGetRandGraph_Click(object sender, RoutedEventArgs e)
        {
            MainGraph = GraphUtils.GenerateRandomDirectedGraphWithValuedEdgeAndPointID("C", "x", 11, 30);
        }

        private DirectedGraphWithValuedEdgeAndPointID GenDefGraph()
        {
            DirectedGraphWithValuedEdgeAndPointID CurrentGraph = new DirectedGraphWithValuedEdgeAndPointID("C", "x", new DirectedValuedEdgeEqualityComparer());
            PointWithID x1 = new PointWithID(1, "x");
            PointWithID x2 = new PointWithID(2, "x");
            PointWithID x3 = new PointWithID(3, "x");
            PointWithID x4 = new PointWithID(4, "x");
            PointWithID x5 = new PointWithID(5, "x");
            PointWithID x6 = new PointWithID(6, "x");
            PointWithID x7 = new PointWithID(7, "x");
            PointWithID x8 = new PointWithID(8, "x");
            PointWithID x9 = new PointWithID(9, "x");
            PointWithID x10 = new PointWithID(10, "x");
            PointWithID x11 = new PointWithID(11, "x");

            CurrentGraph.AddPoint(x1);
            CurrentGraph.AddPoint(x2);
            CurrentGraph.AddPoint(x3);
            CurrentGraph.AddPoint(x4);
            CurrentGraph.AddPoint(x5);
            CurrentGraph.AddPoint(x6);
            CurrentGraph.AddPoint(x7);
            CurrentGraph.AddPoint(x8);
            CurrentGraph.AddPoint(x9);
            CurrentGraph.AddPoint(x10);
            CurrentGraph.AddPoint(x11);

            //for x1
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_1", x1, x2, 2));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_2", x1, x3, 5));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_3", x1, x4, 3));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_4", x1, x6, 4));
            //for x2
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_5", x2, x1, 4));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_6", x2, x4, 8));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_7", x2, x6, 15));
            //for x3
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_8", x3, x1, 9));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_9", x3, x2, 1));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_10", x3, x4, 2));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_11", x3, x5, 5));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_12", x3, x7, 13));
            // for x4
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_13", x4, x2, 8));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_14", x4, x3, 2));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_15", x4, x5, 16));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_16", x4, x7, 3));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_17", x4, x9, 5));
            //for x5
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_18", x5, x3, 5));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_19", x5, x4, 16));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_20", x5, x8, 4));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_21", x5, x10, 6));
            //for x6
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_22", x6, x1, 4));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_23", x6, x2, 15));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_24", x6, x6, 6));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_25", x6, x7, 7));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_26", x6, x9, 8));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_27", x6, x11, 22));
            //for x7
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_28", x7, x3, 13));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_29", x7, x6, 7));
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_30", x7, x8, 3));
            //for x8
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_31", x8, x5, 4));
            //for x9
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_32", x9, x7, 6));
            //for x10
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_33", x10, x10, 2));
            //for x11
            CurrentGraph.AddEdge(new ValuedEdge<PointWithID>("ve_34", x11, x9, 3));

            return CurrentGraph;
        }
    }
    public class DataGridPathItem : INotifyPropertyChanged
    {
        private string _PointName, _Path, _Value;

        public event PropertyChangedEventHandler PropertyChanged;
        public string PointName
        {
            get
            {
                return this._PointName;
            }
            set
            {
                this._PointName = value;
                NotifyPropertyChanged();
            }
        }
        public string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                this._Path = value;
                NotifyPropertyChanged();
            }
        }
        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
                NotifyPropertyChanged();
            }
        }

        public DataGridPathItem(KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, ValuedPath> CurrentKvp)
        {
            this.PointName = CurrentKvp.Key.ToString();
            this.Value = CurrentKvp.Value.PathLengrh.ToString("0.00");
            this.Path = String.Join(",", CurrentKvp.Value.ListPathPoints);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}