using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Boeking
    {
        //Fields
        List<Account> accounts;
        Hoofdboeker hoofdboeker;
        Kampeerplaats kampeerplaats;

        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public Boeking(DateTime beginDatum, DateTime eindDatum)
        {
            this.BeginDatum = beginDatum;
            this.EindDatum = eindDatum;
            accounts = new List<Account>();
            hoofdboeker = new Hoofdboeker();
            kampeerplaats = new Kampeerplaats();
        }

        public void Boek(DateTime beginDatum, DateTime eindDatum)
        {
            //Boeker, kampeerplaats en accounts worden aan de boeking gekoppeld.
            hoofdboeker = hoofdboeker.ZoekBezoeker("email");
            kampeerplaats = kampeerplaats.ZoekPlek(0);
            //accounts.Items.Add(account.ZoekPlek("nummer"));
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}