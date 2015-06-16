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
        public List<voorwerpen> Getvoorwerpen()
        {
            //haalt alle voorwerpen op uit de database en stelt vast of ze al verhuurd zijn of niet.
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                com.Connection = con;
                com.CommandText = @"SELECT v.""productexemplaar_id"", v.""datumUit"",p.""merk"",p.""serie"", c.""naam"",t.""naam"",p.""prijs"" FROM VERHUUR v, PRODUCTEXEMPLAAR e, PRODUCT p, PRODUCTCAT c LEFT OUTER JOIN PRODUCTCAT t ON c.""productcat_id"" = t.""ID"" WHERE v.""productexemplaar_id"" = e.""ID"" AND e.""product_id"" = p.""ID"" AND p.""productcat_id"" = c.""ID"" ";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    List<voorwerpen> voorwerpjes = new List<voorwerpen>();
                    //dropdownmenu                    
                    while (reader.Read())
                    {
                        if (voorwerpjes.Count == 0)
                        {
                            if (reader.IsDBNull(1))
                            {
                                if (reader.IsDBNull(5))
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(4), reader.GetInt32(6));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(5) + "-" + reader.GetString(4), reader.GetInt32(6));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }                                
                            }
                            else
                            {
                                if(reader.IsDBNull(5))
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(4), reader.GetInt32(6));
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(5) + "-" + reader.GetString(4), reader.GetInt32(6));
                                    voorwerpjes.Add(voorwerp);
                                }                                
                            }
                        }
                        else
                        {
                            int soort = 0;
                            foreach (voorwerpen voorwerpje in voorwerpjes)
                            {
                                if (reader.IsDBNull(1))
                                {
                                    if (voorwerpje.exemplaarnummer == reader.GetInt32(0))
                                    {
                                        voorwerpje.Verhuurd = true;
                                    }
                                    else
                                    {
                                        soort = 1;                                        
                                    }
                                }
                                else
                                {
                                    if (voorwerpje.exemplaarnummer != reader.GetInt32(0))
                                    {
                                        soort = 2;
                                    }                                  
                                }
                            }
                            if(soort == 1)
                            {
                                if (reader.IsDBNull(5))
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(4), reader.GetInt32(6));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(5) + "-" + reader.GetString(4), reader.GetInt32(6));
                                    voorwerp.Verhuurd = true;
                                    voorwerpjes.Add(voorwerp);
                                }
                            }
                            else if(soort == 2)
                            {
                                if (reader.IsDBNull(5))
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(4), reader.GetInt32(6));
                                    voorwerpjes.Add(voorwerp);
                                }
                                else
                                {
                                    voorwerpen voorwerp = new voorwerpen(reader.GetInt32(0), reader.GetString(3), reader.GetString(2), reader.GetString(5) + "-" + reader.GetString(4), reader.GetInt32(6));
                                    voorwerpjes.Add(voorwerp);
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
    }
}