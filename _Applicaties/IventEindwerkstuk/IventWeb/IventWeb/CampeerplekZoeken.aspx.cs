using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.ReservatieInhoud
{
    public partial class CampeerplekZoeken : System.Web.UI.Page
    {
        Database db;
        
        //Bij het laden van de pagina worden alle personen en accounts
        //zonder campingplaats ingeladen. Ook worden de nog vrije plaatsen
        //opgehaald.
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
            //Alle hoofdboekers worden aan de listbox 'Reservatiehouder' toegevoegd
            List<Persoon> personen = new List<Persoon>();
            personen = db.GetDataPersoon(@"SELECT * FROM persoon ORDER BY ""voornaam"", ""achternaam""");
            foreach (Persoon p in personen)
            {
                lbReservatieHouder.Items.Add(p.ToString());
            }
            //Vervolgens worden alle personen die nog geen plek hebben getoond
            //in de listbox 'Bezoekers'
            List<Account> accounts = new List<Account>();
            accounts = db.GetDataAccount(@"SELECT * FROM account WHERE id NOT IN (SELECT ""account_id"" FROM reservering_polsbandje)");
            foreach (Account a in accounts)
            {
                lbBezoeker.Items.Add(a.ToString());
            }
            //Als laatste worden de vrije plaatsen opgehaald.
            List<Plek> plekken = new List<Plek>();
            plekken = db.GetDataPlek(@"SELECT * FROM plek WHERE id NOT IN (SELECT ""plek_id"" FROM plek_reservering)");
            foreach (Plek pl in plekken)
            {
                lbPlek.Items.Add(pl.ToString());
            }
        }
    }
}