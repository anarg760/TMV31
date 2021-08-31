using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Npgsql;

namespace TMV31
{
    public static class PublicVariables
    {
        public static bool accesis_on = false;
        public static bool postgressis_on = false;
        public static OleDbConnection accesscon;
        public static NpgsqlConnection postgresscon;
        public static exetogroup selectedgroup;
        public static string selectedsprint;
        public static string selectedsoftware;
        public static string selectedprogram;
        public static string selectedexecution;
        public static List<Executiondata> users = new List<Executiondata>();
        public static bool sprintchanged = false;
        public static List<Task> scriptsinexe = new List<Task>();
        public static bool closeall = false;
        public static bool addexecution = false;
        public static string rackselectedforexe;
        public static string swselectedforexe;
        public static string hwselectedforexe;
        public static string sprintselectedforexe;
        public static string exetype;
        public static string programselectedforexe;
        public static string testtype;
        public static string Executeowner;
        public static string numberofcycles;
        public static int cycletotest;
        public static string requestowner;
        public static int selectedrequestindex;

        public static List<String> Tcsselectedforexe = new List<string>();
        public static List<String> Programslist = new List<string>();
        public static List<String> Racklist = new List<string>();
        public static List<String> globalSprintsList = new List<string>();
        public static List<String> featurelist = new List<string>();
        public static List<String> ownerlist = new List<string>();
        public static List<String> swversionList = new List<string>();
        public static List<String> HWlist = new List<string>();
        public static List<String> SWList = new List<string>();

        public static bool generaterequestisactive = false;
        public static Dictionary<string, string> feature_Script_dic = new Dictionary<string, string>();
        public static List<String> Feature_Script_List = new List<string>();
        public static List<Exetoexecute> Exetoexecute_list = new List<Exetoexecute>();
        public static bool updateswlist = false;
        public static bool updatehwlist = false;
        public static bool updatevariantinfoflash = false;
        public static bool generatereportflag = false;

        public static List<String> datelist = new List<string>();

    }
}
