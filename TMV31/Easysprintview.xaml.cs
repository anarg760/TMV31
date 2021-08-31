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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using TM_Report;

namespace TMV31
{
    /// <summary>
    /// Interaction logic for Easysprintview.xaml
    /// </summary>
    public partial class Easysprintview : Window
    {
        private List<string> RequestIDmodified = new List<string>();
        public Easysprintview()
        {
            //Task = exetogroup
            InitializeComponent();

            // Get a reference to the tasks collection.
            exestogroup _tasks = (exestogroup)this.Resources["exestogroup2"];

            int i = 1;
            // Generate some task data and add it to the task list.
            foreach (Executiondata item in PublicVariables.users)
            {
                i = i + 1;
                bool statusofexe = false;

                //Open//In Progress//Executed //Validated//Closed //Skipped //Aborted //Syntax Error
                //Pass//Fail/Not Tested / Pending


                if (item.Status.ToLower() == "closed")
                {
                    statusofexe = true;
                }

                _tasks.Add(new exetogroup()
                {

                    Program = item.Program,
                    TestBench = item.TestBench,
                    ExecutionID = item.Execution,
                    Status = item.Status,
                    DueDate = item.Executed_date,
                    Complete = statusofexe,
                    Result = item.Result,

                    Owner = item.Requestowner,
                    Hardware = item.Hardware,
                    Software = item.Software,
                    RequestType = item.RequestType,
                    ExecutionTime = item.Executiontime,
                    RequestEpic = item.RequestEpic,

                    StartDate = item.StartExecution_date





                }); ;
            }

        }

        private void UngroupButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView cvexestogroup = CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);
            if (cvexestogroup != null)
            {
                cvexestogroup.GroupDescriptions.Clear();
            }
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView cvexestogroup = CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);
            if (cvexestogroup != null && cvexestogroup.CanGroup == true)
            {
                cvexestogroup.GroupDescriptions.Clear();
                cvexestogroup.GroupDescriptions.Add(new PropertyGroupDescription("Program"));
                cvexestogroup.GroupDescriptions.Add(new PropertyGroupDescription("Complete"));

            }
        }

        private void CompleteFilter_Changed(object sender, RoutedEventArgs e)
        {
            // Refresh the view to apply filters.
            CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource).Refresh();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            exetogroup t = e.Item as exetogroup;
            if (t != null)
            // If filter is turned on, filter completed items.
            {
                if (this.cbCompleteFilter.IsChecked == true && t.Complete == true)
                    e.Accepted = false;
                else
                    e.Accepted = true;
            }
        }

        private void GroupTestbench_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView cvexestogroup = CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);
            if (cvexestogroup != null && cvexestogroup.CanGroup == true)
            {
                cvexestogroup.GroupDescriptions.Clear();
                cvexestogroup.GroupDescriptions.Add(new PropertyGroupDescription("TestBench"));

                cvexestogroup.GroupDescriptions.Add(new PropertyGroupDescription("Complete"));
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rowIndex = this.dataGrid1.SelectedIndex;
            PublicVariables.selectedrequestindex = rowIndex;
            DataGridRow row = (DataGridRow)this.dataGrid1.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row == null)
            {

            }
            else
            {
                TMV31.exetogroup idk = (TMV31.exetogroup)row.Item;
                PublicVariables.selectedexecution = idk.ExecutionID.ToString();
                PublicVariables.selectedgroup = null;
            }
        }

        private void Datagrid1doubleclick(object sender, MouseButtonEventArgs e)
        {
            Scriptsvisualizer win2 = new Scriptsvisualizer();


            var rowIndex = this.dataGrid1.SelectedIndex;
            DataGridRow row = (DataGridRow)this.dataGrid1.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            TMV31.exetogroup idk = (TMV31.exetogroup)row.Item;
            PublicVariables.selectedexecution = idk.ExecutionID.ToString();
            win2.Title = "Request:" + idk.ExecutionID.ToString() + " Program:" + idk.Program + " SWversion:" + idk.Software;
            win2.Show();
        }

        private void GroupSW_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView cvexestogroup = CollectionViewSource.GetDefaultView(dataGrid1.ItemsSource);
            if (cvexestogroup != null && cvexestogroup.CanGroup == true)
            {
                cvexestogroup.GroupDescriptions.Clear();
                cvexestogroup.GroupDescriptions.Add(new PropertyGroupDescription("Software"));
                cvexestogroup.GroupDescriptions.Add(new PropertyGroupDescription("Complete"));
            }
        }

        private void UpdateinfotoDB(object sender, RoutedEventArgs e)
        {
            DataTable HWtable = DBconsults.getdbfullhw();
            DataTable SWtable = DBconsults.getdbfullsw();


            var dat12 = (ListCollectionView)dataGrid1.ItemsSource;
            var data2 = dat12.SourceCollection;
            var dataparasubr = dataGrid1.ItemsSource.GetEnumerator();
            foreach (exetogroup item in data2)
            {
                if (item.Owner.ToString().ToUpper() == PublicVariables.requestowner.ToUpper())
                {
                    foreach (string executionid in RequestIDmodified)
                    {
                        string quedy = "";
                        if (item.ExecutionID.ToString() == executionid)
                        {
                            DataRow[] hwrows = (DataRow[])HWtable.Select("hwname LIKE '" + item.Hardware + "' AND variant LIKE '" + item.Program + "'");
                            DataRow newrow = hwrows[0];
                            int hardwareid = int.Parse(newrow.ItemArray[0].ToString());
                            DataRow[] swrows = (DataRow[])SWtable.Select("swname LIKE '" + item.Software + "' AND swprogram LIKE '" + item.Program + "'");
                            DataRow newswrow = swrows[0];
                            int softwareid = int.Parse(newswrow.ItemArray[0].ToString());
                            quedy = "UPDATE testmanagerdev.requests " +
                                "SET requestcycle = 1, " +
                                "requestowner = '" + item.Owner + "', " +
                                "requeststartdate ='" + item.StartDate + "', " +
                                "requestenddate ='" + item.DueDate + "', " +
                                "requeststatus ='" + item.Status + "', " +
                                "requesttype ='" + item.RequestType + "', " +
                                "requestprogram ='" + item.Program + "', " +
                                "requestsw ='" + softwareid.ToString() + "', " +
                                "requesthw ='" + hardwareid.ToString() + "', " +
                                "requestpreferentrack ='" + item.TestBench + "', " +
                                "requestresult ='" + item.Result + "', " +
                                "requeststaskid = '" + item.RequestEpic + "' " +
                                " WHERE requestid = " + executionid + " ; ";
                            DBconsults.updatetopostgress(quedy, true);


                        }
                    }
                }
                else
                {
                    if (PublicVariables.requestowner.ToUpper() == "IPICAZO" || PublicVariables.requestowner.ToUpper() == "PCORTES")
                    {

                        foreach (string executionid in RequestIDmodified)
                        {
                            string quedy = "";
                            if (item.ExecutionID.ToString() == executionid)
                            {
                                DataRow[] hwrows = (DataRow[])HWtable.Select("hwname LIKE '" + item.Hardware + "' AND variant LIKE '" + item.Program + "'");
                                DataRow newrow = hwrows[0];
                                int hardwareid = int.Parse(newrow.ItemArray[0].ToString());
                                DataRow[] swrows = (DataRow[])SWtable.Select("swname LIKE '" + item.Software + "' AND swprogram LIKE '" + item.Program + "'");
                                DataRow newswrow = swrows[0];
                                int softwareid = int.Parse(newswrow.ItemArray[0].ToString());
                                quedy = "UPDATE testmanagerdev.requests " +
                                    "SET requestcycle = 1, " +
                                    "requestowner = '" + item.Owner + "', " +
                                    "requeststartdate ='" + item.StartDate + "', " +
                                    "requestenddate ='" + item.DueDate + "', " +
                                    "requeststatus ='" + item.Status + "', " +
                                    "requesttype ='" + item.RequestType + "', " +
                                    "requestprogram ='" + item.Program + "', " +
                                    "requestsw ='" + softwareid.ToString() + "', " +
                                    "requesthw ='" + hardwareid.ToString() + "', " +
                                    "requestpreferentrack ='" + item.TestBench + "', " +
                                    "requestresult ='" + item.Result + "', " +
                                    "requeststaskid = '" + item.RequestEpic + "' " +
                                    " WHERE requestid = " + executionid + " ; ";
                                DBconsults.updatetopostgress(quedy, true);


                            }
                        }

                    }

                }
            }
            MessageBox.Show("Update completed ");

        }

        private void dataGrid1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            UpdateInfo.Visibility = Visibility.Visible;
            exetogroup rowIndex = (exetogroup)this.dataGrid1.CurrentItem;
            PublicVariables.selectedgroup = rowIndex;
            //DataGridRow row = (DataGridRow)this.dataGrid1.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            //Testcorddinator_V2.exetogroup idk = (Testcorddinator_V2.exetogroup)row.Item;
            RequestIDmodified.Add(rowIndex.ExecutionID.ToString());
        }

        private void dataGrid1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            exetogroup rowIndex = (exetogroup)this.dataGrid1.CurrentItem;
            if (rowIndex != null)
            {


                ContextMenu cm = new ContextMenu();
                MenuItem val1 = new MenuItem();
                MenuItem val2 = new MenuItem();
                val1.Header = "Skip Request";

                val1.Click += new RoutedEventHandler(this.MenuItem_Click);
                val2.Header = "Edit Request";
                cm.Items.Add(val1);
                cm.Items.Add(val2);

                cm.IsOpen = true;
                cm.StaysOpen = true;
            }

        }

        private RoutedEventHandler skipselectedrequest()
        {

            throw new NotImplementedException();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            exetogroup rowIndex = ((exetogroup)this.dataGrid1.CurrentItem);
            MenuItem val = (MenuItem)sender;
            switch (val.Header)
            {
                case "Skip Request":
                    rowIndex.Status = "Skip";
                    ((exetogroup)this.dataGrid1.CurrentItem).Status = "Skip";
                    RequestIDmodified.Add(rowIndex.ExecutionID.ToString());
                    UpdateInfo.Visibility = Visibility.Visible;

                    break;
                case "Edit Request":
                    string idtoedit = rowIndex.ExecutionID.ToString();


                    break;
                default:
                    MessageBox.Show("Under Development");
                    break;
            }


        }

        private void GenerateSprinteasyscript(object sender, RoutedEventArgs e)
        {

            List<TM_Report.Report.eColumnsToWrite> ColumnsToWrite = new List<TM_Report.Report.eColumnsToWrite>();

            ColumnsToWrite.Add(Report.eColumnsToWrite.Request_Id);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Request_Status);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Request_Result);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Request_Program);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Request_Sprint);
            ColumnsToWrite.Add(Report.eColumnsToWrite.SW_ID);
            ColumnsToWrite.Add(Report.eColumnsToWrite.HW_ID);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Feature);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Test_Case_Name);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Single_Executed_In);
            ColumnsToWrite.Add(Report.eColumnsToWrite.TC_Request_Status);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Request_Results);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Analyzed_By);
            ColumnsToWrite.Add(Report.eColumnsToWrite.TC_Iterations);
            ColumnsToWrite.Add(Report.eColumnsToWrite.Iterations_Done);

            string programtoreport = "";
            string swvertoreport = "";
            string selectedsprinttoreport = PublicVariables.selectedsprint;
            exetogroup rowIndex = (exetogroup)this.dataGrid1.CurrentItem;
            var rowIndex2 = this.dataGrid1.SelectedIndex;
            rowIndex2 = PublicVariables.selectedrequestindex;
            if (rowIndex == null && PublicVariables.selectedgroup == null)
            {

                DataGridRow row = (DataGridRow)this.dataGrid1.ItemContainerGenerator.ContainerFromIndex(rowIndex2);
                TMV31.exetogroup idk = (TMV31.exetogroup)row.Item;
                programtoreport = idk.Program;
                swvertoreport = idk.Software;
            }
            else
            {
                programtoreport = PublicVariables.selectedgroup.Program;
                swvertoreport = PublicVariables.selectedgroup.Software;
            }
            MessageBoxResult dialogResult = MessageBox.Show("You will generate a report for Program:" + programtoreport +
                                            " And SW:" + swvertoreport + "during sprint:" + PublicVariables.selectedsprint,
                                            "Generating report", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.No)
            {
                MessageBoxResult dialogResult2 = MessageBox.Show("You want to create a full program report of " + programtoreport,
                                            "Generating report", MessageBoxButton.YesNo);
                if (dialogResult2 == MessageBoxResult.Yes)
                {
                    TM_Report.Report.PrintReport(programtoreport, ColumnsToWrite: ColumnsToWrite);
                    MessageBox.Show(@"Report Created on C:\temp");
                }
                else
                {


                }

            }
            else
            {

                TM_Report.Report.PrintReport(programtoreport, requestsw: swvertoreport, requestsprint: PublicVariables.selectedsprint, ColumnsToWrite: ColumnsToWrite);

                MessageBox.Show(@"Report Created on C:\temp");

            }
        }

    
    }


    [ValueConversion(typeof(Boolean), typeof(String))]
    public class CompleteConverter2 : IValueConverter
    {
        // This converter changes the value of a Tasks Complete status from true/false to a string value of
        // "Complete"/"Active" for use in the row group header.
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool complete = (bool)value;
            if (complete)
                return "Complete";
            else
                return "Active";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strComplete = (string)value;
            if (strComplete == "Complete")
                return true;
            else
                return false;
        }
    }
    // Task Class
    // Requires using System.ComponentModel;
    public class exetogroup : INotifyPropertyChanged, IEditableObject
    {
        // The Task class implements INotifyPropertyChanged and IEditableObject
        // so that the datagrid can properly respond to changes to the
        // data collection and edits made in the DataGrid.

        // Private task data.
        private string m_Program = string.Empty;
        private string m_TestBench = string.Empty;
        private DateTime m_DueDate = DateTime.Now;
        private bool m_Complete = false;
        private string m_exename = "";
        private long m_exeid;
        private DateTime m_StartDate;
        private string m_hardware;
        private string m_software;
        private string m_result;
        private string m_requesttype;
        private long m_ExecutionTime;
        private string m_Status;
        private string m_Owner;
        private string m_RequestEpic;


        // Data for undoing canceled edits.
        private exetogroup temp_Task = null;
        private bool m_Editing = false;

        // Public properties.
        public string Program
        {
            get { return this.m_Program; }
            set
            {
                if (value != this.m_Program)
                {
                    this.m_Program = value;
                    NotifyPropertyChanged("Program");
                }
            }


        }

        public string TestBench
        {
            get { return this.m_TestBench; }
            set
            {
                if (value != this.m_TestBench)
                {
                    this.m_TestBench = value;
                    NotifyPropertyChanged("TestBench");
                }
            }
        }
        public long ExecutionID
        {
            get { return this.m_exeid; }
            set
            {
                if (value != this.m_exeid)
                {
                    this.m_exeid = value;
                    NotifyPropertyChanged("ExecutionID");
                }
            }

        }
        public string Status
        {
            get { return this.m_Status; }
            set
            {
                if (value != this.m_Status)
                {
                    this.m_Status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }
        public DateTime DueDate
        {
            get { return this.m_DueDate; }
            set
            {
                if (value != this.m_DueDate)
                {
                    this.m_DueDate = value;
                    NotifyPropertyChanged("DueDate");
                }
            }
        }

        public bool Complete
        {
            get { return this.m_Complete; }
            set
            {
                if (value != this.m_Complete)
                {
                    this.m_Complete = value;
                    NotifyPropertyChanged("Complete");
                }
            }
        }



        public DateTime StartDate
        {
            get { return this.m_StartDate; }
            set
            {
                if (value != this.m_StartDate)
                {
                    this.m_StartDate = value;
                    NotifyPropertyChanged("StartDate");
                }
            }
        }
        public string RequestType
        {
            get { return this.m_requesttype; }
            set
            {
                if (value != this.m_requesttype)
                {
                    this.m_requesttype = value;
                    NotifyPropertyChanged("RequestType");
                }
            }
        }
        public string Hardware
        {
            get { return this.m_hardware; }
            set
            {
                if (value != this.m_hardware)
                {
                    this.m_hardware = value;
                    NotifyPropertyChanged("Hardware");
                }
            }
        }


        public string Software
        {
            get { return this.m_software; }
            set
            {
                if (value != this.m_software)
                {
                    this.m_software = value;
                    NotifyPropertyChanged("Software");
                }
            }
        }
        public string Owner
        {
            get { return this.m_Owner; }
            set
            {
                if (value != this.m_Owner)
                {
                    this.m_Owner = value;
                    NotifyPropertyChanged("Owner");
                }
            }
        }
        public long ExecutionTime
        {
            get { return this.m_ExecutionTime; }
            set
            {
                if (value != this.m_ExecutionTime)
                {
                    this.m_ExecutionTime = value;
                    NotifyPropertyChanged("ExecutionTime");
                }
            }
        }
        public string Result
        {
            get { return this.m_result; }
            set
            {
                if (value != this.m_result)
                {
                    this.m_result = value;
                    NotifyPropertyChanged("Result");
                }
            }
        }
        public string RequestEpic
        {
            get { return this.m_RequestEpic; }
            set
            {
                if (value != this.m_RequestEpic)
                {
                    this.m_RequestEpic = value;
                    NotifyPropertyChanged("RequestEpic");
                }
            }
        }



        // Implement INotifyPropertyChanged interface.
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Implement IEditableObject interface.
        public void BeginEdit()
        {

            if (m_Editing == false)
            {
                temp_Task = this.MemberwiseClone() as exetogroup;
                m_Editing = true;

            }
        }

        public void CancelEdit()
        {
            if (m_Editing == true)
            {
                this.Program = temp_Task.Program;
                this.TestBench = temp_Task.TestBench;
                this.DueDate = temp_Task.DueDate;
                this.Complete = temp_Task.Complete;
                this.Hardware = temp_Task.Hardware;
                this.Result = temp_Task.Result;
                this.RequestType = temp_Task.RequestType;
                this.Software = temp_Task.Software;
                this.StartDate = temp_Task.StartDate;
                this.DueDate = temp_Task.DueDate;
                this.ExecutionID = temp_Task.ExecutionID;
                this.ExecutionTime = temp_Task.ExecutionTime;

                this.Status = temp_Task.Status;
                this.RequestEpic = temp_Task.RequestEpic;
                m_Editing = false;
            }
        }

        public void EndEdit()
        {
            if (m_Editing == true)
            {
                temp_Task = null;
                m_Editing = false;
            }
        }
    }
    // Requires using System.Collections.ObjectModel;
    public class exestogroup : ObservableCollection<exetogroup>
    {
        // Creating the Tasks collection in this way enables data binding from XAML.
    }
}
