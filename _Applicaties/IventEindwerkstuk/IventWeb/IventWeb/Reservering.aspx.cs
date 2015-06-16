using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace IventWeb.ReservatieInhoud
{
    public partial class ReserveringPlaatsen : System.Web.UI.Page
    {
        Database db;
        string username;
        string email;
        bool dubbeleUsername = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
        }

        /// <summary>
        /// Als de regisratie geldig is dan worden alle accounts toegevoegd
        /// aan de database. Verder wordt voor de hoofdboeker zijn
        /// gegevens opgeslagen in de PERSOON tabel.
        /// </summary>
        protected void btnRegistreer_Click(object sender, EventArgs e)
        {
            lblHoofdBoekerError.Visible = false;
            lblHoofdBoekerEmailError.Visible = false;
            lblDubbeleUsername.Visible = false;
            lblDubbeleEmail.Visible = false;
            lblValidation.Visible = true;
            //ALs de alle Validators 'true' zijn wordt Page.IsValid 'true'
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                //Kijken of de ingevulde gebruikers(namen) uniek zijn
                //Zo niet, dan worden de inserts niet uitgevoerd.
                for (int i = 0; i < Convert.ToInt32(ddlAantal.Text); i++)
                {
                    Account a = new Account();
                    switch (i)
                    {
                        case 0: username = tbGebruikersnaam.Text; email = tbEmail.Text;
                            break;
                        case 1: username = tbAccount1.Text; email = tbEmail1.Text;
                            break;
                        case 2: username = tbAccount2.Text; email = tbEmail2.Text;
                            break;
                        case 3: username = tbAccount3.Text; email = tbEmail3.Text;
                            break;
                        case 4: username = tbAccount4.Text; email = tbEmail4.Text;
                            break;
                        case 5: username = tbAccount5.Text; email = tbEmail5.Text;
                            break;
                        default:
                            break;
                    }
                    //Kijken of er geen duplicaties voorkomen
                    //Eerste zoeken naar een matchende username
                    try { a = db.GetDataAccount(@"SELECT * FROM account WHERE ""gebruikersnaam"" = '" + username + "'")[0]; }
                    catch { a.GebruikersNaam = ""; }
                    string u = a.GebruikersNaam;
                    if (u == username) { lblHoofdBoekerError.Visible = true; dubbeleUsername = true; }
                    //Het controleren van de andere velden.
                    string g = tbGebruikersnaam.Text;
                    string a1 = tbAccount1.Text; string a2 = tbAccount2.Text; string a3 = tbAccount3.Text;
                    string a4 = tbAccount4.Text; string a5 = tbAccount5.Text;
                    if (g == a1 || g == a2 || g == a3 || g == a4 || g == a5 || a1 == a2 || a1 == a3 || a1 == a4 || a1 == a5 || a2 == a3 || a2 == a4 || a2 == a5 || a3 == a4 || a3 == a5 || a4 == a5)
                    {
                        lblDubbeleUsername.Visible = true; dubbeleUsername = true;
                    }
                    //Daarna zoeken naar een matchend email adres
                    try { a = db.GetDataAccount(@"SELECT * FROM account WHERE ""email"" = '" + email + "'")[0]; }
                    catch { a.Email = ""; }
                    string m = a.Email;
                    if (m == email) { lblHoofdBoekerEmailError.Visible = true; dubbeleUsername = true; }
                    //Het controleren van de andere velden.
                    g = tbEmail.Text;
                    a1 = tbEmail1.Text; a2 = tbEmail2.Text; a3 = tbEmail3.Text;
                    a4 = tbEmail4.Text; a5 = tbEmail5.Text;
                    if (g == a1 || g == a2 || g == a3 || g == a4 || g == a5 || a1 == a2 || a1 == a3 || a1 == a4 || a1 == a5 || a2 == a3 || a2 == a4 || a2 == a5 || a3 == a4 || a3 == a5 || a4 == a5)
                    {
                        lblDubbeleEmail.Visible = true; dubbeleUsername = true;
                    }
                }
                //Als er een gebruikersnaam niet uniek is worden
                //de gebruiker(s) niet toegevoegd aan de database.
                if (dubbeleUsername) { return; }
                lblValidation.ForeColor = Color.Black;
                lblValidation.Text = "Uw gegevens zijn geldig.";
                //Eerst wordt er geprobeerd om de persoon toe te voegen aan de database.
                //Als dit is gelukt wordt er voor de hoofdboeker en iedere extra bezoeker een account toegevoegd.
                if (db.AddData(@"INSERT INTO persoon (""voornaam"", ""tussenvoegsel"", ""achternaam"", ""straat"", ""huisnr"", ""woonplaats"", ""banknr"") VALUES ('" + tbGebruikersnaam.Text + "','" + tbTussenvoegsel.Text + "','" + tbAchternaam.Text + "','" + tbStraat.Text + "','" + tbHuisnummer.Text + "','" + tbWoonplaats.Text + "','" + tbBankrekening.Text + "')"))
                {
                    //Het genereren van een wilekeurige hash
                    var chars = "abcdef0123456789";
                    var stringChars = new char[32];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    string activatiehash = new String(stringChars);
                    //Het toevoegen van het account van de hoofdboeker
                    db.AddData(@"INSERT INTO account (""gebruikersnaam"", ""email"", ""activatiehash"", ""geactiveerd"") VALUES ('" + tbGebruikersnaam.Text + "','" + tbEmail.Text + "','" + activatiehash + "',0)");
                    for (int i = 1; i < Convert.ToInt32(ddlAantal.Text); i++)
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
                        switch (i)
                        {
                            case 1: username = tbAccount1.Text; email = tbEmail1.Text;
                                break;
                            case 2: username = tbAccount2.Text; email = tbEmail2.Text;
                                break;
                            case 3: username = tbAccount3.Text; email = tbEmail3.Text;
                                break;
                            case 4: username = tbAccount4.Text; email = tbEmail4.Text;
                                break;
                            case 5: username = tbAccount5.Text; email = tbEmail5.Text;
                                break;
                            default:
                                break;
                        }
                        db.AddData(@"INSERT INTO account (""gebruikersnaam"", ""email"", ""activatiehash"", ""geactiveerd"") VALUES ('" + username + "','" + email + "','" + activatiehash + "',0)");
                    }
                    Response.Redirect("CampeerplekZoeken.aspx");
                }
                else
                {
                    lblValidation.Text = "Account(s) konden niet worden toegevoegd.";
                }
            }
            else
            {
                lblValidation.ForeColor = Color.Red;
                lblValidation.Text = "Uw gegevens zijn ongeldig!";
            }
        }

        protected void ddlAantal_TextChanged(object sender, EventArgs e)
        {
            lblAccount1.Visible = false; tbAccount1.Visible = false; tbAccount1.Text = "-1"; rfvAccount1.Enabled = false; lblEmail1.Visible = false; tbEmail1.Visible = false; tbEmail1.Text = "-1"; rfvEmail1.Enabled = false; revEmail1.Enabled = false;
            lblAccount2.Visible = false; tbAccount2.Visible = false; tbAccount2.Text = "-2"; rfvAccount2.Enabled = false; lblEmail2.Visible = false; tbEmail2.Visible = false; tbEmail2.Text = "-2"; rfvEmail2.Enabled = false; revEmail2.Enabled = false;
            lblAccount3.Visible = false; tbAccount3.Visible = false; tbAccount3.Text = "-3"; rfvAccount3.Enabled = false; lblEmail3.Visible = false; tbEmail3.Visible = false; tbEmail3.Text = "-3"; rfvEmail3.Enabled = false; revEmail3.Enabled = false;
            lblAccount4.Visible = false; tbAccount4.Visible = false; tbAccount4.Text = "-4"; rfvAccount4.Enabled = false; lblEmail4.Visible = false; tbEmail4.Visible = false; tbEmail4.Text = "-4"; rfvEmail4.Enabled = false; revEmail4.Enabled = false;
            lblAccount5.Visible = false; tbAccount5.Visible = false; tbAccount5.Text = "-5"; rfvAccount5.Enabled = false; lblEmail5.Visible = false; tbEmail5.Visible = false; tbEmail5.Text = "-5"; rfvEmail5.Enabled = false; revEmail5.Enabled = false;
            int aantal = Convert.ToInt32(ddlAantal.Text);
            for (int i = 0; i < aantal; i++)
            {
                if (i == 1) { lblAccount1.Visible = true; tbAccount1.Visible = true; tbAccount1.Text = ""; rfvAccount1.Enabled = true; lblEmail1.Visible = true; tbEmail1.Visible = true; tbEmail1.Text = ""; rfvEmail1.Enabled = true; revEmail1.Enabled = true; }
                if (i == 2) { lblAccount2.Visible = true; tbAccount2.Visible = true; tbAccount2.Text = ""; rfvAccount2.Enabled = true; lblEmail2.Visible = true; tbEmail2.Visible = true; tbEmail2.Text = ""; rfvEmail2.Enabled = true; revEmail2.Enabled = true; }
                if (i == 3) { lblAccount3.Visible = true; tbAccount3.Visible = true; tbAccount3.Text = ""; rfvAccount3.Enabled = true; lblEmail3.Visible = true; tbEmail3.Visible = true; tbEmail3.Text = ""; rfvEmail3.Enabled = true; revEmail3.Enabled = true; }
                if (i == 4) { lblAccount4.Visible = true; tbAccount4.Visible = true; tbAccount4.Text = ""; rfvAccount4.Enabled = true; lblEmail4.Visible = true; tbEmail4.Visible = true; tbEmail4.Text = ""; rfvEmail4.Enabled = true; revEmail4.Enabled = true; }
                if (i == 5) { lblAccount5.Visible = true; tbAccount5.Visible = true; tbAccount5.Text = ""; rfvAccount5.Enabled = true; lblEmail5.Visible = true; tbEmail5.Visible = true; tbEmail5.Text = ""; rfvEmail5.Enabled = true; revEmail5.Enabled = true; }
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

        }
    }
}