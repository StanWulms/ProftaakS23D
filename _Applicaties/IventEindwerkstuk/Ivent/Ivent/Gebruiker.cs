using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Gebruiker
    {
        public int GebruikerID { get; set; }
        public string GebruikersNaam { get; set; }
        public string GebruikersWachtwoord { get; set; }
        public string GebruikersEmail { get; set; }

        public Gebruiker(int gebruikerid, string GebruikersNaam, string gebruikersWachtwoord, string gebruikersEmail)
        {
            this.GebruikerID = gebruikerid;
            this.GebruikersNaam = GebruikersNaam;
            this.GebruikersWachtwoord = gebruikersWachtwoord;
            this.GebruikersEmail = gebruikersEmail;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
