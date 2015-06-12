using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace SocialMediaSharing
{
    public partial class Form1 : Form
    {
        //Fields.
        string filePath;
        string fileExtention;
        bool loggin = false;
        string gebruikersnaam;
        string wachtwoord;
        string voornaam;
        string achernaam;
        int bezoekerid;
        int submapid;
        bool submap = false;
        string mapnaam;

        //Voor de database.
        int teller = 0;
        int dezevariablevoordedatabase;
        private OracleConnection conn;

        //Voor het tekenen en oproepen van alle objecten.
        GroupBox gbUser;
        GroupBox gbMappen;
        Button btn;
        Button btn2;
        Button btnBack;
        TextBox tbBericht;
        TextBox tbMapNaam;
        Panel panelOther;

        //Lists en objecten van de klassen.
        List<Bezoeker> bezoekers = new List<Bezoeker>();
        List<Map> mappen = new List<Map>();
        Bericht bericht = new Bericht();
        Mediasharing mediasharing = new Mediasharing();

        //Lists waar alle dynamische buttons in staan.
        List<Button> buttonss = new List<Button>();
        List<Control> dynControls = new List<Control>();

        public Form1()
        {
            InitializeComponent();
            conn = new OracleConnection();
            ConnectDatabase();
        }

        /// <summary>
        /// Methode die wordt aangeroepen bij de initializatie van het formulier.
        /// Deze methode dient voor de communicatie met de database.
        /// </summary>
        public void ConnectDatabase()
        {
            try
            {
                String user = "dbi324575"; //gebruikersnaam van de database.
                String pw = "YT4Yr6gF81"; //wachtwoord van de database.
                conn.ConnectionString = "User Id=" + user + ";Password=" + pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";  //string om verbinding te maken.
                conn.Open(); //opent connectie met de Connectionstring die voor deze connectie is ingesteld.
                MessageBox.Show("Connectie gelukt!");
            }
            catch { MessageBox.Show("Kon geen connectie maken"); }

            try
            {
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie.
                cmd.CommandType = CommandType.Text; //commandtype instellen.
                cmd.CommandText = "SELECT toegankelijkheid, accountnaam, accountwachtwoord, bezoekerID, voornaam, achternaam FROM bezoeker"; //De querie.
                OracleDataReader dr = cmd.ExecuteReader(); //een OracleDataReader aanmaken en deze linken aan het command die zojuist aangemaakt is.
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Bezoeker b = new Bezoeker(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetInt32(3), dr.GetString(4), dr.GetString(5));
                    bezoekers.Add(b);
                }
                dr.Close();           
            }
            catch { conn.Close(); MessageBox.Show("Geen gegevens in de database gevonden."); }
        }

        private void btnInloggen_Click(object sender, EventArgs e)
        {
            gebruikersnaam = tbUsername.Text;
            wachtwoord = tbPassword.Text;
            foreach (Bezoeker b in bezoekers)
            {
                if (b.AccountNaam == gebruikersnaam && b.AccountWachtwoord == wachtwoord)
                {
                    //Hier wordt er gekeken of de desbetreffende persoon 
                    //bevoegdheid heeft em de applicatie te gebruiken.
                    this.loggin = true;
                    if (b.Toegankelijkheid == "A" || b.Toegankelijkheid == "E" || b.Toegankelijkheid == "F")
                    {
                        bezoekerid = b.BezoekerID;
                        //Hoofdmappen laden.
                        try
                        {
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT mapID, bezoekerID, mapnaam FROM mediamap WHERE submapID IS NULL";

                            OracleDataReader dar = cmd.ExecuteReader();
                            while (dar.Read())
                            {
                                Map m = new Map(dar.GetInt32(1), dar.GetInt32(0), dar.GetString(2));
                                mappen.Add(m);
                            }
                            dezevariablevoordedatabase = mappen.Count();
                            dar.Close(); //Sluit de reader.
                            cmd.Dispose(); //verwijdert command.
                        }
                        catch (OracleException ex) { 
                            MessageBox.Show("geen gegevens gevonden in de database 2.0" + ex.Message); }

                        //Als het inloggen gelukt is wordt hierna
                        //het dynamisch form getekend.
                        maakHome();
                        mappenAanmaken();
                    }
                    else
                    {
                        MessageBox.Show("U hebt helaas geen toegang tot deze applicatie.");
                    }
                }
            }
            if (loggin == false)
            {
                MessageBox.Show("Ongeldige gebruikersnaam/wachtwoord.");
            }
            loggin = false;            
        }

        /// <summary>
        /// Zorgt ervoor dat het mogelijk is om bestanden te uploaden.
        /// Het pad van het bestand wordt hiermee opgevraagd.
        /// </summary>
        /// <param name="sender">De button "uploaden"</param>
        /// <param name="e">Path van de file</param>
        public void btnUploaden_Click(object sender, EventArgs e)
        {
            if (Uploaden.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Dit is het pad naar de file:   " + Path.GetFullPath(Uploaden.FileName) + "\n" + "De extensie is:   " + Path.GetExtension(Uploaden.FileName));
                filePath = Path.GetFullPath(Uploaden.FileName);
                fileExtention = Path.GetExtension(Uploaden.FileName);
            }
        }

        /// <summary>
        /// Plaats waar de berichten in de behorende map komen.
        /// Dit zijn altijd rely`s op een hetzelfde bestand.
        /// Je kunt dus geen commentaar geven op een reply van iemand anders.
        /// </summary>
        /// <param name="sender">De button "bericht plaatsen"</param>
        /// <param name="e">String met het bericht</param>
        public void btnBerichtplaatsen_Click(object sender, EventArgs e)
        {
            if(tbBericht.Text != "")
            {
                bericht.BerichtPlaatsen("mediaID uit database", tbBericht.Text);
                MessageBox.Show("Uw bericht: " + tbBericht.Text + "\n" + "is geplaatst.");
            }
            else
            {
                MessageBox.Show("U dient eerst een bericht te typen.");
            }
        }

        /// <summary>
        /// Methode die ervoor zorgt dat alle mappen op ieder gewenst moment getekend kunnen worden.
        /// Hij zorgt o.a voor het verversen van de mappen.
        /// </summary>
        /// <param name="sender">Iedere button die als "map" dient</param>
        /// <param name="e">Lijst met de juiste te tekenen mapepen</param>
        public void btnMapAanmaken_Click(object sender, EventArgs e)
        {
            //Als eerste wordt er gekeken wat de naam van de map is.
            //Dit veld mag niet leeg zijn en zijn waarde moet uniek zijn.
            //Verder mag het niet zo zijn dat de map aangemaakt wordt in de hoofdsectie.
            string mapnaampje = "";
            try { mapnaampje = tbMapNaam.Text; }
            catch { MessageBox.Show("Vul een mapnaam in!"); }
            bool gevonden = false;
            foreach (Map m in mappen)
            {
                if (m.MapNaam == mapnaampje && gevonden != true || mapnaampje == "" && gevonden != true )
                {
                    MessageBox.Show("Deze naam is al bezet of je hebt een geen naam ingevuld");
                    gevonden = true;
                }
            }
            if (gevonden == true)
            {
                gevonden = false;
            }
            else
            {
                if (submapid == 0)
                {
                    MessageBox.Show("Je kunt hier geen mappen maken");
                }
                else
                {
                    //Als de map aan alle bovenstaande voorwaares voldoet wordt de map aan de database toegevoegd.
                    //Het bezoekerid werd bijgehouden vlak na het inloggen.
                    //De submapid is berekend door te kijken in welke folder je zit voordat je op de "Maak map aan" button klikt.
                    OracleCommand cmd = conn.CreateCommand();
                    try
                    {
                        mappen.Clear();
                        OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO MEDIAMAP (bezoekerID, submapID, mapnaam) VALUES (" + bezoekerid + "," + submapid + ", '" + mapnaampje + "')";
                        cmd.ExecuteNonQuery();
                        otn.Commit();
                    }
                    catch (OracleException ex) { MessageBox.Show("Error: " + ex.Message); }

                    //Hier wordt de niewe record in de database opgehaald en toegevoegd in de lijst.
                    OracleCommand cmdje = conn.CreateCommand();
                    cmdje.CommandType = CommandType.Text;
                    cmdje.CommandText = "SELECT mapID, bezoekerID, mapnaam FROM mediamap WHERE submapID = " + submapid;

                    OracleDataReader dar = cmdje.ExecuteReader();
                    while (dar.Read())
                    {
                        Map m = new Map(dar.GetInt32(1), dar.GetInt32(0), dar.GetString(2));
                        mappen.Add(m);
                    }
                    dar.Close();
                    cmd.Dispose();

                    //Daarna wordt de map ververst en verschijnt de nieuw gemaakte knop meteen in het form.
                    //Hier wordt er ook voor gezorgd dat de groupbox steeds groter wordt naarmate er meer knoppen komen.
                    dezevariablevoordedatabase++;
                    gbMappen.Controls.Clear();
                    maakBackButton();
                    gbMappen.Size = new Size(500, 65 * mappen.Count + 30);
                    mappenAanmaken();
                }               
            }
        }

        /// <summary>
        /// Zorgt ervoor dat als er op een map wordt geklikt, 
        /// de inhoud van de desbetreffende map wordt geladen.
        /// </summary>
        /// <param name="sender">Alle LINKER buttons in de mappenstructuur</param>
        /// <param name="e">Geeft een lijst met submappen in de map</param>
        public void btn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            MessageBox.Show(button.Name);
            mapnaam = button.Name;
            submap = true;
            MappenGenereren();
            gbMappen.Size = new Size(500, 60 * mappen.Count + 100);
            submap = false;
        }

        /// <summary>
        /// Zorgt ervoor dat als er op een map wordt geklikt, 
        /// de inhoud van de desbetreffende map wordt geladen.
        /// </summary>
        /// <param name="sender">Alle RECHTER buttons in de mappenstructuur</param>
        /// <param name="e">Geeft een lijst met submappen in de map</param>
        public void btn2_Click(object sender, EventArgs e)
        {
            Button button2 = sender as Button;
            MessageBox.Show(button2.Name);
            mapnaam = button2.Name;
            submap = true;
            MappenGenereren();
            gbMappen.Size = new Size(500, 60 * mappen.Count + 100);
            submap = false;
        }

        /// <summary>
        /// Maakt de 'back button' die ervoor zorgt dat je weer terug kan gaan naar de hoofdmap.
        /// Je kan alleen helemaal terug gaan naar de "startmappen".
        /// </summary>
        /// <param name="sender">De button "back"</param>
        /// <param name="e">Maakt de 'back button' zichtbaar</param>
        public void btnBack_Click(object sender, EventArgs e)
        {
            submapid = 0;
            MappenGenereren();
            gbMappen.Size = new Size(500, 60 * mappen.Count + 100);
            gbMappen.Controls.Remove(btnBack);
        }

        /// <summary>
        /// Deze methode zorgt ervoor om de juiste mappen te genereren.
        /// Er wordt gekeken in welke (sub)map je bevind waarna de juiste mappen worden geladen.
        /// Aan het einde van de methode wordt er opgeroepen om de knoppen te tekenen.
        /// </summary>
        public void MappenGenereren()
        {
            //submap is true als je verder in de mappen gaat.
            gbMappen.Controls.Clear();
            if(submap)
            {
                mappen.Clear();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT m.mapID, m.bezoekerID, m.mapnaam FROM mediamap m, mediamap ma WHERE m.submapID = ma.mapID AND ma.mapnaam = '" + mapnaam + "'";
           
                OracleDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    Map m = new Map(dar.GetInt32(1), dar.GetInt32(0), dar.GetString(2));
                    mappen.Add(m);
                    submapid = dar.GetInt32(0);
                }
                dezevariablevoordedatabase = mappen.Count();
                dar.Close();
                cmd.Dispose();
            }
            //submap is false als terug gaat naar de "hoofdmappen".
            else
            {
                mappen.Clear();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT mapID, bezoekerID, mapnaam FROM mediamap WHERE submapID IS NULL";
       
                OracleDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    Map m = new Map(dar.GetInt32(1), dar.GetInt32(0), dar.GetString(2)); ;
                    mappen.Add(m);
                }
                submapid = 0;
                dezevariablevoordedatabase = mappen.Count();
                dar.Close();
                cmd.Dispose();
            }
            mappenAanmaken();
            maakBackButton();
        }

        //Gaat terug naar het inlogscherm.
        public void maakLogUitButton()
        {
            Button btnLogUit = new Button();
            btnLogUit.Location = new Point(1110, 10);
            btnLogUit.Size = new Size(60, 50);
            btnLogUit.Text = "Log uit";
            this.Controls.Add(btnLogUit);
            btnLogUit.Click += new EventHandler(this.btnLogUit_Click);
            dynControls.Add(btnLogUit);
        }

        //Dit is de button waarmee je terug naar de "hoofdmappen" kunt gaan.
        public void maakBackButton()
        {
            btnBack = new Button();
            btnBack.Location = new Point(10, 16);
            btnBack.Size = new Size(35, 30);
            btnBack.Text = "<";
            btnBack.Click += new EventHandler(this.btnBack_Click);
            gbMappen.Controls.Add(btnBack);
        }

        //Maakt het gehele scherm leeg.
        //Wordt aangeroepen bij het uitloggen.
        public void maakLeeg()
        {
            foreach (Control c in dynControls)
            {
                this.Controls.Remove(c);
            }
        }


        /// <summary>
        /// Dynamische methode die ervoor zorgt dat alle mappen op de juiste plek worden getekend.
        /// Dit wordt gedaan dmv een loop die door een lijst loopt waarin de mappen staan die getekend moeten worden.
        /// Deze lijst wordt op andere plaatsen gegenereerd.
        /// </summary>
        public void mappenAanmaken()
        {
            //Voor als er exact 1 map in de submap zit.
            if (dezevariablevoordedatabase == 1)
            {
                int teller = 0;
                string dynamischetekst = mappen[teller].MapNaam;
                btn = new Button();
                btn.Name = dynamischetekst;
                btn.Text = dynamischetekst;
                btn.Tag = 0;
                btn.Location = new Point(50, 50 + 100 * 0);
                btn.Size = new Size(180, 50);
                gbMappen.Controls.Add(btn);
                btn.Click += new EventHandler(this.btn_Click);
                buttonss.Add(btn);
                dynControls.Add(btn);
            }
            else if (dezevariablevoordedatabase % 2 == 0) //Dan is er sprake van een even aantal mappen.
            {
                int teller = 0;
                for (int i = 0; i < dezevariablevoordedatabase / 2; i++)
                {
                    string dynamischetekst = mappen[teller].MapNaam;
                    btn = new Button();
                    btn.Name = dynamischetekst;
                    btn.Text = dynamischetekst;
                    btn.Tag = i;
                    btn.Location = new Point(50, 50 + 100 * i);
                    btn.Size = new Size(180, 50);
                    gbMappen.Controls.Add(btn);
                    btn.Click += new EventHandler(this.btn_Click);
                    buttonss.Add(btn);
                    dynControls.Add(btn);

                    teller++;
                    dynamischetekst = mappen[teller].MapNaam;
                    btn2 = new Button();
                    btn2.Name = dynamischetekst;
                    btn2.Text = dynamischetekst;
                    btn2.Location = new Point(300, 50 + 100 * i);
                    btn2.Size = new Size(180, 50);
                    gbMappen.Controls.Add(btn2);
                    btn2.Click += new EventHandler(this.btn2_Click);
                    buttonss.Add(btn2);
                    dynControls.Add(btn2);
                    teller++;
                }
            }
            else //Dan is er sprake van een oneven aantal mappen.
            {
                teller = 0;
                for (int i = 0; i < (dezevariablevoordedatabase - 1) / 2; i++)
                {
                    string dynamischetekst = mappen[teller].MapNaam;
                    btn = new Button();
                    btn.Name = dynamischetekst;
                    btn.Text = dynamischetekst;
                    btn.Location = new Point(50, 50 + 100 * i);
                    btn.Size = new Size(180, 50);
                    gbMappen.Controls.Add(btn);
                    btn.Click += new EventHandler(this.btn_Click);
                    buttonss.Add(btn);
                    dynControls.Add(btn);

                    teller++;
                    dynamischetekst = mappen[teller].MapNaam;
                    btn2 = new Button();
                    btn2.Name = dynamischetekst;
                    btn2.Text = dynamischetekst;
                    btn2.Location = new Point(300, 50 + 100 * i);
                    btn2.Size = new Size(180, 50);
                    gbMappen.Controls.Add(btn2);
                    btn2.Click += new EventHandler(this.btn2_Click);
                    buttonss.Add(btn2);
                    dynControls.Add(btn2);
                    teller++;
                }
                string dynamischetekstje = mappen[teller].MapNaam;
                btn = new Button();
                btn.Name = dynamischetekstje;
                btn.Text = dynamischetekstje;
                btn.Location = new Point(50, 50 + 100 * (dezevariablevoordedatabase - 1) / 2);
                btn.Size = new Size(180, 50);
                gbMappen.Controls.Add(btn);
                btn.Click += new EventHandler(this.btn_Click);
                buttonss.Add(btn);
                dynControls.Add(btn);
            }
        }

        /// <summary>
        /// Deze functie tekend de gehele interface.
        /// Hij wordt alleen direct na het inloggen aangeroepen.
        /// </summary>
        public void maakHome()
        {
            //Loop die de voornaam en achernaam van de ingelogde 
            //bezoeker opvraagt en die in een label stopt.
            foreach (Bezoeker b in bezoekers)
            {
                if (b.AccountNaam == gebruikersnaam)
                {
                    voornaam = b.VoorNaam;
                    achernaam = b.Achternaam;
                }
            }
            Panel panelUser = new Panel();
            panelUser.Location = new Point(3, 3);
            panelUser.Size = new Size(210, 630);
            this.Controls.Add(panelUser);
            dynControls.Add(panelUser);

            panelOther = new Panel();
            panelOther.Location = new Point(20, 10);
            panelOther.Size = new Size(750, 700);
            panelOther.AutoScroll = true;
            this.Controls.Add(panelOther);
            dynControls.Add(panelOther);

            gbLogin.Visible = false;
            gbUser = new GroupBox();
            gbUser.Location = new Point(10, 10);
            gbUser.Size = new Size(200, 600);
            gbUser.Text = "User";
            gbUser.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            panelUser.Controls.Add(gbUser);
            dynControls.Add(gbUser);

            gbMappen = new GroupBox();
            gbMappen.Location = new Point(220, 5);
            gbMappen.Size = new Size(500, 60 * mappen.Count + 100);
            gbMappen.Text = "Mappen";
            panelOther.Controls.Add(gbMappen);

            Label lblName = new Label();
            lblName.Location = new Point(20, 25);
            lblName.Size = new Size(150, 40);
            lblName.Text = voornaam + " " + achernaam;
            gbUser.Controls.Add(lblName);

            maakLogUitButton();

            Button btnUploaden = new Button();
            btnUploaden.Location = new Point(900, 100);
            btnUploaden.Size = new Size(80, 40);
            btnUploaden.Text = "Uploaden";
            btnUploaden.Click += new EventHandler(this.btnUploaden_Click);
            this.Controls.Add(btnUploaden);
            dynControls.Add(btnUploaden);

            tbBericht = new TextBox();
            tbBericht.Location = new Point(820, 500);
            tbBericht.Size = new Size(250, 100);
            this.Controls.Add(tbBericht);
            dynControls.Add(tbBericht);

            Button btnBerichtPlaatsen = new Button();
            btnBerichtPlaatsen.Location = new Point(820, 550);
            btnBerichtPlaatsen.Size = new Size(100, 50);
            btnBerichtPlaatsen.Text = "Plaats bericht";
            btnBerichtPlaatsen.Click += new EventHandler(this.btnBerichtplaatsen_Click);
            this.Controls.Add(btnBerichtPlaatsen);
            dynControls.Add(btnBerichtPlaatsen);

            Button btnMapAanmaken = new Button();
            btnMapAanmaken.Location = new Point(15, 540);
            btnMapAanmaken.Size = new Size(100, 50);
            btnMapAanmaken.Text = "Maak nieuwe map aan";
            btnMapAanmaken.Click += new EventHandler(this.btnMapAanmaken_Click);
            gbUser.Controls.Add(btnMapAanmaken);
            dynControls.Add(btnMapAanmaken);

            Label lblMapNaam = new Label();
            lblMapNaam.Location = new Point(15, 500);
            lblMapNaam.Size = new Size(60, 20);
            lblMapNaam.Text = "Map naam:";
            gbUser.Controls.Add(lblMapNaam);

            tbMapNaam = new TextBox();
            tbMapNaam.Location = new Point(75, 500);
            tbMapNaam.Size = new Size(100, 20);
            gbUser.Controls.Add(tbMapNaam);
        }

        //Het maken van de uitlog button.
        public void btnLogUit_Click(object sender, System.EventArgs e)
        {
            maakLeeg();
            gbLogin.Visible = true;
        }
    }
}
