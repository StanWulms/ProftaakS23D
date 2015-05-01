using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Categorie
    {
        public int BijdrageID { get; set; }
        public int SubCategorieID { get; set; }
        public string Naam { get; set; }

        public Categorie(int bijdrageid, int subcategorie, string naam)
        {
            this.BijdrageID = bijdrageid;
            this.SubCategorieID = subcategorie;
            this.Naam = naam;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}