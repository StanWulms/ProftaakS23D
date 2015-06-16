using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data.SqlClient;

namespace IventWeb
{
    public class Database
    {
        private OracleConnection conn;
        private SqlConnection connection;

        public Database()
        {
           connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
           // ConnectionStringSettings mySettings = ConfigurationManager.ConnectionStrings["DatabaseConnection"];
        }

        /// <summary>
        /// Wordt aangeroepen om een connectie met de database te creeeren.
        /// </summary>
        /// <param name="username">Gebruikersnaam van de database.</param>
        /// <param name="Password">Wachtwoord van de database.</param>
        /// <param name="connectieString">String om de verbinding te maken.</param>
        /// <returns>True als het is gelukt; false als het niet is gelukt</returns>
        public bool ConnectDatabase(string username, string Password, string connectieString)
        {
            try
            {
                String user = username;
                String pw = Password;
                conn.ConnectionString = connectieString;
                conn.Open(); //opent connectie met de Connectionstring die voor deze connectie is ingesteld.
                return true;
            }
            catch (Exception ex) 
            { global::System.Windows.Forms.MessageBox.Show(ex.Message ," db tier connectie" );
                return false; }
        }

        /// <summary>
        /// AddData wordt aangeroepen als je data aan de database
        /// toe wil voegen. Er wordt een insertstatement gedaan.
        /// </summary>
        /// <param name="query">String van de instert query die je uit wil voeren</param>
        /// <returns>True als het is gelukt; false als het niet is gelukt</returns>
        public bool AddData(string query)
        {
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                otn.Commit();
                return true;
            }
            catch { return false; }
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
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Account> accounts = new List<Account>();
            while (dr.Read())
            {
                Account a = new Account(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetInt32(4));
                accounts.Add(a);
            }
            dr.Close();
            cmd.Dispose();
            return accounts;
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
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Persoon> personen = new List<Persoon>();
            while (dr.Read())
            {
                Persoon p = new Persoon(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7));
                personen.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return personen;
        }
        public List<Plek> GetDataPlek(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Plek> plekken = new List<Plek>();
            while (dr.Read())
            {
                Plek p = new Plek(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetInt32(3));
                plekken.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return plekken;
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
    }
}