using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Specificatie
    {
        public int SpecificatieID { get; set; }
        public string Naam { get; set; }

        public Specificatie(int specificatieid, string naam)
        {
            this.SpecificatieID = specificatieid;
            this.Naam = naam;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}