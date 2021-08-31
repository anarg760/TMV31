using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;
using System.Data;


namespace TMV31
{
   public static class DBconsults
    {
        public static DataTable getdbracks()
        {

            DataTable buffertable = new DataTable();
            string quedy = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT name FROM testmanagerdev.racks ORDER by name; ";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Racks.Rack_Name FROM Racks;";
                buffertable = DBconnection.accessselectquedry(quedy);
            }
            return buffertable;
        }
        public static DataTable getdbPrograms()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT variant FROM testmanagerdev.variantprogram;";
                buffertable = DBconnection.postgresselectquedy(quedy);
            }
            else
            {
                quedy = "SELECT Racks.Rack_Name FROM Racks;";
                buffertable = DBconnection.accessselectquedry(quedy);
            }
            return buffertable;
        }
        public static DataTable getdbexecutions()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            quedy = "SELECT Racks.Rack_Name FROM Racks;";
            buffertable = DBconnection.accessselectquedry(quedy);
            return buffertable;
        }
        public static DataTable getdbsprints()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {

                quedy = "SELECT sprintsname FROM testmanagerdev.sprints; ";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;
        }
        public static DataTable getrequestfromsprint(string sprinttosearch)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {

                quedy = "SELECT requestid, requestcycle, requestowner, requeststartdate, requestenddate, " +
                    "requeststatus, requestcreationdate, requestsinglebundle, requesttype, requestprogram, " +
                    "requestsw, requesthw, requestsprint, requestpreferentrack, requestpriority, requestname, " +
                    "requestresult, requesttimeout, requestshutdowndate, requestexecutiontime," +
                    "testmanagerdev.sw.swname, testmanagerdev.hw.hwname, testmanagerdev.requests.requeststaskid FROM testmanagerdev.requests " +
                    "Join testmanagerdev.sw ON testmanagerdev.sw.swid = requestsw " +
                    "join testmanagerdev.hw ON testmanagerdev.hw.hwid = requesthw WHERE requestsprint = '" + sprinttosearch + "';";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;


        }



        public static DataTable getrequestfromsw(string swsearch)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {

                quedy = "SELECT requestid, requestcycle, requestowner, requeststartdate, requestenddate, " +
                    "requeststatus, requestcreationdate, requestsinglebundle, requesttype, requestprogram, " +
                    "requestsw, requesthw, requestsprint, requestpreferentrack, requestpriority, requestname, " +
                    "requestresult, requesttimeout, requestshutdowndate, requestexecutiontime," +
                    "testmanagerdev.sw.swname, testmanagerdev.hw.hwname, testmanagerdev.requests.requeststaskid FROM testmanagerdev.requests " +
                    "Join testmanagerdev.sw ON testmanagerdev.sw.swid = requestsw " +
                    "join testmanagerdev.hw ON testmanagerdev.hw.hwid = requesthw WHERE requestsprint = '" + swsearch + "';";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }


        public static DataTable getrequestfromdate(string startdate, string enddate)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {

                quedy = "SELECT requestid, requestcycle, requestowner, requeststartdate, requestenddate, " +
                    "requeststatus, requestcreationdate, requestsinglebundle, requesttype, requestprogram, " +
                    "requestsw, requesthw, requestsprint, requestpreferentrack, requestpriority, requestname, " +
                    "requestresult, requesttimeout, requestshutdowndate, requestexecutiontime," +
                    "testmanagerdev.sw.swname, testmanagerdev.hw.hwname, testmanagerdev.requests.requeststaskid FROM testmanagerdev.requests " +
                    "Join testmanagerdev.sw ON testmanagerdev.sw.swid = requestsw " +
                    "join testmanagerdev.hw ON testmanagerdev.hw.hwid = requesthw WHERE requestcreationdate " +
                    "BETWEEN '" + startdate + "' AND '" + enddate + "' ;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;


        }
        public static DataTable getswtable(string variant)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.sw where swprogram = '" + variant + "' ORDER by swname;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }
        /// karem
        public static DataTable getSwversion()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.sw";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }

        /// karen



        public static DataTable getdbfullsw()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.sw ORDER by swname;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }
        public static DataTable getdbhwtable(string variant)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.hw where variant= '" + variant + "' ORDER by hwname;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }
        public static DataTable getdbfullhw()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.hw ORDER by hwname;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }

        public static DataTable getdbFeatures()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.features order by featurename;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }
        public static DataTable getdbTCs()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT * FROM testmanagerdev.testcases ORDER BY tcname ;";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;

        }
        public static DataTable getdbTCsonrequest()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT testmanagerdev.requesttc.requestid, tcname, requestenddate ," +
                "testmanagerdev.testcases.tcfeature,testmanagerdev.requesttc.tciterations, " +
                "testmanagerdev.requesttc.requesttimeout, testmanagerdev.requesttc.requeststoponfail, " +
                "testmanagerdev.requesttc.requestprioritry, testmanagerdev.requesttc.singleexecutedin, " +
                "testmanagerdev.requesttc.requeststatus, requestresults, resultspaths,requeststartdate," +
                "testmanagerdev.requests.requestowner, testmanagerdev.requesttc.analyzedby, " +
                "testmanagerdev.requesttc.iterationsdone, testmanagerdev.requesttc.id, testmanagerdev.requesttc.reveiwed " +
                "FROM testmanagerdev.requesttc " +
                "JOIN testmanagerdev.testcases ON testmanagerdev.testcases.tcid = testmanagerdev.requesttc.tcid " +
                "JOIN testmanagerdev.requests ON testmanagerdev.requests.requestid = testmanagerdev.requesttc.requestid " +
                "where testmanagerdev.requesttc.requestid = " + PublicVariables.selectedexecution + ";";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return buffertable;
        }
        public static long getdbonlyTCid(string tcname)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            long tcid = 0;
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT tcid, tcname FROM testmanagerdev.testcases where tcname = '" + tcname + "';";
                buffertable = DBconnection.postgresselectquedy(quedy);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            foreach (DataRow item in buffertable.Rows)
            {
                tcid = Int64.Parse(item["tcid"].ToString());
            }
            return tcid;
        }

        public static DataTable getdbowners()
        {
            {
                DataTable buffertable = new DataTable();
                string quedy = "";
                string col1 = "";
                if (PublicVariables.postgressis_on == true)
                {
                    quedy = "SELECT * FROM testmanagerdev.owners;";
                    buffertable = DBconnection.postgresselectquedy(quedy);

                }
                else
                {
                    quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                    buffertable = DBconnection.accessselectquedry(quedy);

                }
                return buffertable;

            }
        }
        public static void insertnoresponse(string query)
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            string col1 = "";
            if (PublicVariables.postgressis_on == true)
            {

                DBconnection.postgresinsertnoresponse(query);

            }
            else
            {
                quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                buffertable = DBconnection.accessselectquedry(quedy);

            }
            return;
        }

        public static long insertdbrequestwithresponse(string query, bool getid)
        {
            DataTable buffertable = new DataTable();

            long id = 0;
            if (PublicVariables.postgressis_on == true)
            {

                buffertable = DBconnection.postgressinsertwithresponse(query);

            }
            else
            {

                buffertable = DBconnection.accessselectquedry(query);

            }
            if (getid)
            {
                foreach (DataRow item in buffertable.Rows)
                {

                    id = Int64.Parse(item["requestid"].ToString());

                }
            }


            return id;

        }
        public static void updatetopostgress(string quedy, bool createdquedy)
        {
            if (createdquedy)
            {
                DBconnection.updatepostgress(quedy);
            }
            else
            {
                //underdev
            }


        }

        public static DataTable getracksdbfull()
        {
            {
                DataTable buffertable = new DataTable();
                string quedy = "";
                string col1 = "";
                if (PublicVariables.postgressis_on == true)
                {
                    quedy = "SELECT * FROM testmanagerdev.racks;";
                    buffertable = DBconnection.postgresselectquedy(quedy);

                }
                else
                {
                    quedy = "SELECT Sprints.ID, Sprints.Sprint_Name FROM Sprints_Dev;";
                    buffertable = DBconnection.accessselectquedry(quedy);

                }
                return buffertable;

            }
        }

        public static DataTable getdbmissingtovalidate()
        {
            DataTable buffertable = new DataTable();
            string quedy = "";
            if (PublicVariables.postgressis_on == true)
            {
                quedy = "SELECT testmanagerdev.requesttc.requestid, tcname, requestenddate , " +
                    "testmanagerdev.testcases.tcfeature,testmanagerdev.requesttc.tciterations,testmanagerdev.sw.swprogram,testmanagerdev.sw.swname, " +
                    "testmanagerdev.requesttc.requesttimeout, testmanagerdev.requesttc.requeststoponfail, " +
                    "testmanagerdev.requesttc.requestprioritry,requestsprint, testmanagerdev.requesttc.singleexecutedin, " +
                    "testmanagerdev.requesttc.requeststatus, requestresults, resultspaths,requeststartdate, " +
                    "testmanagerdev.requests.requestowner, testmanagerdev.requesttc.analyzedby, " +
                    "testmanagerdev.requesttc.iterationsdone, testmanagerdev.requesttc.id, testmanagerdev.requesttc.reveiwed, requeststaskid " +
                    "FROM testmanagerdev.requests " +
                    "JOIN testmanagerdev.requesttc ON testmanagerdev.requests.requestid = testmanagerdev.requesttc.requestid " +
                    "JOIN testmanagerdev.testcases ON testmanagerdev.testcases.tcid = testmanagerdev.requesttc.tcid " +
                    "JOIN testmanagerdev.sw ON testmanagerdev.sw.swid = testmanagerdev.requests.requestsw " +
                    "where reveiwed = false";
                buffertable = DBconnection.postgresselectquedy(quedy);
            }
            else
            {
                quedy = "SELECT Racks.Rack_Name FROM Racks;";
                buffertable = DBconnection.accessselectquedry(quedy);
            }
            return buffertable;
        }
    }
}
