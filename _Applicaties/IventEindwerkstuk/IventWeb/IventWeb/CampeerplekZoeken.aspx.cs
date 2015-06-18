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
            string laadpagina = (String)Session["loadpage"];
            if (laadpagina != "false")
            {
                Refresh();
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
                Session["loadpage"] = "false";
            }         
        }

        /// <summary>
        /// Het selecteren van de Hoofdboeker. Deze boeker wordt
        /// in een label geplaatst en opgeslagen in een sessie.
        /// </summary>
        protected void btnSelecteerReservatieHouder_Click(object sender, EventArgs e)
        {
            lblReserveerError.Visible = false;
            try
            {
                string p;
                int persoonid;
                p = lbReservatieHouder.SelectedValue;
                persoonid = Convert.ToInt32(p.Substring(4, p.IndexOf(":", 3) - 4));
                p = p.Substring(p.IndexOf(":", 3) + 1);
                lblReservatiehouder.Text = p;
                Session["persoonid"] = persoonid;
            }
            catch { lblReserveerError.Text = "Selecteer een reservatiehouder."; lblReserveerError.Visible = true; }
        }

        /// <summary>
        /// (De lb bezoeker bevat alle bezoekers die nog geen plaats hebben).
        /// Het selecteren van de Bezoekers. Als er een bezoeker geselecteerd is en er
        /// wordt op de knop gedrukt, dan wordt deze bezoeker verwijderd uit de bezoeker_listbox
        /// en geplaats in de geselecteerdebezoeker_listbox.
        /// </summary>
        protected void btnSelecteerBezoeker_Click(object sender, EventArgs e)
        {
            lblReserveerError.Visible = false;
            string a;
            a = lbBezoeker.SelectedValue;
            if (a == "") { lblReserveerError.Text = "Selecteer een bezoeker."; lblReserveerError.Visible = true; }
            else
            {
                lbGeselecteerdePersonen.Items.Add(a);
                lbBezoeker.Items.Remove(a);
            }
        }

        /// <summary>
        /// De personen in de geselecteerdebezoeker_listbox zijn de bezoekers waar de plaatsreservering
        /// over gedaan wordt. Het is ook mogelijk d.m.v. deze knop om de geselecteerde bezoekers
        /// weer terug te zetten naar de bezoeker_listbox
        /// </summary>
        protected void btnDeSelecteerBezoeker_Click(object sender, EventArgs e)
        {
            lblReserveerError.Visible = false;
            string a;
            a = lbGeselecteerdePersonen.SelectedValue;
            if (a == "") { lblReserveerError.Text = "Selecteer een bezoeker."; lblReserveerError.Visible = true; }
            else
            {
                lbBezoeker.Items.Add(a);
                lbGeselecteerdePersonen.Items.Remove(a);
            }
        }

        /// <summary>
        /// Door het klikken op deze button wordt de reservering geplaatst. De bezoekers
        /// in de geselecteerdebezoekers_listbox worden gekoppeld aan de geselecteerde plaats.
        /// </summary>
        protected void btnPlaatsReservering_Click(object sender, EventArgs e)
        {
            lblReserveerError.Visible = false;
            //Als er geen bezoekers geselecteerd wordt er een error weergegeven.
            if (lbGeselecteerdePersonen.Items.Count == 0 || lblReservatiehouder.Text == "")
            {
                lblReserveerError.Text = "Selecteerd eerst bezoekers en een hoofdboeker";
                lblReserveerError.Visible = true;
            }
            else
            {
                string plek;
                plek = lbPlek.SelectedValue;
                //Vervolgens wordt er gekeken of er wel een plek geselecteerd is.
                //Zo niet, dan wordt er een error gedisplayd.
                if (plek != "")
                {
                    int capaciteit = Convert.ToInt32(plek.Substring(plek.LastIndexOf(":") + 1));
                    int plekid = Convert.ToInt32(plek.Substring(4, plek.IndexOf(":", 3) - 4));
                    List<Plek> plekken = new List<Plek>();
                    plekken = db.GetDataPlek(@"SELECT * FROM plek WHERE id = " + plekid);
                    //Als laatste wordt er gekeken of er niet te veel mensen op 1 plaats worden gezet.
                    if (plekken[0].Capaciteit >= lbGeselecteerdePersonen.Items.Count)
                    {
                        //INSERT statement voor de nieuwe RESERVERING.
                        int hoofdboeker = (Int32)Session["persoonid"];
                        string begindate = tbBeginDatum.Text;
                        string einddate = tbEindDatum.Text;
                        db.AddData(@"INSERT INTO reservering (""persoon_id"", ""datumStart"", ""datumEinde"", ""betaald"") VALUES (" + hoofdboeker + ",'" + begindate + "','" + einddate + "',0)");
                        //Kijken wat het ID is van de zo juist toegevoegde RESERVERING.
                        //De PLEK_RESERVERING wordt vervolgens ingevuld
                        List<Reservering> reservaties = new List<Reservering>();
                        reservaties = db.GetDataReservering(@"SELECT * FROM reservering WHERE id = (SELECT MAX(id) FROM reservering)");
                        db.AddData(@"INSERT INTO plek_reservering (""plek_id"", ""reservering_id"") VALUES (" + plekid + "," + reservaties[0].ReserveringID + ")");
                        //INSERT statements voor de RESERVERING_POSLSBANDJE tabel.
                        //En voor iedere bezoeker een POLSBANDJE.
                        for (int i = 0; i < lbGeselecteerdePersonen.Items.Count; i++)
                        {
                            //Het genereren van een wilekeurige barcode
                            var chars = "0123456789";
                            var stringChars = new char[12];
                            var random = new Random();
                            for (int j = 0; j < stringChars.Length; j++)
                            {
                                stringChars[j] = chars[random.Next(chars.Length)];
                            }
                            string barcode = new String(stringChars);
                            db.AddData(@"INSERT INTO polsbandje (""barcode"", ""actief"") VALUES (" + barcode + ",0)");
                            //Kijken wat het ID is van het zo juist toegevoegde POLSBANDJE.
                            List<Polsbandje> polsbandjes = new List<Polsbandje>();
                            polsbandjes = db.GetDataPolsbandje(@"SELECT * FROM polsbandje WHERE id = (SELECT MAX(id) FROM polsbandje)");
                            //Kijken wat het ID van het account is.
                            List<Account> accounts = new List<Account>();
                            accounts = db.GetDataAccount(@"SELECT * FROM account WHERE ""gebruikersnaam"" = '" + lbGeselecteerdePersonen.Items[i].Text +"'");
                            db.AddData(@"INSERT INTO reservering_polsbandje (""reservering_id"", ""polsbandje_id"", ""account_id"", ""aanwezig"") VALUES (" + reservaties[0].ReserveringID + ",'" + polsbandjes[0].PolsbandjeID + "'," + accounts[0].AccountID + ",0)");                           
                        }
                        Session["loadpage"] = "true";
                        Response.Redirect("CampeerplekZoeken.aspx");
                    }
                    else
                    {
                        lblReserveerError.Text = "Er kunnen niet zo veel mensen op die plaats, selecteer een andere plek.";
                        lblReserveerError.Visible = true;
                    }
                }
                else
                {
                    lblReserveerError.Text = "Selecteer een campeerplaats.";
                    lblReserveerError.Visible = true;
                }
            }          
        }

        public void Refresh()
        {
            lbReservatieHouder.Items.Clear();
            lbBezoeker.Items.Clear();
            lbGeselecteerdePersonen.Items.Clear();
            lbPlek.Items.Clear();
        }       
    }
}