using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace toegangscontrole_asp
{
    public class bezoekers
    {
        public int aanwezig { get; set; }
        public string naam { get; set; }
        public bezoekers(string naam, int aanwezig)
        {
            this.naam = naam;
            this.aanwezig = aanwezig;
        }
    }
}