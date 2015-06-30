using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reservering_Reparatie
{
    public partial class Campeerplaats : System.Web.UI.Page
    {
        Boeking b;
        Kampeerplaats kp;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    lbAccounts.Items.Clear();
                    lbVrijePlaatsen.Items.Clear();

                    b = new Boeking();
                    kp = new Kampeerplaats();

                    List<Account> accounts = (List<Account>)Session["Accounts"];
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        lbAccounts.Items.Add(accounts[i].ToString());
                    }

                    List<Kampeerplaats> VrijeKampeerplaatsen = new List<Kampeerplaats>();
                    VrijeKampeerplaatsen = kp.ZoekVrijePlek(b.ZoekAlleKampeerplaatsen());
                    foreach (Kampeerplaats kpplts in VrijeKampeerplaatsen)
                    {
                        lbVrijePlaatsen.Items.Add(kpplts.ToString());
                    }
                }
                catch { Response.Redirect("Reservering.aspx"); }
            }
        }

        protected void btnKampeerplaatsReserveren_Click(object sender, EventArgs e)
        {
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                string plek = lbVrijePlaatsen.SelectedValue;
                
                int capaciteit = Convert.ToInt32(plek.Substring(plek.LastIndexOf(":") + 1));
                //TEST - lbVrijePlaatsen.Items.Add(capaciteit.ToString());
                if(Convert.ToInt32(lbSelectedAccounts.Items.Count.ToString()) <= capaciteit)
                {
                    lbVrijePlaatsen.Items.Add("Reservering Geslaagd.");
                }
            }
        }

        protected void btnSelecteer_Click(object sender, EventArgs e)
        {
            try
            {
                string a;
                a = lbAccounts.SelectedValue;
                if (a != "")
                {
                    lbSelectedAccounts.Items.Add(a);
                    lbAccounts.Items.Remove(a);
                }
            }
            catch { }
        }
    }
}