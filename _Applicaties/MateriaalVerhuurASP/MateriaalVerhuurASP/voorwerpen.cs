﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MateriaalVerhuurASP
{
    public class voorwerpen
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
            set{ verhuurd = value; }
        }
        public voorwerpen(int exemplaarnummer, string serie, string merk, string categorie, int prijs)
        {
            this.exemplaarnummer = exemplaarnummer;
            this.serie = serie;
            this.merk = merk;
            this.categorie = categorie;
            this.prijs = prijs;
        }
    }
}