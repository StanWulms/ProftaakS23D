using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventbeheersysteemasp
{
    public class eventmaken
    { 
        
        public int eventid { get; set; }
        public string name { get; set; }
        public DateTime begindatum { get; set; }
        public DateTime einddatum { get; set; }
        public string plaats { get; set; }       
        public string postcode { get; set; }
        public string straat { get; set; }
        public string huisnummer { get; set; }

        public int aantal { get; set; }

        public eventmaken(int eventid, string name, DateTime begindatum, DateTime einddatum, string plaats, string postcode, string straat, string huisnummer, int aantal)
        {
            this.eventid = eventid;
            this.name = name;
            this.begindatum = begindatum;
            this.einddatum = einddatum;
            this.plaats = plaats;
            this.postcode = postcode;
            this.straat = straat;
            this.huisnummer = huisnummer;
            this.aantal = aantal;
        }
        
    }
}