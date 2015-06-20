using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace MateriaalVerhuurASP
{
    public class Database
    {
        List<Account> accounts;
        public Database()
        {            
        }
        public List<String> getproducten()
        {
            //haalt alle producten uit de producten tabel om deze te gebruiken voor het maken van een nieuw exemplaar van een van deze producten
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"SELECT p.""serie"",c.""naam"" FROM PRODUCT p, PRODUCTCAT c WHERE p.""productcat_id"" = c.""ID"" ORDER BY p.id" ;
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
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
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
                        if(reader.IsDBNull(1))
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
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
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
                com.CommandText = @"insert INTO PRODUCTEXEMPLAAR(""product_id"",""volgnummer"",""barcode"") VALUES ("+ productid +","+ volgcode + "," + barcode + ")";
                com.ExecuteNonQuery();
            }
        }
        public void insertverhuur(Voorwerp voorwerp, int rpnummer)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
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
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT COUNT(*) FROM PRODUCT";
                DbDataReader reader = comm.ExecuteReader();
                reader.Read();
                int type = reader.GetInt32(0) + 1001;                
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"INSERT INTO PRODUCT(""productcat_id"",""merk"",""serie"",""typenummer"",""prijs"") VALUES("+productid+",'"+merk+"','"+serie+"',"+type+","+prijs+")";
                com.ExecuteNonQuery();
            }
        }
        public void updateterugbrengen(int productid, int rpnummer)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"UPDATE VERHUUR SET ""datumUit"" = SYSDATE where ""productexemplaar_id"" =" + productid + @" AND ""res_pb_id""=" + rpnummer + @"AND ""datumUit""is null";
                com.ExecuteNonQuery();
            }
        }
        public List<Voorwerp> Getvoorwerpen()
        {
            //haalt alle voorwerpen op uit de database en stelt vast of ze al verhuurd zijn of niet.
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"SELECT e.""ID"", v.""datumIn"",v.""datumUit"",p.""merk"",p.""serie"", c.""naam"",t.""naam"",p.""prijs"" FROM PRODUCTEXEMPLAAR e LEFT OUTER JOIN VERHUUR v ON v.""productexemplaar_id"" = e.""ID"" , PRODUCT p, PRODUCTCAT c LEFT OUTER JOIN PRODUCTCAT t ON c.""productcat_id"" = t.""ID"" WHERE p.""productcat_id"" = c.""ID"" AND e.""product_id"" = p.""ID"" ORDER BY e.""ID""";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    List<Voorwerp> voorwerpjes = new List<Voorwerp>();
                    //dropdownmenu                    
                    while (reader.Read())
                    {
                        if (voorwerpjes.Count == 0)
                        {
                            
                            if (reader.IsDBNull(2) && !reader.IsDBNull(1))
                            {
                                if (reader.IsDBNull(6))
                                {
                                    Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }                                
                            }
                            else
                            {
                                if(reader.IsDBNull(6))
                                {
                                    Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                    voorwerpjes.Add(voorwerp);
                                }                                
                            }
                        }
                        else
                        {
                            int soort = 0;
                            foreach (Voorwerp voorwerpje in voorwerpjes)
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
                            if(soort == 0)
                            {
                                if (reader.IsDBNull(2) && !reader.IsDBNull(1))
                                {
                                    if (reader.IsDBNull(6))
                                    {
                                        Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                        voorwerp.Verhuurd = true;
                                        voorwerpjes.Add(voorwerp);
                                    }
                                    else
                                    {
                                        Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
                                        voorwerp.Verhuurd = true;
                                        voorwerpjes.Add(voorwerp);
                                    }
                                }
                                else
                                {
                                    if (reader.IsDBNull(6))
                                    {
                                        Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(5), reader.GetInt32(7));
                                        voorwerpjes.Add(voorwerp);
                                    }
                                    else
                                    {
                                        Voorwerp voorwerp = new Voorwerp(reader.GetInt32(0), reader.GetString(4), reader.GetString(3), reader.GetString(6) + "-" + reader.GetString(5), reader.GetInt32(7));
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

                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
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
                
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand comm = OracleClientFactory.Instance.CreateCommand();
                comm.Connection = con;
                comm.CommandText = @"SELECT a.""gebruikersnaam"", RP.id FROM PERSOON p, ""ACCOUNT"" a, RESERVERING_POLSBANDJE RP, RESERVERING R, POLSBANDJE Po WHERE a.""ID"" = RP.""account_id"" AND RP.""polsbandje_id"" = po.""ID"" AND RP.""reservering_id"" = R.""ID"" AND R.""persoon_id"" = p.""ID"" AND Po.""barcode"" = '" + barcode + "' AND ROWNUM < 2";
                DbDataReader reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    string naam = reader.GetInt32(1)+","+reader.GetString(0);
                    return naam;
                }
               
            }
            return null;
        }

    }
}