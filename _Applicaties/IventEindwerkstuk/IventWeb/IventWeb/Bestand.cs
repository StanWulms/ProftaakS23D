using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Bestand
    {
        public int BijdrageID { get; set; }
        public int CategorieID { get; set; }
        public string BestandsLocatie { get; set; }
        public int Grootte { get; set; }

        public Bestand(int bijdrageid, int categorieid, string bestandsLocatie, int grootte)
        {
            this.BijdrageID = bijdrageid;
            this.CategorieID = categorieid;
            this.BestandsLocatie = bestandsLocatie;
            this.Grootte = grootte;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}