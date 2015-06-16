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
        Database database;
        protected void Page_Load(object sender, EventArgs e)
        {
            //haalt alle exemplaren op en displayed alle exemplaren die nu niet verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            List<voorwerpen> Voorwerpen = database.Getvoorwerpen();
            foreach(voorwerpen voorwerp in Voorwerpen)
            {
                if(voorwerp.Verhuurd == false)
                {
                    lbItems.Items.Add(Convert.ToString(voorwerp.exemplaarnummer));
                }                                
            }
        }

        protected void btnVerhuur_Click(object sender, EventArgs e)
        {

            Response.Redirect("WebForm1.aspx");
        }

    }
}