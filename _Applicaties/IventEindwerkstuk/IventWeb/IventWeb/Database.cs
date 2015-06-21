using System;
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
        List<Account> accounts;

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
        public List<Event> GetDataEvent(string query)
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
                List<Event> evenementen = new List<Event>();
                try
                {
                    while (reader.Read())
                    {
                        Event e = new Event(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetInt32(5));
                        evenementen.Add(e);
                    }
                    return evenementen;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public List<Locatie> GetDataLocatie(string query)
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
                List<Locatie> locaties = new List<Locatie>();
                try
                {
                    while (reader.Read())
                    {
                        Locatie l = new Locatie(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                        locaties.Add(l);
                    }
                    return locaties;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
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
                List<PlekSpecificatie> plekspecificaties = new List<PlekSpecificatie>();
                try
                {
                    while (reader.Read())
                    {
                        PlekSpecificatie p = new PlekSpecificatie(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3));
                        plekspecificaties.Add(p);
                    }
                    return plekspecificaties;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public List<Polsbandje> GetDataPolsbandje(string query)
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
                List<Polsbandje> polsbandjes = new List<Polsbandje>();
                try
                {
                    while (reader.Read())
                    {
                        Polsbandje p = new Polsbandje(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        polsbandjes.Add(p);
                    }
                    return polsbandjes;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
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
                List<Reservering> reservaties = new List<Reservering>();
                try
                {
                    while (reader.Read())
                    {
                        Reservering r = new Reservering(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt32(4));
                        reservaties.Add(r);
                    }
                    return reservaties;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
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
                                com.CommandText = @"UPDATE RESERVERING_POLSBANDJE SET ""aanwezig"" = 0 WHERE ""polsbandje_id""=" + reader.GetInt32(3) + @"AND""account_id""=" + reader.GetInt32(4);
                                com.ExecuteNonQuery();
                                return "Bezoeker is uitgecheckt";
                            }
                        }
                        else
                        {
                            return "FOUT: bezoeker heeft nog niet betaald";
                        }
                    }
                    else
                    {
                        return "FOUT: tag niet bekend";
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
        public bool insertlocation(string naam, string straat, string huisnr, string postcode, string plaats)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                    con.Open();
                    DbCommand com = OracleClientFactory.Instance.CreateCommand();
                    com.Connection = con;
                    com.CommandText = @"insert into locatie(""naam"",""straat"",""nr"",""postcode"",""plaats"") values('" + naam + "','" + straat + "','" + huisnr + "','" + postcode + "','" + plaats + "')";
                    com.ExecuteNonQuery();
                    return true;
                }
                catch { return false; }
            }
        }
        public bool insertevent(string enaam, string lnaam, string begindatum, string einddatum, string maxbezoekers)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                try
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
                    return true;
                }
                catch { return false; }
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


        /// <summary>
        /// Alle onderstaande klassen worden gebruikt bij het materiaal verhuur.
        /// Er zijn klassen om data op te halen en data te inserten.
        /// </summary>
        public List<String> getproducten()
        {
            //haalt alle producten uit de producten tabel om deze te gebruiken voor het maken van een nieuw exemplaar van een van deze producten
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"SELECT p.""serie"",c.""naam"" FROM PRODUCT p, PRODUCTCAT c WHERE p.""productcat_id"" = c.""ID"" ORDER BY p.id";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    List<String> producten = new List<String>();
                    while (reader.Read())
                    {
                        producten.Add(reader.GetString(1) + "-" + reader.GetString(0));
                    }
                    return producten;
                }
                catch (NullReferenceException)
                {

                }
                return null;
            }
        }
        public List<String> getcategorieproduct()
        {
            //haalt alle categorieen uit de productcat tabel om deze te gebruiken voor het maken van een nieuw product van een van deze categorieen
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"SELECT p.""naam"", c.""naam"" FROM PRODUCTCAT p LEFT OUTER JOIN PRODUCTCAT c ON p.""productcat_id"" = c.""ID"" ORDER BY p.""ID""";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    List<String> categorieen = new List<String>();
                    //dropdownmenu                    
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(1))
                        {
                            categorieen.Add(reader.GetString(0));
                        }
                        else
                        {
                            categorieen.Add(reader.GetString(1) + "-" + reader.GetString(0));
                        }

                    }
                    return categorieen;
                }
                catch (NullReferenceException)
                {

                }
                return null;
            }
        }
        public void insertexemplaar(int productid)
        {
            //maakt een nieuw exemplaar aan met een opgestuurt productid
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT COUNT(*) FROM PRODUCTEXEMPLAAR";
                DbDataReader reader = comm.ExecuteReader();
                reader.Read();
                int volgcode = reader.GetInt32(0) + 1;
                int barcode = 100000 + volgcode;
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"insert INTO PRODUCTEXEMPLAAR(""product_id"",""volgnummer"",""barcode"") VALUES (" + productid + "," + volgcode + "," + barcode + ")";
                com.ExecuteNonQuery();
            }
        }
        public void insertverhuur(DataBaseKlassen.Voorwerp voorwerp, int rpnummer)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"insert INTO VERHUUR (""productexemplaar_id"",""res_pb_id"",""datumIn"",""prijs"",""betaald"") VALUES (" + voorwerp.exemplaarnummer + "," + rpnummer + ",SYSDATE," + voorwerp.prijs + ",1)";
                com.ExecuteNonQuery();
            }
        }
        public void insertproduct(int productid, string merk, string serie, int prijs)
        {
            //maakt een nieuw product aan door alle gegevens van het product mee te gegeven die bij het nieuwe product horen
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT COUNT(*) FROM PRODUCT";
                DbDataReader reader = comm.ExecuteReader();
                reader.Read();
                int type = reader.GetInt32(0) + 1001;
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"INSERT INTO PRODUCT(""productcat_id"",""merk"",""serie"",""typenummer"",""prijs"") VALUES(" + productid + ",'" + merk + "','" + serie + "'," + type + "," + prijs + ")";
                com.ExecuteNonQuery();
            }
        }
        public void updateterugbrengen(int productid, int rpnummer)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"UPDATE VERHUUR SET ""datumUit"" = SYSDATE where ""productexemplaar_id"" =" + productid + @" AND ""res_pb_id""=" + rpnummer + @"AND ""datumUit""is null";
                com.ExecuteNonQuery();
            }
        }
        public List<DataBaseKlassen.Voorwerp> Getvoorwerpen(string query)
        {
            //haalt alle voorwerpen op uit de database en stelt vast of ze al verhuurd zijn of niet.
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = query;
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    List<DataBaseKlassen.Voorwerp> voorwerpjes = new List<DataBaseKlassen.Voorwerp>();
                    //dropdownmenu                    
                    while (reader.Read())
                    {
                        if (voorwerpjes.Count == 0)
                        {

                            if (reader.IsDBNull(2) && !reader.IsDBNull(1))
                            {
                                if (reader.IsDBNull(6))
                                {
                                    DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }
                            }
                            else
                            {
                                if (reader.IsDBNull(6))
                                {
                                    DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                    voorwerpjes.Add(voorwerp);
                                }
                            }
                        }
                        else
                        {
                            int soort = 0;
                            foreach (DataBaseKlassen.Voorwerp voorwerpje in voorwerpjes)
                            {
                                if (voorwerpje.exemplaarnummer == reader.GetInt32(0))
                                {
                                    if (reader.IsDBNull(2) && !reader.IsDBNull(1))
                                    {
                                        voorwerpje.Verhuurd = true;
                                        soort = 1;
                                    }
                                    else
                                    {
                                        soort = 1;
                                    }
                                }
                            }
                            if (soort == 0)
                            {
                                if (reader.IsDBNull(2) && !reader.IsDBNull(1))
                                {
                                    if (reader.IsDBNull(6))
                                    {
                                        DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                        voorwerp.Verhuurd = true;
                                        voorwerpjes.Add(voorwerp);
                                    }
                                    else
                                    {
                                        DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                        voorwerp.Verhuurd = true;
                                        voorwerpjes.Add(voorwerp);
                                    }
                                }
                                else
                                {
                                    if (reader.IsDBNull(6))
                                    {
                                        DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                        voorwerpjes.Add(voorwerp);
                                    }
                                    else
                                    {
                                        DataBaseKlassen.Voorwerp voorwerp = new DataBaseKlassen.Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                        voorwerpjes.Add(voorwerp);
                                    }
                                }
                            }
                        }
                    }
                    return voorwerpjes;
                }
                catch (NullReferenceException)
                {
                }
                return null;
            }
        }


        public void getAccounts()
        {
            accounts = new List<Account>();
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {

                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT * FROM ""ACCOUNT""";
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Account account = new Account(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                    accounts.Add(account);
                }
            }
        }
        public string accountnummer(string barcode)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {

                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT a.""gebruikersnaam"", RP.id FROM PERSOON p, ""ACCOUNT"" a, RESERVERING_POLSBANDJE RP, RESERVERING R, POLSBANDJE Po WHERE a.""ID"" = RP.""account_id"" AND RP.""polsbandje_id"" = po.""ID"" AND RP.""reservering_id"" = R.""ID"" AND R.""persoon_id"" = p.""ID"" AND Po.""barcode"" = '" + barcode + "' AND ROWNUM < 2";
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    string naam = reader.GetInt32(1) + " " + reader.GetString(0);
                    return naam;
                }

            }
            return null;
        }
    }
}