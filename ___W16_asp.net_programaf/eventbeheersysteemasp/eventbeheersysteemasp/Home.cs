using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
namespace eventbeheersysteemasp
{
    public class Home
    {
        List<eventmaken> events;
        public Home()
        {

        }
        public void insertlocation(string naam, string straat, string huisnr, string postcode, string plaats)
        {
                using(DbConnection con = OracleClientFactory.Instance.CreateConnection())
                {                    
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
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
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
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
        public List<eventmaken> getevents()
        {
            events = new List<eventmaken>();
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = "SELECT * FROM event";
                DbDataReader reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    DbCommand com = OracleClientFactory.Instance.CreateCommand();
                    com.Connection = con;
                    com.CommandText = @"SELECT * FROM locatie WHERE id = "+ reader.GetInt32(1) + " AND rownum < 2";
                    DbDataReader rd = com.ExecuteReader();
                    rd.Read();
                    if (!rd.IsDBNull(5) || !rd.IsDBNull(4) || !rd.IsDBNull(2) || !rd.IsDBNull(3))
                    {
                        eventmaken nieuwevent = new eventmaken(reader.GetInt32(0), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), rd.GetString(5), rd.GetString(4), rd.GetString(2), rd.GetString(3), reader.GetInt32(5));
                        events.Add(nieuwevent);
                    }
                    else
                    {
                        
                    }
                    
                }
                
                return events;                
            }
        }
    }
}