using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;

namespace Reservering_Reparatie
{
    public class Database
    {
        //Fields
        private OracleConnection conn;
        List<Account> accounts;

        public Database()
        {
            // connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
            DbConnection con = OracleClientFactory.Instance.CreateConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            // ConnectionStringSettings mySettings = ConfigurationManager.ConnectionStrings["DatabaseConnection"];
        }

        //GetAll... retourneert een lijst met alle objecten die in de
        //database te vinden zijn.
        public List<Account> GetAllAccounts()
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
                com.CommandText = "SELECT * FROM account;";
                DbDataReader reader = com.ExecuteReader();
                List<Account> accounts = new List<Account>();
                try
                {
                    while (reader.Read())
                    {
                        Account a = new Account(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                        accounts.Add(a);
                    }
                    return accounts;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public List<Hoofdboeker> GetAllHoofdboekers()
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
                com.CommandText = "SELECT * PERSOON";
                DbDataReader reader = com.ExecuteReader();
                List<Hoofdboeker> hoofdboekers = new List<Hoofdboeker>();
                try
                {
                    while (reader.Read())
                    {
                        string toevoeging;
                        try { toevoeging = reader.GetString(1); }
                        catch { toevoeging = ""; }
                        Hoofdboeker b = new Hoofdboeker(reader.GetString(0), toevoeging, reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                        hoofdboekers.Add(b);
                    }
                    return hoofdboekers;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public List<Kampeerplaats> GetAllKampeerplaatsen()
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
                //Alle gereserveerde plekken.
                com.CommandText = @"SELECT * FROM plek WHERE id IN (SELECT ""plek_id"" FROM plek_reservering)";
                DbDataReader reader = com.ExecuteReader();
                List<Kampeerplaats> plekken = new List<Kampeerplaats>();
                try
                {
                    while (reader.Read())
                    {
                        Kampeerplaats p = new Kampeerplaats(reader.GetInt32(2), reader.GetInt32(3), true);
                        plekken.Add(p);
                    }
                    //Alle niet gereserveerde plekken.
                    com.CommandText = @"SELECT * FROM plek WHERE id NOT IN (SELECT ""plek_id"" FROM plek_reservering)";
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Kampeerplaats p = new Kampeerplaats(reader.GetInt32(2), reader.GetInt32(3), false);
                        plekken.Add(p);
                    }                   
                    return plekken;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

    }
}