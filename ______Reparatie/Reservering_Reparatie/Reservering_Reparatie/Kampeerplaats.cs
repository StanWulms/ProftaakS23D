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
        /// Zet de boolean "gereserveerd" van de kampeerplek die overeenkomt met het
        /// ingevoerde nummer op true. De plaats is vervolgens gereserveerd.
        /// </summary>
        /// <param name="Nummer">Nummer van de plaats</param>
        public void Reserveer(int Nummer)
        {
            //TODO: implementeer methode
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

        public override string ToString()
        {
            return "Nummer: " + Nummer + " - " + "Capaciteit: " + Capaciteit;
        }
    }
}