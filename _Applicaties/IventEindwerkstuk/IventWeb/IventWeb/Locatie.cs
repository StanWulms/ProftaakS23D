using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Locatie
    {
        public int LocatieID { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Nr { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }

        public Locatie(int locatieid, string naam, string straat, string nr, string postcode, string plaats)
        {
            this.LocatieID = locatieid;
            this.Naam = naam;
            this.Straat = straat;
            this.Nr = nr;
            this.Postcode = postcode;
            this.Plaats = plaats;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}