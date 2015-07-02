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

        public int ID { get; set; }
        public String BeginDatum { get; set; }
        public String EindDatum { get; set; }

        public Boeking(String beginDatum, String eindDatum)
        {
            this.BeginDatum = beginDatum;
            this.EindDatum = eindDatum;
            accounts = new List<Account>();
            hoofdboeker = new Hoofdboeker();
            kampeerplaats = new Kampeerplaats();
            db = new Database();
        }

        public Boeking(int id, String beginDatum, String eindDatum)
        {
            this.ID = id;
            this.BeginDatum = beginDatum;
            this.EindDatum = eindDatum;
            accounts = new List<Account>();
            hoofdboeker = new Hoofdboeker();
            kampeerplaats = new Kampeerplaats();
            db = new Database();
        }

        public Boeking()
        {
            accounts = new List<Account>();
            hoofdboeker = new Hoofdboeker();
            kampeerplaats = new Kampeerplaats();
            db = new Database();
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
                else if (b.Achternaam.Contains(zoekCriteria))
                {
                    hfdb.Add(b);
                }
            }
            return hfdb;
        }

        public Hoofdboeker ZoekJuisteHoofdboeker(string id)
        {
            Hoofdboeker hfdb = new Hoofdboeker();
            List<Hoofdboeker> hoofdboekers = new List<Hoofdboeker>();
            hoofdboekers = db.GetAllHoofdboekers();
            foreach (Hoofdboeker b in hoofdboekers)
            {
                if (b.ID == Convert.ToInt32(id))
                {
                    hfdb = b;
                }
            }
            return hfdb;
        }

        /// <summary>
        /// Zoekt naar een kampeerplaats waarvan het ingevulde nummer overeenkomt met het nummer van de plek.
        /// Als er een match is gevonden wordt de desbetreffende kampeerplaats geretourneerd.
        /// </summary>
        /// <param name="email">Nummer van de kampeerplaats</param>
        public Kampeerplaats ZoekKampeerplaats(string nummer)
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
        /// Zoek zowel de gereserveerde als de lege kampeerplaatsen.
        /// </summary>
        /// <returns></returns>
        public List<Kampeerplaats> ZoekAlleKampeerplaatsen()
        {
            List<Kampeerplaats> kampeerplaatsen = new List<Kampeerplaats>();
            kampeerplaatsen = db.GetAllKampeerplaatsen();
            return kampeerplaatsen;
        }

        /// <summary>
        /// Het aanmaken van een nieuwe persoon (Hoofdboeker)
        /// </summary>
        /// <param name="hoofdboeker"></param>
        /// <returns></returns>
        public string maakpersoon(Hoofdboeker hoofdboeker)
        {
            if(db.maakpersoon(hoofdboeker) != "goed")
            {
                return db.maakpersoon(hoofdboeker);
            }
            else
            {
                return "Persoon aangemaakt.";
            }
        }

        /// <summary>
        /// Het aanmaken van nieuwe accounts.
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public string MaakAccounts(List<Account> accounts)
        {
            foreach(Account a in accounts)
            {
                if(db.checknaamemail(a) != "Goed")
                {
                    return db.checknaamemail(a);
                }
            }
            foreach(Account a in accounts)
            {
                if(db.accountmaken(a) != "account aangemaakt")
                {
                    return db.accountmaken(a);
                }
            }
            return "Account(s) aangemaakt.";
        }

        /// <summary>
        /// Zoeken naar de laatst ingevoerde hoofdboeker
        /// </summary>
        /// <param name="hb"></param>
        /// <returns></returns>
        public Hoofdboeker LaatsteHoofdbezoeker(Hoofdboeker hb)
        {
            Hoofdboeker hoofdbezoeker = db.ZoekLaatstGeInsertBoeker(hb);
            return hoofdbezoeker;
        }

        //retourneert het nieuwe account id
        public int GetMaxAccount()
        {
            int id = db.GetMaxAccount();
            return id;
        }

        /// <summary>
        /// De methode Boek wordt als laatste aageroepen en dat is ook de plek
        /// vanwaar de reservering wordt aangepast in de database.
        /// </summary>
        /// <param name="beginDatum">Begin datum van het event</param>
        /// <param name="eindDatum">Eind datum van het event</param>
        public void Boek(string beginDatum, string eindDatum, string nummer)
        {
            //Boeker, kampeerplaats en accounts worden aan de boeking gekoppeld.
            Hoofdboeker hoofdboeker = (Hoofdboeker)System.Web.HttpContext.Current.Session["Hoofdboeker"];
            accounts = (List<Account>)System.Web.HttpContext.Current.Session["Accounts"];
            Kampeerplaats kampeerplaats = ZoekKampeerplaats(nummer);
 
            Boeking b = new Boeking(1, beginDatum, eindDatum);
            db.InsertReservering(b, hoofdboeker);            
            b.ID = db.GetMaxReservering();
            db.InsertPlekReservering(b, kampeerplaats);
            
            foreach(Account account in accounts)
            {
              string polsbandje = db.maakpolsbandje();                
              db.reserveer_polsbandje(b.ID, account, Convert.ToInt32(polsbandje));                           
            }
            
        }
        
        /// <summary>
        /// De ToString methode zorgt dat je het ondergegeven formaat krijgt als je op een Boeking .ToString() aanroept.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ID: " + ID + " - " + "Begindatum: " + BeginDatum + " - " + "Einddatum: " + EindDatum;
        }
    }
}