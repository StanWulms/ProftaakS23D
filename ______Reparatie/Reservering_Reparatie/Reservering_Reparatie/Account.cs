using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    [Serializable]
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

        public Account()
        {
            
        }

        public override string ToString()
        {
            return "Gebruikersnaam: " + Gebruikersnaam;
        }

    }
}