using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Voorwerp
    {
        public int VoorwerpID { get; set; }
        public string VoorwerpSoort { get; set; }
        public string  VoorwerpMerk { get; set; }
        public double VoorwerpHuurPrijs { get; set; }

        public Voorwerp(int voorwerpid, string voorwerpSoort, string voorwerpMerk, double voorwerpHuurPrijs)
        {
            this.VoorwerpID = voorwerpid;
            this.VoorwerpSoort = voorwerpSoort;
            this.VoorwerpMerk = voorwerpMerk;
            this.VoorwerpHuurPrijs = voorwerpHuurPrijs;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
