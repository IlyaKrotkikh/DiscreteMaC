using DiscreteMaC_Lib.Graphes;
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

namespace Lab4_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DirectedGraphWithPointID _MainGraph;
        private string _MainGraphName, _TaskText;
        private int _MainGraphPointCount, _MainGraphEdgeCount;
        private DiscreteMaC_Lib.Graphes.Points.Point _FirstSelectedPoint, _SecondSelectedPoint, _TransitiveClosurePoint;
        private List<string> _lstNothing;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TaskText
        {
            get
            {
                return this._TaskText;
            }
            set
            {
                this._TaskText = value;
                NotifyPropertyChanged();
            }
        }

        public DirectedGraphWithPointID MainGraph
        {
            get
            {
                return this._MainGraph;
            }
            set
            {
                if (value is DirectedGraphWithPointID)
                {
                    this._MainGraph = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListPathPoints");
                    TransitiveClosurePoint = SetDefaultClosurePoint(value);
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
                this._MainGraphName = value;
                NotifyPropertyChanged();
                if (MainGraph != null)
                {
                    MainGraph.GraphName = _MainGraphName;
                    NotifyPropertyChanged("MainGraph");
                }
            }
        }
        public int MainGraphPointCount
        {
            get
            {
                return this._MainGraphPointCount;
            }
            set
            {
                this._MainGraphPointCount = value;
                NotifyPropertyChanged();
            }
        }
        public int MainGraphEdgeCount
        {
            get
            {
                return this._MainGraphEdgeCount;
            }
            set
            {
                this._MainGraphEdgeCount = value;
                NotifyPropertyChanged();
            }
        }

        public DiscreteMaC_Lib.Graphes.Points.Point FirstSelectedPoint
        {
            get
            {
                return this._FirstSelectedPoint;
            }
            set
            {
                if (value != null && value is DiscreteMaC_Lib.Graphes.Points.Point)
                {
                    this._FirstSelectedPoint = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListPathPoints");
                }
            }
        }
        public DiscreteMaC_Lib.Graphes.Points.Point SecondSelectedPoint
        {
            get
            {
                return this._SecondSelectedPoint;
            }
            set
            {
                if (value != null && value is DiscreteMaC_Lib.Graphes.Points.Point)
                {
                    this._SecondSelectedPoint = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ListPathPoints");
                }
            }
        }
        public DiscreteMaC_Lib.Graphes.Points.Point TransitiveClosurePoint
        {
            get
            {
                return this._TransitiveClosurePoint;
            }
            set
            {
                if (value is DiscreteMaC_Lib.Graphes.Points.Point)
                {
                    this._TransitiveClosurePoint = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("InClosureForPoint");
                    NotifyPropertyChanged("OutClosureForPoint");
                }
            }
        }

        public string InClosureForPoint
        {
            get
            {
                if (TransitiveClosurePoint != null && MainGraph.PointCollection.Contains(TransitiveClosurePoint))
                    return String.Join(",", GraphUtils.GetInTransitiveClosureForPoint(MainGraph, TransitiveClosurePoint));
                else return "-";
            }
        }

        public string OutClosureForPoint
        {
            get
            {
                if (TransitiveClosurePoint != null && MainGraph.PointCollection.Contains(TransitiveClosurePoint))
                    return String.Join(",", GraphUtils.GetOutTransitiveClosureForPoint(MainGraph, TransitiveClosurePoint));
                else return "-";
            }
        }

        public IEnumerable<string> ListPathPoints
        {
            get
            {
                if (FirstSelectedPoint != null
                    && SecondSelectedPoint != null
                    && GraphUtils.GetOutTransitiveClosureForPoint(MainGraph, FirstSelectedPoint).Contains(SecondSelectedPoint)
                    && GraphUtils.GetInTransitiveClosureForPoint(MainGraph, SecondSelectedPoint).Contains(FirstSelectedPoint))
                {
                    return GraphUtils.GetPathsForPointInGraph(MainGraph, FirstSelectedPoint)
                        .Where(i1 => i1.ListPathPoints.Last().Equals(SecondSelectedPoint))
                        .Select(i1 => String.Join(", ", i1.ListPathPoints));
                }
                else
                    return _lstNothing;
            }
        }

        public MainWindow()
        {
            _lstNothing = new List<string>();
            _lstNothing.Add("Нет путей с вершинами");

            InitializeComponent();

            this.DataContext = this;

            MainGraphName = "G1";
            MainGraphPointCount = 4;
            MainGraphEdgeCount = 7;

            MainGraph = GraphUtils.GenerateRandomDirectedGraphWithPointID(MainGraphName, "x", MainGraphPointCount, MainGraphEdgeCount);
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void BtnGenMainGraph_Click(object sender, RoutedEventArgs e)
        {
            MainGraph = GraphUtils.GenerateRandomDirectedGraphWithPointID(MainGraphName, "x", MainGraphPointCount, MainGraphEdgeCount);
        }

        private DiscreteMaC_Lib.Graphes.Points.Point SetDefaultClosurePoint(DirectedGraphWithPointID CurrentGraph)
        {
            int varNum = 8;

            if (CurrentGraph.PointCollection.Count(i1 => i1.ID == varNum) > 0)
                return CurrentGraph.PointCollection.SingleOrDefault((i1 => i1.ID == varNum));
            else return CurrentGraph.PointCollection.Last();
        }
    }
}
