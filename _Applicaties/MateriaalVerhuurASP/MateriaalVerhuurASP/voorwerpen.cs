using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MateriaalVerhuurASP
{
    public class voorwerpen
    {
        public int exemplaarnummer { get; set; }
        public string soort { get; set; }
        public string merk { get; set; }
        public bool gereserveerd { get; set; }

        public voorwerpen(int exemplaarnummer, string soort, string merk)
        {
            this.exemplaarnummer = exemplaarnummer;
            this.soort = soort;
            this.merk = merk;
        }
    }
}