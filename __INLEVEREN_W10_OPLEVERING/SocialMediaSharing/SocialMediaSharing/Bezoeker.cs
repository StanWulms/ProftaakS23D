using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Bezoeker
    {
        public string Toegankelijkheid { get; set; }
        public string AccountNaam { get; set; }
        public string AccountWachtwoord { get; set; }
        public int BezoekerID { get; set; }
        public string VoorNaam { get; set; }
        public string Achternaam { get; set; }


        public Bezoeker(string toegang, string accountnaam, string accountwachtwoord)
        {
            this.Toegankelijkheid = toegang;
            this.AccountNaam = accountnaam;
            this.AccountWachtwoord = accountwachtwoord;
        }

        public Bezoeker(string toegang, string accountnaam, string accountwachtwoord, int bezoekerID, string voornaam, string achternaam)
        {
            this.Toegankelijkheid = toegang;
            this.AccountNaam = accountnaam;
            this.AccountWachtwoord = accountwachtwoord;
            this.BezoekerID = bezoekerID;
            this.VoorNaam = voornaam;
            this.Achternaam = achternaam;
        }
    }
}
