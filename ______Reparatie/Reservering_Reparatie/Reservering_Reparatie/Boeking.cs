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
        Database db;

        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public Boeking(DateTime beginDatum, DateTime eindDatum)
        {
            this.BeginDatum = beginDatum;
            this.EindDatum = eindDatum;
            accounts = new List<Account>();
            hoofdboeker = new Hoofdboeker();
            kampeerplaats = new Kampeerplaats();
            db = new Database();
        }

        /// <summary>
        /// Zoekt naar een account waarvan de ingevulde gebruikersnaam overeenkomt met zijn gebruikersnaam.
        /// Als er een match is gevonden wordt het desbetreffende account geretourneerd.
        /// </summary>
        /// <param name="email">Email van de hoofdboeker</param>
        public Account ZoekAccount(string gebruikersnaam)
        {
            Account acc = new Account();
            List<Account> accounts = new List<Account>();
            accounts = db.GetAllAccounts();
            foreach (Account a in accounts)
            {
                if (a.Gebruikersnaam == gebruikersnaam)
                {
                    acc = a;
                }
            }
            return acc;
        }

        /// <summary>
        /// Zoekt naar een hoofdboeker waarvan de ingevulde zoekcriteria overeenkomt met zijn naam of achternaam.
        /// Als er een match is gevonden wordt de desbetreffende boeker geretourneerd.
        /// </summary>
        /// <param name="zoekCriteria">String waarop gezocht wordt. Gaat over de naam en achternaam</param>
        public List<Hoofdboeker> ZoekHoofdboekers(string zoekCriteria)
        {
            List<Hoofdboeker> hfdb = new List<Hoofdboeker>();
            List<Hoofdboeker> hoofdboekers = new List<Hoofdboeker>();
            hoofdboekers = db.GetAllHoofdboekers();
            foreach (Hoofdboeker b in hoofdboekers)
            {
                if (b.Naam.Contains(zoekCriteria))
                {
                    hfdb.Add(b);
                }
                if (b.Achternaam.Contains(zoekCriteria))
                {
                    hfdb.Add(b);
                }
            }
            return hfdb;
        }

        /// <summary>
        /// Zoekt naar een kampeerplaats waarvan het ingevulde nummer overeenkomt met het nummer van de plek.
        /// Als er een match is gevonden wordt de desbetreffende kampeerplaats geretourneerd.
        /// </summary>
        /// <param name="email">Nummer van de kampeerplaats</param>
        public Kampeerplaats ZoekKampeerplaats(int nummer)
        {
            Kampeerplaats kampr = new Kampeerplaats();
            List<Kampeerplaats> kampeerplaatsen = new List<Kampeerplaats>();
            kampeerplaatsen = db.GetAllKampeerplaatsen();
            foreach (Kampeerplaats k in kampeerplaatsen)
            {
                if (k.Nummer == nummer)
                {
                    kampr = k;
                }
            }
            return kampr;
        }

        /// <summary>
        /// De methode Boek wordt als laatste aageroepen en dat is ook de plek
        /// vanwaar de reservering wordt aangepast in de database.
        /// </summary>
        /// <param name="beginDatum">Begin datum van het event</param>
        /// <param name="eindDatum">Eind datum van het event</param>
        public void Boek(DateTime beginDatum, DateTime eindDatum)
        {
            //Boeker, kampeerplaats en accounts worden aan de boeking gekoppeld.
            hoofdboeker = //De boeker van de reservering.
            kampeerplaats = ZoekKampeerplaats(0); //De kampeerplaats
            //accounts.Items.Add(account.ZoekPlek("nummer")); //Alle accounts die bij de reservering horen.
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}