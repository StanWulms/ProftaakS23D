using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.VerhuurInhoud
{
    public partial class TerugBrengen : System.Web.UI.Page
    {
        Database database;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["loadpageadditem"] = "true";
            lbTerugbrengen.Items.Clear();
            //haalt alle exemplaren op en displayed alle exemplaren die nu verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            List<DataBaseKlassen.Voorwerp> Voorwerpen = database.Getvoorwerpen();

            foreach (DataBaseKlassen.Voorwerp voorwerp in Voorwerpen)
            {
                if (voorwerp.Verhuurd == true)
                {
                    lbTerugbrengen.Items.Add(voorwerp.ToString());
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            lblTerugBrengenError.Visible = false;
            Page.Validate("TerugBrengenValidators");
            if (Page.IsValid)
            {
                try
                {
                    int rpnummer = Convert.ToInt32(lblnaamd.Text.Substring(0, 1));
                    database.updateterugbrengen(Convert.ToInt32(tbEventnummer.Text), rpnummer);
                    Response.Redirect("TerugBrengen.aspx");
                }
                catch { lblTerugBrengenError.Visible = true; }
            }           
        }

        protected void btnzoeknaam_Click(object sender, EventArgs e)
        {
            lblTerugBrengenError.Visible = false;
            Page.Validate("ZoekNaamValidators");
            if (Page.IsValid)
            {
                lblnaamd.Text = database.accountnummer(tbBarcode.Text);
            } 
        }

    }
}