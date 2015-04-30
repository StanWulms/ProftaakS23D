using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InschrijvingSysteem
{
    class Bezoeker
    {
        public String RFID;
        public String Accountnaam;
        public String Accountwachtwoord;
        public String Toegankelijkheid;

        public Bezoeker(String rfid, String accountnaam, String accountwachtwoord, String toegankelijkheid)
        {
            this.RFID = rfid;
            this.Accountnaam = accountnaam;
            this.Accountwachtwoord = accountwachtwoord;
            this.Toegankelijkheid = toegankelijkheid;
        }

    }
}
