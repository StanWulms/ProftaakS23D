using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Persoon
    {
        public int PersoonID { get; set; }
        public string VoorNaam { get; set; }
        public string TussenVoegsel { get; set; }
        public string AchterNaam { get; set; }
        public string Straat { get; set; }
        public string HuisNr { get; set; }
        public string WoonPlaats { get; set; }
        public string BankNr { get; set; }

        public Persoon(int persoonid, string voorNaam, string tussenVoegsel, string achterNaam, string straat, string huisNr, string woonPlaats, string bankNr)
        {
            this.PersoonID = persoonid;
            this.VoorNaam = voorNaam;
            this.TussenVoegsel = tussenVoegsel;
            this.AchterNaam = achterNaam;
            this.Straat = straat;
            this.HuisNr = huisNr;
            this.WoonPlaats = woonPlaats;
            this.BankNr = bankNr;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}