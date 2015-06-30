using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Hoofdboeker
    {
        public string Naam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Iban { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Woonplaats { get; set; }
        public string Postcode { get; set; }

        public Hoofdboeker(string naam, string tussenvoegsel, string achternaam, string email, string iban, string straat, string huisnummer, string woonplaats, string postcode)
        {
            this.Naam = naam;
            this.Tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.Email = email;
            this.Iban = iban;
            this.Straat = straat;
            this.Huisnummer = huisnummer;
            this.Woonplaats = woonplaats;
            this.Postcode = postcode;
        }

        public Hoofdboeker()
        {

        }

        /// <summary>
        /// Zoekt naar een hoofdboeker waarvan de ingevulde email overeenkomt met zijn email.
        /// Als er een match is gevonden wordt de desbetreffende boeker geretourneerd.
        /// </summary>
        /// <param name="email">Email van de hoofdboeker</param>
        public Hoofdboeker ZoekBezoeker(string email)
        {
            return null;
        }
    }
}