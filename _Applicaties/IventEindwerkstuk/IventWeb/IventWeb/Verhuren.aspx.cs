using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.VerhuurInhoud
{
    public partial class Verhuren : System.Web.UI.Page
    {      
        Database database;
        List<DataBaseKlassen.Voorwerp> Voorwerpen;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["naambezoeker"] = "";
            Session["itemsbezoeker"] = "";
            Session["loadpageadditem"] = "true";
            lbItems.Items.Clear();
            //haalt alle exemplaren op en displayed alle exemplaren die nu niet verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            database.getAccounts();
            Voorwerpen = database.Getvoorwerpen(@"SELECT e.""ID"", v.""datumIn"",v.""datumUit"",p.""merk"",p.""serie"", c.""naam"",t.""naam"",p.""prijs"" FROM PRODUCTEXEMPLAAR e LEFT OUTER JOIN VERHUUR v ON v.""productexemplaar_id"" = e.""ID"" , PRODUCT p, PRODUCTCAT c LEFT OUTER JOIN PRODUCTCAT t ON c.""productcat_id"" = t.""ID"" WHERE p.""productcat_id"" = c.""ID"" AND e.""product_id"" = p.""ID"" ORDER BY e.""ID""");
            foreach (DataBaseKlassen.Voorwerp voorwerp in Voorwerpen)
            {
                if(voorwerp.Verhuurd == false)
                {
                    lbItems.Items.Add(Convert.ToString(voorwerp));
                }                                
            }
        }
    
        protected void BtnZoek_Click(object sender, EventArgs e)
        {
            Page.Validate("NaamZoekValidators");
            if (Page.IsValid)
            {
                lblDbNaam.Text = database.accountnummer(tbBarcode.Text);
            } 
        }

        protected void btnVerhuur_Click(object sender, EventArgs e)
        {
            Page.Validate("VerhuurValidators");
            if (Page.IsValid)
            {
                foreach (DataBaseKlassen.Voorwerp voorwerp in Voorwerpen)
                {
                    if (voorwerp.exemplaarnummer == Convert.ToInt32(tbExemplaarnummer.Text))
                    {
                        if (voorwerp.Verhuurd == false && lblDbNaam.Text != null)
                        {
                            int rpnummer = Convert.ToInt32(lblDbNaam.Text.Substring(0, 1));
                            database.insertverhuur(voorwerp, rpnummer);
                            Response.Redirect("Verhuren.aspx");
                        }
                    }
                }
            }
        }

    }
}