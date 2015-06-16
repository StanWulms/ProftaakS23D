using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MateriaalVerhuurASP
{
    public partial class MateriaalTerugbrengen : System.Web.UI.Page
    {
        Database database;
        protected void Page_Load(object sender, EventArgs e)
        {
           //haalt alle exemplaren op en displayed alle exemplaren die nu verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            List<voorwerpen> Voorwerpen = database.Getvoorwerpen();
            foreach (voorwerpen voorwerp in Voorwerpen)
            {
                if (voorwerp.Verhuurd == true)
                {
                    lbTerugbrengen.Items.Add(Convert.ToString(voorwerp.exemplaarnummer));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");
        }
    }
}