using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Polsbandje
    {
        public int PolsbandjeID { get; set; }
        public string Barcode { get; set; }
        public int Actief { get; set; }

        public Polsbandje(int polsbandjeid, string barcode, int actief)
        {
            this.PolsbandjeID = polsbandjeid;
            this.Barcode = barcode;
            this.Actief = actief;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}