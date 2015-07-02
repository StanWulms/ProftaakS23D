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
            b = new Boeking();
            kp = new Kampeerplaats();
            if (!Page.IsPostBack)
            {
                try
                {
                    lbAccounts.Items.Clear();
                    lbVrijePlaatsen.Items.Clear();

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

        /// <summary>
        /// Met deze knop reserveer je een kampeerplek.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnKampeerplaatsReserveren_Click(object sender, EventArgs e)
        {
            int capaciteit = -1;
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                string plek = lbVrijePlaatsen.SelectedValue;
                //Als er geen plek geselecteerd wordt, wordt dit afgevangen met een MessageBox.
                try
                {
                    capaciteit = Convert.ToInt32(plek.Substring(plek.LastIndexOf(":") + 1));
                }
                catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Selecteer een Plaats.');</script>"); }
                
                //Als het aantal accounts niet kleiner (of gelijk aan) de capaciteit van de kampeerplaats is, dan wordt er een MessageBox getoond.
                //Als het aantal past, dan wordt de reservatie geplaatst.
                if(Convert.ToInt32(lbAccounts.Items.Count.ToString()) <= capaciteit)
                {
                    string nummer = lbVrijePlaatsen.SelectedValue.Substring(8,lbVrijePlaatsen.SelectedValue.IndexOf("-")-9);
                    b.Boek(tbBeginDatum.Text, tbEindDatum.Text, nummer);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Uw reservatie is geplaatst.');</script>");
                    Response.Redirect("Reservering.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Selecteer een plaats met meer Capaciteit. U heeft een plaats geselecteerd met minder capaciteit dan het aantal personen.');</script>");
                }
            }
        }
    }
}