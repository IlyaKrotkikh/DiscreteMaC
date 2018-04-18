using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Edges.EdgeComparers;
using DiscreteMaC_Lib.Graphes.Paths;
using DiscreteMaC_Lib.Graphes.Points;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Lab6_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DirectedGraphWithValuedEdgeAndPointID MainGraph { get; set; }

        public IEnumerable<KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, ValuedPath>> ListMinPaths
        {
            get
            {
                return GraphUtils.GetMinimalPathFromPointToAllPointsInGraph(MainGraph, MainGraph.PointCollection.Single(i1 => i1.ID == 6));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            MainGraph = new DirectedGraphWithValuedEdgeAndPointID("C","x",new DirectedValuedEdgeEqualityComparer());
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

            MainGraph.AddPoint(x1);
            MainGraph.AddPoint(x2);
            MainGraph.AddPoint(x3);
            MainGraph.AddPoint(x4);
            MainGraph.AddPoint(x5);
            MainGraph.AddPoint(x6);
            MainGraph.AddPoint(x7);
            MainGraph.AddPoint(x8);
            MainGraph.AddPoint(x9);
            MainGraph.AddPoint(x10);
            MainGraph.AddPoint(x11);

            //for x1
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_1", x1, x2, 2));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_2", x1, x3, 5));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_3", x1, x4, 3));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_4", x1, x6, 4));
            //for x2
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_5", x2, x1, 4));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_6", x2, x4, 8));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_7", x2, x6, 15));
            //for x3
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_8",  x3, x1, 9));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_9",  x3, x2, 1));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_10", x3, x4, 2));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_11", x3, x5, 5));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_12", x3, x7, 13));
            // for x4
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_13", x4, x2, 8));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_14", x4, x3, 2));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_15", x4, x5, 16));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_16", x4, x7, 3));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_17", x4, x9, 5));
            //for x5
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_18", x5, x3, 5));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_19", x5, x4, 16));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_20", x5, x8, 4));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_21", x5, x10, 6));
            //for x6
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_22", x6, x1, 4));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_23", x6, x2, 15));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_24", x6, x6, 6));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_25", x6, x7, 7));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_26", x6, x9, 8));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_27", x6, x11, 22));
            //for x7
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_28", x7, x3, 13));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_29", x7, x6, 7));
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_30", x7, x8, 3));
            //for x8
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_31", x8, x5, 4));
            //for x9
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_32", x9, x7, 6));
            //for x10
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_33", x10, x10, 2));
            //for x11
            MainGraph.AddEdge(new ValuedEdge<PointWithID>("ve_34", x11, x9, 3));


        }
    }
}
