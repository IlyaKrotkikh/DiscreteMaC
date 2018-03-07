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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.Graphes.Points;
using DiscreteMaC_Lib.GraphNotations;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _HtmlDataG1, _HtmlDataG2, _HtmlDataGOut;
        private int _TaskAnswer;
        private ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point> _ListPointsGrapchOut;
        public event PropertyChangedEventHandler PropertyChanged;

        public string HtmlDataG1
        {
            get
            {
                return this._HtmlDataG1;
            }
            set
            {
                this._HtmlDataG1 = value;
                NotifyPropertyChanged();
            }
        }
        public string HtmlDataG2
        {
            get
            {
                return this._HtmlDataG2;
            }
            set
            {
                this._HtmlDataG2 = value;
                NotifyPropertyChanged();
            }
        }
        public string HtmlDataGOut
        {
            get
            {
                return this._HtmlDataGOut;
            }
            set
            {
                this._HtmlDataGOut = value;
                NotifyPropertyChanged();
            }
        }

        public string TaskText { get; set; }

        public int G1_Points { get; set; }
        public int G1_Edges { get; set; }
        public string G1_Name { get; set; }

        public int G2_Points { get; set; }
        public int G2_Edges { get; set; }
        public string G2_Name { get; set; }

        public Graph<Edge> GraphG1 { get; set; }
        public Graph<Edge> GraphG2 { get; set; }
        public Graph<Edge> GraphGOut { get; set; }

        public int TaskAnswer
        {
            get
            {
                return this._TaskAnswer;
            }
            set
            {
                this._TaskAnswer = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point> ListPointsGrapchOut
        {
            get
            {
                return this._ListPointsGrapchOut;
            }
            set
            {
                this._ListPointsGrapchOut = value;
                NotifyPropertyChanged();
            }
        }

        TypedGraphNotation<string> CurrentNotation { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            CurrentNotation = new HtmlAdjacencyMatrixNotation();

            TaskText = "8 вариант\nНайти количество дуг, инцидентных вершине графа G1 * G2, номер которой вводится с клавиатуры.";
            G1_Name = "G1";
            G1_Points = 4;
            G1_Edges = 7;

            G2_Name = "G2";
            G2_Points = 4;
            G2_Edges = 7;


            GraphG1 = GraphUtils.GenerateRandomDirectedGraph(G1_Name, G1_Points, G1_Edges);
            GraphG2 = GraphUtils.GenerateRandomDirectedGraph(G2_Name, G2_Points, G2_Edges);
            GraphGOut = GraphUtils.DirectedGraphIntersection(GraphG1, GraphG2);

            ListPointsGrapchOut = new ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point>(GraphGOut.ListPoint.Keys);

            HtmlDataG1 = CurrentNotation.ConvertFromGrapch(GraphG1);
            HtmlDataG2 = CurrentNotation.ConvertFromGrapch(GraphG2);
            HtmlDataGOut = CurrentNotation.ConvertFromGrapch(GraphGOut);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private int CalcIncidentEdges(Graph<Edge> InitialGraph, DiscreteMaC_Lib.Graphes.Points.Point SomePoint)
        {
            if (InitialGraph == null || SomePoint == null)
                return 0;

            IEnumerable<Edge> ListEdges = InitialGraph.ListEdges.Keys.ToList();
            ListEdges = ListEdges.Where(i1 => i1.StartPoint.Equals(SomePoint) || i1.EndPoint.Equals(SomePoint));
            return ListEdges.Count();
        }

        private void CmbGraphGOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((CmbGraphGOut.SelectedItem as DiscreteMaC_Lib.Graphes.Points.Point) != null)
            {
                TaskAnswer = CalcIncidentEdges(GraphGOut, CmbGraphGOut.SelectedItem as DiscreteMaC_Lib.Graphes.Points.Point);
            }
        }

        private void BtnGraphGOutRebuild_Click(object sender, RoutedEventArgs e)
        {
            GraphGOut = GraphUtils.DirectedGraphIntersection(GraphG1, GraphG2);

            ListPointsGrapchOut = new ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point>(GraphGOut.ListPoint.Keys);

            HtmlDataGOut = CurrentNotation.ConvertFromGrapch(GraphGOut);
        }

        private void BtnGenGraphG1_Click(object sender, RoutedEventArgs e)
        {
            GraphG1 = GraphUtils.GenerateRandomDirectedGraph(G1_Name, G1_Points, G1_Edges);
            HtmlDataG1 = CurrentNotation.ConvertFromGrapch(GraphG1);
        }


        private void BtnGenGraphG2_Click(object sender, RoutedEventArgs e)
        {
            GraphG2 = GraphUtils.GenerateRandomDirectedGraph(G2_Name, G2_Points, G2_Edges);
            HtmlDataG2 = CurrentNotation.ConvertFromGrapch(GraphG2);
        }
    }
}