using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InschrijvingSysteem
{
    class Registratie
    {


        public bool CheckVerplicht(TextBox textbox, String errormessage)
        {
            if (textbox.Text == "")
            {
                MessageBox.Show(errormessage);
                return true;
            }
            else return false;
        }

        public bool CheckGetal(TextBox textbox)
        {
            int a;
            if (Int32.TryParse(textbox.Text, out a) == false)
            {
                MessageBox.Show("ERROR: vul een getal in bij getalverplichte velden (**).");
                return true;
            }
            else return false;
        }

        public int CheckBetaald(bool check)
        {
            int betaald;
            if (check == true)
            { betaald = 1; }
            else
            { betaald = 0; }

            return betaald;
        }

        public String GenereerWachtwoord()
        {
            Random rnd = new Random();

            List<string> tekens = new List<String>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            String ww = (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)] + (string)tekens[rnd.Next(tekens.Count)];

            return ww;
        }
    }
}

