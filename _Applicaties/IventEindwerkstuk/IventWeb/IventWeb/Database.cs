﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

using System.Data.SqlClient;
//
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace IventWeb
{
    public class Database
    {
        private OracleConnection conn;
        private SqlConnection connection;

        public Database()
        {
          // connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
            DbConnection con = OracleClientFactory.Instance.CreateConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            // ConnectionStringSettings mySettings = ConfigurationManager.ConnectionStrings["DatabaseConnection"];
        }

        /// <summary>
        /// AddData wordt aangeroepen als je data aan de database
        /// toe wil voegen. Er wordt een insertstatement gedaan.
        /// </summary>
        /// <param name="query">String van de instert query die je uit wil voeren</param>
        /// <returns>True als het is gelukt; false als het niet is gelukt</returns>
        public bool AddData(string query)
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
                OracleCommand cmd = (OracleCommand)con.CreateCommand();
                try
                {
                    OracleTransaction otn = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    otn.Commit();
                    return true;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Alle onderstaande (GetData) methodes voeren een select query uit en
        /// returnen de reslulaten in een lijst. Het type van de lijst die
        /// wordt terug gestuurd wordt bepaald aan de hand van in welke tabel 
        /// je gegevens op aan het halen bent.
        /// </summary>
        /// <param name="query">String van de select query die je uit wil voeren</param>
        /// <returns>Lijst met resultaten van je select query</returns>
        public List<Account> GetDataAccount(string query)
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
                com.CommandText = query;
                DbDataReader reader = com.ExecuteReader();
                List<Account> accounts = new List<Account>();
                try
                {
                    while (reader.Read())
                    {
                        Account a = new Account(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
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
        public List<AccountBijdrage> GetDataAccountBijdrage(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<AccountBijdrage> accountbijdragen = new List<AccountBijdrage>();
            while (dr.Read())
            {
                AccountBijdrage a = new AccountBijdrage(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetInt32(3), dr.GetInt32(4));
                accountbijdragen.Add(a);
            }
            dr.Close();
            cmd.Dispose();
            return accountbijdragen;
        }
        public List<Bericht> GetDataBericht(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Bericht> berichten = new List<Bericht>();
            while (dr.Read())
            {
                Bericht b = new Bericht(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                berichten.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return berichten;
        }
        public List<Bestand> GetDataBestand(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Bestand> bestanden = new List<Bestand>();
            while (dr.Read())
            {
                Bestand b = new Bestand(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetInt32(3));
                bestanden.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return bestanden;
        }
        public List<DataBaseKlassen.Bezoeker> getbezoekers(string search)
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
                com.CommandText = @"SELECT a.""gebruikersnaam"", r.""aanwezig""FROM account a, RESERVERING_POLSBANDJE r WHERE a.""ID"" = r.""account_id"" AND a.""gebruikersnaam"" LIKE '%" + search + "%'";
                DbDataReader reader = com.ExecuteReader();
                List<DataBaseKlassen.Bezoeker> bezoekers = new List<DataBaseKlassen.Bezoeker>();
                try
                {
                    while (reader.Read())
                    {
                        DataBaseKlassen.Bezoeker bezoeker = new DataBaseKlassen.Bezoeker(reader.GetString(0), reader.GetInt32(1));
                        bezoekers.Add(bezoeker);
                    }
                    return bezoekers;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public List<Bijdrage> GetDataBijdrage(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Bijdrage> bijdragen = new List<Bijdrage>();
            while (dr.Read())
            {
                Bijdrage b = new Bijdrage(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetString(3));
                bijdragen.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return bijdragen;
        }
        public List<BijdrageBericht> GetDataBijdrageBericht(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<BijdrageBericht> bijdrageberichten = new List<BijdrageBericht>();
            while (dr.Read())
            {
                BijdrageBericht b = new BijdrageBericht(dr.GetInt32(0), dr.GetInt32(1));
                bijdrageberichten.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return bijdrageberichten;
        }
        public List<Categorie> GetDataCategorie(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Categorie> categorieen = new List<Categorie>();
            while (dr.Read())
            {
                Categorie c = new Categorie(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2));
                categorieen.Add(c);
            }
            dr.Close();
            cmd.Dispose();
            return categorieen;
        }
        public List<Event> GetDataEvent(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Event> events = new List<Event>();
            while (dr.Read())
            {
                Event e = new Event(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetDateTime(3), dr.GetDateTime(4), dr.GetInt32(5));
                events.Add(e);
            }
            dr.Close();
            cmd.Dispose();
            return events;
        }
        public List<Locatie> GetDataLocatie(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Locatie> locaties = new List<Locatie>();
            while (dr.Read())
            {
                Locatie l = new Locatie(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                locaties.Add(l);
            }
            dr.Close();
            cmd.Dispose();
            return locaties;
        }
        public List<Persoon> GetDataPersoon(string query)
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
                com.CommandText = query;
                DbDataReader reader = com.ExecuteReader();
                List<Persoon> personen = new List<Persoon>();
                try
                {
                    while (reader.Read())
                    {
                        string toevoeging;
                        try { toevoeging = reader.GetString(2); }
                        catch { toevoeging = ""; }
                        Persoon p = new Persoon(reader.GetInt32(0), reader.GetString(1), toevoeging, reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
                        personen.Add(p);
                    }
                    return personen;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public List<Plek> GetDataPlek(string query)
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
                com.CommandText = query;
                DbDataReader reader = com.ExecuteReader();
                List<Plek> plekken = new List<Plek>();
                try
                {
                    while (reader.Read())
                    {
                        Plek p = new Plek(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
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
        public List<PlekReservering> GetDataPlekReservering(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<PlekReservering> plekreserveringen = new List<PlekReservering>();
            while (dr.Read())
            {
                PlekReservering p = new PlekReservering(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2));
                plekreserveringen.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return plekreserveringen;
        }
        public List<PlekSpecificatie> GetDataPlekSpecificatie(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<PlekSpecificatie> plekspecificaties = new List<PlekSpecificatie>();
            while (dr.Read())
            {
                PlekSpecificatie p = new PlekSpecificatie(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetString(3));
                plekspecificaties.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return plekspecificaties;
        }
        public List<Polsbandje> GetDataPolsbandje(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Polsbandje> polsbandjes = new List<Polsbandje>();
            while (dr.Read())
            {
                Polsbandje p = new Polsbandje(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2));
                polsbandjes.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return polsbandjes;
        }
        public List<Product> GetDataProduct(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Product> producten = new List<Product>();
            while (dr.Read())
            {
                Product p = new Product(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetDouble(5));
                producten.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return producten;
        }
        public List<Productcat> GetDataProductcat(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Productcat> productcatten = new List<Productcat>();
            while (dr.Read())
            {
                Productcat p = new Productcat(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2));
                productcatten.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return productcatten;
        }
        public List<Productexemplaar> GetDataProductexemplaar(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Productexemplaar> productexemplaren = new List<Productexemplaar>();
            while (dr.Read())
            {
                Productexemplaar p = new Productexemplaar(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetString(3));
                productexemplaren.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return productexemplaren;
        }
        public List<Reservering> GetDataReservering(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Reservering> reserveringen = new List<Reservering>();
            while (dr.Read())
            {
                Reservering r = new Reservering(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetDateTime(3), dr.GetInt32(4));
                reserveringen.Add(r);
            }
            dr.Close();
            cmd.Dispose();
            return reserveringen;
        }
        public List<ReserveringPolsbandje> GetDataReserveringPolsbandje(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<ReserveringPolsbandje> reserveringpolsbandjes = new List<ReserveringPolsbandje>();
            while (dr.Read())
            {
                ReserveringPolsbandje r = new ReserveringPolsbandje(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetInt32(3), dr.GetInt32(4));
                reserveringpolsbandjes.Add(r);
            }
            dr.Close();
            cmd.Dispose();
            return reserveringpolsbandjes;
        }
        public List<Specificatie> GetDataSpecificatie(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Specificatie> specificaties = new List<Specificatie>();
            while (dr.Read())
            {
                Specificatie s = new Specificatie(dr.GetInt32(0), dr.GetString(1));
                specificaties.Add(s);
            }
            dr.Close();
            cmd.Dispose();
            return specificaties;
        }
        public List<Verhuur> GetDataVerhuur(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Verhuur> verhuurdingen = new List<Verhuur>();
            while (dr.Read())
            {
                Verhuur v = new Verhuur(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetDateTime(3), dr.GetDateTime(4), dr.GetDouble(5), dr.GetInt32(6));
                verhuurdingen.Add(v);
            }
            dr.Close();
            cmd.Dispose();
            return verhuurdingen;
        }

        /// <summary>
        /// Eerst wordt er uit de databse de gegevens over de bezoeker
        /// gehaald. Hij controleerd of de 'tag' overeen komt met de hash van
        /// de bezoeker. Zo ja, dan gaat wordt zijn account actief (aanwezig = 1)
        /// </summary>
        /// <param name="tag">Barcode</param>
        /// <returns>Update de aanwezigheid van de ingecheckte bezoeker; De naam van de bezoeker wordt gereturnd</returns>
        public string Tagger(string tag)
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
                com.CommandText = @"SELECT a.""gebruikersnaam"", R.""betaald"",RP.""aanwezig"",RP.""polsbandje_id"",RP.""account_id"" FROM PERSOON p, ""ACCOUNT"" a, RESERVERING_POLSBANDJE RP, RESERVERING R, POLSBANDJE Po WHERE a.""ID"" = RP.""account_id"" AND RP.""polsbandje_id"" = po.""ID"" AND RP.""reservering_id"" = R.""ID"" AND R.""persoon_id"" = p.""ID"" AND Po.""barcode"" = " + tag + "";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    reader.Read();
                    if (reader.HasRows == true)
                    {
                        string naam = reader.GetString(0);
                        if (reader.GetInt32(1) == 1)
                        {
                            if (reader.GetInt32(2) == 0)
                            {
                                com.CommandText = @"UPDATE RESERVERING_POLSBANDJE SET ""aanwezig"" = 1 WHERE ""polsbandje_id""=" + reader.GetInt32(3) + @"AND""account_id""=" + reader.GetInt32(4);
                                com.ExecuteNonQuery();
                                return naam;
                            }
                            else
                            {
                                throw new Exception(naam + " is al aanwezig");
                            }
                        }
                        else
                        {
                            throw new Exception(naam + " heeft nog niet betaald");
                        }
                    }
                    else
                    {
                        throw new Exception("tag niet bekend");
                    }
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }  
        }

        /// <summary>
        /// Er wordt en de tabel LOCATION een nieuwe locatie toegevoegd.
        /// Het zijn de plaatsgegevens van waar (nieuwe) events zich plaats kunnen vinden.
        /// Isnertevent heeft ongeveer dezelfde functionaliteit en zorgt er ook voor dat
        /// er een nieuwe evenement wordt aangemaakt.
        /// </summary>
        /// <param name="naam">De naam van de locatie; bijv.'camping reeendal'</param>
        /// <param name="straat">Straatnaam</param>
        /// <param name="huisnr">Huisnummer (met toevoeging)</param>
        /// <param name="postcode">Postcode</param>
        /// <param name="plaats">Plaatsnaam</param>
        public void insertlocation(string naam, string straat, string huisnr, string postcode, string plaats)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"insert into locatie(""naam"",""straat"",""nr"",""postcode"",""plaats"") values('" + naam + "','" + straat + "'," + huisnr + ",'" + postcode + "','" + plaats + "')";
                com.ExecuteNonQuery();
            }
        }
        public void insertevent(string enaam, string lnaam, string begindatum, string einddatum, string maxbezoekers)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT * FROM locatie WHERE ""naam"" like '%" + lnaam + "%' AND rownum < 2";
                DbDataReader reader = comm.ExecuteReader();
                reader.Read();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"insert into event(""locatie_id"",""naam"",""datumstart"",""datumEinde"",""maxBezoekers"") values(" + reader.GetInt32(0) + ",'" + enaam + "','" + begindatum + "','" + einddatum + "'," + maxbezoekers + ")";
                com.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Wordt aan geroepen om alle evenementen mee op te vragen.
        /// Evenementen met bijhorende locatie worden geretouneerd.
        /// </summary>
        /// <returns>Lijst met alle evenementen</returns>
        public List<DataBaseKlassen.EventAanmaken> getevents()
        {
            List<DataBaseKlassen.EventAanmaken> events = new List<DataBaseKlassen.EventAanmaken>();
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = "SELECT * FROM event";
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DbCommand com = OracleClientFactory.Instance.CreateCommand();
                    com.Connection = con;
                    com.CommandText = @"SELECT * FROM locatie WHERE id = " + reader.GetInt32(1) + " AND rownum < 2";
                    DbDataReader rd = com.ExecuteReader();
                    rd.Read();
                    if (!rd.IsDBNull(5) || !rd.IsDBNull(4) || !rd.IsDBNull(2) || !rd.IsDBNull(3))
                    {
                        DataBaseKlassen.EventAanmaken nieuwevent = new DataBaseKlassen.EventAanmaken(reader.GetInt32(0), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), rd.GetString(5), rd.GetString(4), rd.GetString(2), rd.GetString(3), reader.GetInt32(5));
                        events.Add(nieuwevent);
                    }
                }
                return events;
            }
        }
    }
}