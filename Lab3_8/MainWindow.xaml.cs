using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
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

namespace Lab3_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DirectedGraphWithPointID _MainGraph, _InducedSubgraph, _SpanningSubgraph;
        private string _MainGraphName,_TaskText;
        private int _MainGraphPointCount,_MainGraphEdgeCount;

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
                this._MainGraph = value;
                NotifyPropertyChanged();
            }
        }
        public DirectedGraphWithPointID InducedSubgrap
        {
            get
            {
                return this._InducedSubgraph;
            }
            set
            {
                this._InducedSubgraph = value;
                NotifyPropertyChanged();
            }
        }
        public DirectedGraphWithPointID SpanningSubgraph
        {
            get
            {
                return this._SpanningSubgraph;
            }
            set
            {
                this._SpanningSubgraph = value;
                NotifyPropertyChanged();
            }
        }

        public string MainGraphName
        {
            get {
                return this._MainGraphName;
            }
            set {
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
            set {
                this._MainGraphEdgeCount = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            TaskText = "Граф задается матрицей смежности, которая формируется случайным образом.";

            MainGraphName = "G1";
            MainGraphPointCount = 4;
            MainGraphEdgeCount = 7;

            MainGraph = GraphUtils.GenerateRandomDirectedGrapchWithPointID(MainGraphName, "x", MainGraphPointCount, MainGraphEdgeCount);
            SpanningSubgraph = GraphUtils.GetSpanningSubgraph(MainGraph, GraphUtils.Lab3_8EdgeSelectionCondition);
            InducedSubgrap = GraphUtils.GetInducedSubgraph(MainGraph, GraphUtils.Lab3_8PointSelectionCondition);
        }

        private void BtnGenMainGraph_Click(object sender, RoutedEventArgs e)
        {
            MainGraph = GraphUtils.GenerateRandomDirectedGrapchWithPointID(MainGraphName, "x", MainGraphPointCount, MainGraphEdgeCount);
            SpanningSubgraph = GraphUtils.GetSpanningSubgraph(MainGraph, GraphUtils.Lab3_8EdgeSelectionCondition);
            InducedSubgrap = GraphUtils.GetInducedSubgraph(MainGraph, GraphUtils.Lab3_8PointSelectionCondition);
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
