using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace event_beheer_systeem
{
    class Bezoeker
    {
        
        public int AccountNaam { get; set; }
        public string AccountWachtwoord { get; set; }


        public Bezoeker(int accountnaam, string accountwachtwoord)
        {
            this.AccountNaam = accountnaam;
            this.AccountWachtwoord = accountwachtwoord;
        }
    }
}
