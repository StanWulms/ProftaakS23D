using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InschrijvingSysteem
{
    class Voorwerp
    {
        string ID;
        string Soort;
        string Merk;
        string Huurprijs;

        public Voorwerp(string id, string soort, string merk, string huurprijs)
        {
            this.ID = id;
            this.Soort = soort;
            this.Merk = merk;
            this.Huurprijs = huurprijs;
        }

    }
}
