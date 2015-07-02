using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reservering_Reparatie
{
    public partial class Reservering : System.Web.UI.Page
    {
        Hoofdboeker hb;
        Boeking b;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["nieuweHoofdboeker"] = null;
            }
            b = new Boeking();
            if((String)Session["nieuweHoofdboeker"] == null)
            {
                Session["nieuweHoofdboeker"] = "true";
            }
        }

        /// <summary>
        /// Als alle verplichte velden zijn ingevuld wordt er een SP in de 
        /// database aangeroepen die de ingevulde waardes controleert op correctheid.
        /// Zijn de gegevens geldig, dan worden deze toegevoegd aan de database.
        /// Zo niet, dan wordt er een string terug gestuurd met een melding van de desbetreffende fout.
        /// </summary>
        protected void btnRegistreer_Click(object sender, EventArgs e)
        {
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                    //Het aanroepen van de SP.


                    lblValidation.Text = "Goeie ouwe";
                    lblValidation.Visible = true;
                    //Hoofdboeker h = new Hoofdboeker()
                    Session["Hoofdbezoeker"] = tbAccount1.Text; // Nu weet je de gebruikersnaam ?¿?
                    //Maken  van de sessie voor de accounts.
                    List<Account> accounts = new List<Account>();

                    //Toevoegen van een Account in de lijst.

                    var chars = "abcdef0123456789";
                    var stringChars = new char[32];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    string activatiehash = new String(stringChars);
                    //Het toevoegen van het account van de hoofdboeker
                    //

                    for (int k = 1; k < Convert.ToInt32(ddlAantal.Text) + 1; k++)
                    {
                        //Vervolgens (als die er zijn) worden de extra bezoekers
                        //Toegevoegd aan de tabel ACCOUNT.
                        random = new Random();
                        for (int j = 0; j < stringChars.Length; j++)
                        {
                            stringChars[j] = chars[random.Next(chars.Length)];
                        }
                        activatiehash = new String(stringChars);
                        //Er wordt gekeken welke tbAccount en tbEmail opgehaald moet worden
                        //Deze worden in het volgende insertstatement ingevoerd.
                        Account a;
                        //Nieuw account ID
                        int id = b.GetMaxAccount();
                        switch (k)
                        {
                            case 1: a = new Account((id+k), tbAccount1.Text, tbEmail1.Text, activatiehash); accounts.Add(a);
                                break;
                            case 2: a = new Account((id + k), tbAccount2.Text, tbEmail2.Text, activatiehash); accounts.Add(a);
                                break;
                            case 3: a = new Account((id + k), tbAccount3.Text, tbEmail3.Text, activatiehash); accounts.Add(a);
                                break;
                            case 4: a = new Account((id + k), tbAccount4.Text, tbEmail4.Text, activatiehash); accounts.Add(a);
                                break;
                            case 5: a = new Account((id + k), tbAccount5.Text, tbEmail5.Text, activatiehash); accounts.Add(a);
                                break;
                            default:
                                break;
                        }
                    }
                    if ((String)Session["nieuweHoofdboeker"] == "false")
                    {
                        lblValidation.Text = "Persoon geselecteerd.";

                        Hoofdboeker hoofdboeker = new Hoofdboeker(Convert.ToInt32(lbHoofdbezoekers.SelectedValue.Substring(4, lbHoofdbezoekers.SelectedValue.IndexOf("-") - 5)), tbVoornaam.Text, tbTussenvoegsel.Text, tbAchternaam.Text, tbStraat.Text, tbHuisnummer.Text, tbWoonplaats.Text, tbBankrekening.Text);
                        Session["Hoofdboeker"] = hoofdboeker;
                    }
                    else if ((String)Session["nieuweHoofdboeker"] == "true")
                    {
                        Hoofdboeker hb = new Hoofdboeker(tbVoornaam.Text, tbTussenvoegsel.Text, tbAchternaam.Text, tbStraat.Text, tbHuisnummer.Text, tbWoonplaats.Text, tbBankrekening.Text);
                        lblValidation.Text = b.maakpersoon(hb);
                        lblValidation.Visible = true;
                        hb = b.LaatsteHoofdbezoeker(hb);
                        Session["Hoofdboeker"] = hb;
                    }
                    

                if (lblValidation.Text == "Persoon aangemaakt." || lblValidation.Text == "Persoon geselecteerd.")
                {
                    Session["Accounts"] = accounts;
                    lblValidation2.Text = b.MaakAccounts(accounts);
                    lblValidation2.Visible = true;
                    if (lblValidation2.Text == "Account(s) aangemaakt.")
                    {
                        Response.Redirect("Kampeerplaats.aspx");
                    }
                }
            }
        }

        /// <summary>
        /// Iedere keer dat er een ander aantal extra bezoekers wordt geselecteerd
        /// dienen de juiste velden zichtbaar te worden. De textboxen hebben standaard waardes -1 t/m -5.
        /// Dit doe ik omdat ik kijk of er geen van de velden gelijk zijn aan elkaar.
        /// Als de textboxes leeg waren zou deze check altijd 'true' geven.
        /// </summary>
        protected void ddlAantal_TextChanged(object sender, EventArgs e)
        {
            lblAccount1.Visible = false; tbAccount1.Visible = false; tbAccount1.Text = "-1"; rfvAccount1.Enabled = false; lblEmail1.Visible = false; tbEmail1.Visible = false; tbEmail1.Text = "-1"; rfvEmail1.Enabled = false;
            lblAccount2.Visible = false; tbAccount2.Visible = false; tbAccount2.Text = "-2"; rfvAccount2.Enabled = false; lblEmail2.Visible = false; tbEmail2.Visible = false; tbEmail2.Text = "-2"; rfvEmail2.Enabled = false;
            lblAccount3.Visible = false; tbAccount3.Visible = false; tbAccount3.Text = "-3"; rfvAccount3.Enabled = false; lblEmail3.Visible = false; tbEmail3.Visible = false; tbEmail3.Text = "-3"; rfvEmail3.Enabled = false;
            lblAccount4.Visible = false; tbAccount4.Visible = false; tbAccount4.Text = "-4"; rfvAccount4.Enabled = false; lblEmail4.Visible = false; tbEmail4.Visible = false; tbEmail4.Text = "-4"; rfvEmail4.Enabled = false;
            lblAccount5.Visible = false; tbAccount5.Visible = false; tbAccount5.Text = "-5"; rfvAccount5.Enabled = false; lblEmail5.Visible = false; tbEmail5.Visible = false; tbEmail5.Text = "-5"; rfvEmail5.Enabled = false;
            int aantal = Convert.ToInt32(ddlAantal.Text);
            for (int i = 0; i < aantal; i++)
            {
                if (i == 0) { lblAccount1.Visible = true; tbAccount1.Visible = true; tbAccount1.Text = ""; rfvAccount1.Enabled = true; lblEmail1.Visible = true; tbEmail1.Visible = true; tbEmail1.Text = ""; rfvEmail1.Enabled = true; }
                if (i == 1) { lblAccount2.Visible = true; tbAccount2.Visible = true; tbAccount2.Text = ""; rfvAccount2.Enabled = true; lblEmail2.Visible = true; tbEmail2.Visible = true; tbEmail2.Text = ""; rfvEmail2.Enabled = true; }
                if (i == 2) { lblAccount3.Visible = true; tbAccount3.Visible = true; tbAccount3.Text = ""; rfvAccount3.Enabled = true; lblEmail3.Visible = true; tbEmail3.Visible = true; tbEmail3.Text = ""; rfvEmail3.Enabled = true; }
                if (i == 3) { lblAccount4.Visible = true; tbAccount4.Visible = true; tbAccount4.Text = ""; rfvAccount4.Enabled = true; lblEmail4.Visible = true; tbEmail4.Visible = true; tbEmail4.Text = ""; rfvEmail4.Enabled = true; }
                if (i == 4) { lblAccount5.Visible = true; tbAccount5.Visible = true; tbAccount5.Text = ""; rfvAccount5.Enabled = true; lblEmail5.Visible = true; tbEmail5.Visible = true; tbEmail5.Text = ""; rfvEmail5.Enabled = true; }
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

        }

        protected void btnZoekHoofdboeker_Click(object sender, EventArgs e)
        {
            b = new Boeking();
            List<Hoofdboeker> hoofdboekers = b.ZoekHoofdboekers(tbHoofdboeker.Text);
            lbHoofdbezoekers.Items.Clear();
            foreach (Hoofdboeker hb in hoofdboekers)
            {
                lbHoofdbezoekers.Items.Add(hb.ToString());
            }
        }

        protected void btnSelecteerHoofdBezoeker_Click(object sender, EventArgs e)
        {
            Session["Hoofdboeker"] = lbHoofdbezoekers.SelectedItem.Value.Substring(3, lbHoofdbezoekers.SelectedItem.Value.IndexOf("-") - 3);
            Hoofdboeker hb = b.ZoekJuisteHoofdboeker((String)Session["Hoofdboeker"]);
            
            tbVoornaam.Text = hb.Naam;
            tbVoornaam.Enabled = false;

            tbTussenvoegsel.Text = hb.Tussenvoegsel;
            tbTussenvoegsel.Enabled = false;

            tbAchternaam.Text = hb.Achternaam;
            tbAchternaam.Enabled = false;

            tbStraat.Text = hb.Straat;
            tbStraat.Enabled = false;

            tbHuisnummer.Text = hb.Huisnummer;
            tbHuisnummer.Enabled = false;

            tbWoonplaats.Text = hb.Woonplaats;
            tbWoonplaats.Enabled = false;

            tbBankrekening.Text = hb.Iban;
            tbBankrekening.Enabled = false;

            Session["nieuweHoofdboeker"] = "false";
        }
    }
}