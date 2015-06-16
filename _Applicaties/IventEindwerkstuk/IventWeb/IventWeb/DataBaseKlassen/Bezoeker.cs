using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb.DataBaseKlassen
{
    public class Bezoeker
    {
        public int Aanwezig { get; set; }
        public string Naam { get; set; }
        public Bezoeker(string naam, int aanwezig)
        {
            this.Aanwezig = aanwezig;
            this.Naam = naam;       
        }
    }
}