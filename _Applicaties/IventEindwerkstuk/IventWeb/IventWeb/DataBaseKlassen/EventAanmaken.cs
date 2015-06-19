using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb.DataBaseKlassen
{
    public class EventAanmaken
    {

        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public string Plaats { get; set; }
        public string Postcode { get; set; }
        public string Straat { get; set; }
        public string HuisNummer { get; set; }
        public int Aantal { get; set; }

        public EventAanmaken(int eventid, string name, DateTime begindatum, DateTime einddatum, string plaats, string postcode, string straat, string huisnummer, int aantal)
        {
            this.EventID = eventid;
            this.Name = name;
            this.BeginDatum = begindatum;
            this.EindDatum = einddatum;
            this.Plaats = plaats;
            this.Postcode = postcode;
            this.Straat = straat;
            this.HuisNummer = huisnummer;
            this.Aantal = aantal;
        }
    }
}