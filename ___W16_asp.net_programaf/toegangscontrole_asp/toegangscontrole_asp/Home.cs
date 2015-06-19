using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace toegangscontrole_asp
{
    public class Home
    {
        private OracleConnection conn;
        List<bezoekers> Bezoekers;
        string pagina = "";
        public Home()
        {
            Bezoekers = new List<bezoekers>();
            conn = new OracleConnection();
            string user = "dbi322740";
            string pw = " wDjvYBVqEV";

            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();


        }
        
        
        public string tagger(string tag)
        {
            pagina = "toegang";
            if (pagina == "toegang")
            {


                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT a.""gebruikersnaam"", R.""betaald"",RP.""aanwezig"",RP.""polsbandje_id"",RP.""account_id"" FROM PERSOON p, ""ACCOUNT"" a, RESERVERING_POLSBANDJE RP, RESERVERING R, POLSBANDJE Po WHERE a.""ID"" = RP.""account_id"" AND RP.""polsbandje_id"" = po.""ID"" AND RP.""reservering_id"" = R.""ID"" AND R.""persoon_id"" = p.""ID"" AND Po.""barcode"" = " + tag + "";
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows == true)
                {
                    
                    string naam = dr.GetString(0);
                    if (dr.GetInt32(1) == 1)
                    {
                        if (dr.GetInt32(2) == 0)
                        {
                            OracleCommand com = conn.CreateCommand();
                            com.CommandType = CommandType.Text;
                            com.CommandText = @"UPDATE RESERVERING_POLSBANDJE SET ""aanwezig"" = 1 WHERE ""polsbandje_id""=" + dr.GetInt32(3) + @"AND""account_id""="+dr.GetInt32(4);
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
            return null;
        }

        public List<bezoekers> getbezoekers(string search)
        {
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT a.""gebruikersnaam"", r.""aanwezig""FROM account a, RESERVERING_POLSBANDJE r WHERE a.""ID"" = r.""account_id"" AND a.""gebruikersnaam"" LIKE '%" + search + "%'";
            OracleDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                bezoekers bezoeker = new bezoekers(dr.GetString(0), dr.GetInt32(1));
                Bezoekers.Add(bezoeker);
            }
            return Bezoekers;
        }
    }
}