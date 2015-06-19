using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.ToegangscontroleInhoud
{
    public partial class ZoekPersonen : System.Web.UI.Page
    {
        Database db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
        }

        /// <summary>
        /// De zoek-butten wordt gebruikt om een lijst met bezoekers op te halen
        /// waarvan de zoekcriteria voorkomt in de gebruikersnaam. De nog niet actieve bezoekers
        /// komen in de bovenstaande listbox te staan en de actieve mensen in de onderste listbox.
        /// </summary>
        protected void Button1_Click(object sender, EventArgs e)
        {
            lbaanwezig.Items.Clear();
            lbnietaanwezig.Items.Clear();
            foreach (DataBaseKlassen.Bezoeker bezoeker in db.getbezoekers(Tbsearch.Text))
            if (bezoeker.Aanwezig == 0)
            {
                lbnietaanwezig.Items.Add(bezoeker.Naam);
            }
            else
            {
                lbaanwezig.Items.Add(bezoeker.Naam);
            }
        }
    }
}