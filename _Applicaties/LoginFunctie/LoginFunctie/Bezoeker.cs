using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginFunctie
{
    class Bezoeker
    {
        public string Toegankelijkheid { get; set; }
        public string AccountNaam { get; set; }
        public string AccountWachtwoord { get; set; }


        public Bezoeker(string toegang, string accountnaam, string accountwachtwoord)
        {
            this.Toegankelijkheid = toegang;
            this.AccountNaam = accountnaam;
            this.AccountWachtwoord = accountwachtwoord;
        }
    }
}
