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
    public class Bericht
    {
        public int BijdrageID { get; set; }
        public String Titel { get; set; }
        public String Inhoud { get; set; }
        public List<Bericht> Berichten { get; set; }
        public List<Bericht> Reacties { get; set; }
        public List<Bericht> Inhouden { get; set; }

        public Bericht()
        {
            Berichten = new List<Bericht>();
            Reacties = new List<Bericht>();
            Inhouden = new List<Bericht>();

        }

        public Bericht(string inhoud)
        {
            this.Inhoud = inhoud;
            Reacties = new List<Bericht>();
            Inhouden = new List<Bericht>();
        }

        public Bericht(int bijdrageID, string titel, string inhoud)
        {
            this.BijdrageID = bijdrageID;
            this.Titel = titel;
            this.Inhoud = inhoud;
            Berichten = new List<Bericht>();
            Reacties = new List<Bericht>();
            Inhouden = new List<Bericht>();
        }

        public void GetBericht()
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
                com.CommandText = @"SELECT ""bijdrage_id"", ""titel"", ""inhoud"" FROM BERICHT WHERE ""titel"" IS NOT NULL";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Bericht b;
                    Berichten.Clear();
                    while (reader.Read())
                    {
                        string titel;
                        try
                        {
                            titel = reader.GetString(1);
                        }
                        catch { titel = ""; }

                        b = new Bericht(reader.GetInt32(0), titel, reader.GetString(2));
                        Berichten.Add(b);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }
        public void GetInhoud()
        {
            Berichten.Clear();
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bijdrage"];
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
                com.CommandText = @"SELECT ""inhoud"" FROM BERICHT WHERE ""bijdrage_id"" =" + bijdrageID;
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Bericht b;
                    Berichten.Clear();
                    while (reader.Read())
                    {
                        b = new Bericht(reader.GetString(0));
                        Inhouden.Add(b);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }
        public void GetReacties()
        {
            Berichten.Clear();
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bijdrage"];
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
                com.CommandText = @"SELECT b.""inhoud"" FROM bijdrage bij, bericht b, bijdrage_bericht bb WHERE bij.id = bb.""bijdrage_id"" AND bb.""bericht_id"" = b.""bijdrage_id"" AND bij.id = " + bijdrageID;
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Bericht b;
                    Berichten.Clear();
                    while (reader.Read())
                    {
                        b = new Bericht(reader.GetString(0));
                        Reacties.Add(b);
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        //Nieuw bericht plaatsen.
        public void InsertBericht(string titel, string inhoud)
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bijdrage"];
            if (titel != "")
            {
                int maxid = 0;

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
                        cmd.CommandText = @"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES(1,SYSDATE,'bericht')";
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
                        cmd.CommandText = @"INSERT INTO BERICHT(""bijdrage_id"", ""titel"", ""inhoud"") VALUES(" + maxid + " ,'" + titel + "','" + inhoud + "')";
                        cmd.ExecuteNonQuery();
                        otn2.Commit();

                    }
                    catch (NullReferenceException)
                    {

                    }
                }
            }
            else if (titel == "")
            {
                int maxid = 0;

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
                        cmd.CommandText = @"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES(1,SYSDATE,'bericht')";
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
                        //VERANDEREN
                        OracleTransaction otn2 = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.CommandText = @"INSERT INTO BERICHT(""bijdrage_id"", ""titel"", ""inhoud"") VALUES(" + maxid + " ,'" + titel + "','" + inhoud + "')";
                        cmd.ExecuteNonQuery();
                        otn2.Commit();

                        OracleTransaction otn3 = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.CommandText = @"INSERT INTO BIJDRAGE_BERICHT(""bijdrage_id"", ""bericht_id"") VALUES(" + bijdrageID + " ," + maxid + ")";
                        cmd.ExecuteNonQuery();
                        otn3.Commit();

                    }
                    catch (NullReferenceException)
                    {

                    }
                }
            }
        }

        public override string ToString()
        {
            return BijdrageID + ": " + Titel;
        }
    }
}