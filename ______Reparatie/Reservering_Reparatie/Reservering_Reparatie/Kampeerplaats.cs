using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Kampeerplaats
    {
        public int ID { get; set; }
        public String Nummer { get; set; }
        public int Capaciteit { get; set; }
        public bool Gereserveerd { get; set; }

        public Kampeerplaats(string nummer, int capaciteit, bool gereserveerd)
        {
            this.Nummer = nummer;
            this.Capaciteit = capaciteit;
            this.Gereserveerd = gereserveerd;
        }

        public Kampeerplaats(int id, string nummer, int capaciteit, bool gereserveerd)
        {
            this.ID = id;
            this.Nummer = nummer;
            this.Capaciteit = capaciteit;
            this.Gereserveerd = gereserveerd;
        }

        public Kampeerplaats()
        {

        }     
        
        /// <summary>
        /// Returnt een lijst met alle kampeerplaatsen waarvan "Gereserveerd" 0 is.
        /// </summary>
        /// <param name="kampeerplaatsen">Lijst met alle plaatsen van het event</param>
        /// <returns>Lijst met alle vrije plaatsen</returns>
        public List<Kampeerplaats> ZoekVrijePlek(List<Kampeerplaats> kampeerplaatsen)
        {
            List<Kampeerplaats> kamprs = new List<Kampeerplaats>();
            foreach (Kampeerplaats k in kampeerplaatsen)
            {
                if (k.Gereserveerd == false)
                {
                    kamprs.Add(k);
                }
            }
            return kamprs;
        }

        /// <summary>
        /// De ToString methode zorgt dat je het ondergegeven formaat krijgt als je op een Kampeerplaats .ToString() aanroept.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Nummer: " + Nummer + " - " + "Capaciteit: " + Capaciteit;
        }
    }
}