using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Phidgets;
using Phidgets.Events;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace MateriaalVerhuur
{
    public partial class Form1 : Form
    {
        private OracleConnection conn;
        List<Control> dynControls = new List<Control>();
        private RFID rfid;        
        bool RFIDscanned = false;
        string tag = "";
        Label bezNaam;
        string pagina = "";
        Label lbverhuurnaam;
        TextBox tbverhuur;
        ListBox lbVerhuurbareItems;
        List<voorwerpen> voorwerpjes;
        DateTimePicker dtbhuur;
        DateTimePicker dtbinlever;
        DateTimePicker dtbretour;
        ListBox lbGehuurdeItems;
        TextBox tbterug;
        TextBox tbbeschrijving;
        TextBox tbmerk;
        TextBox tbsoort;
        ComboBox cbsoort;
        List<int> verhuurnummers;
        string laatsterfid;
        bool loggin = false;
        string gebruikersnaam;
        string wachtwoord;
        string naam = "";
        List<Bezoeker> bezoekers = new List<Bezoeker>();
        public Form1()
        {
            InitializeComponent();
            rfid = new RFID();
            rfid.Attach += new AttachEventHandler(rfid_Attach);
            rfid.Detach += new DetachEventHandler(rfid_Detach);
            rfid.Tag += new TagEventHandler(rfid_Tag);
            openCmdLine(rfid);
            conn = new OracleConnection();
            voorwerpjes = new List<voorwerpen>();
            string user = "dbi324575";
            string pw = "YT4Yr6gF81";
            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();
            verhuurnummers = new List<int>();
             try
            {
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT toegankelijkheid, accountnaam, accountwachtwoord FROM bezoeker";

                OracleDataReader dr = cmd.ExecuteReader(); //een OracleDataReader aanmaken en deze linken aan het command dat je zojuist hebt gemaakt.
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Bezoeker b = new Bezoeker(dr.GetString(0), dr.GetString(1), dr.GetString(2));
                    bezoekers.Add(b);
                }
                
            }
            catch { MessageBox.Show("Geen gegevens in de database gevonden."); }
        }

        //button waarmee je kunt inloggen, hij controleert de toegankelijkheid of je er wel in mag en als je er in mag laad hij het home screen
        private void btnInloggen_Click(object sender, EventArgs e)
        {
            loggin = false;
            gebruikersnaam = tbUsername.Text;
            wachtwoord = tbPassword.Text;
            foreach (Bezoeker b in bezoekers)
            {
                if (b.AccountNaam == gebruikersnaam && b.AccountWachtwoord == wachtwoord)
                {
                    this.loggin = true;
                    //TODO: match toegangscode met behorende applicatie.
                    if (b.Toegankelijkheid == "B" || b.Toegankelijkheid == "E" || b.Toegankelijkheid == "F")
                    {
                        gbLogin.Visible = false;

                        terugHome();
                        //uitlenen.ShowDialog();
                        //this.Close();
                        OracleCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT e.exemplaarid, g.voorwerpsoort, g.VOORWERPMERK FROM EXEMPLAAR e, voorwerp g WHERE e.VOORWERPID = g.VOORWERPID";
                        OracleDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            voorwerpen voorwerp = new voorwerpen(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                            voorwerpjes.Add(voorwerp);
                        }
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
            
        
                
        
            
        }

        //maakt de log uit button aan op het home scherm
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

        //maakt de terug naar home button aan
        public void maakHomeButton()
        {
            Button btnHome = new Button();
            btnHome.Location = new Point(10, 10);
            btnHome.Size = new Size(60, 50);
            btnHome.Text = "Home";
            this.Controls.Add(btnHome);
            btnHome.Click += new EventHandler(this.btnHome_Click);
            dynControls.Add(btnHome);
            }

        // maakt het home scherm aan
        public void terugHome()
        {
            Button btnVerhuren = new Button();
            btnVerhuren.Location = new Point(375, 100);
            btnVerhuren.Size = new Size(450, 150);
            btnVerhuren.Text = "Verhuren";
            this.Controls.Add(btnVerhuren);
            btnVerhuren.Click += new EventHandler(this.btnVerhuur_Click);
            dynControls.Add(btnVerhuren);

            Button btnnieuwproduct = new Button();
            btnnieuwproduct.Location = new Point(25, 220);
            btnnieuwproduct.Size = new Size(350, 150);
            btnnieuwproduct.Text = "nieuw product";
            this.Controls.Add(btnnieuwproduct);
            btnnieuwproduct.Click += new EventHandler(this.btnnieuwproduct_click);
            dynControls.Add(btnnieuwproduct);

            Button btnTerugbrengen = new Button();
            btnTerugbrengen.Location = new Point(375, 350);
            btnTerugbrengen.Size = new Size(450, 150);
            btnTerugbrengen.Text = "Terugbrengen";
            this.Controls.Add(btnTerugbrengen);
            btnTerugbrengen.Click += new EventHandler(this.btnTerugBrengen_Click);
            dynControls.Add(btnTerugbrengen);

            maakLogUitButton();
            pagina = "";
            
        }

        //maakt het scherm leeg van alle controls
        public void maakLeeg()
        {
            foreach (Control c in dynControls)
            {
                this.Controls.Remove(c);
            }
        }

        //maakt de verhuur interface en laad de producten die niet bezet zijn in de listbox
        public void btnVerhuur_Click(object sender, System.EventArgs e)
        {
           // MessageBox.Show("Scan de RFID van de bezoeker." + "\n" + "\n" + "Druk daarna op OK");

            maakLeeg();

            Label lbWachtOpScan = new Label();
            lbWachtOpScan.Location = new Point(400, 300);
            lbWachtOpScan.Size = new Size(1000, 50);
            lbWachtOpScan.Font = new Font("Microsoft Sans Serif", 20F);
            lbWachtOpScan.Text = "Scan het voorwerp... ...";
            this.Controls.Add(lbWachtOpScan);
            dynControls.Add(lbWachtOpScan);

            tag = "";
           RFIDscanned = true;
            while(RFIDscanned == true)
                if (RFIDscanned)
                {
                    RFIDscanned = false;
                    foreach (Control b in dynControls)
                    {
                        this.Controls.Remove(b);

                    }

                    lbverhuurnaam = new Label();
                    lbverhuurnaam.Location = new Point(75, 150);
                    lbverhuurnaam.Size = new Size(100, 50);
                    lbverhuurnaam.Text = "Naam:";
                    this.Controls.Add(lbverhuurnaam);
                    dynControls.Add(lbverhuurnaam);

                    lbVerhuurbareItems = new ListBox();
                    lbVerhuurbareItems.Location = new Point(350, 50);
                    lbVerhuurbareItems.Size = new Size(400, 600);
                    this.Controls.Add(lbVerhuurbareItems);
                    dynControls.Add(lbVerhuurbareItems);

                    //lbVerhuurbareItems.Items.Add();

                    Label listboxlabel = new Label();
                    listboxlabel.Location = new Point(350, 30);
                    listboxlabel.Size = new Size(300, 20);
                    listboxlabel.Text = "Selecteer een voorwerp uit onderstaande lijst.";
                    this.Controls.Add(listboxlabel);
                    dynControls.Add(listboxlabel);

                    //lbVerhuurbareItems.Items.Add();

                    Button btnVerhuren = new Button();
                    btnVerhuren.Location = new Point(800, 140);
                    btnVerhuren.Size = new Size(150, 50);
                    btnVerhuren.Text = "Verhuren";
                    this.Controls.Add(btnVerhuren);
                    dynControls.Add(btnVerhuren);

                    btnVerhuren.Click += new EventHandler(this.btnVerhuren_Click);

                    Label textboxlabel = new Label();
                    textboxlabel.Location = new Point(800, 50);
                    textboxlabel.Size = new Size(90, 20);
                    textboxlabel.Text = "exemplaarID";
                    this.Controls.Add(textboxlabel);
                    dynControls.Add(textboxlabel);

                    tbverhuur = new TextBox();
                    tbverhuur.Location = new Point(890, 50);
                    tbverhuur.Name = "tbverhuur";
                    tbverhuur.Size = new Size(50, 22);
                    this.Controls.Add(tbverhuur);
                    dynControls.Add(tbverhuur);

                    dtbhuur = new DateTimePicker();
                    dtbhuur.Location = new System.Drawing.Point(800, 80);
                    dtbhuur.Name = "dtpeind";
                    dtbhuur.Size = new System.Drawing.Size(213, 20);
                    this.Controls.Add(dtbhuur);
                    dynControls.Add(dtbhuur);

                    dtbinlever = new DateTimePicker();
                    dtbinlever.Location = new System.Drawing.Point(800, 110);
                    dtbinlever.Name = "dtpeind";
                    dtbinlever.Size = new System.Drawing.Size(213, 20);
                    this.Controls.Add(dtbinlever);
                    dynControls.Add(dtbinlever);

                    foreach (voorwerpen voorwerp in voorwerpjes)
                    {
                        OracleCommand command = conn.CreateCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = "SELECT e.exemplaarid, h.retourdatum FROM EXEMPLAAR e, huuropdracht h WHERE e.exemplaarid = h.exemplaarid";
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(1) && reader.GetInt32(0) == voorwerp.Exemplaarnummer)
                            {
                                voorwerp.Verhuurd = true;

                            }
                        }
                    }
                    foreach (voorwerpen voorwerp in voorwerpjes)
                    {


                        int i = 0;
                        int voorwerpnummer = voorwerp.Exemplaarnummer;
                        
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT e.exemplaarid, h.retourdatum FROM EXEMPLAAR e, huuropdracht h WHERE e.exemplaarid = h.exemplaarid";
                            OracleDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {

                                if (voorwerpnummer == rd.GetInt32(0))
                                {
                                    i = 1;
                                    
                                    if (voorwerp.Verhuurd == false)
                                    {
                                        int k = 0;
                                        foreach (int nummers in verhuurnummers)
                                        {
                                            if (nummers == voorwerpnummer)
                                            {
                                                k = 1;
                                            }
                                        }
                                        if (k == 0 && voorwerp.Verhuurd == false)
                                        {
                                            lbVerhuurbareItems.Items.Add("exemplaarID:" + Convert.ToString(voorwerpnummer));
                                            lbVerhuurbareItems.Items.Add("voorwerp:" + voorwerp.Soort + ",merk:" + voorwerp.Merk);
                                            lbVerhuurbareItems.Items.Add("____________________________________________________");
                                            verhuurnummers.Add(voorwerpnummer);
                                        }
                                    }

                                }


                            }
                            if (i == 0)
                            {
                                int k = 0;
                                foreach (int nummers in verhuurnummers)
                                {
                                    if (nummers == voorwerpnummer)
                                    {
                                        k = 1;
                                    }
                                }
                                if (k == 0 && voorwerp.Verhuurd == false)
                                {
                                    lbVerhuurbareItems.Items.Add("exemplaarID:" + Convert.ToString(voorwerpnummer));
                                    lbVerhuurbareItems.Items.Add("voorwerp:" + voorwerp.Soort + ",merk:" + voorwerp.Merk);

                                    lbVerhuurbareItems.Items.Add("____________________________________________________");
                                    verhuurnummers.Add(voorwerpnummer);
                                }

                            }

                        }

                    
                }
            
            pagina = "verhuren";
            maakLogUitButton();
            maakHomeButton();
        }

        //maakt de terugbreng interface aan
        public void btnTerugBrengen_Click(object sender, System.EventArgs e)
        {

            
                    maakLeeg();
                    tag = "";
                    
                    bezNaam = new Label();
                    bezNaam.Location = new Point(75, 150);
                    bezNaam.Size = new Size(100, 50);
                    bezNaam.Text = "Naam:";
                    this.Controls.Add(bezNaam);
                    dynControls.Add(bezNaam);
                    

                    lbGehuurdeItems = new ListBox();
                    lbGehuurdeItems.Location = new Point(400, 50);
                    lbGehuurdeItems.Size = new Size(400, 600);
                    this.Controls.Add(lbGehuurdeItems);
                    dynControls.Add(lbGehuurdeItems);

                    tbterug = new TextBox();
                    tbterug.Location = new Point(890, 50);
                    tbterug.Name = "tbverhuur";
                    tbterug.Size = new Size(50, 22);
                    this.Controls.Add(tbterug);
                    dynControls.Add(tbterug);

                    Label textboxlabel = new Label();
                    textboxlabel.Location = new Point(800, 50);
                    textboxlabel.Size = new Size(100, 20);
                    textboxlabel.Text = "exemplaarID";
                    this.Controls.Add(textboxlabel);
                    dynControls.Add(textboxlabel);
                    
                    Button btnTerugBrengen = new Button();
                    btnTerugBrengen.Location = new Point(850, 140);
                    btnTerugBrengen.Size = new Size(150, 50);
                    btnTerugBrengen.Text = "Terug brengen";
                    this.Controls.Add(btnTerugBrengen);
                    dynControls.Add(btnTerugBrengen);

                    btnTerugBrengen.Click += new EventHandler(this.btnTerugbrengen_Click);

                    dtbretour = new DateTimePicker();
                    dtbretour.Location = new System.Drawing.Point(850, 110);
                    dtbretour.Name = "dtpretour";
                    dtbretour.Size = new System.Drawing.Size(213, 20);
                    this.Controls.Add(dtbretour);
                    dynControls.Add(dtbretour);
                 
            pagina = "terug";
            maakHomeButton();
            maakLogUitButton();
        }

        //verhuurt het aangegeven product gebonden aan het rfid dat gescant is
        public void btnVerhuren_Click(object sender, System.EventArgs e)
        {
            if (tbverhuur.Text != "")
            {
                int exemplaarid = Convert.ToInt32(tbverhuur.Text);
                DateTime huur = dtbhuur.Value;
                DateTime inlever = dtbinlever.Value;
                OracleCommand cmd = conn.CreateCommand();
                if (tag != "")
                {
                    int bezoeker = -1;
                    OracleCommand command = conn.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT bezoekerid FROM bezoeker WHERE RFID='" + tag + "'";
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bezoeker = reader.GetInt32(0);
                    }
                    foreach (voorwerpen voorwerp in voorwerpjes)
                    {
                        if (voorwerp.Exemplaarnummer == exemplaarid)
                        {
                            voorwerp.Verhuurd = true;
                        }
                    }
                    try{
                        
                    var sql = "insert into huuropdracht(exemplaarid,bezoekerid, huurdatum, inleverdatum) values(:exemplaarid,:bezoekerid, :huurdatum, :inleverdatum)";
                    using (cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(":exemplaarid", exemplaarid);
                        cmd.Parameters.Add(":bezoekerid", bezoeker);
                        cmd.Parameters.Add(":huurdatum", huur);
                        cmd.Parameters.Add(":inleverdatum", inlever);
                        cmd.ExecuteNonQuery();
                    }
                    }
            catch (OracleException ex)
            {
                MessageBox.Show("Record is not inserted into the database table.");
                MessageBox.Show("Exception Message: " + ex.Message);
                MessageBox.Show("Exception Source: " + ex.Source);
            }
                    maakLeeg();
                    terugHome();
                    verhuurnummers.Clear();
                }
                else
                {
                    MessageBox.Show("geen tag gevonden!");
                }
            }
            else
            {
                MessageBox.Show("geen eventid gevonden");
            }
        }

        //brengt het product terug van de lijst van de persoon waarvan het rfid is gescant
        public void btnTerugbrengen_Click(object sender, System.EventArgs e)
        {
            if (tag != "")
            {
                if (tbterug.Text != "")
                {
                    int eventnummer = Convert.ToInt32(tbterug.Text);
                    OracleCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    var sql = "update huuropdracht set retourdatum = :param1 where exemplaarid = :keyValue";
                    using (cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add("param1", dtbretour.Value);
                        cmd.Parameters.Add("keyValue", eventnummer);
                        cmd.ExecuteNonQuery();
                    }
                    foreach (voorwerpen voorwerp in voorwerpjes)
                    {
                        if (eventnummer == voorwerp.Exemplaarnummer)
                        {
                            voorwerp.Verhuurd = false;
                        }
                    }
                    maakLeeg();
                    terugHome();
                }
                else 
                {
                    MessageBox.Show("geen id ingevoert");
                }
            }
            else
            {
                MessageBox.Show("geen tag gevonden");
            }
            
        }

        //maakt het login scherm aan
        public void btnLogUit_Click(object sender, System.EventArgs e)
        {
            maakLeeg();
            gbLogin.Visible = true;
        }
        //maakt het home aan
        public void btnHome_Click(object sender, System.EventArgs e)
        {
            verhuurnummers.Clear();
            maakLeeg();
            terugHome();
        }
        //dit wordt uit gevoert als rfid scanner wordt ingeplugged
        public void btnnieuwproduct_click(object sender, System.EventArgs e)
        {
            maakLeeg();
            maakHomeButton();
            maakLogUitButton();

            Label lblnieuweexemplaar = new Label();
            lblnieuweexemplaar.Location = new Point(140, 190);
            lblnieuweexemplaar.Size = new Size(250, 22);
            lblnieuweexemplaar.Text = "exemplaar toevoegen aan de database:";
            this.Controls.Add(lblnieuweexemplaar);
            dynControls.Add(lblnieuweexemplaar);

            Label lblsoort = new Label();
            lblsoort.Location = new Point(130, 222);
            lblsoort.Size = new Size(70, 22);
            lblsoort.Text = "voorwerp:";
            this.Controls.Add(lblsoort);
            dynControls.Add(lblsoort);

            Label lblbeschrijving = new Label();
            lblbeschrijving.Location = new Point(100, 250);
            lblbeschrijving.Size = new Size(100, 22);
            lblbeschrijving.Text = "beschrijving staat:";
            this.Controls.Add(lblbeschrijving);
            dynControls.Add(lblbeschrijving);

            cbsoort = new ComboBox();
            cbsoort.FormattingEnabled = true;
            cbsoort.Location = new System.Drawing.Point(200, 222);
            cbsoort.Name = "cbaanpassen";
            cbsoort.Size = new System.Drawing.Size(121, 21);
            this.Controls.Add(cbsoort);
            dynControls.Add(cbsoort);

            tbbeschrijving = new TextBox();
            tbbeschrijving.Location = new Point(200, 250);
            tbbeschrijving.Name = "tbverhuur";
            tbbeschrijving.Size = new Size(250, 22);
            this.Controls.Add(tbbeschrijving);
            dynControls.Add(tbbeschrijving);

            Button btnaddexemplaar = new Button();
            btnaddexemplaar.Location = new Point(200, 300);
            btnaddexemplaar.Size = new Size(150, 50);
            btnaddexemplaar.Text = "voeg exemplaar toe";
            this.Controls.Add(btnaddexemplaar);
            btnaddexemplaar.Click += new EventHandler(this.btnaddexemplaar_Click);
            dynControls.Add(btnaddexemplaar);


            Label lblnieuweproduct = new Label();
            lblnieuweproduct.Location = new Point(660, 190);
            lblnieuweproduct.Size = new Size(250, 22);
            lblnieuweproduct.Text = "product toevoegen aan de database:";
            this.Controls.Add(lblnieuweproduct);
            dynControls.Add(lblnieuweproduct);

            Label lblsoortje = new Label();
            lblsoortje.Location = new Point(660, 222);
            lblsoortje.Size = new Size(40, 22);
            lblsoortje.Text = "soort:";
            this.Controls.Add(lblsoortje);
            dynControls.Add(lblsoortje);

            Label lblmerk = new Label();
            lblmerk.Location = new Point(660, 250);
            lblmerk.Size = new Size(40, 22);
            lblmerk.Text = "merk:";
            this.Controls.Add(lblmerk);
            dynControls.Add(lblmerk);

            //Label lblprijs = new Label();
            //lblprijs.Location = new Point(660, 275);
            //lblprijs.Size = new Size(40, 22);
            //lblprijs.Text = "prijs:";
            //this.Controls.Add(lblprijs);
            //dynControls.Add(lblprijs);

            tbsoort = new TextBox();
            tbsoort.Location = new Point(700, 222);
            tbsoort.Name = "tbsoort";
            tbsoort.Size = new Size(250, 22);
            this.Controls.Add(tbsoort);
            dynControls.Add(tbsoort);

            tbmerk = new TextBox();
            tbmerk.Location = new Point(700, 250);
            tbmerk.Name = "tbmerk";
            tbmerk.Size = new Size(250, 22);
            this.Controls.Add(tbmerk);
            dynControls.Add(tbmerk);

            
            Button btnaddvoorwerp = new Button();
            btnaddvoorwerp.Location = new Point(700, 300);
            btnaddvoorwerp.Size = new Size(150, 50);
            btnaddvoorwerp.Text = "voeg voorwerp toe";
            this.Controls.Add(btnaddvoorwerp);
            btnaddvoorwerp.Click += new EventHandler(this.btnaddvoorwerp_Click);
            dynControls.Add(btnaddvoorwerp);

            OracleCommand command = conn.CreateCommand();
             command.CommandType = CommandType.Text;
             command.CommandText = "SELECT voorwerpsoort, voorwerpmerk FROM voorwerp";
             OracleDataReader reader = command.ExecuteReader();
             while (reader.Read())
             {
                 cbsoort.Items.Add(reader.GetString(0) + "-" + reader.GetString(1));
             }

        }
        void rfid_Attach(object sender, AttachEventArgs e)
        {
            RFID attached = (RFID)sender;
            rfid.Antenna = true;
        }
        //dit wordt uit gevoert als rfid scanner wordt uitgeplugged
        void rfid_Detach(object sender, DetachEventArgs e)
        {
            RFID detached = (RFID)sender;
        }
        //leest de scanner uit en geeft weer wat hij gelezen heeft hij doet bij beide een verschillende fuctie voor het huren en terug brengen
        void rfid_Tag(object sender, TagEventArgs e)
        {
            tag = e.Tag;            
            int bezoekerid = -1;
            if(pagina == "terug")
            {
               //if(laatsterfid != tag && e.Tag != "")
               //{
                   
                OracleCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT voornaam, bezoekerid FROM bezoeker WHERE RFID='" + tag + "'";
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bezoekerid = reader.GetInt32(1);
                    naam = reader.GetString(0);
                }
                bezNaam.Text = "Naam:" + naam;
                 OracleCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT e.exemplaarid, g.voorwerpsoort, g.VOORWERPMERK, h.retourdatum FROM EXEMPLAAR e, voorwerp g, huuropdracht h WHERE e.VOORWERPID = g.VOORWERPID AND h.exemplaarid = e.exemplaarid AND h.bezoekerid ="+bezoekerid;
                        OracleDataReader rd = cmd.ExecuteReader();
                        lbGehuurdeItems.Items.Clear();
                        while (rd.Read())
                        {
                            if (rd.IsDBNull(3))
                            {
                                lbGehuurdeItems.Items.Add("exemplaarid:" + Convert.ToString(rd.GetInt32(0)));
                                lbGehuurdeItems.Items.Add("voorwerp:" + rd.GetString(1) + ",merk:" + rd.GetString(2));

                                lbGehuurdeItems.Items.Add("____________________________________________________");
                            }

                      //  }
                        }
                        laatsterfid = tag;
            }
            else if(pagina == "verhuren")
            {
                OracleCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT voornaam FROM bezoeker WHERE RFID='" + tag + "'";
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    naam = reader.GetString(0);
                }
                lbverhuurnaam.Text = "Naam:" + naam;
            }
                

        }
        //standard code voor de rfid scanner

         #region Command line open functions
        private void openCmdLine(Phidget p)
        {
            openCmdLine(p, null);
        }
        private void openCmdLine(Phidget p, String pass)
        {
            int serial = -1;
            String logFile = null;
            int port = 5001;
            String host = null;
            bool remote = false, remoteIP = false;
            string[] args = Environment.GetCommandLineArgs();
            String appName = args[0];

            try
            { //Parse the flags
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                        switch (args[i].Remove(0, 1).ToLower())
                        {
                            case "l":
                                logFile = (args[++i]);
                                break;
                            case "n":
                                serial = int.Parse(args[++i]);
                                break;
                            case "r":
                                remote = true;
                                break;
                            case "s":
                                remote = true;
                                host = args[++i];
                                break;
                            case "p":
                                pass = args[++i];
                                break;
                            case "i":
                                remoteIP = true;
                                host = args[++i];
                                if (host.Contains(":"))
                                {
                                    port = int.Parse(host.Split(':')[1]);
                                    host = host.Split(':')[0];
                                }
                                break;
                            default:
                                goto usage;
                        }
                    else
                        goto usage;
                }
                if (logFile != null)
                    Phidget.enableLogging(Phidget.LogLevel.PHIDGET_LOG_INFO, logFile);
                if (remoteIP)
                    p.open(serial, host, port, pass);
                else if (remote)
                    p.open(serial, host, pass);
                else
                    p.open(serial);
                return; //success
            }
            catch { }
        usage:
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Invalid Command line arguments." + Environment.NewLine);
            sb.AppendLine("Usage: " + appName + " [Flags...]");
            sb.AppendLine("Flags:\t-n   serialNumber\tSerial Number, omit for any serial");
            sb.AppendLine("\t-l   logFile\tEnable phidget21 logging to logFile.");
            sb.AppendLine("\t-r\t\tOpen remotely");
            sb.AppendLine("\t-s   serverID\tServer ID, omit for any server");
            sb.AppendLine("\t-i   ipAddress:port\tIp Address and Port. Port is optional, defaults to 5001");
            sb.AppendLine("\t-p   password\tPassword, omit for no password" + Environment.NewLine);
            sb.AppendLine("Examples: ");
            sb.AppendLine(appName + " -n 50098");
            sb.AppendLine(appName + " -r");
            sb.AppendLine(appName + " -s myphidgetserver");
            sb.AppendLine(appName + " -n 45670 -i 127.0.0.1:5001 -p paswrd");
            MessageBox.Show(sb.ToString(), "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.Exit();
        }
        #endregion
        public void btnaddexemplaar_Click(object sender, System.EventArgs e)
         {
             string cbtekst = cbsoort.Text;
             int l = cbtekst.IndexOf("-");
             string soort = cbtekst.Substring(0, l);
             string merk = cbtekst.Substring(l +1, cbtekst.Length - l -1);
             OracleCommand command = conn.CreateCommand();
             command.CommandType = CommandType.Text;
             command.CommandText = "SELECT voorwerpid FROM voorwerp WHERE voorwerpsoort='" + soort + "'AND voorwerpmerk='"+ merk +"'";
             OracleDataReader reader = command.ExecuteReader();
             while (reader.Read())
             {                
                 OracleCommand cmd = conn.CreateCommand();
                 var sql = "insert into exemplaar(voorwerpid, exemplaarstaat) values(:voorwerpid, :exemplaarstaat)";
                 using (cmd = new OracleCommand(sql, conn))
                 {
                     cmd.Parameters.Add(":voorwerpid", reader.GetInt32(0));
                     cmd.Parameters.Add(":exemplaarstaat", tbbeschrijving.Text);
                     cmd.ExecuteNonQuery();
                     tbbeschrijving.Clear();
                     voorwerpjes.Clear();
                     OracleCommand cmmnd = conn.CreateCommand();
                     cmmnd.CommandType = CommandType.Text;
                     cmmnd.CommandText = "SELECT e.exemplaarid, g.voorwerpsoort, g.VOORWERPMERK FROM EXEMPLAAR e, voorwerp g WHERE e.VOORWERPID = g.VOORWERPID";
                     OracleDataReader dr = cmmnd.ExecuteReader();
                        while (dr.Read())
                        {
                            voorwerpen voorwerp = new voorwerpen(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                            voorwerpjes.Add(voorwerp);
                        }
                     
                     MessageBox.Show("exemplaar toegevoegd");
                 }
             }
             

         }
        public void btnaddvoorwerp_Click(object sender, System.EventArgs e)
        {
            OracleCommand cmd = conn.CreateCommand();
            var sql = "insert into voorwerp(voorwerpsoort, voorwerpmerk) values(:voorwerpsoort, :voorwerpmerk)";
            using (cmd = new OracleCommand(sql, conn))
            {
                try
                {
                cmd.Parameters.Add(":voorwerpsoort", tbsoort.Text);
                cmd.Parameters.Add(":voorwerpmerk", tbmerk.Text);
                cmd.ExecuteNonQuery();
                cbsoort.Items.Add(tbsoort.Text + "-" + tbmerk.Text);
                tbsoort.Clear();
                tbmerk.Clear();
                MessageBox.Show("voorwerp toegevoegd");
                }
            catch (OracleException ex)
            {
                MessageBox.Show("Record is not inserted into the database table.");
                MessageBox.Show("Exception Message: " + ex.Message);
                MessageBox.Show("Exception Source: " + ex.Source);
            }
            }
        }
        
        
    }
    }

