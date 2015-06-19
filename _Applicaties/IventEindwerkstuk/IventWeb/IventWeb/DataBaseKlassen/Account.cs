using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Account
    {
        public int AccountID { get; set; }
        public string GebruikersNaam { get; set; }
        public string Email { get; set; }
        public string ActivatieHash { get; set; }
        public int Geactiveerd { get; set; }

        public Account(int accountid, string gebruikersNaam, string email, string activatiehash, int geactiveerd)
        {
            this.AccountID = accountid;
            this.GebruikersNaam = gebruikersNaam;
            this.Email = email;
            this.ActivatieHash = activatiehash;
            this.Geactiveerd = geactiveerd;
        }

        public Account()
        {

        }

        public override string ToString()
        {
            return GebruikersNaam.ToString();
        }
    }
}