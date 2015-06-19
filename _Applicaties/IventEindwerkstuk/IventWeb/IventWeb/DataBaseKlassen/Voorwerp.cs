using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb.DataBaseKlassen
{
    public class Voorwerp
    {
        public bool verhuurd = false;

        public int exemplaarnummer { get; set; }
        public string serie { get; set; }
        public string merk { get; set; }
        public string categorie { get; set; }
        public int prijs { get; set; }
        public bool Verhuurd
        {
            get { return verhuurd; }
            set { verhuurd = value; }
        }
        public Voorwerp(int exemplaarnummer, string serie, string merk, string categorie, int prijs)
        {
            this.exemplaarnummer = exemplaarnummer;
            this.serie = serie;
            this.merk = merk;
            this.categorie = categorie;
            this.prijs = prijs;
        }
        public override string ToString()
        {
            return "nummer: " + Convert.ToString(exemplaarnummer) + " serie: " + serie + " merk: " + merk + " categorie: " + categorie + " prijs: " + Convert.ToString(prijs);
        }
    }
}