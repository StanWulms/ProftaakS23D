using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace event_beheer_systeem
{
    public partial class Form1 : Form
    {
        Button btngenereer;
        Button btnverwijderen;
        TextBox tbemail;
        TextBox tbnaam;
        TextBox tbVerwijderen;
        ListBox lbevents;
        Button btnuitloggen;
        List<Control> dynControls = new List<Control>();        
        List<int> eventids;
        List<eventaanmaken> aanmaak;
        int aanpassingsnummer = -1;
        Random getal;
        int username = -1;
        eventaanmaken eventaanpas;
        ComboBox cbaanpassen;
        private OracleConnection conn;
        bool loggin = false;
        int gebruikersnaam;
        string wachtwoord;
        List<Bezoeker> bezoekers = new List<Bezoeker>();

        public Form1()
        {
            InitializeComponent();            
            eventids = new List<int>();
            aanmaak = new List<eventaanmaken>();
            conn = new OracleConnection();

            string user = "dbi324575";
            string pw = "YT4Yr6gF81";
            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();

             try
            {
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT gebruikerid, gebruikerswachtwoord FROM gebruiker";

                OracleDataReader dr = cmd.ExecuteReader(); //een OracleDataReader aanmaken en deze linken aan het command dat je zojuist hebt gemaakt.
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Bezoeker b = new Bezoeker(dr.GetInt32(0), dr.GetString(1));
                    bezoekers.Add(b);
                }
                
            }
            catch { MessageBox.Show("Geen gegevens in de database gevonden."); }
        }
        

        private void btnInloggen_Click(object sender, EventArgs e)
        {
            loggin = false;
            gebruikersnaam = Convert.ToInt32(tbUsername.Text);
            wachtwoord = /*Convert.ToInt32(*/tbPassword.Text/*)*/;
            foreach (Bezoeker b in bezoekers)
            {
                if (b.AccountNaam == gebruikersnaam && b.AccountWachtwoord == wachtwoord)
                {
                    this.loggin = true;
                    //TODO: match toegangscode met behorende applicatie.
                    
                        if (File.Exists("lijst.bin") == true)
                        {
                            BinaryFormatter fmt;

                            using (FileStream f = new FileStream("lijst.bin", FileMode.Open, FileAccess.ReadWrite))
                            {
                                fmt = new BinaryFormatter();
                                aanmaak = (List<eventaanmaken>)fmt.Deserialize(f);
                            }
                        }
                        username = Convert.ToInt32(tbUsername.Text);
                        gbLogin.Visible = false;
                        gbaanmaak.Visible = true;
                        gbaanpassen.Visible = true;
                        if (username == 1)
                        {
                            btngenereer = new Button();
                            btngenereer.Location = new Point(850, 700);
                            btngenereer.Size = new Size(110, 100);
                            btngenereer.Text = "genereer login gegevens";
                            this.Controls.Add(btngenereer);
                            dynControls.Add(btngenereer);
                            btngenereer.Click += new EventHandler(this.btngenereer_Click);

                            tbemail = new TextBox();
                            tbemail.Location = new Point(850, 650);
                            tbemail.Size = new Size(174, 20);
                            this.Controls.Add(tbemail);
                            dynControls.Add(tbemail);

                            tbnaam = new TextBox();
                            tbnaam.Location = new Point(850, 620);
                            tbnaam.Size = new Size(174, 20);
                            this.Controls.Add(tbnaam);
                            dynControls.Add(tbnaam);

                            Label label1 = new Label();
                            label1.AutoSize = true;
                            label1.Location = new System.Drawing.Point(810, 655);
                            label1.Name = "label2";
                            label1.Size = new System.Drawing.Size(52, 13);
                            label1.Text = "e-mail";
                            this.Controls.Add(label1);
                            dynControls.Add(label1);

                            Label label2 = new Label();
                            label2.AutoSize = true;
                            label2.Location = new System.Drawing.Point(810, 625);
                            label2.Name = "label2";
                            label2.Size = new System.Drawing.Size(52, 13);
                            label2.Text = "naam";
                            this.Controls.Add(label2);
                            dynControls.Add(label2);

                            
                        }
                        lbevents = new ListBox();
                        lbevents.Location = new Point(200, 75);
                        lbevents.Size = new Size(400, 600);
                        this.Controls.Add(lbevents);
                        dynControls.Add(lbevents);
                                           
                        /*tbVerwijderen = new TextBox();
                        tbVerwijderen.Location = new Point(920, 250);
                        tbVerwijderen.Size = new Size(121, 21);
                        this.Controls.Add(tbVerwijderen);
                        dynControls.Add(tbVerwijderen);

                        btnverwijderen = new Button();
                        btnverwijderen.Location = new Point(790, 250);                        
                        btnverwijderen.Name = "cbaanpassen";
                        btnverwijderen.Size = new System.Drawing.Size(121, 21);
                        btnverwijderen.Text = "verwijderen";
                        this.Controls.Add(btnverwijderen);
                        dynControls.Add(btnverwijderen);
                        btnverwijderen.Click += new EventHandler(this.btnverwijderen_Click); */

                        btnuitloggen = new Button();
                        btnuitloggen.Location = new Point(1510, 10);
                        btnuitloggen.Size = new Size(60, 50);
                        btnuitloggen.Text = "log uit";
                        this.Controls.Add(btnuitloggen);
                        dynControls.Add(btnuitloggen);

                        btnuitloggen.Click += new EventHandler(this.btnuitloggen_Click);



                        if (username != 1)
                        {
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM event WHERE gebruikerid = '" + Convert.ToString(username) + "'";
                            OracleDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {

                                try
                                {
                                    int i = 0;
                                    eventaanmaken nieuw = new eventaanmaken(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetDateTime(3), dr.GetString(4), dr.GetInt32(5), dr.GetString(6), dr.GetString(7), dr.GetInt32(8), dr.GetString(9));
                                    foreach (eventaanmaken inhoud in aanmaak)
                                    {
                                        if (dr.GetInt32(0) == inhoud.eventid)
                                        {
                                            i = 1;
                                        }
                                    }
                                    if (i != 1)
                                    {
                                        aanmaak.Add(nieuw);
                                    }
                                    
                                    lbevents.Items.Add("Event: " + Convert.ToString(dr.GetInt32(0)));
                                    lbevents.Items.Add("User: " + Convert.ToString(dr.GetInt32(1)));
                                    lbevents.Items.Add("Begin datum: " + Convert.ToString(dr.GetDateTime(2)) + " - "+ "Eind datum: " + Convert.ToString(dr.GetDateTime(3)));
                                    lbevents.Items.Add("Woonplaats: " + dr.GetString(4) + " " + Convert.ToString(dr.GetInt32(5)) + dr.GetString(6));
                                    lbevents.Items.Add("Straat: "+ dr.GetString(7) + " " + Convert.ToString(dr.GetInt32(8)) + dr.GetString(9));
                                    lbevents.Items.Add("____________________________________________________");

                                }
                                catch
                                {
                                    int i = 0;
                                    eventaanmaken nieuw = new eventaanmaken(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetDateTime(3), dr.GetString(4), dr.GetInt32(5), dr.GetString(6), dr.GetString(7), dr.GetInt32(8));
                                    foreach (eventaanmaken inhoud in aanmaak)
                                    {
                                        if (dr.GetInt32(0) == inhoud.eventid)
                                        {
                                            i = 1;
                                        }
                                    }
                                    if (i != 1)
                                    {
                                        aanmaak.Add(nieuw);
                                    }

                                    lbevents.Items.Add("Event: " + Convert.ToString(dr.GetInt32(0)));
                                    lbevents.Items.Add("User: " + Convert.ToString(dr.GetInt32(1)));
                                    lbevents.Items.Add("Begin datum: " + Convert.ToString(dr.GetDateTime(2)) + " - "+ "Eind datum: " + Convert.ToString(dr.GetDateTime(3)));
                                    lbevents.Items.Add("Woonplaats: "+ dr.GetString(4) + " " + Convert.ToString(dr.GetInt32(5)) + dr.GetString(6));
                                    lbevents.Items.Add("Straat: " + dr.GetString(7) + " " + Convert.ToString(dr.GetInt32(8)));
                                    lbevents.Items.Add("____________________________________________________");
                                }
                            }
                        }

                        else if (username == 1)
                        {
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM event";
                            OracleDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {

                                try
                                {
                                    int i = 0;
                                    eventaanmaken nieuw = new eventaanmaken(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetDateTime(3), dr.GetString(4), dr.GetInt32(5), dr.GetString(6), dr.GetString(7), dr.GetInt32(8), dr.GetString(9));
                                    foreach (eventaanmaken inhoud in aanmaak)
                                    {
                                        if (dr.GetInt32(0) == inhoud.eventid)
                                        {
                                            i = 1;
                                        }
                                    }
                                    if (i != 1)
                                    {
                                        aanmaak.Add(nieuw);
                                    }

                                    lbevents.Items.Add("Event: " + Convert.ToString(dr.GetInt32(0)));
                                    lbevents.Items.Add("User: " + Convert.ToString(dr.GetInt32(1)));
                                    lbevents.Items.Add("Begin datum: " + Convert.ToString(dr.GetDateTime(2)) + " - " + "Eind datum: " + Convert.ToString(dr.GetDateTime(3)));
                                    lbevents.Items.Add("Woonplaats: " + dr.GetString(4) + " " + Convert.ToString(dr.GetInt32(5)) + dr.GetString(6));
                                    lbevents.Items.Add("Straat: " + dr.GetString(7) + " " + Convert.ToString(dr.GetInt32(8)) + dr.GetString(9));
                                    lbevents.Items.Add("____________________________________________________");

                                }
                                catch
                                {
                                    int i = 0;
                                    eventaanmaken nieuw = new eventaanmaken(dr.GetInt32(0), dr.GetInt32(1), dr.GetDateTime(2), dr.GetDateTime(3), dr.GetString(4), dr.GetInt32(5), dr.GetString(6), dr.GetString(7), dr.GetInt32(8));
                                    foreach (eventaanmaken inhoud in aanmaak)
                                    {
                                        if (dr.GetInt32(0) == inhoud.eventid)
                                        {
                                            i = 1;
                                        }
                                    }
                                    if (i != 1)
                                    {
                                        aanmaak.Add(nieuw);
                                    }

                                    lbevents.Items.Add("Event: " + Convert.ToString(dr.GetInt32(0)));
                                    lbevents.Items.Add("User: " + Convert.ToString(dr.GetInt32(1)));
                                    lbevents.Items.Add("Begin datum: " + Convert.ToString(dr.GetDateTime(2)) + " - " + "Eind datum: " + Convert.ToString(dr.GetDateTime(3)));
                                    lbevents.Items.Add("Woonplaats: " + dr.GetString(4) + " " + Convert.ToString(dr.GetInt32(5)) + dr.GetString(6));
                                    lbevents.Items.Add("Straat: " + dr.GetString(7) + " " + Convert.ToString(dr.GetInt32(8)));
                                    lbevents.Items.Add("____________________________________________________");
                                }


                            }
                        }                                                           
                }
            }
            if (loggin == false)
            {
                MessageBox.Show("Ongeldige gebruikersnaam/wachtwoord.");
            }
            
            
            }
        
        
       
        public void btnuitloggen_Click(object sender, System.EventArgs e)
        {
            bezoekers.Clear();
            try
            {
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT gebruikerid, gebruikerswachtwoord FROM gebruiker";

                OracleDataReader dr = cmd.ExecuteReader(); //een OracleDataReader aanmaken en deze linken aan het command dat je zojuist hebt gemaakt.
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Bezoeker b = new Bezoeker(dr.GetInt32(0), dr.GetString(1));
                    bezoekers.Add(b);
                }
                
            }
            catch { MessageBox.Show("Geen gegevens in de database gevonden.");
         
        }

            foreach (Control c in dynControls)
            {
                this.Controls.Remove(c);
            }
            gbLogin.Visible = true;
            gbaanmaak.Visible = false;
            gbaanpassen.Visible = false;
            tbaanpassingen.Visible = false;

            BinaryFormatter fmt;
            using (FileStream f = new FileStream("lijst.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fmt = new BinaryFormatter();
                fmt.Serialize(f, aanmaak);

            }
        }
        public void btngenereer_Click(object sender, System.EventArgs e)
        {
            if (tbnaam.Text != "")
            {


                if (tbemail.Text != "")
                {


                    string email = tbemail.Text;
                    string naam = tbnaam.Text;
                    getal = new Random();
                    int wwgetal = getal.Next(100000, 999999);
                    try
                    {
                        OracleCommand cmd = conn.CreateCommand();
                        var sql = "insert into gebruiker(gebruikersnaam, gebruikerswachtwoord, gebruikersemail) values(:gebruikersnaam, :gebruikerswachtwoord, :gebruikersemail)";
                        using (cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":gebruikersnaam", tbnaam.Text);
                            cmd.Parameters.Add(":gebruikerswachtwoord", wwgetal);
                            cmd.Parameters.Add(":gebruikersemail", email);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("account is gegenereerd");

                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show("Record is not inserted into the database table.");
                        MessageBox.Show("Exception Message: " + ex.Message);
                        MessageBox.Show("Exception Source: " + ex.Source);
                    }
                }
                else
                {
                    MessageBox.Show("Voer email in alsjeblieft");
                }
            }
            else
            {
                MessageBox.Show("Voer naam in alsjeblieft");
            }
        }

        private void btnmaakaan_Click(object sender, EventArgs e)
        {
            if (tbplaats.Text != "")
            {


                if (tbnumeriek.Text != "")
                {


                    if (tbalfa.Text != "")
                    {


                        if (tbstraatnaam.Text != "")
                        {


                            if (tbhuisn.Text != "")
                            {


                                if (tbtoevoeg.Text == "")
                                {

                                    OracleCommand cmd = conn.CreateCommand();
                                    var sql = "insert into event( gebruikerID, eventbegindatum, eventeinddatum, eventplaats, eventpostcodenumeriek, eventpostcodealfanumeriek, eventstraatnaam, eventhuisnummer, eventhuisnummertoevoeging) values(:gebruikerID, :eventbegindatum, :eventeinddatum, :eventplaats, :eventpostcodenumeriek, :eventpostcodealfanumeriek, :eventstraatnaam, :eventhuisnummer, :eventhuisnummertoevoeging)";
                                    using (cmd = new OracleCommand(sql, conn))
                                    {

                                        cmd.Parameters.Add(":gebruikerID", Convert.ToInt32(username));
                                        cmd.Parameters.Add(":eventbegindatum", dtpbegin.Value);
                                        cmd.Parameters.Add(":eventeinddatum", dtpbegin.Value);
                                        cmd.Parameters.Add(":eventplaats", tbplaats.Text);
                                        cmd.Parameters.Add(":eventpostcodenumeriek", Convert.ToInt32(tbnumeriek.Text));
                                        cmd.Parameters.Add(":eventpostcodealfanumeriek", tbalfa.Text);
                                        cmd.Parameters.Add(":eventstraatnaam", tbstraatnaam.Text);
                                        cmd.Parameters.Add(":eventhuisnummer", Convert.ToInt32(tbhuisn.Text));
                                        cmd.Parameters.Add(":eventhuisnummertoevoeging", null);
                                        cmd.ExecuteNonQuery();

                                        int eventnummer = -1;
                                        OracleCommand command = conn.CreateCommand();
                                        command.CommandType = CommandType.Text;
                                        command.CommandText = "SELECT MAX(eventid) FROM event WHERE gebruikerid='" + Convert.ToInt32(username) + "'";
                                        OracleDataReader dr = command.ExecuteReader();
                                        while (dr.Read())
                                        {
                                            eventnummer = dr.GetInt32(0);
                                        }
                                        eventaanmaken nieuw = new eventaanmaken(eventnummer, Convert.ToInt32(username), dtpbegin.Value, dtpbegin.Value, tbplaats.Text, Convert.ToInt32(tbnumeriek.Text), tbalfa.Text, tbstraatnaam.Text, Convert.ToInt32(tbhuisn.Text));
                                        aanmaak.Add(nieuw);
                                        lbevents.Items.Add(eventnummer);
                                        lbevents.Items.Add(username);
                                        lbevents.Items.Add("Begin datum: " + Convert.ToString(dtpbegin.Value) + " - "+ "Eind datum: " + Convert.ToString(dtpeind.Value));
                                        lbevents.Items.Add("Woonplaats: " + tbplaats.Text + " " + tbnumeriek.Text + tbalfa.Text);
                                        lbevents.Items.Add("Straat: " + tbstraatnaam.Text + " " + tbhuisn.Text + tbtoevoeg.Text);
                                        lbevents.Items.Add("____________________________________________________");
                                    }
                                }
                                else
                                {
                                    OracleCommand cmd = conn.CreateCommand();
                                    var sql = "insert into event(gebruikerID, eventbegindatum, eventeinddatum, eventplaats, eventpostcodenumeriek, eventpostcodealfanumeriek, eventstraatnaam, eventhuisnummer, eventhuisnummertoevoeging) values(:gebruikerID, :eventbegindatum, :eventeinddatum, :eventplaats, :eventpostcodenumeriek, :eventpostcodealfanumeriek, :eventstraatnaam, :eventhuisnummer, :eventhuisnummertoevoeging)";
                                    using (cmd = new OracleCommand(sql, conn))
                                    {

                                        cmd.Parameters.Add(":gebruikerID", Convert.ToInt32(username));
                                        cmd.Parameters.Add(":eventbegindatum", dtpbegin.Value);
                                        cmd.Parameters.Add(":eventeinddatum", dtpbegin.Value);
                                        cmd.Parameters.Add(":eventplaats", tbplaats.Text);
                                        cmd.Parameters.Add(":eventpostcodenumeriek", Convert.ToInt32(tbnumeriek.Text));
                                        cmd.Parameters.Add(":eventpostcodealfanumeriek", tbalfa.Text);
                                        cmd.Parameters.Add(":eventstraatnaam", tbstraatnaam.Text);
                                        cmd.Parameters.Add(":eventhuisnummer", Convert.ToInt32(tbhuisn.Text));
                                        cmd.Parameters.Add(":eventhuisnummertoevoeging", tbtoevoeg.Text);
                                        cmd.ExecuteNonQuery();
                                    }



                                    int eventnummer = -1;
                                    OracleCommand command = conn.CreateCommand();
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = "SELECT MAX(eventid) FROM event WHERE gebruikerid='" + Convert.ToInt32(username) + "'";
                                    OracleDataReader dr = command.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        eventnummer = dr.GetInt32(0);
                                    }
                                    eventaanmaken nieuw = new eventaanmaken(eventnummer, Convert.ToInt32(username), dtpbegin.Value, dtpbegin.Value, tbplaats.Text, Convert.ToInt32(tbnumeriek.Text), tbalfa.Text, tbstraatnaam.Text, Convert.ToInt32(tbhuisn.Text));
                                    aanmaak.Add(nieuw);

                                    lbevents.Items.Add(eventnummer);
                                    lbevents.Items.Add(username);
                                    lbevents.Items.Add("Begin datum: " +  Convert.ToString(dtpbegin.Value) + " - " + "Eind datum: " + Convert.ToString(dtpeind.Value));
                                    lbevents.Items.Add("Woonplaats: "+ tbplaats.Text + " " + tbnumeriek.Text + tbalfa.Text);
                                    lbevents.Items.Add("Straat: " + tbstraatnaam.Text + " " + tbhuisn.Text + tbtoevoeg.Text);
                                    lbevents.Items.Add("____________________________________________________");
                                }
                            }
                            else
                            {
                                MessageBox.Show("huisnummer nog niet ingevuld");
                            }
                        }
                        else
                        {
                            MessageBox.Show("straatnaam is nog niet ingevuld");
                        }
                    }
                    else
                    {
                        MessageBox.Show("alfanumerieke postcode is nog niet ingevuld");
                    }
                }
                else
                {
                    MessageBox.Show("numerieke postcode is nog niet ingevuld");
                }
            }
            else
            {
                MessageBox.Show("plaats is nog niet ingevuld");
            }
        }

        private void btnaanpassen_Click(object sender, EventArgs e)
        {
            if (tbaanpassen.Text != "")
            {
                int k = 0;
                aanpassingsnummer = Convert.ToInt32(tbaanpassen.Text);
                foreach (eventaanmaken eventje in aanmaak)
                {
                    if (aanpassingsnummer == eventje.eventid && eventje.username == username)
                    {
                        k = 1;
                        this.Controls.Remove(cbaanpassen);
                        cbaanpassen = new ComboBox();
                        cbaanpassen.FormattingEnabled = true;
                        cbaanpassen.Items.AddRange(new object[] {
                    "camera",
                    "RFID",
                    "TV",
                    "caravan"});
                        cbaanpassen.Location = new System.Drawing.Point(1100, 273);
                        cbaanpassen.Name = "cbaanpassen";
                        cbaanpassen.Size = new System.Drawing.Size(121, 21);
                        cbaanpassen.SelectedIndexChanged += new System.EventHandler(this.cbaanpassen_SelectedIndexChanged);
                        cbaanpassen.Visible = true;
                        tbaanpassingen.Visible = true;
                        tbaanpassingen.Clear();
                        this.Controls.Add(cbaanpassen);
                        dynControls.Add(cbaanpassen);
                    }

                }
                if (k == 0)
                {
                    MessageBox.Show("event niet beschikbaar voor U");
                }
            }
            else
            {
                MessageBox.Show("voer nummer in alsjeblieft");
            }
        }

        private void cbaanpassen_SelectedIndexChanged(object sender, EventArgs e)
        {
            
             foreach(eventaanmaken eventje in aanmaak)
            {
                 if(aanpassingsnummer == eventje.eventid)
                 {
                    eventaanpas = eventje;
                 }
            }            
            if(cbaanpassen.Text == "camera")
            {
                tbaanpassingen.Text = Convert.ToString(eventaanpas.Camera);
            }
            else if(cbaanpassen.Text == "RFID")
            {
                tbaanpassingen.Text = Convert.ToString(eventaanpas.rfid);
            }
            else if(cbaanpassen.Text == "TV")
            {
                tbaanpassingen.Text = Convert.ToString(eventaanpas.tv);
            }
            else if(cbaanpassen.Text == "caravan")
            {
                tbaanpassingen.Text = Convert.ToString(eventaanpas.Caravan);
            }
        }

        private void tbaanpassingen_TextChanged(object sender, EventArgs e)
        {
            if (tbaanpassingen.Text != "")
            {
                if (cbaanpassen.Text == "camera")
                {
                    eventaanpas.Camera = Convert.ToInt32(tbaanpassingen.Text);
                }
                else if (cbaanpassen.Text == "RFID")
                {
                    eventaanpas.rfid = Convert.ToInt32(tbaanpassingen.Text);
                }
                else if (cbaanpassen.Text == "TV")
                {
                    eventaanpas.tv = Convert.ToInt32(tbaanpassingen.Text);
                }
                else if (cbaanpassen.Text == "caravan")
                {
                    eventaanpas.Caravan = Convert.ToInt32(tbaanpassingen.Text);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinaryFormatter fmt;
            using (FileStream f = new FileStream("lijst.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fmt = new BinaryFormatter();
                fmt.Serialize(f, aanmaak);

            }
        }
         
        }
        
        

        

        
    }

