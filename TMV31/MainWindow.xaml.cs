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

namespace TMV31
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
         
            }
        }
    }

    public class Executiondata
    {
        public int Execution { get; set; }
        public string Program { get; set; }
        public DateTime Executed_date { get; set; }
        public DateTime StartExecution_date { get; set; }
        public string Status { get; set; }
        public string Issues { get; set; }
        public string Result { get; set; }
        public string Priority { get; set; }
        public string TestBench { get; set; }
        public string Exename { get; set; }
        public long Executiontime { get; set; }
        public string ForcedEndDate { get; set; }
        public string Requestowner { get; set; }

        public string RequestType { get; set; }

        public string Hardware { get; set; }
        public string Software { get; set; }
        public string RequestEpic { get; set; }

    }

    public class Exetoexecute
    {
        public long TCID { get; set; }
        public string TC_name { get; set; }
        public string Feature { get; set; }
        public string Iteration { get; set; }
        public string Time_Limit { get; set; }
        public string Owner { get; set; }
        public string RequestType { get; set; }
        public string Sprint { get; set; }
        public long Priority { get; set; }
        public string TestBench { get; set; }
        public string Program { get; set; }
        public string HW { get; set; }
        public string SW { get; set; }
        public string Cyclic { get; set; }
        public bool Stoponfirstfail { get; set; }



    }
}
