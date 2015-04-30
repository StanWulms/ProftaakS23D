using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Ivent
{
    class Database
    {
        private OracleConnection conn;

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
            catch { return false; }
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
        public List<Bericht> GetDataBericht(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Bericht> berichten = new List<Bericht>();
            while (dr.Read())
            {
                Bericht b = new Bericht(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetString(3));
                berichten.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return berichten;
        }
        public List<Bezoeker> GetDataBezoeker(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Bezoeker> bezoeker = new List<Bezoeker>();
            while (dr.Read())
            {
                Bezoeker b = new Bezoeker(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetInt32(7));
                bezoeker.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return bezoeker;
        }
        public List<Bungalow> GetDataBungalow(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Bungalow> bungalows = new List<Bungalow>();
            while (dr.Read())
            {
                Bungalow b = new Bungalow(dr.GetInt32(0), dr.GetDouble(1));
                bungalows.Add(b);
            }
            dr.Close();
            cmd.Dispose();
            return bungalows;
        }
        public List<Caravan> GetDataCaravan(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Caravan> caravans = new List<Caravan>();
            while (dr.Read())
            {
                Caravan c = new Caravan(dr.GetInt32(0), dr.GetDouble(1));
                caravans.Add(c);
            }
            dr.Close();
            cmd.Dispose();
            return caravans;
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
                Event e = new Event(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetDateTime(3), dr.GetString(4), dr.GetInt32(5), dr.GetString(6), dr.GetString(7), dr.GetInt32(8), dr.GetString(9));
                events.Add(e);
            }
            dr.Close();
            cmd.Dispose();
            return events;
        }
        public List<Eventbezoeker> GetDataEventbezoeker(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Eventbezoeker> eventbezoekers = new List<Eventbezoeker>();
            while (dr.Read())
            {
                Eventbezoeker e = new Eventbezoeker(dr.GetInt32(0), dr.GetInt32(1));
                eventbezoekers.Add(e);
            }
            dr.Close();
            cmd.Dispose();
            return eventbezoekers;
        }
        public List<Exemplaar> GetDataExemplaar(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Exemplaar> exemplaren = new List<Exemplaar>();
            while (dr.Read())
            {
                Exemplaar e = new Exemplaar(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2));
                exemplaren.Add(e);
            }
            dr.Close();
            cmd.Dispose();
            return exemplaren;
        }
        public List<Gebruiker> GetDataGebruiker(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            while (dr.Read())
            {
                Gebruiker g = new Gebruiker(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3));
                gebruikers.Add(g);
            }
            dr.Close();
            cmd.Dispose();
            return gebruikers;
        }
        public List<Huuropdracht> GetDataHuuropdracht(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Huuropdracht> huuropdrachten = new List<Huuropdracht>();
            while (dr.Read())
            {
                Huuropdracht h = new Huuropdracht(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetDateTime(3), dr.GetDateTime(4), dr.GetDateTime(5));
                huuropdrachten.Add(h);
            }
            dr.Close();
            cmd.Dispose();
            return huuropdrachten;
        }
        public List<Huurtent> GetDataHuurtent(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Huurtent> huurtenten = new List<Huurtent>();
            while (dr.Read())
            {
                Huurtent h = new Huurtent(dr.GetInt32(0), dr.GetDouble(1));
                huurtenten.Add(h);
            }
            dr.Close();
            cmd.Dispose();
            return huurtenten;
        }
        public List<Map> GetDataMap(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Map> mappen = new List<Map>();
            while (dr.Read())
            {
                Map m = new Map(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetString(3));
                mappen.Add(m);
            }
            dr.Close();
            cmd.Dispose();
            return mappen;
        }
        public List<Plaats> GetDataPlaats(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Plaats> plaatsen = new List<Plaats>();
            while (dr.Read())
            {
                Plaats p = new Plaats(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2));
                plaatsen.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return plaatsen;
        }
        public List<Post> GetDataPost(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Post> posts = new List<Post>();
            while (dr.Read())
            {
                Post p = new Post(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                posts.Add(p);
            }
            dr.Close();
            cmd.Dispose();
            return posts;
        }
        public List<Rating> GetDataRating(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Rating> ratings = new List<Rating>();
            while (dr.Read())
            {
                Rating r = new Rating(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2));
                ratings.Add(r);
            }
            dr.Close();
            cmd.Dispose();
            return ratings;
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
                Reservering r = new Reservering(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetDateTime(3), dr.GetDateTime(4), dr.GetInt32(5));
                reserveringen.Add(r);
            }
            dr.Close();
            cmd.Dispose();
            return reserveringen;
        }
        public List<Reserveringsopdracht> GetDataReserveringsopdracht(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Reserveringsopdracht> reserveringsopdrachten = new List<Reserveringsopdracht>();
            while (dr.Read())
            {
                Reserveringsopdracht r = new Reserveringsopdracht(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetInt32(3));
                reserveringsopdrachten.Add(r);
            }
            dr.Close();
            cmd.Dispose();
            return reserveringsopdrachten;
        }
        public List<Staplaats> GetDataStaplaats(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Staplaats> staplaatsen = new List<Staplaats>();
            while (dr.Read())
            {
                Staplaats s = new Staplaats(dr.GetInt32(0), dr.GetDouble(1), dr.GetInt32(2));
                staplaatsen.Add(s);
            }
            dr.Close();
            cmd.Dispose();
            return staplaatsen;
        }
        public List<Voorwerp> GetDataVoorwerp(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Voorwerp> voorwerpen = new List<Voorwerp>();
            while (dr.Read())
            {
                Voorwerp v = new Voorwerp(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetDouble(3));
                voorwerpen.Add(v);
            }
            dr.Close();
            cmd.Dispose();
            return voorwerpen;
        }
        public List<Vriend> GetDataVriend(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Vriend> vrienden = new List<Vriend>();
            while (dr.Read())
            {
                Vriend v = new Vriend(dr.GetInt32(0), dr.GetInt32(1));
                vrienden.Add(v);
            }
            dr.Close();
            cmd.Dispose();
            return vrienden;
        }
        public List<Woonplaats> GetDataWoonplaats(string query)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dr = cmd.ExecuteReader();
            List<Woonplaats> woonplaatsen = new List<Woonplaats>();
            while (dr.Read())
            {
                Woonplaats w = new Woonplaats(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetInt32(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6), dr.GetString(7));
                woonplaatsen.Add(w);
            }
            dr.Close();
            cmd.Dispose();
            return woonplaatsen;
        }
    }
}
