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
                    //dropdownmenu                    
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
        public List<voorwerpen> Getnietgereserveerde()
        {

        }
    }
}