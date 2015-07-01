using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Account
    {
        public int ID { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Activatiehash { get; set; }

        public Account(string gebruikersnaam, string email, string activatiehash)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Email = email;
            this.Activatiehash = activatiehash;
        }

        public Account(int id, string gebruikersnaam, string email, string activatiehash)
        {
            this.ID = id;
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