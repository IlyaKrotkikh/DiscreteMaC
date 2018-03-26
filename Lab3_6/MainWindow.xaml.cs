using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
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

namespace Lab3_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Graph<Edge> _MainGraph;
        private int _MainGrapPointsCount;
        private string _MainGraphName;
        private ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point> _ListMainGraphPoints;
        private DiscreteMaC_Lib.Graphes.Points.Point _SelectedStartPoint, _SelectedEndPoint;
        private bool _TaskAnswer;

        public event PropertyChangedEventHandler PropertyChanged;

        public Graph<Edge> MainGraph
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
        public string MainGraphName
        {
            get {
                return this._MainGraphName;
            }
            set {
                this._MainGraphName = value;
                if (MainGraph != null)
                    MainGraph.GraphName = _MainGraphName;
                NotifyPropertyChanged();
                NotifyPropertyChanged("MainGarph");
            }
        }
        public int MainGraphPointsCount
        {
            get
            {
                return this._MainGrapPointsCount;
            }
            set
            {
                this._MainGrapPointsCount = value;
                NotifyPropertyChanged();
            }
        }

        public DiscreteMaC_Lib.Graphes.Points.Point SelectedStartPoint
        {
            get
            {
                return this._SelectedStartPoint;
            }
            set
            {
                this._SelectedStartPoint = value;
            }
        }
        public DiscreteMaC_Lib.Graphes.Points.Point SelectedEndPoint
        {
            get
            {
                return this._SelectedEndPoint;
            }
            set
            {
                this._SelectedEndPoint = value;
            }
        }

        public bool TaskAnswer
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

        public ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point> ListMainGraphPoints
        {
            get
            {
                return this._ListMainGraphPoints;
            }
            set
            {
                this._ListMainGraphPoints = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            MainGraphName = "g1";
            MainGraphPointsCount = 5;
            TaskAnswer = false;

            MainGraph = GraphUtils.GenerateRandomDirectedGraph(MainGraphName, MainGraphPointsCount);

            ListMainGraphPoints = new ObservableCollection<DiscreteMaC_Lib.Graphes.Points.Point>(MainGraph.ListPoint.Keys);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
