﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reservering_Reparatie
{
    public class Hoofdboeker
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Woonplaats { get; set; }
        public string Iban { get; set; }

        public Hoofdboeker(string naam, string tussenvoegsel, string achternaam, string straat, string huisnummer, string woonplaats, string iban)
        {
            this.Naam = naam;
            this.Tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.Straat = straat;
            this.Huisnummer = huisnummer;
            this.Woonplaats = woonplaats;
            this.Iban = iban;
        }

        public Hoofdboeker(int id, string naam, string tussenvoegsel, string achternaam, string straat, string huisnummer, string woonplaats, string iban)
        {
            this.ID = id;
            this.Naam = naam;
            this.Tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.Straat = straat;
            this.Huisnummer = huisnummer;
            this.Woonplaats = woonplaats;
            this.Iban = iban;
        }

        public Hoofdboeker()
        {

        }

        /// <summary>
        /// De ToString methode zorgt dat je het ondergegeven formaat krijgt als je op een Hoofdboeker .ToString() aanroept.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ID: " + ID + " - " + "Naam: " + Naam + " - " + "Achternaam: " + Achternaam + " - " + "Iban: " + Iban;
        }
    }
}