using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Woonplaats
    {
        public int WoonplaatsID { get; set; }
        public int BezoekerID { get; set; }
        public string PlaatsNaam { get; set; }
        public int PostcodeNumeriek { get; set; }
        public string PostcodeAlfanumeriek { get; set; }
        public string StraatNaam { get; set; }
        public int HuisNummer { get; set; }
        public string HuisnummerToevoeging { get; set; }

        public Woonplaats(int woonplaats, int bezoekerid, string plaatsNaam, int postcodeNumeriek, string postcodeAlfanumeriek, string straatNaam, int huisNummer, string huisnummerToevoeging)
        {
            this.WoonplaatsID = woonplaats;
            this.BezoekerID = bezoekerid;
            this.PlaatsNaam = plaatsNaam;
            this.PostcodeNumeriek = postcodeNumeriek;
            this.PostcodeAlfanumeriek = postcodeAlfanumeriek;
            this.StraatNaam = straatNaam;
            this.HuisNummer = huisNummer;
            this.HuisnummerToevoeging = huisnummerToevoeging;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
