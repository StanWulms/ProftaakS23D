using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateriaalVerhuur
{
    class voorwerpen
    {
        private int exemplaarnummer;
        private string soort;
        private string merk;
        private bool verhuurd;
        public int Exemplaarnummer
        {
            get { return exemplaarnummer; }
            set { exemplaarnummer = value; }
        }
        public string Soort
        {
            get { return soort; }
            set { soort = value; }
        }
        public string Merk
        {
            get { return merk; }
            set { merk = value; }
        }
        public bool Verhuurd 
        { 
         get{return verhuurd;}
         set{verhuurd = value;}
        }
        public voorwerpen(int exemplaarnummer, string soort, string merk)
        {
            this.exemplaarnummer = exemplaarnummer;
            this.soort = soort;
            this.merk = merk;
        }
    }
}
