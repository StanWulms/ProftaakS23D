using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Account
    {
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Activatiehash { get; set; }

        public Account(string gebruikersnaam, string email, string activatiehash)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Email = email;
            this.Activatiehash = activatiehash;
        }

        /// <summary>
        /// Zoekt naar een account waarvan de ingevulde gebruikersnaam overeenkomt met zijn gebruikersnaam.
        /// Als er een match is gevonden wordt het desbetreffende account geretourneerd.
        /// </summary>
        /// <param name="email">Email van de hoofdboeker</param>
        public Account ZoekAccount(string gebruikersnaam)
        {
            return null;
        }
    }
}