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
            DbConnection con = OracleClientFactory.Instance.CreateConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Hiermee wordt de connectie met de database gemaakt.
        /// </summary>
        /// <returns></returns>
        public OracleConnection Connect()
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            return con;
        }

        //GetMax... retourneert het hoogst gevonden id in de database van een bepaalde tabel
        /// <summary>
        /// Het opvragen van het maximale Reserverings ID.
        /// </summary>
        /// <returns>Het maximaal gevonden reserveringsID.</returns>
        public int GetMaxReservering()
        {
            int id = 0;

                conn = Connect();
                conn.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = conn;
                com.CommandText = "SELECT id FROM reservering WHERE id = (SELECT MAX(id) FROM reservering)";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    reader.Read();
                    id = reader.GetInt32(0);
                    reader.Close();
                    return id;
                }
                catch (NullReferenceException)
                {
                    return 0;
                }
        }

        /// <summary>
        /// Het opvragen van het maximale account ID.
        /// </summary>
        /// <returns>Het maximaal gevonden Account ID.</returns>
        public int GetMaxAccount()
        {
            int id = 0;
            conn = Connect();
            conn.Open();
            DbCommand com = OracleClientFactory.Instance.CreateCommand();
            if (com == null)
            {
                //return "Error! No Command";
            }
            com.Connection = conn;
            com.CommandText = "SELECT id FROM account WHERE id = (SELECT MAX(id) FROM account)";
            DbDataReader reader = com.ExecuteReader();
            try
            {
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                return id;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }


        //GetAll... retourneert een lijst met alle objecten die in de
        //database te vinden zijn.
        public List<Hoofdboeker> GetAllHoofdboekers()
        {
            conn = Connect();
            conn.Open();
            DbCommand com = OracleClientFactory.Instance.CreateCommand();
            if (com == null)
            {
                //return "Error! No Command";
            }
            com.Connection = conn;
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

        /// <summary>
        /// Het ophalen van alle kampeerplaatsen
        /// </summary>
        /// <returns>Een lijst met alle kampeerplaatsen wordt gereturned</returns>
        public List<Kampeerplaats> GetAllKampeerplaatsen()
        {
            conn = Connect();
            conn.Open();
            DbCommand com = OracleClientFactory.Instance.CreateCommand();
            if (com == null)
            {
                //return "Error! No Command";
            }
            com.Connection = conn;
            //Alle gereserveerde plekken.
            com.CommandText = @"SELECT id, ""nummer"", ""capaciteit"" FROM plek WHERE id IN (SELECT ""plek_id"" FROM plek_reservering)";
            DbDataReader reader = com.ExecuteReader();
            List<Kampeerplaats> plekken = new List<Kampeerplaats>();
            try
            {
                while (reader.Read())
                {
                    Kampeerplaats p = new Kampeerplaats(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), true);
                    plekken.Add(p);
                }
                //Alle niet gereserveerde plekken.
                com.CommandText = @"SELECT id, ""nummer"", ""capaciteit"" FROM plek WHERE id NOT IN (SELECT ""plek_id"" FROM plek_reservering)";
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Kampeerplaats p = new Kampeerplaats(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), false);
                    plekken.Add(p);
                }
                return plekken;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Het zoeken naar de laatst geinserte hoofdboeker
        /// </summary>
        /// <param name="h"></param>
        /// <returns>De laatst geinserte hoofdboeker wordt gereturned</returns>
        public Hoofdboeker ZoekLaatstGeInsertBoeker(Hoofdboeker h)
        {
           conn = Connect();
           conn.Open();
           DbCommand com = OracleClientFactory.Instance.CreateCommand();
           if (com == null)
           {
               //return "Error! No Command";
           }
           com.Connection = conn;
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


        /// <summary>
        /// Voor het aanmaken van de accounts
        /// </summary>
        /// <param name="hoofdboeker"></param>
        /// <returns>Er wordt een string gereturned met tekst of het gelukt is of niet</returns>
        public string maakpersoon(Hoofdboeker hoofdboeker)
        {
            try
            {
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
        
        /// <summary>
        /// Het maken van een nieuw account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Er wordt een string gereturned met tekst of het gelukt is of niet</returns>
        public string accountmaken(Account account)
        {
            try
            {
                conn = Connect();
                conn.Open();
                OracleCommand ora_cmd = new OracleCommand("subinschrijving", conn);
                ora_cmd.BindByName = true;
                ora_cmd.CommandType = CommandType.StoredProcedure;

                ora_cmd.Parameters.Add("email", OracleDbType.Varchar2, account.Email, ParameterDirection.Input);
                ora_cmd.Parameters.Add("gebruiker", OracleDbType.Varchar2, account.Gebruikersnaam, ParameterDirection.Input);
                ora_cmd.Parameters.Add("actievatiehash", OracleDbType.Varchar2, account.Activatiehash, ParameterDirection.Input);
                ora_cmd.Parameters.Add("text", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output;

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

        /// <summary>
        /// Er wordt gecontroleerd of gebruikersnaam en email voldoen aan formaat.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Er wordt een string gereturned met tekst of het gelukt is of niet.</returns>
        public string checknaamemail(Account account)
        {
            try
            {
                conn = Connect();
                conn.Open();
                OracleCommand ora_cm = new OracleCommand("checkbeschikbaar", conn);
                ora_cm.BindByName = true;
                ora_cm.CommandType = CommandType.StoredProcedure;


                ora_cm.Parameters.Add("gebruikersnaam", OracleDbType.Varchar2, account.Gebruikersnaam, ParameterDirection.Input);
                ora_cm.Parameters.Add("email", OracleDbType.Varchar2, account.Email, ParameterDirection.Input);
                ora_cm.Parameters.Add("text", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output;

                ora_cm.ExecuteNonQuery();
                return ora_cm.Parameters["text"].Value.ToString();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
            }
            return "foutmelding";
        }


        /// <summary>
        /// Het inserten van de tabellen horend bij de reservatie.
        /// o.a. de nieuwe reservering en de koppeling met de plaats, hoofdboeker en accounts
        /// </summary>
        /// <param name="boeking"></param>
        /// <param name="hoofdboeker"></param>
        public void InsertReservering(Boeking boeking, Hoofdboeker hoofdboeker)
        {
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

        /// <summary>
        /// Inserten van een nieuwe plek_reservering d.m.v. een boeking en kampeerplaats.
        /// </summary>
        /// <param name="boeking"></param>
        /// <param name="kampeerplaats"></param>
        public void InsertPlekReservering(Boeking boeking, Kampeerplaats kampeerplaats)
        {
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
                cmd.Parameters.Add("plek_id", kampeerplaats.ID);
                cmd.Parameters.Add("reservering_id", boeking.ID);
                OracleTransaction otn = (OracleTransaction)conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandText = @"INSERT INTO PLEK_RESERVERING (""plek_id"", ""reservering_id"") VALUES (:1,:2)";
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (NullReferenceException)
            {

            }
        }

        /// <summary>
        /// Inserten van een nieuw Polsbandje.
        /// </summary>
        /// <returns></returns>
        public string maakpolsbandje()
        {
            try
            {
               conn = Connect();
               conn.Open();               
               OracleCommand ora_cm = new OracleCommand("polsbandjes", conn );
               ora_cm.BindByName = true;
               ora_cm.CommandType = CommandType.StoredProcedure;                            
               ora_cm.Parameters.Add("nieuwid", OracleDbType.Int32, 32767).Direction = ParameterDirection.Output;
               ora_cm.ExecuteNonQuery();                 
               return ora_cm.Parameters["nieuwid"].Value.ToString();         //het nieuwe polsbandjeid    
           }
           catch (Exception ex)
           {
               System.Console.WriteLine("Exception: {0}", ex.ToString());
           }  
           return null;
        }

        /// <summary>
        /// Inserten van een nieuwe row in Reservering_Polsbandje
        /// </summary>
        /// <param name="reserveringid"></param>
        /// <param name="account"></param>
        /// <param name="polsbandjeid"></param>
        public void reserveer_polsbandje(int reserveringid, Account account, int polsbandjeid)
        {
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
                cmd.Parameters.Add("reservering_id", reserveringid);
                cmd.Parameters.Add("polsbandje_id", polsbandjeid);
                cmd.Parameters.Add("account_id", account.ID);                
                OracleTransaction otn = (OracleTransaction)conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandText = @"INSERT INTO RESERVERING_POLSBANDJE (""reservering_id"", ""polsbandje_id"", ""account_id"", ""aanwezig"") VALUES (:1,:2,:3,0)";
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (NullReferenceException)
            {

            } 
        }
    }
}