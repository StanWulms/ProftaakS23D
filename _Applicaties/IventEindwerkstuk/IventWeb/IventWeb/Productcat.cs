using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Productcat
    {
        public int ProductCatID { get; set; }
        public int ProductCatSubID { get; set; }
        public string Naam { get; set; }

        public Productcat(int productcatid, int productcatsubid, string naam)
        {
            this.ProductCatID = productcatid;
            this.ProductCatSubID = productcatsubid;
            this.Naam = naam;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}