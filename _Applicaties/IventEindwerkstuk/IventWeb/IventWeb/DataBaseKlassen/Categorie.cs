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
    public class Categorie
    {
        public String Naam { get; set; }
        public int BijdrageID { get; set; }
        public List<Categorie> categorieen { get; set; }

        public Categorie()
        {
            categorieen = new List<Categorie>();
        }

        public Categorie(int id, string naam)
        {
            this.BijdrageID = id;
            this.Naam = naam;
            categorieen = new List<Categorie>();
        }

        public Categorie(string naam)
        {
            this.Naam = naam;
            categorieen = new List<Categorie>();
        }

        public void getCategorie()
        {
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
                com.CommandText = @"SELECT ""bijdrage_id"", ""naam"" FROM CATEGORIE WHERE ""categorie_id"" IS NULL";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Categorie c;
                    while (reader.Read())
                    {
                        c = new Categorie(reader.GetInt32(0), reader.GetString(1));
                        categorieen.Add(c);
                    }
                }
                catch (NullReferenceException)
                {

                }

            }
        }

        public void getSUBCategorie()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["categorie"];
            // int subcategorie = Convert.ToInt32(System.Web.HttpContext.Current.Session["smd"]);
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
                com.CommandText = @"SELECT ""bijdrage_id"", ""naam"" FROM CATEGORIE WHERE ""categorie_id"" = " + bijdrageID + "";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Categorie c;
                    while (reader.Read())
                    {
                        c = new Categorie(reader.GetInt32(0), reader.GetString(1));
                        categorieen.Add(c);
                    }
                }
                catch (NullReferenceException)
                {

                }

            }
        }

        public void InsertMap(string query)
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
                    cmd.CommandText = @"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES(1,SYSDATE,'categorie')";
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
                    cmd.CommandText = @"INSERT INTO CATEGORIE(""bijdrage_id"", ""categorie_id"", ""naam"") VALUES(" + maxid + " ," + bijdrageID + ",'" + query + "')";
                    cmd.ExecuteNonQuery();
                    otn2.Commit();

                }
                catch (NullReferenceException)
                {

                }
            }
        }
    }
}