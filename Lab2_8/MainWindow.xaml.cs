using DiscreteMaC_Lib.Graphes;
using DiscreteMaC_Lib.Graphes.Edges;
using DiscreteMaC_Lib.GraphNotations;
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

namespace Lab2_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _HtmlDataGAjacM, _HtmlDataGIncidM;

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
        public int GAjacM_Edges { get; set; }
        public string GAjacM_Name { get; set; }

        TypedGraphNotation<string> AjacMNotation { get; set; }
        TypedGraphNotation<string> IncidMNotation { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            TaskText = "8 вариант\nПо матрице смежности построить матрицу инцидентности и по матрице инцидентности найти вершину графа, имеющую максимальную полустепень исхода.";
            
            GAjacM_Name = "G1";
            GAjacM_Points = 4;
            GAjacM_Edges = 7;

            GraphGAjacM = GraphUtils.GenerateRandomDirectedGraph(GAjacM_Name, GAjacM_Points, GAjacM_Edges);

            AjacMNotation = new HtmlAdjacencyMatrixNotation();
            HtmlDataGAjacM = AjacMNotation.ConvertFromGrapch(GraphGAjacM);

            IncidMNotation = new HtmlIncidenceMatrixNotation();
            HtmlDataGIncidM = IncidMNotation.ConvertFromGrapch(GraphGAjacM);
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
