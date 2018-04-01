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

namespace Lab2_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _HtmlDataGAjacM, _HtmlDataGIncidM;
        private string _TaskAnswer;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TaskText { get; set; }
        public string HtmlDataGAjacM
        {
            get
            {
                return this._HtmlDataGAjacM;
            }
            set
            {
                this._HtmlDataGAjacM = value;
                NotifyPropertyChanged();
            }
        }
        public string HtmlDataGIncidM
        {
            get
            {
                return this._HtmlDataGIncidM;
            }
            set
            {
                this._HtmlDataGIncidM = value;
                NotifyPropertyChanged();
            }
        }

        public Graph<Edge> GraphGAjacM { get; set; }
        public int GAjacM_Points { get; set; }
        public string GAjacM_Name { get; set; }

        public string TaskAnswer
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

        TypedGraphNotation<string> AjacMNotation { get; set; }
        TypedGraphNotation<string> IncidMNotation { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            GAjacM_Name = "G1";
            GAjacM_Points = 6;

            GraphGAjacM = GraphUtils.GenerateRandomDirectedGraph(GAjacM_Name, GAjacM_Points);

            AjacMNotation = new HtmlAdjacencyMatrixNotation();
            HtmlDataGAjacM = AjacMNotation.ConvertFromGrapch(GraphGAjacM);

            IncidMNotation = new HtmlIncidenceMatrixNotation();
            HtmlDataGIncidM = IncidMNotation.ConvertFromGrapch(GraphGAjacM);

            TaskAnswer = GraphUtils.GenerateGraphDescription(GraphGAjacM);
        }

        private void BtnGenGraphGAjacM_Click(object sender, RoutedEventArgs e)
        {
            GraphGAjacM = GraphUtils.GenerateRandomDirectedGraph(GAjacM_Name, GAjacM_Points);

            HtmlDataGAjacM = AjacMNotation.ConvertFromGrapch(GraphGAjacM);
            HtmlDataGIncidM = IncidMNotation.ConvertFromGrapch(GraphGAjacM);

            TaskAnswer = GraphUtils.GenerateGraphDescription(GraphGAjacM);
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
