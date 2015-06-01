using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Productexemplaar
    {
        public int ProductExemplarID { get; set; }
        public int ProductID { get; set; }
        public int VolgNummer { get; set; }
        public string Barcode { get; set; }

        public Productexemplaar(int productexemplaarid, int productid, int volgNummer, string barcode)
        {
            this.ProductExemplarID = productexemplaarid;
            this.ProductID = productid;
            this.VolgNummer = volgNummer;
            this.Barcode = barcode;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}