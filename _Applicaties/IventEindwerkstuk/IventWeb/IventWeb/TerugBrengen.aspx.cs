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
            //Het zetten van de huidige bezoeker in de naam_label
            lblnaamd.Text = (String)Session["naambezoeker"];
            lbTerugbrengen.Items.Clear();
            //haalt alle exemplaren op en displayed alle exemplaren die nu verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            //Als er geen gebruiker is ingecheckt worden alle geleende items getoond.
            //Is er wel iemand ingelogd, dan worden alleen de geleende items van de bezoeker getoond.
            string bezoekeritems = (String)Session["itemsbezoeker"];
            List<DataBaseKlassen.Voorwerp> Voorwerpen;
            if (bezoekeritems == "" || bezoekeritems == null) {  Voorwerpen = database.Getvoorwerpen(@"SELECT e.""ID"", v.""datumIn"",v.""datumUit"",p.""merk"",p.""serie"", c.""naam"",t.""naam"",p.""prijs"" FROM PRODUCTEXEMPLAAR e LEFT OUTER JOIN VERHUUR v ON v.""productexemplaar_id"" = e.""ID"" , PRODUCT p, PRODUCTCAT c LEFT OUTER JOIN PRODUCTCAT t ON c.""productcat_id"" = t.""ID"" WHERE p.""productcat_id"" = c.""ID"" AND e.""product_id"" = p.""ID"" ORDER BY e.""ID"""); }
            else { Voorwerpen = database.Getvoorwerpen(bezoekeritems); }
            //Het tekenen van alle gevonden items
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
                    Session["naambezoeker"] = "";
                    Response.Redirect("TerugBrengen.aspx");
                }
                catch { lblTerugBrengenError.Text = "Scan eerst een bezoeker"; lblTerugBrengenError.Visible = true; }
            }           
        }

        protected void btnzoeknaam_Click(object sender, EventArgs e)
        {
            lblTerugBrengenError.Visible = false;
            Page.Validate("ZoekNaamValidators");
            if (Page.IsValid)
            {
                try
                {
                    string accountid = database.accountnummer(tbBarcode.Text);
                    Session["naambezoeker"] = accountid;
                    accountid = accountid.Substring(0, accountid.IndexOf(" "));
                    Session["itemsbezoeker"] = @"SELECT e.""ID"", v.""datumIn"",v.""datumUit"",p.""merk"",p.""serie"", c.""naam"",t.""naam"",p.""prijs"" FROM PRODUCTEXEMPLAAR e LEFT OUTER JOIN VERHUUR v ON v.""productexemplaar_id"" = e.""ID"" , PRODUCT p, RESERVERING_POLSBANDJE r ,PRODUCTCAT c LEFT OUTER JOIN PRODUCTCAT t ON c.""productcat_id"" = t.""ID"" WHERE p.""productcat_id"" = c.""ID"" AND e.""product_id"" = p.""ID"" AND r.""ID"" = v.""res_pb_id"" AND r.""account_id"" = " + accountid + @"ORDER BY e.""ID""";
                    Response.Redirect("TerugBrengen.aspx");
                }
                catch { lblTerugBrengenError.Text = "Barcode bestaad niet."; lblTerugBrengenError.Visible = true; }
            } 
        }

    }
}