using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Verhuur
    {
        public int VerhuurID { get; set; }
        public int ProductExemplaarID { get; set; }
        public int ResPbID { get; set; }
        public DateTime DatumIn { get; set; }
        public DateTime DatumUit { get; set; }
        public double Prijs { get; set; }
        public int Betaald { get; set; }

        public Verhuur(int verhuurid, int productexemplaarid, int respbid, DateTime datumIn, DateTime datumUit, double prijs, int betaald)
        {
            this.VerhuurID = verhuurid;
            this.ProductExemplaarID = productexemplaarid;
            this.ResPbID = respbid;
            this.DatumIn = datumIn;
            this.DatumUit = datumUit;
            this.Prijs = prijs;
            this.Betaald = betaald;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}