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

namespace DBconnectietest
{
    public partial class Form1 : Form
    {

        private OracleConnection conn;

        public Form1()
        {
            InitializeComponent();

            conn = new OracleConnection();

        }

        private void btn_dbconnect_Click(object sender, EventArgs e)
        {
        String user="dbi314512"; //gebruikersnaam van je database
        String pw = "wTlSHwW51o"; //wachtwoord van je database
        conn.ConnectionString = "User Id="+user+";Password="+pw+";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";  //string om verbinding te maken, kan ook handmatig
        conn.Open(); //opent connectie met de Connectionstring die voor deze connectie is ingesteld
        btn_dbconnect.Enabled = false;
        MessageBox.Show("Connectie gelukt!");
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            string sql = "select voornaam FROM BEZOEKER where voornaam = 'hans'"; //je query

            OracleCommand cmd = new OracleCommand(sql, conn); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie

            cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text



            OracleDataReader dr = cmd.ExecuteReader(); //een OracleDataReader aanmaken en deze linken aan het command dat je zojuist hebt gemaakt.

            dr.Read(); //leest het OracleDatareader en daarmee het command dat je eraan linkt.

            label1.Text = dr.GetString(0); //zet te text van label 1 als de 1ste rij uit de resultaten (hier heb je dr.xxxxxxx, dus even goed uitzoeken wat voor iets je nodig hebt)

            dr.Dispose(); //verwijdert datareader

            cmd.Dispose(); //verwijdert command

            conn.Dispose(); //verwijdert connectie (heb je meestal niet nodig)

            


        }

        private void rtb_displayinfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql2 = "INSERT INTO ........";

            OracleCommand cmd2 = new OracleCommand(sql2, conn); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie

            cmd2.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text

            cmd2.ExecuteNonQuery(); //uitvoeren van een query, bedoeld voor INSERT, UPDATE en DELETE queries.
        }


    }
}
