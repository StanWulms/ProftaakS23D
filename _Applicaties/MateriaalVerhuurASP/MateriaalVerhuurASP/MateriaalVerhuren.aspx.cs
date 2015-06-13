using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace MateriaalVerhuurASP
{
    public partial class MateriaalVerhuren : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                com.CommandText = @"SELECT ""gebruikersnaam"" FROM ACCOUNT";
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    //dropdownmenu
                    lbItems.Items.Clear();
                    while (reader.Read())
                    {
                        lbItems.Items.Add(reader[0].ToString());
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        protected void btnVerhuur_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");
        }

    }
}