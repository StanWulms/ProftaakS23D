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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace toegangscontrole
{
    public partial class Form1 : Form
    {        
        private RFID rfid;
        private OracleConnection conn;
        List<bezoekers> bezoekerlijst;
        Button btnuitloggen;
        List<Control> dynControls;
        ListBox lbaanwezig;
        ListBox lbniet;
        ListBox lbres;
        Label label1;
        Label label2;
        Label label3;
        bool loggin = false;
        string gebruikersnaam;
        string wachtwoord;
        List<Bezoeker> bezoekers = new List<Bezoeker>();
        string pagina = "";
        public Form1()
        {
            InitializeComponent();
            dynControls = new List<Control>();
            bezoekerlijst = new List<bezoekers>();

            conn = new OracleConnection();
            string user = "dbi324575";
            string pw = "YT4Yr6gF81";

            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();


            rfid = new RFID();
            rfid.Attach += new AttachEventHandler(rfid_Attach);
            rfid.Detach += new DetachEventHandler(rfid_Detach);
            rfid.Tag += new TagEventHandler(rfid_Tag);
            openCmdLine(rfid);

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
        

        private void btnInloggen_Click(object sender, EventArgs e)
        {
            pagina = "toegang";
            loggin = false;
            gebruikersnaam = tbUsername.Text;
            wachtwoord = tbPassword.Text;
            foreach (Bezoeker b in bezoekers)
            {
                if (b.AccountNaam == gebruikersnaam && b.AccountWachtwoord == wachtwoord)
                {
                    this.loggin = true;
                    
                    if (b.Toegankelijkheid == "C" || b.Toegankelijkheid == "E" || b.Toegankelijkheid == "F")
                    {
                        gbLogin.Visible = false;

                        btnuitloggen = new Button();
                        btnuitloggen.Location = new Point(20, 20);
                        btnuitloggen.Size = new Size(60, 50);
                        btnuitloggen.Text = "log uit";
                        this.Controls.Add(btnuitloggen);
                        dynControls.Add(btnuitloggen);
                        btnuitloggen.Click += new EventHandler(this.btnuitloggen_Click);

                        lbaanwezig = new ListBox();
                        lbaanwezig.FormattingEnabled = true;
                        lbaanwezig.Location = new System.Drawing.Point(400, 58);
                        lbaanwezig.Name = "lbaanwezig";
                        lbaanwezig.Size = new System.Drawing.Size(177, 303);
                        this.Controls.Add(lbaanwezig);
                        dynControls.Add(lbaanwezig);

                        lbniet = new ListBox();
                        lbniet.FormattingEnabled = true;
                        lbniet.Location = new System.Drawing.Point(200, 58);
                        lbniet.Name = "lbniet";
                        lbniet.Size = new System.Drawing.Size(177, 303);
                        this.Controls.Add(lbniet);
                        dynControls.Add(lbniet);
                                               
                        label1 = new Label();
                        label1.AutoSize = true;
                        label1.Location = new System.Drawing.Point(197, 26);
                        label1.Name = "label1";
                        label1.Size = new System.Drawing.Size(72, 13);
                        label1.Text = "niet aanwezig";
                        this.Controls.Add(label1);
                        dynControls.Add(label1);

                        lbres = new ListBox();
                        lbres.FormattingEnabled = true;
                        lbres.Location = new System.Drawing.Point(600, 58);
                        lbres.Name = "lbres";
                        lbres.Size = new System.Drawing.Size(177, 303);
                        this.Controls.Add(lbres);
                        dynControls.Add(lbres);

                        label3 = new Label();
                        label3.AutoSize = true;
                        label3.Location = new System.Drawing.Point(600, 26);
                        label3.Name = "label3";
                        label3.Size = new System.Drawing.Size(72, 13);
                        label3.Text = "reservering";
                        this.Controls.Add(label3);
                        dynControls.Add(label3);

                        label2 = new Label();
                        label2.AutoSize = true;
                        label2.Location = new System.Drawing.Point(400, 26);
                        label2.Name = "label2";
                        label2.Size = new System.Drawing.Size(52, 13);
                        label2.Text = "aanwezig";
                        this.Controls.Add(label2);
                        dynControls.Add(label2);

                        if (File.Exists("lijst.bin") == true)
                        {
                            BinaryFormatter fmt;

                            using (FileStream f = new FileStream("lijst.bin", FileMode.Open, FileAccess.ReadWrite))
                            {
                                fmt = new BinaryFormatter();
                                bezoekerlijst = (List<bezoekers>)fmt.Deserialize(f);
                            }
                            foreach (bezoekers gast in bezoekerlijst)
                            {
                                if (gast.Bob == true)
                                {
                                    lbaanwezig.Items.Add(gast.Naam);
                                }
                                else
                                {
                                    lbniet.Items.Add(gast.Naam);
                                }
                            }
                        }
                        else
                        {
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT voornaam FROM bezoeker";
                            OracleDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                lbniet.Items.Add(dr.GetString(0));
                                bezoekers bezoeker = new bezoekers(dr.GetString(0));
                                bezoekerlijst.Add(bezoeker);

                            }
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
        void rfid_Attach(object sender, AttachEventArgs e)
        {
            RFID attached = (RFID)sender;
            rfid.Antenna = true;
        }
        void rfid_Detach(object sender, DetachEventArgs e)
        {
            RFID detached = (RFID)sender;
        }
        void rfid_Tag(object sender, TagEventArgs e)
        {

            if (pagina == "toegang")
            {


                string tag = e.Tag;

                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT voornaam, betaald FROM bezoeker WHERE RFID ='" + tag + "'";
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows == true)
                {
                    int betaald = dr.GetInt32(1);
                    string naam = dr.GetString(0);
                    if (betaald == 1)
                    {
                        if (lbniet.Items.Contains(naam))
                        {
                            lbniet.Items.Remove(naam);
                            lbaanwezig.Items.Add(naam);
                            foreach (bezoekers gast in bezoekerlijst)
                            {
                                if (gast.Naam == naam)
                                {
                                    gast.Bob = true;
                                }
                            }
                            OracleCommand command = conn.CreateCommand();
                            command.CommandType = CommandType.Text;
                            command.CommandText = "SELECT b.voornaam, b.achternaam, r.plaatsid FROM bezoeker b, reservering r WHERE b.bezoekerid = r.bezoekerid AND RFID='" + tag + "'";
                            OracleDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                lbres.Items.Add("Naam:" +reader.GetString(0) +" " + reader.GetString(1));
                                lbres.Items.Add("reservatieplaats: "+reader.GetInt32(2));
                                lbres.Items.Add("__________________________________________________________");
                            }                                   
                          }
                        else
                        {
                            MessageBox.Show(naam + " is al ingecheckt");
                        }

                    }
                    else
                    {
                        MessageBox.Show(naam + " heeft nog niet betaald");
                    }
                }
                else
                {
                    MessageBox.Show("tag niet bekend");
                }
            
            
                
                
            }
        }
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinaryFormatter fmt;
            using (FileStream f = new FileStream("lijst.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fmt = new BinaryFormatter();
                fmt.Serialize(f, bezoekerlijst);

            }
        }
        public void btnuitloggen_Click(object sender, System.EventArgs e)
        {
            BinaryFormatter fmt;
            using (FileStream f = new FileStream("lijst.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fmt = new BinaryFormatter();
                fmt.Serialize(f, bezoekerlijst);

            }
            bezoekerlijst.Clear();

            foreach (Control c in dynControls)
            {
                this.Controls.Remove(c);
            }
            gbLogin.Visible = true;
            pagina = "";
        }
             
        
    }
}
