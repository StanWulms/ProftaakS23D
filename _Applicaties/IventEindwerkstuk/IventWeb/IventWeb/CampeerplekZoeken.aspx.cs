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
            //Eerst kijk ik of er barcodes toegevoegd moeten worden.
            //Dit is een appart deel van de site dus alleen de 
            //methode addBarcode() moet aangeroepen worden.
            string barcodescanning = (String)Session["barcodeAdder"];            
            if (barcodescanning != "true")
            {
                Session["loadpagebetaling"] = "true";
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
            else
            {
                string voegdatatoe = (String)Session["addData"];
                if (voegdatatoe != "false")
                {
                    addBarcode();
                }
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
                            db.AddData(@"INSERT INTO polsbandje (""barcode"", ""actief"") VALUES ('" + Convert.ToString(i) + "',0)");
                            //Kijken wat het ID is van het zo juist toegevoegde POLSBANDJE.
                            List<Polsbandje> polsbandjes = new List<Polsbandje>();
                            polsbandjes = db.GetDataPolsbandje(@"SELECT * FROM polsbandje WHERE id = (SELECT MAX(id) FROM polsbandje)");
                            //Kijken wat het ID van het account is.
                            List<Account> accounts = new List<Account>();
                            accounts = db.GetDataAccount(@"SELECT * FROM account WHERE ""gebruikersnaam"" = '" + lbGeselecteerdePersonen.Items[i].Text +"'");
                            db.AddData(@"INSERT INTO reservering_polsbandje (""reservering_id"", ""polsbandje_id"", ""account_id"", ""aanwezig"") VALUES (" + reservaties[0].ReserveringID + ",'" + polsbandjes[0].PolsbandjeID + "'," + accounts[0].AccountID + ",0)");                           
                        }
                        //het toekennen van de barcodes aan de accounts                                              
                        //Alle gesleceteerde bezoekers opslaan in een array.
                        //Deze namen heb ik nogid om de juiste barcode aan te passen
                        string[] a = new string[lbGeselecteerdePersonen.Items.Count];
                        for (int i = 0; i < lbGeselecteerdePersonen.Items.Count; i++)
                        {
                            a[i] = lbGeselecteerdePersonen.Items[i].Text;
                        }
                        Session["bezoekersArray"] = a;
                        Session["gebruikersvolorde"] = 0;
                        //Pop-up venster waar de barcode ingevuld wordt  
                        ScriptManager.RegisterStartupScript(this, GetType(), "barcodeScanner", @"barcodeScanner(""Scan een nieuwe barcode voor: " + lbGeselecteerdePersonen.Items[0].Text + @""");", true);
                        btnDeSelecteerBezoeker.Enabled = false;
                        btnPlaatsReservering.Enabled = false;
                        btnSelecteerBezoeker.Enabled = false;
                        btnSelecteerReservatieHouder.Enabled = false;
                        btnNext.Visible = true;
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

        //Als de personen aan de database zijn toegevoegd moeten de barcodes nog ingevuld worden.
        //Dat gebeurd door het aanroepen van deze methode.
        //De gebruikers 
        public void addBarcode()
        {
            int volgorde = (Int32)Session["gebruikersvolorde"];
            //Het ophalen van het juiste account en vervolgens de bijhorende polsband
            List<Account> accounts = new List<Account>();

            //De bezoekersArray bevat alle zojuist toegevoegde accounts.
            //Deze worden een voor een opgenomen.
            string[] data = (string[])Session["bezoekersArray"];  

            accounts = db.GetDataAccount(@"SELECT * FROM account WHERE ""gebruikersnaam"" = '" + data[volgorde] + "'");
            List<Polsbandje> polsbandjes = new List<Polsbandje>();
            polsbandjes = db.GetDataPolsbandje(@"SELECT * FROM POLSBANDJE WHERE id = (SELECT MIN(id) FROM polsbandje WHERE LENGTH(""barcode"") <> 13)");
            //Vervolgens het polsbandje updaten
            string huidigebarcode = (String)Session["huidigebarcode"];
            //Er wordt gekeken of de functie AddData() true of false terug geeft.
            //Geeft deze false terug dan is er sprake van een ingevoerde barcode die al
            //in gebruik is. Zo niet dan kan de volgende bezoeker gekoppeld worden aan deen barcode
            List<Polsbandje> allepolsbandjes = new List<Polsbandje>();
            allepolsbandjes = db.GetDataPolsbandje(@"SELECT * FROM POLSBANDJE");
            bool dubbeleBarcode = false;
            foreach (Polsbandje p in allepolsbandjes)
            {
                if (p.Barcode == huidigebarcode)
                {
                    dubbeleBarcode = true;
                }
            }
            if (!dubbeleBarcode)
            {
                if (huidigebarcode == null || huidigebarcode == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "barcodeScanner", @"barcodeScanner(""Veld was leeg, scan nogmaals voor: " + data[volgorde] + @""");", true);
                    btnDeSelecteerBezoeker.Enabled = false;
                    btnPlaatsReservering.Enabled = false;
                    btnSelecteerBezoeker.Enabled = false;
                    btnSelecteerReservatieHouder.Enabled = false;
                    btnNext.Visible = true;
                    Session["addData"] = "false";
                }
                else
                {
                    bool noNumber = false;
                    try { Int64 test = Convert.ToInt64(huidigebarcode); }
                    catch { noNumber = true; }
                    if (noNumber == true || huidigebarcode.Length != 13)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "barcodeScanner", @"barcodeScanner(""Geen 13 cijferige barcode, scan nogmaals voor: " + data[volgorde] + @""");", true);
                        btnDeSelecteerBezoeker.Enabled = false;
                        btnPlaatsReservering.Enabled = false;
                        btnSelecteerBezoeker.Enabled = false;
                        btnSelecteerReservatieHouder.Enabled = false;
                        btnNext.Visible = true;
                        Session["addData"] = "false";
                    }
                    else
                    {
                        db.AddData(@"UPDATE polsbandje SET ""barcode"" = '" + huidigebarcode + "' WHERE id = " + polsbandjes[0].PolsbandjeID);
                        //Indien er nog meer gebruikers zijn een volgende venster openen.
                        //Zo niet, dan wordt de pagina gereset.
                        try
                        {
                            volgorde++;
                            Session["gebruikersvolorde"] = volgorde;
                            ScriptManager.RegisterStartupScript(this, GetType(), "barcodeScanner", @"barcodeScanner(""Scan een nieuwe barcode voor: " + data[volgorde] + @""");", true);
                            btnDeSelecteerBezoeker.Enabled = false;
                            btnPlaatsReservering.Enabled = false;
                            btnSelecteerBezoeker.Enabled = false;
                            btnSelecteerReservatieHouder.Enabled = false;
                            btnNext.Visible = true;
                            Session["addData"] = "false";
                        }
                        catch
                        {
                            Session["gebruikersvolorde"] = 0;
                            Session["barcodeAdder"] = "false";
                            Session["loadpage"] = "true";
                            Response.Redirect("CampeerplekZoeken.aspx");
                        } 
                    }                   
                }               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "barcodeScanner", @"barcodeScanner(""Barcode al in gebruik, scan nogmaals voor: " + data[volgorde] + @""");", true);
                btnDeSelecteerBezoeker.Enabled = false;
                btnPlaatsReservering.Enabled = false;
                btnSelecteerBezoeker.Enabled = false;
                btnSelecteerReservatieHouder.Enabled = false;
                btnNext.Visible = true;
                Session["addData"] = "false";
            }           
        }

        /// <summary>
        /// De next-button verschijnt als er een barcode is ingevuld in de 
        /// javascript pop-up. Door op de knop te klikken wordt (als die er is)
        /// de volgende bezoeker opgehaald om de barcode voor te scannen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Session["barcodeAdder"] = "true";
            Session["huidigebarcode"] = Hidden1.Value;
            string hiddenvlueee = (String)Session["huidigebarcode"];
            Session["addData"] = "true";
            Response.Redirect("CampeerplekZoeken.aspx");
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