using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Product
    {
        public int ProductID { get; set; }
        public int ProductCatID { get; set; }
        public string Merk { get; set; }
        public string Serie { get; set; }
        public string TypeNummer { get; set; }
        public double Prijs { get; set; }

        public Product(int productid, int productcatid, string merk, string serie, string typeNummer, double prijs)
        {
            this.ProductID = productid;
            this.ProductCatID = productcatid;
            this.Merk = merk;
            this.Serie = serie;
            this.TypeNummer = typeNummer;
            this.Prijs = prijs;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}