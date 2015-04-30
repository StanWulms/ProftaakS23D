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
using Phidgets;
using Phidgets.Events;

namespace InschrijvingSysteem
{
    public partial class Registerform : Form
    {
        bool loggin = false;
        string gebruikersnaam;
        string wachtwoord;

        //classes
        Database database = new Database();
        Registratie registratie = new Registratie();

        //db connection
        String dbconnect_user = "dbi324575";
        String dbconnect_pw = "YT4Yr6gF81";
        private OracleConnection conn;

        //RFID reader Phidgets
        private RFID rfid;
        
        //Database gegevens lists
        List<Bezoeker> bezoekers = new List<Bezoeker>();
        //List<Woonplaats> woonplaatsen = new List<Woonplaats>();
        //List<Voorwerp> voorwerpen = new List<Voorwerp>();
        //List<Staplaats> staplaatsen = new List<Staplaats>();



        public Registerform()
        {
            rfid = new RFID();
            InitializeComponent();
            gbLogin.Location = new Point(242,154);

            rfid.Attach += new AttachEventHandler(rfid_Attach);
            rfid.Detach += new DetachEventHandler(rfid_Detach);
            rfid.Tag += new TagEventHandler(rfid_Tag);
            openCmdLine(rfid);
            conn = new OracleConnection();
        }


        private void Registerform_Load(object sender, EventArgs e)
        {

            #region bezoekers uit DB laden naar list.bezoekers
            string user = "dbi324575";
            string pw = "YT4Yr6gF81";
            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();

            try
            {
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT RFID, accountnaam, accountwachtwoord, toegankelijkheid FROM bezoeker";

                OracleDataReader dr = cmd.ExecuteReader(); //een OracleDataReader aanmaken en deze linken aan het command dat je zojuist hebt gemaakt.
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Bezoeker b = new Bezoeker(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3));
                    bezoekers.Add(b);
                }

            }
            catch { MessageBox.Show("Geen gegevens in de database gevonden."); }

                conn.Close();
            #endregion

        }



        #region loginform
        private void btnInloggen_Click(object sender, EventArgs e)
        {
            gebruikersnaam = tbUsername.Text;
            wachtwoord = tbPassword.Text;
            foreach (Bezoeker b in bezoekers)
            {
                if (b.Accountnaam == gebruikersnaam && b.Accountwachtwoord == wachtwoord)
                {
                    this.loggin = true;
                    if ( (b.Toegankelijkheid == "D") || (b.Toegankelijkheid == "E") || (b.Toegankelijkheid == "F") )
                    {
                        pnl_reserveringsmenu.Visible = true;
                       
                        gbLogin.Visible = false;
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

        #endregion



        #region Registratieform
        /// <summary>
        /// neemt de gegevens ingevoerd in de textboxes en insert deze in de database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_registreerpersoon_Click(object sender, EventArgs e)
        {

            #region set het betaald of niet betaald zijn.

                int betaald = registratie.CheckBetaald(rb_betaaldja.Checked);

            #endregion


            #region controle op lege verplichte velden

            List<TextBox> verplichtevelden = new List<TextBox> {txtbox_voornaam, txtbox_achternaam, txtbox_accountnaam, txtbox_adresstraat, txtbox_adresgetal, txtbox_geboortedag, txtbox_geboortemaand, txtbox_geboortejaar, txtbox_woonplaats, txtbox_postcodeletters, txtbox_postcodegetallen };
            
                foreach (TextBox txt in verplichtevelden)
                {
                    if (registratie.CheckVerplicht(txt, "ERROR: vul verplichte velden (*) in."))
                    { return; }
                }

            #endregion


            #region controle of bepaalde textboxes (waar nodig) ingevuld zijn met getallen

                List<TextBox> verplichtegetal = new List<TextBox> {txtbox_geboortedag, txtbox_geboortemaand, txtbox_geboortejaar, txtbox_adresgetal, txtbox_postcodegetallen};
                
                foreach (TextBox txtbox in verplichtegetal)
                {
                    if (registratie.CheckGetal(txtbox))
                    { return; } 
                }

            #endregion


            #region maandcontrole + aanmaak geboortedatum String

                //controleert of maand bestaat
                if (Convert.ToInt32(txtbox_geboortemaand.Text) > 12)
                {
                    MessageBox.Show("ERROR: vul een bestaande maand in.");
                    return;
                }

                String[] maand = new String[12] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
                
                String geboortedatum = txtbox_geboortedag.Text + "-" + maand[Convert.ToInt32(txtbox_geboortemaand.Text)-1] + "-" + txtbox_geboortejaar.Text;
            
                //controleert of het een correcte datum is
                DateTime b;
                if (DateTime.TryParse(geboortedatum, out b) == false)
                {
                    MessageBox.Show("ERROR: vul een correcte datum in.");
                    return;
                }

                if (Convert.ToDateTime(geboortedatum) > System.DateTime.Today)
                {
                    MessageBox.Show("ERROR: vul een correcte datum in.");
                    return;
                }

            #endregion


            #region genereert een random wachtwoord

                String wachtwoord = registratie.GenereerWachtwoord();

            #endregion


            #region controleert of de persoon niet al in de database bestaat
            //controleert accountnaam
            foreach (Bezoeker bezx in bezoekers)
            {
                if (txtbox_accountnaam.Text == bezx.Accountnaam)
                {
                    MessageBox.Show("ERROR: accountnaam bestaat al in de database.");
                    return;
                }
                
            }

            #endregion



            #region SQL query om de ingevoerde gegevens in de persoontabel te registreren

                String registerpersonquery = "INSERT INTO BEZOEKER (RFID, toegankelijkheid, voornaam, achternaam, accountnaam, accountwachtwoord, betaald, geboortedatum) VALUES('" + txtbox_rfid.Text + "','A','" + txtbox_voornaam.Text + "','" + txtbox_achternaam.Text + "','" + txtbox_accountnaam.Text + "','" + wachtwoord + "'," + betaald + ",'" + geboortedatum + "')";

                database.ExecuteInsertQuery(registerpersonquery);

                Bezoeker x = new Bezoeker(txtbox_rfid.Text, txtbox_accountnaam.Text, wachtwoord, "A");
                bezoekers.Add(x);

            #endregion


            #region SQL query om de ingevoerde gegevens in de woonplaatstabel te registreren

                String registerwoonplaatsquery = "INSERT INTO WOONPLAATS (bezoekerID, woonplaats, postcodenumeriek, postcodealfanumeriek, straatnaam, huisnummer, toevoeging) VALUES((SELECT bezoekerID FROM BEZOEKER WHERE RFID = '" + txtbox_rfid.Text + "'),'" + txtbox_woonplaats.Text + "'," + txtbox_postcodegetallen.Text + ",'" + txtbox_postcodeletters.Text + "','" + txtbox_adresstraat.Text + "'," + txtbox_adresgetal.Text + ",'" + txtbox_adrestoevoeging.Text + "')";

                database.ExecuteInsertQuery(registerwoonplaatsquery);

            #endregion


            MessageBox.Show(txtbox_voornaam.Text + " " + txtbox_achternaam.Text + " geregistreerd!");
        }



        /// <summary>
        /// controleert of een accountnaam aanwezig is in de database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_betaald_check_Click(object sender, EventArgs e)
        {
            String getbetaaldquery = "SELECT betaald FROM BEZOEKER WHERE accountnaam = '" + txtbox_betaald_accountnaam.Text + "'";

            conn.ConnectionString = "User Id=" + dbconnect_user + ";Password=" + dbconnect_pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
            conn.Open();

            OracleCommand cmd = new OracleCommand("", conn);
            cmd.CommandText = getbetaaldquery;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            
            if (dr.Read() == false)
            {
                txtbox_betaald_naamcheck.Text = "persoon niet gevonden.";
                conn.Close();
                return;
            }

            if (Convert.ToInt32(dr.GetInt32(0)) == 1)
            {
                txtbox_betaald_naamcheck.Text = "persoon heeft al betaald.";
                conn.Close();
                return;
            }

            conn.Close();
            txtbox_betaald_naamcheck.Text = txtbox_betaald_accountnaam.Text;
        }
            

        /// <summary>
        /// update de database voor de persoon gekoppeld aan de gecontroleerde accountnaam, zodat hij/zij geregistreerd is als betaald.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_registreeralsbetaald_Click(object sender, EventArgs e)
        {   
            if (txtbox_betaald_naamcheck.Text == "")
            {
                MessageBox.Show("check eerst een naam.");
                return;
            }

            //SQL query om een bezoeker's betaald parameter te updaten naar "1" (wel betaald)
            String betaaldregistreerquery = "UPDATE BEZOEKER SET betaald = 1 WHERE accountnaam = '" + txtbox_betaald_naamcheck.Text + "'";
            database.ExecuteInsertQuery(betaaldregistreerquery);

            MessageBox.Show(txtbox_betaald_naamcheck.Text + " geregistreerd als betaald.");
        }


        private void btn_openmateriaalreservering_Click(object sender, EventArgs e)
        {
        
        }

        private void btn_openplaatsreservering_Click(object sender, EventArgs e)
        {
            pnl_plaatsreservering.Visible = true;
            pnl_reserveringsmenu.Visible = false;
            picbox_kaart.Visible = true;
        }

        private void btn_reserveringloguit_Click(object sender, EventArgs e)
        {
            pnl_reserveringsmenu.Visible = false;
            gbLogin.Visible = true;
        }

        #endregion



        #region Plaatsreservering



        private void btn_setreserveerder_Click(object sender, EventArgs e)
        {
            String getbetaaldquery = "SELECT betaald FROM BEZOEKER WHERE accountnaam = '" + txtbox_resaccountnaam.Text + "'";

            conn.ConnectionString = "User Id=" + dbconnect_user + ";Password=" + dbconnect_pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
            conn.Open();

            OracleCommand cmd = new OracleCommand("", conn);
            cmd.CommandText = getbetaaldquery;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == false)
            {
                MessageBox.Show("persoon niet gevonden.");
                conn.Close();
                return;
            }
            else
            {
                txtbox_reserveerder.Text = txtbox_resaccountnaam.Text;
                conn.Close();
            }
        }


        private void btn_voegpersoontoe_Click(object sender, EventArgs e)
        {
            String getbetaaldquery = "SELECT betaald FROM BEZOEKER WHERE accountnaam = '" + txtbox_listaccountnaam.Text + "'";

            conn.ConnectionString = "User Id=" + dbconnect_user + ";Password=" + dbconnect_pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
            conn.Open();

            OracleCommand cmd = new OracleCommand("", conn);
            cmd.CommandText = getbetaaldquery;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == false)
            {
                txtbox_betaald_naamcheck.Text = "persoon niet gevonden.";
                conn.Close();
                return;
            }
            else
            {
                foreach (String accnaam in lb_personen.Items)
                {
                    if (accnaam == txtbox_listaccountnaam.Text)
                    {
                        MessageBox.Show("persoon staat al in de lijst.");
                        return;
                    }

                }
                lb_personen.Items.Add(txtbox_listaccountnaam.Text);
                conn.Close();
            }

        }


        private void btn_verwijderpersoon_Click(object sender, EventArgs e)
        {
            lb_personen.Items.Remove(lb_personen.SelectedItem);
        }


        private void btn_leeglijst_Click(object sender, EventArgs e)
        {
            lb_personen.Items.Clear();
        }


        private void btn_plaatsreserveren_Click(object sender, EventArgs e)
        {
            if (txtbox_reserveerder.Text == "")
            {
                MessageBox.Show("ERROR: selecteer een reserveerder.");
                return;
            }

            if (lb_personen.Items.Count ==  0)
            {
                MessageBox.Show("ERROR: zet personen in de list.");
                return;
            }

            foreach (string accnaam in lb_personen.Items)
            {
                String bezoekeridquery = "SELECT bezoekerID FROM BEZOEKER WHERE accountnaam = '" + accnaam + "'";
                String reserveerderidquery = "SELECT bezoekerID FROM BEZOEKER WHERE accountnaam = '" + txtbox_reserveerder.Text + "'";

                String[] maand = new String[12] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

                String begindatum = dtp_begindatum.Value.Day + "-" + maand[dtp_begindatum.Value.Month - 1] + "-" + dtp_begindatum.Value.Year;
                String einddatum = dtp_einddatum.Value.Day + "-" + maand[dtp_einddatum.Value.Month - 1] + "-" + dtp_einddatum.Value.Year;


                String reserveerplaatsquery = "INSERT INTO RESERVERING(bezoekerID, plaatsID, reserveringsbegindatum, reserveringseinddatum, reserveerder) VALUES((" + bezoekeridquery + ")," + nud_campingplaats.Value + ",'" + begindatum +"','" + einddatum +"',(" + reserveerderidquery + "))";

                database.ExecuteInsertQuery(reserveerplaatsquery);
            }
            MessageBox.Show("personen geregistreerd.");

        }
        

        private void btn_prterug_Click(object sender, EventArgs e)
        {
            pnl_plaatsreservering.Visible = false;
            pnl_reserveringsmenu.Visible = true;
            picbox_kaart.Visible = false;
        }


        #endregion



        #region MateriaalReservering (leeg)



        #endregion

        

        #region Methodes voor de RFID scanner
        /// <summary>
        /// registreert of de RFID scanner aangesloten wordt. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rfid_Attach(object sender, AttachEventArgs e)
        {
            RFID attached = (RFID)sender;
            rfid.Antenna = true;
        }

        /// <summary>
        /// registreert of de RFID scanner losgekoppeld wordt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rfid_Detach(object sender, DetachEventArgs e)
        {
            RFID detached = (RFID)sender;
        }

        /// <summary>
        /// vult de RFID textbox met de data van het gelezen RFID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rfid_Tag(object sender, TagEventArgs e)
        {
            this.txtbox_rfid.Text = e.Tag;
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


        #endregion

    }
}
