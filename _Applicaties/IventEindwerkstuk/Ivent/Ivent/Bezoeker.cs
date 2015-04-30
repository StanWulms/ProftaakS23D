using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Bezoeker
    {
        public int BezoekerID { get; set; }
        public string RFID { get; set; }
        public string Toegankelijkheid { get; set; }
        public string VoorNaam { get; set; }
        public string AchterNaam { get; set; }
        public string AccountNaam { get; set; }
        public string AccountWachtwoord { get; set; }
        public bool Betaald { get; set; }

        public Bezoeker(int bezoekerid, string rfid, string toegankelijkheid, string voorNaam, string achterNaam, string accountNaam, string accountWachtwoord, int betaald)
        {
            this.BezoekerID = bezoekerid;
            this.RFID = rfid;
            this.Toegankelijkheid = toegankelijkheid;
            this.VoorNaam = voorNaam;
            this.AchterNaam = achterNaam;
            this.AccountNaam = accountNaam;
            this.AccountWachtwoord = accountWachtwoord;
            if (betaald == 1)
            {
                this.Betaald = true;
            }
            else
            {
                this.Betaald = false;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
