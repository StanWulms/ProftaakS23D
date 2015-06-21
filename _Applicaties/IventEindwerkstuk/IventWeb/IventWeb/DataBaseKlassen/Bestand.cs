using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;

namespace IventWeb
{
    public class Bestand
    {
        public int BijdrageID { get; set; }
        public int CategorieID { get; set; }
        public String BestandLocatie { get; set; }
        public int Grootte { get; set; }
        public List<Bestand> bestanden { get; set; }

        public Bestand()
        {
            bestanden = new List<Bestand>();
        }

        public Bestand(int bijdrageid, int categorieid, string bestandlocatie, int grootte)
        {
            this.BijdrageID = bijdrageid;
            this.CategorieID = categorieid;
            this.BestandLocatie = bestandlocatie;
            this.Grootte = grootte;
            bestanden = new List<Bestand>();
        }

        public void GetBestand()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["categorie"];
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                com.CommandText = @"SELECT ""bijdrage_id"", ""categorie_id"", ""bestandslocatie"", ""grootte"" FROM BESTAND WHERE ""categorie_id"" =" + bijdrageID;
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Bestand b;
                    while (reader.Read())
                    {
                        b = new Bestand(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                        bestanden.Add(b);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }
        //Bestand toevoegen
        public void InsertBestand(string locatie, int grootte)
        {
            int maxid = 0;
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["categorie"];
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                OracleCommand cmd = (OracleCommand)con.CreateCommand();
                try
                {
                    OracleTransaction otn = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = @"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES(1,SYSDATE,'bestand')";
                    cmd.ExecuteNonQuery();
                    otn.Commit();

                    com.CommandText = "SELECT id FROM BIJDRAGE WHERE id = (SELECT MAX(id) FROM BIJDRAGE)";
                    DbDataReader reader = com.ExecuteReader();
                    try
                    {
                        reader.Read();
                        maxid = reader.GetInt32(0);
                    }
                    catch (NullReferenceException)
                    {

                    }

                    OracleTransaction otn2 = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = @"INSERT INTO BESTAND(""bijdrage_id"", ""categorie_id"", ""bestandslocatie"", ""grootte"") VALUES(" + maxid + " ," + bijdrageID + ",'" + locatie + "'," + grootte + ")";
                    cmd.ExecuteNonQuery();
                    otn2.Commit();

                }
                catch (NullReferenceException)
                {

                }
            }
        }

        public String GetBestandInhoud()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bestand"];
            string source = "";

            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                com.CommandText = @"SELECT ""bestandslocatie"" FROM BESTAND WHERE ""bijdrage_id"" =" + bijdrageID;
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    reader.Read();
                    source = reader.GetString(0);
                }
                catch (NullReferenceException)
                {

                }
                return source;
            }
        }

        public override string ToString()
        {
            return "ID: " + BijdrageID + " - Naam: " + BestandLocatie.Substring(7, BestandLocatie.Length - 11);
        }
    }
}