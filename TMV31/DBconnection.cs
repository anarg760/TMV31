using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;
using System.Data;
using Npgsql;

namespace TMV31
{
    public static class DBconnection
    {
        public static OleDbConnection con1;

        public static void initaccesconnect()
        {

            try
            {
                PublicVariables.accesscon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Ipicazo\\Documents\\DBforTM1_dev1.accdb");
                PublicVariables.accesscon.Open();
                PublicVariables.accesscon.Close();
                PublicVariables.accesis_on = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Acces connection coulndt be completed ");
                MessageBox.Show(ex.Message);

                throw;
            }

        }
        public static void initpostgresscon()
        {
            string conectionstring = "";
            conectionstring = "Host=MXVTCQFD06;Username=provetech;Password=Vis12345;Database=stress";
            try
            {
                PublicVariables.postgresscon = new NpgsqlConnection(conectionstring);
                PublicVariables.postgresscon.Open();
                PublicVariables.postgresscon.Close();
                PublicVariables.postgressis_on = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("DB connection  lost ");
                MessageBox.Show(ex.Message);

            }
        }
        public static void postgresinsertnoresponse(string query)
        {
            try
            {
                PublicVariables.postgresscon.Open();
                NpgsqlCommand postcom = new NpgsqlCommand(query, PublicVariables.postgresscon);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(postcom);
                PublicVariables.postgresscon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB connection  lost ");
                MessageBox.Show(ex.Message);

            }
        }
        public static void updatepostgress(string quedy)
        {
            try
            {
                PublicVariables.postgresscon.Open();
                NpgsqlCommand postcom = new NpgsqlCommand(quedy, PublicVariables.postgresscon);
                postcom.ExecuteNonQuery();
                PublicVariables.postgresscon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("DB connection  lost ");
                MessageBox.Show(ex.Message);

            }
        }
        public static DataTable postgresselectquedy(String query)
        {
            DataTable querytable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                PublicVariables.postgresscon.Open();
                NpgsqlCommand postcom = new NpgsqlCommand(query, PublicVariables.postgresscon);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(postcom);
                adapter.Fill(ds);
                querytable = ds.Tables[0];
                PublicVariables.postgresscon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("DB connection  lost ");
                MessageBox.Show(ex.Message);
                return querytable;
            }
            return querytable;
        }


        public static DataTable accessselectquedry(String query)
        {
            DataTable querytable = new DataTable();
            try
            {
                PublicVariables.accesscon.Open();

                DataSet ds = new DataSet();
                OleDbCommand com = new OleDbCommand(query, PublicVariables.accesscon);
                OleDbDataAdapter adapter = new OleDbDataAdapter(com);
                adapter.Fill(ds);
                querytable = ds.Tables[0];
                PublicVariables.accesscon.Close();

            }
            catch (Exception)
            {


            }
            return querytable;

        }

        public static bool accessinsertquery(String query)
        {
            bool isexecuted = false;
            try
            {
                PublicVariables.accesscon.Open();
                OleDbCommand command = new OleDbCommand(query, PublicVariables.accesscon);
                command.ExecuteNonQuery();
                PublicVariables.accesscon.Close();
                isexecuted = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB connection  lost ");
                MessageBox.Show(ex.Message);
                isexecuted = false;
            }
            return isexecuted;

        }

        public static DataTable postgressinsertwithresponse(string query)
        {
            DataTable querytable = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                PublicVariables.postgresscon.Open();
                NpgsqlCommand postcom = new NpgsqlCommand(query, PublicVariables.postgresscon);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(postcom);

                adapter.Fill(ds);
                querytable = ds.Tables[0];
                PublicVariables.postgresscon.Close();

            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            return querytable;

        }
    }
}
