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

namespace LoginFunctie
{
    public partial class Form1 : Form
    {
        bool error = false;
        bool loggin = false;
        string gebruikersnaam;
        string wachtwoord;
        List<Bezoeker> bezoekers = new List<Bezoeker>();
        private OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
            conn = new OracleConnection();
            ConnectDatabase();
        }

        public void ConnectDatabase()
        {
            try
            {
                String user = "dbi324575"; //gebruikersnaam van je database
                String pw = "YOqeyTrQIY"; //wachtwoord van je database
                conn.ConnectionString = "User Id=" + user + ";Password=" + pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";  //string om verbinding te maken, kan ook handmatig
                conn.Open(); //opent connectie met de Connectionstring die voor deze connectie is ingesteld
                MessageBox.Show("Connectie gelukt!");
            }
            catch { error = true; }
            if (error)
            {
                error = false;
                throw new DatabaseConnectionFailed(" Er ging iets mis met de connectie");
            }

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
                dr.Close();
                cmd.Dispose(); //verwijdert command
                conn.Close();
            }
            catch { MessageBox.Show("Geen gegevens in de database gevonden."); }
        }

        private void btnInloggen_Click(object sender, EventArgs e)
        {
            gebruikersnaam = tbUsername.Text;
            wachtwoord = tbPassword.Text;
            foreach (Bezoeker b in bezoekers)
            {
                if (b.AccountNaam == gebruikersnaam && b.AccountWachtwoord == wachtwoord)
                {
                    this.loggin = true;
                    //TODO: match toegangscode met behorende applicatie.
                    if (b.Toegankelijkheid == "" /*|| "" etc)*/)
                    {
                        //TODO: open je form.
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
    }
}
