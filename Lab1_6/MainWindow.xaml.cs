using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.GraphNotations;
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

namespace Lab1_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _HtmlDataG1, _HtmlDataG2, _HtmlDataGOut;
        private ObservableCollection<KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, int>> _TaskAnswer;

        private TypedGraphNotation<string> CurrentNotation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TaskText { get; set; }
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

        public Graph<Edge> GraphG1 { get; set; }
        public int G1_Points { get; set; }
        public string G1_Name { get; set; }

        public Graph<Edge> GraphG2 { get; set; }
        public int G2_Points { get; set; }
        public string G2_Name { get; set; }

        public Graph<Edge> GraphGOut { get; set; }

        public ObservableCollection<KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, int>> TaskAnswer
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

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
            CurrentNotation = new HtmlAdjacencyMatrixNotation();

            G1_Name = "g1";
            G1_Points = 6;

            G2_Name = "g2";
            G2_Points = 6;

            GraphG1 = GraphUtils.GenerateRandomDirectedGraph(G1_Name, G1_Points);
            GraphG2 = GraphUtils.GenerateRandomDirectedGraph(G2_Name, G2_Points);
            GraphGOut = GraphUtils.DirectedGraphUnion(GraphG1, GraphG2);

            TaskAnswer = new ObservableCollection<KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, int>>(GraphUtils.GetPointsWithMinOutDegree(GraphGOut));

            HtmlDataG1 = CurrentNotation.ConvertFromGrapch(GraphG1);
            HtmlDataG2 = CurrentNotation.ConvertFromGrapch(GraphG2);
            HtmlDataGOut = CurrentNotation.ConvertFromGrapch(GraphGOut);
        }

        private void BtnGenGraphG1_Click(object sender, RoutedEventArgs e)
        {
            GraphG1 = GraphUtils.GenerateRandomDirectedGraph(G1_Name, G1_Points);
            GraphGOut = GraphUtils.DirectedGraphUnion(GraphG1, GraphG2);

            HtmlDataG1 = CurrentNotation.ConvertFromGrapch(GraphG1);
            HtmlDataGOut = CurrentNotation.ConvertFromGrapch(GraphGOut);

            TaskAnswer = new ObservableCollection<KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, int>>(GraphUtils.GetPointsWithMinOutDegree(GraphGOut));
        }

        private void BtnGenGraphG2_Click(object sender, RoutedEventArgs e)
        {
            GraphG2 = GraphUtils.GenerateRandomDirectedGraph(G2_Name, G2_Points);
            GraphGOut = GraphUtils.DirectedGraphUnion(GraphG1, GraphG2);

            HtmlDataG2 = CurrentNotation.ConvertFromGrapch(GraphG2);
            HtmlDataGOut = CurrentNotation.ConvertFromGrapch(GraphGOut);

            TaskAnswer = new ObservableCollection<KeyValuePair<DiscreteMaC_Lib.Graphes.Points.Point, int>>(GraphUtils.GetPointsWithMinOutDegree(GraphGOut));
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
