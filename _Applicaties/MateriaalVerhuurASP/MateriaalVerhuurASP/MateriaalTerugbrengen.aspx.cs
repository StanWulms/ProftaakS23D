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
            lbTerugbrengen.Items.Clear();
           //haalt alle exemplaren op en displayed alle exemplaren die nu verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            List<voorwerp> Voorwerpen = database.Getvoorwerpen();

            foreach (voorwerp voorwerp in Voorwerpen)
            {
                if (voorwerp.Verhuurd == true)
                {
                    lbTerugbrengen.Items.Add(voorwerp.ToString());
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int rpnummer = Convert.ToInt32(lblnaamd.Text.Substring(0, 1));
            database.updateterugbrengen(Convert.ToInt32(tbEventnummer.Text), rpnummer);
            Response.Redirect("WebForm1.aspx");
        }

        protected void btnzoeknaam_Click(object sender, EventArgs e)
        {
            {
                lblnaamd.Text = database.accountnummer(tbBarcode.Text);
            }
        }      
    }
}