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

namespace Lab5_8
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DirectedGraph _GraphG1, _GraphG2;
        IGraphBasics<DiscreteMaC_Lib.Graphes.Points.Point, IEdgeBasics<DiscreteMaC_Lib.Graphes.Points.Point>> _GraphGStrongComp12;
        private int _GraphG1Points, _GraphG2Points, _GraphG1Edges, _GraphG2Edges;
        private string _GraphG1Name, _GraphG2Name;

        public DirectedGraph GraphG1
        {
            get
            {
                return this._GraphG1;
            }
            set
            {
                if (value is DirectedGraph)
                {
                    this._GraphG1 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("GraphG12");
                    NotifyPropertyChanged("ListStrongComps");
                }

            }
        }
        public DirectedGraph GraphG2
        {
            get
            {
                return this._GraphG2;
            }
            set
            {
                if (value is DirectedGraph)
                {
                    this._GraphG2 = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("GraphG12");
                    NotifyPropertyChanged("ListStrongComps");
                }

            }
        }
        public DirectedGraph GraphG12
        {
            get
            {
                if (GraphG1 != null && GraphG2 != null)
                    return GraphUtils.DirectedGraphIntersection(GraphG1, GraphG2);
                else return GraphUtils.GenerateEmptyDirectedGraph("Ошибка в графах", 1);
            }

        }

        public IGraphBasics<DiscreteMaC_Lib.Graphes.Points.Point, IEdgeBasics<DiscreteMaC_Lib.Graphes.Points.Point>> SelectedStrongComponent
        {
            get
            {
                if (this._GraphGStrongComp12 == null)
                    return GraphUtils.GenerateEmptyDirectedGraph("Не_выбран_компонент", 1);
                else return this._GraphGStrongComp12;
            }
            set
            {
                if (value is IGraphBasics<DiscreteMaC_Lib.Graphes.Points.Point, IEdgeBasics<DiscreteMaC_Lib.Graphes.Points.Point>>)
                {
                    this._GraphGStrongComp12 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int GraphG1Points
        {
            get
            {
                return this._GraphG1Points;
            }
            set
            {
                this._GraphG1Points = value;
                NotifyPropertyChanged();
            }
        }
        public int GraphG2Points
        {
            get
            {
                return this._GraphG2Points;
            }
            set
            {
                this._GraphG2Points = value;
                NotifyPropertyChanged();
            }
        }

        public int GraphG1Edges
        {
            get
            {
                return this._GraphG1Edges;
            }
            set
            {
                this._GraphG1Edges = value;
                NotifyPropertyChanged();
            }
        }
        public int GraphG2Edges
        {
            get
            {
                return this._GraphG2Edges;
            }
            set
            {
                this._GraphG2Edges = value;
                NotifyPropertyChanged();
            }
        }
        public string GraphG1Name
        {
            get
            {
                return this._GraphG1Name;
            }
            set
            {
                this._GraphG1Name = value;
                NotifyPropertyChanged();
            }
        }


        public string GraphG2Name
        {
            get
            {
                return this._GraphG2Name;
            }
            set
            {
                this._GraphG2Name = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<IGraphBasics<DiscreteMaC_Lib.Graphes.Points.Point, IEdgeBasics<DiscreteMaC_Lib.Graphes.Points.Point>>> ListStrongComps
        {
            get
            {
                if (GraphG12.PointCollection.Count(i1 => i1.Name == "x6") == 1)
                    return GraphUtils.GetCollectionOneSidedStrongComponentsOfGraph(GraphG12, GraphG12.PointCollection.SingleOrDefault(i1 => i1.Name == "x6"));
                else
                    return GraphUtils.GetCollectionOneSidedStrongComponentsOfGraph(GraphG12, GraphG12.PointCollection.Last());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            GraphG1Name = "G1";
            GraphG1Points = 8;
            GraphG1Edges = 20;
            GraphG2Name = "G2";
            GraphG2Points = 8;
            GraphG2Edges = 20;

            GraphG1 = GraphUtils.GenerateRandomDirectedGraph(GraphG1Name, GraphG1Points, GraphG1Edges);
            GraphG2 = GraphUtils.GenerateRandomDirectedGraph(GraphG2Name, GraphG2Points, GraphG2Edges);
        }
        private void BtnGenGraphG1_Click(object sender, RoutedEventArgs e)
        {
            GraphG1 = GraphUtils.GenerateRandomDirectedGraph(GraphG1Name, GraphG1Points, GraphG1Edges);
        }

        private void BtnGenGraphG2_Click(object sender, RoutedEventArgs e)
        {
            GraphG2 = GraphUtils.GenerateRandomDirectedGraph(GraphG2Name, GraphG2Points, GraphG2Edges);
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