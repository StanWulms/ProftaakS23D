using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

using System.Web.UI.HtmlControls;

namespace SocialMediaSharingASP
{
    public class SUBCategorie
    {
        public String Naam { get; set; }
        public int BijdrageID { get; set; }
        public List<SUBCategorie> categorieen { get; set; }

        public SUBCategorie()
        {
            categorieen = new List<SUBCategorie>();
        }

        public SUBCategorie(int id, string naam)
        {
            this.BijdrageID = id;
            this.Naam = naam;
            categorieen = new List<SUBCategorie>();
        }

        public SUBCategorie(string naam)
        {
            this.Naam = naam;
            categorieen = new List<SUBCategorie>();
        }

        public void getSUBCategorie()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["categorie"];
            // int subcategorie = Convert.ToInt32(System.Web.HttpContext.Current.Session["smd"]);
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectieStr"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                com.CommandText = @"SELECT ""naam"" FROM CATEGORIE WHERE ""categorie_id"" = " + bijdrageID + "";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    SUBCategorie sc;
                    while (reader.Read())
                    {
                        sc = new SUBCategorie(reader.GetString(0));
                        categorieen.Add(sc);
                    }
                }
                catch (NullReferenceException)
                {

                }

            }
        }
    }
}