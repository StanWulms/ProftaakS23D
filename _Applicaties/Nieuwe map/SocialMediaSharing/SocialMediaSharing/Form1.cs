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

        //blob 
        public string strImageName = "";
        private int _imageLength;
        private byte[] _imageData;
        byte[] data;
        //Fields.
        string filePath;
        string fileExtention;
        bool loggin = false;
        string gebruikersnaam;
        string wachtwoord;
        string voornaam;
        string achernaam;
        int bezoekerid;
        int huidigemapid;
        bool submap = false;
        bool dubbelepostnaam = false;
        string mapnaam;

        //Voor de database.
        int teller = 0;
        int dezevariablevoordedatabase;
        private OracleConnection conn;

        //Voor het tekenen en oproepen van alle objecten.
        GroupBox gbUser;
        GroupBox gbMappen;
        GroupBox gbPost;
        GroupBox gbPostHandler;
        Button btn;
        Button btn2;
        Button btnBack;
        TextBox tbBericht;
        TextBox tbMapNaam;
        TextBox tbPostnaam;
        ListBox lbPosts;
        Panel panelOther;
        Panel panelPost;
        Panel panelPostHandler;

        //Lists en objecten van de klassen.
        List<Bezoeker> bezoekers = new List<Bezoeker>();
        List<Map> mappen = new List<Map>();
        List<Post> posts = new List<Post>();
        List<Afbeelding> afbeeldingen = new List<Afbeelding>();
        Bericht bericht = new Bericht();
        Mediasharing mediasharing = new Mediasharing();
        Post p = new Post();

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
                String pw = "GBNITclsn2"; //wachtwoord van de database.
                //String user = "dbi323305";
                //String pw = "dRuklSz8nY";
                
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
            //hier neergezet omdat ik het wilde testen, moet natuurlijk ergens anders --> tot //BLOB

            /*try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT mapID, utl_raw.cast_to_varchar2(dbms_lob.substr(bestand)) FROM POST";

                OracleDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    try
                    {
                        Afbeelding a = new Afbeelding(dar.GetInt32(0), dar.GetString(1));
                        afbeeldingen.Add(a);
                        

                    }
                    catch
                    {
                        MessageBox.Show("afbeelingen Failed.");
                    }


                }
                dar.Close(); //Sluit de reader.
                cmd.Dispose(); //verwijdert command.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
            //BLOB
            
            // Picture box onder de naam gepositioneerd
            PictureBox pbPhoto = new PictureBox();
            pbPhoto.Location = new Point(30, 100);
            pbPhoto.Size = new Size(50, 50);
            gbUser.Controls.Add(pbPhoto);
            
            // how to get picture in picture box from path
            //pbPhoto.Image = System.Drawing.Bitmap.FromFile("het path uit de list met afbeeldingen");

            // Instantiate File Dialog box
            FileDialog fileDlg = new OpenFileDialog();

            // Set the initial directory
            fileDlg.InitialDirectory =
            "C:\\";

            // Filter image(.jpg, .bmp, .gif) files only
            fileDlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";

            // Restores the current directory before closing
            fileDlg.RestoreDirectory = true;

            // When file is selected from the File Dialog
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                //Path of the selected file
                filePath = Path.GetFullPath(fileDlg.FileName);

                // Store the name of selected file into a variable
                strImageName = fileDlg.FileName;

                // Create a bitmap for selected image
                Bitmap newImage = new Bitmap(strImageName);

                // Fit the image to the size of picture box
                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                // Show the bitmap in picture box
                pbPhoto.Image = (Image)newImage;   
            }

            // No Image chosen
            fileDlg = null;

            FileStream fs;

            // Get Image Data from the Filesystem if User has loaded a Photo
            // by the 'Browse' button
            if (strImageName != "")
            {
                fs = new FileStream(@strImageName, FileMode.Open, FileAccess.Read);
                _imageLength = (int)fs.Length;

                // Create a byte array of file stream length
                _imageData = new byte[fs.Length];

                // Read block of bytes from stream into the byte array
                fs.Read(_imageData, 0, System.Convert.ToInt32(fs.Length));

                // Close the File Stream
                fs.Close();
            }

            try
            {
                OracleCommand cmd = conn.CreateCommand();

                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                string empty = "";
                cmd.CommandText = "INSERT INTO POST (bezoekerID, mapID, postnaam, tekst, bestand) VALUES (" + bezoekerid + ", " + huidigemapid + ", '" + "afbeelding" + "', '" + empty + "', utl_raw.cast_to_raw('" + filePath + "'))";


                cmd.ExecuteNonQuery();
                otn.Commit();
               
                //**
                //Initialize OracleCommand object for insert.
                cmd = new OracleCommand(cmd.CommandText, conn);
               
                //Open connection and execute insert query.
                //conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Image added to blob field");
                cmd.Dispose();
                //conn.Close();
                //this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;

        }

        /// <summary>
        /// Zorgt dat een geselecteerd bericht gedownload word.
        /// </summary>
        /// <param name="sender">De button "Download".</param>
        /// <param name="e">Slaat een geselecteerd bestand op in je local files.</param>
        public void btnDownloaden_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Plaats waar de berichten in de behorende map komen.
        /// Dit zijn altijd rely`s op een hetzelfde bestand.
        /// Je kunt dus geen commentaar geven op een reply van iemand anders.
        /// </summary>
        /// <param name="sender">De button "bericht plaatsen"</param>
        /// <param name="e">String met het bericht</param>
        public void btnPostMaken_Click(object sender, EventArgs e)
        {
            foreach (Post p in posts)
            {
                if (p.PostNaam == tbPostnaam.Text)
                {
                    MessageBox.Show("Deze postnaam is al gebruikt in deze map. \nVul een niewe postnaam in.");
                    tbPostnaam.Clear();
                    dubbelepostnaam = true;
                }
            }
            if (!dubbelepostnaam)
            {
                if (tbPostnaam.Text != "")
                {
                    OracleCommand cmd = conn.CreateCommand();
                    try
                    {
                        //TODO: empty moet een bestand worden, kan later toegevoegd worden.
                        string empty = "";
                        OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO POST (bezoekerID, mapID, postnaam, tekst, bestand) VALUES (" + bezoekerid + ", " + huidigemapid + ", '" + tbPostnaam.Text + "', '" + tbBericht.Text + "', '" + empty + "')";
                        cmd.ExecuteNonQuery();
                        otn.Commit();
                        PostHandler(false);
                        //MessageBox.Show("Uw Post: " + tbPostnaam.Text + "\n" + "is gemaakt.");
                    }
                    catch (OracleException ex) { MessageBox.Show("Error: " + ex.Message); }
                }
                else
                {
                    MessageBox.Show("U dient eerst een bericht te typen.");
                }
            }
            dubbelepostnaam = false;
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
                if (huidigemapid == 0)
                {
                    MessageBox.Show("Je kunt hier geen mappen maken");
                }
                else
                {
                    //Als de map aan alle bovenstaande voorwaares voldoet wordt de map aan de database toegevoegd.
                    //Het bezoekerid werd bijgehouden vlak na het inloggen.
                    //De huidigemapid is berekend door te kijken in welke folder je zit voordat je op de "Maak map aan" button klikt.
                    OracleCommand cmd = conn.CreateCommand();
                    try
                    {
                        mappen.Clear();
                        OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO MEDIAMAP (bezoekerID, submapID, mapnaam) VALUES (" + bezoekerid + "," + huidigemapid + ", '" + mapnaampje + "')";
                        cmd.ExecuteNonQuery();
                        otn.Commit();
                    }
                    catch (OracleException ex) { MessageBox.Show("Error: " + ex.Message); }

                    //Hier wordt de niewe record in de database opgehaald en toegevoegd in de lijst.
                    OracleCommand cmdje = conn.CreateCommand();
                    cmdje.CommandType = CommandType.Text;
                    cmdje.CommandText = "SELECT mapID, bezoekerID, mapnaam FROM mediamap WHERE submapID = " + huidigemapid;

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
                    gbMappen.Size = new Size(500, 65 * mappen.Count + 50);
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
            huidigemapid = 0;
            mapnaam = "";
            MappenGenereren();
            PostHandler(true);
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
                int testid = huidigemapid;
                mappen.Clear();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT m.mapID, m.bezoekerID, m.mapnaam, ma.mapID FROM mediamap m, mediamap ma WHERE m.submapID = ma.mapID AND ma.mapnaam = '" + mapnaam + "'";
           
                OracleDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    Map m = new Map(dar.GetInt32(1), dar.GetInt32(0), dar.GetString(2));
                    mappen.Add(m);
                    huidigemapid = dar.GetInt32(3);
                }
                dar.Close();
                cmd.Dispose();
                if (testid == huidigemapid)
                {
                    OracleCommand cmdtje = conn.CreateCommand();
                    cmdtje.CommandType = CommandType.Text;
                    cmdtje.CommandText = "SELECT mapID FROM mediamap WHERE submapID = " + testid;
                    OracleDataReader dr = cmdtje.ExecuteReader();
                    dr.Read();
                    huidigemapid = dr.GetInt32(0);
                    dr.Close();
                }
                gbMappen.Text = mapnaam + " " + huidigemapid;
                dezevariablevoordedatabase = mappen.Count();
                PostHandler(false);
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
                    Map m = new Map(dar.GetInt32(1), dar.GetInt32(0), dar.GetString(2));
                    gbMappen.Text = "Mappen";
                    mappen.Add(m);
                }
                huidigemapid = 0;
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
        /// Laat de posts in de map waar je naar toe navigeert.
        /// Wordt ook aangeroepen om de posts te verversen na het maken van een nieuwe.
        /// </summary>
        /// <param name="Menu">Is true als je in de hoofdmappen bent.</param>
        public void PostHandler(bool Menu)
        {
            if (!Menu)
            {
                posts.Clear();
                lbPosts.Items.Clear();
                lbPosts.Visible = true;
                //Ophalen van alle Posts in de desbetreffende map
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT p.postID, p.bezoekerID, b.accountnaam, p.mapID, p.postnaam, p.tekst, p.bestand FROM post p, bezoeker b WHERE p.bezoekerID = b.bezoekerID AND p.mapID = " + huidigemapid;

                string tekst;
                string pad;

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    try { tekst = dr.GetString(5); }
                    catch { tekst = ""; }
                    try { pad = dr.GetString(6); }
                    catch { pad = ""; }
                    p = new Post(/*dr.GetInt32(0),*/ dr.GetInt32(1), dr.GetString(2), dr.GetInt32(3), dr.GetString(4), tekst, pad);
                    posts.Add(p);
                }
                dr.Close();
                cmd.Dispose();
                foreach (Post post in posts)
                {
                    lbPosts.Items.Add(post);
                }
            }
            else
            {
                lbPosts.Items.Clear();
                lbPosts.Visible = false;
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

            //Panels

            Panel panelUser = new Panel();
            panelUser.Location = new Point(3, 3);
            panelUser.Size = new Size(210, 630);
            this.Controls.Add(panelUser);
            dynControls.Add(panelUser);

            panelOther = new Panel();
            panelOther.Location = new Point(20, 10);
            panelOther.Size = new Size(725, 700);
            panelOther.AutoScroll = true;
            this.Controls.Add(panelOther);
            dynControls.Add(panelOther);

            panelPost = new Panel();
            panelPost.Location = new Point(750, 10);
            panelPost.Size = new Size(348, 440);
            panelPost.AutoScroll = true;
            this.Controls.Add(panelPost);
            dynControls.Add(panelPost);

            panelPostHandler = new Panel();
            panelPostHandler.Location = new Point(750, 458);
            panelPostHandler.Size = new Size(348, 200);
            panelPostHandler.AutoScroll = true;
            this.Controls.Add(panelPostHandler);
            dynControls.Add(panelPostHandler);

            //Groupboxes

            gbPost = new GroupBox();
            gbPost.Location = new Point(18, 5);
            gbPost.Size = new Size(330, 435);
            gbPost.Text = "Posts";
            panelPost.Controls.Add(gbPost);

            gbPostHandler = new GroupBox();
            gbPostHandler.Location = new Point(18, 5);
            gbPostHandler.Size = new Size(330, 195);
            gbPostHandler.Text = "Post Handeler";
            panelPostHandler.Controls.Add(gbPostHandler);

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

            //Posts

            lbPosts = new ListBox();
            lbPosts.Name = "lbPosts";
            lbPosts.Location = new Point(20, 20);
            lbPosts.Size = new Size(290, 400);
            lbPosts.Visible = false;
            lbPosts.DoubleClick += new EventHandler(this.lbPosts_DoubleClick);
            gbPost.Controls.Add(lbPosts);


            //PostHandler
            //TODO: STAN HALP D:

            Button btnUploaden = new Button();
            btnUploaden.Location = new Point(15, 140);
            btnUploaden.Size = new Size(95, 40);
            btnUploaden.Text = "Uploaden";
            btnUploaden.Click += new EventHandler(this.btnUploaden_Click);
            gbPostHandler.Controls.Add(btnUploaden);
            //this.Controls.Add(btnUploaden);
            dynControls.Add(btnUploaden);

            Button btnDownloaden = new Button();
            btnDownloaden.Location = new Point(110, 140);
            btnDownloaden.Size = new Size(95, 40);
            btnDownloaden.Text = "Downloaden";
            btnDownloaden.Click += new EventHandler(this.btnDownloaden_Click);
            gbPostHandler.Controls.Add(btnDownloaden);
            //gbPostHandler.Controls.Add(btnDownloaden);
            dynControls.Add(btnDownloaden);

            tbPostnaam = new TextBox();
            tbPostnaam.Location = new Point(120, 20);
            tbPostnaam.Size = new Size(200, 100);
            //gbPostHandler.Controls.Add(tbPostnaam);
            gbPostHandler.Controls.Add(tbPostnaam);
            dynControls.Add(tbPostnaam);

            Label lblPostNaam = new Label();
            lblPostNaam.Location = new Point(20, 20);
            lblPostNaam.Size = new Size(100, 20);
            lblPostNaam.Text = "Post Naam:";
            gbPostHandler.Controls.Add(lblPostNaam);

            tbBericht = new TextBox();
            tbBericht.Location = new Point(120, 65);
            tbBericht.Size = new Size(200, 100);
            gbPostHandler.Controls.Add(tbBericht);
            gbPostHandler.Controls.Add(tbBericht);
            dynControls.Add(tbBericht);

            Label lblBericht = new Label();
            lblBericht.Location = new Point(20, 65);
            lblBericht.Size = new Size(100, 20);
            lblBericht.Text = "Omschrijving:";
            gbPostHandler.Controls.Add(lblBericht);

            Button btnBerichtPlaatsen = new Button();
            btnBerichtPlaatsen.Location = new Point(205, 140);
            btnBerichtPlaatsen.Size = new Size(120, 40);
            btnBerichtPlaatsen.Text = "Plaats bericht";
            btnBerichtPlaatsen.Click += new EventHandler(this.btnPostMaken_Click);
            gbPostHandler.Controls.Add(btnBerichtPlaatsen);
            //this.Controls.Add(btnBerichtPlaatsen);
            dynControls.Add(btnBerichtPlaatsen);

            //Mappen

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

            //User

            Label lblName = new Label();
            lblName.Location = new Point(20, 25);
            lblName.Size = new Size(150, 40);
            lblName.Text = voornaam + " " + achernaam;
            gbUser.Controls.Add(lblName);

            maakLogUitButton();
        }

        //Het maken van de uitlog button.
        public void btnLogUit_Click(object sender, System.EventArgs e)
        {
            maakLeeg();
            mappen.Clear();
            gbLogin.Visible = true;
        }

        private void lbPosts_DoubleClick(object sender, EventArgs e)
        {
            lbPosts.Visible = false;

            Label lblPost = new Label();
            lblPost.Location = new Point(20, 100);
            lblPost.Size = new Size(200, 300);
            lblPost.Text = p.Bericht;
            gbPost.Controls.Add(lblPost);
        }

    }
}
