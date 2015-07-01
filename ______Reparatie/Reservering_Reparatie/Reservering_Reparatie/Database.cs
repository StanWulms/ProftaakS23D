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

        public OracleConnection Connect()
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            return con;
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
                com.CommandText = "SELECT * FROM PERSOON";
                DbDataReader reader = com.ExecuteReader();
                List<Hoofdboeker> hoofdboekers = new List<Hoofdboeker>();
                try
                {
                    while (reader.Read())
                    {
                        string toevoeging;
                        try { toevoeging = reader.GetString(2); }
                        catch { toevoeging = ""; }
                        Hoofdboeker b = new Hoofdboeker(reader.GetInt32(0), reader.GetString(1), toevoeging, reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
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
                com.CommandText = @"SELECT ""nummer"", ""capaciteit"" FROM plek WHERE id IN (SELECT ""plek_id"" FROM plek_reservering)";
                DbDataReader reader = com.ExecuteReader();
                List<Kampeerplaats> plekken = new List<Kampeerplaats>();
                try
                {
                    while (reader.Read())
                    {
                        Kampeerplaats p = new Kampeerplaats(reader.GetString(0), reader.GetInt32(1), true);
                        plekken.Add(p);
                    }
                    //Alle niet gereserveerde plekken.
                    com.CommandText = @"SELECT ""nummer"", ""capaciteit"" FROM plek WHERE id NOT IN (SELECT ""plek_id"" FROM plek_reservering)";
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Kampeerplaats p = new Kampeerplaats(reader.GetString(0), reader.GetInt32(1), false);
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

        public Hoofdboeker ZoekLaatstGeInsertBoeker(Hoofdboeker h)
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
                com.CommandText = @"SELECT id FROM PERSOON WHERE ""voornaam"" ='" + h.Naam + @"' AND ""tussenvoegsel"" ='" + h.Tussenvoegsel + @"' AND ""achternaam"" ='" + h.Achternaam + @"' AND ""straat"" ='" + h.Straat + @"' AND ""huisnr"" =" + h.Huisnummer + @" AND ""woonplaats"" ='" + h.Woonplaats + @"' AND ""banknr"" ='" + h.Iban + "'";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    Hoofdboeker hb = new Hoofdboeker();
                    while (reader.Read())
                    {
                        hb = new Hoofdboeker(reader.GetInt32(0), h.Naam, h.Tussenvoegsel, h.Achternaam, h.Straat, h.Huisnummer, h.Woonplaats, h.Iban);
                    }
                    return hb;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        public string maakpersoon(Hoofdboeker hoofdboeker)
        {
            try
            {
                /*********************Oracle Command**********************************************************************/
                OracleConnection conn;
                conn = Connect();
                conn.Open();
                OracleCommand ora_cmd = new OracleCommand("hoofdinschrijving", conn);
                ora_cmd.BindByName = true;
                ora_cmd.CommandType = CommandType.StoredProcedure;


                ora_cmd.Parameters.Add("voornaam ", OracleDbType.NVarchar2, hoofdboeker.Naam, ParameterDirection.Input);
                ora_cmd.Parameters.Add("tussenvoegsel ", OracleDbType.NVarchar2, hoofdboeker.Tussenvoegsel, ParameterDirection.Input);
                ora_cmd.Parameters.Add("achternaam ", OracleDbType.NVarchar2, hoofdboeker.Achternaam, ParameterDirection.Input);
                ora_cmd.Parameters.Add("straat ", OracleDbType.NVarchar2, hoofdboeker.Straat, ParameterDirection.Input);
                ora_cmd.Parameters.Add("huisnr ", OracleDbType.NVarchar2, hoofdboeker.Huisnummer, ParameterDirection.Input);
                ora_cmd.Parameters.Add("woonplaats ", OracleDbType.NVarchar2, hoofdboeker.Woonplaats, ParameterDirection.Input);
                ora_cmd.Parameters.Add("banknr ", OracleDbType.NVarchar2, hoofdboeker.Iban, ParameterDirection.Input);
                ora_cmd.Parameters.Add("text", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output;

                /*********************Oracle Command**********************************************************************/

                ora_cmd.ExecuteNonQuery();

                //Now get the values output by the stored procedure    
                return ora_cmd.Parameters["text"].Value.ToString();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
            }
            return "foutmelding";
        }
        public string checknaamemail(Account account)
        {
            try
            {
                OracleConnection conn;
                conn = Connect();
                conn.Open();
                OracleCommand ora_cm = new OracleCommand("checkbeschikbaar", conn);
                ora_cm.BindByName = true;
                ora_cm.CommandType = CommandType.StoredProcedure;


                ora_cm.Parameters.Add("gebruikersnaam", OracleDbType.Varchar2, account.Gebruikersnaam, ParameterDirection.Input);
                ora_cm.Parameters.Add("email", OracleDbType.Varchar2, account.Email, ParameterDirection.Input);
                ora_cm.Parameters.Add("text", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output;

                /*********************Oracle Command**********************************************************************/

                ora_cm.ExecuteNonQuery();
                return ora_cm.Parameters["text"].Value.ToString();
                //Now get the values output by the stored procedure    

                /*********************Oracle Command**********************************************************************/

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
            }
            return "foutmelding";
        }

        public string accountmaken(Account account)
        {
            try
            {
                OracleConnection conn;
                conn = Connect();
                conn.Open();
                OracleCommand ora_cmd = new OracleCommand("subinschrijving", conn);
                ora_cmd.BindByName = true;
                ora_cmd.CommandType = CommandType.StoredProcedure;


                ora_cmd.Parameters.Add("email", OracleDbType.Varchar2, account.Email, ParameterDirection.Input);
                ora_cmd.Parameters.Add("gebruiker", OracleDbType.Varchar2, account.Gebruikersnaam, ParameterDirection.Input);
                ora_cmd.Parameters.Add("actievatiehash", OracleDbType.Varchar2, account.Activatiehash, ParameterDirection.Input);
                ora_cmd.Parameters.Add("text", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output;

                /*********************Oracle Command**********************************************************************/

                ora_cmd.ExecuteNonQuery();

                //Now get the values output by the stored procedure    
                return ora_cmd.Parameters["text"].Value.ToString();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
            }
            return "foutmelding";
        }

        public void InsertReservering(Kampeerplaats kampeerplaats)
        {

        }

        public void InsertPolsbandje()
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                
                OracleConnection conn;
                conn = Connect();
                conn.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = conn;
                OracleCommand cmd = (OracleCommand)con.CreateCommand();
                try
                {
                    /*cmd.Parameters.Add("email", email);
                    cmd.Parameters.Add("titel", titel);
                    cmd.Parameters.Add("voornaam",voornaam);
                    cmd.Parameters.Add("achternaam",achternaam);
                    cmd.Parameters.Add("btwnummer",btwnummer);
                    cmd.Parameters.Add("wachtwoord",wachtwoord);
                    cmd.Parameters.Add("nieuwsbrief",nieuwsbrief);*/
                    OracleTransaction otn = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = "INSERT INTO POLSBANDJE (actief) VALUES (0)";
                    cmd.ExecuteNonQuery();
                    otn.Commit();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        public void InsertReservering(Boeking boeking, Hoofdboeker hoofdboeker)
        {
                OracleConnection conn;
                conn = Connect();
                conn.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = conn;
                OracleCommand cmd = (OracleCommand)conn.CreateCommand();
                try
                {
                    cmd.Parameters.Add("persoon_id", hoofdboeker.ID);
                    cmd.Parameters.Add("datumStart", boeking.BeginDatum);
                    cmd.Parameters.Add("datumEinde", boeking.EindDatum);
                    OracleTransaction otn = (OracleTransaction)conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = @"INSERT INTO RESERVERING (""persoon_id"", ""datumStart"", ""datumEinde"", ""betaald"") VALUES (:1,:2,:3,0)";
                    cmd.ExecuteNonQuery();
                    otn.Commit();
                }
                catch (NullReferenceException)
                {

                }
            
        }
    }
}