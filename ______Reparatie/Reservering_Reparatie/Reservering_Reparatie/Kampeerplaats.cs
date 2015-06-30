using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Kampeerplaats
    {
        public int Nummer { get; set; }
        public int Capaciteit { get; set; }
        public bool Gereserveerd { get; set; }

        public Kampeerplaats(int nummer, int capaciteit, bool gereserveerd)
        {
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

        }

        /// <summary>
        /// Zoekt naar een kampeerplaats waarvan het ingevulde nummer overeenkomt met het nummer van de plek.
        /// Als er een match is gevonden wordt de desbetreffende kampeerplaats geretourneerd.
        /// </summary>
        /// <param name="email">Nummer van de kampeerplaats</param>
        public Kampeerplaats ZoekPlek(int nummer)
        {
            return null;
        }

        //Returnt een lijst met alle kampeerplaatsen waarvan "Gereserveerd" 0 is.
        public List<Kampeerplaats> ZoekVrijePlek()
        {
            return null;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}