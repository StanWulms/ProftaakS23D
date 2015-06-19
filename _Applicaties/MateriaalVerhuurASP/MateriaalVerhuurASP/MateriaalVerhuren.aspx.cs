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
        List<Voorwerp> Voorwerpen;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbItems.Items.Clear();
            //haalt alle exemplaren op en displayed alle exemplaren die nu niet verhuurd zijn de exemplaarnummers van deze voorwerpen worden in een listbox gezet.
            database = new Database();
            database.getAccounts();
            Voorwerpen = database.Getvoorwerpen();
            foreach(Voorwerp voorwerp in Voorwerpen)
            {
                if(voorwerp.Verhuurd == false)
                {
                    lbItems.Items.Add(Convert.ToString(voorwerp));
                }                                
            }
        }

        protected void btnVerhuur_Click(object sender, EventArgs e)
        {            
            foreach (Voorwerp voorwerp in Voorwerpen)
            {
                if (voorwerp.exemplaarnummer == Convert.ToInt32(tbExemplaarnummer.Text))
                {
                    if (voorwerp.Verhuurd == false && lblDbNaam.Text != null)
                    {
                        int rpnummer = Convert.ToInt32(lblDbNaam.Text.Substring(0, 1));
                        database.insertverhuur(voorwerp, rpnummer);
                        Response.Redirect("WebForm1.aspx");
                    }
                }
                
            }
            
        }

        protected void BtnZoek_Click(object sender, EventArgs e)
        {
             lblDbNaam.Text = database.accountnummer(tbBarcode.Text);
        }

    }
}