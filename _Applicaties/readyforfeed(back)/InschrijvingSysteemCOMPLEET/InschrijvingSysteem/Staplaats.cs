using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InschrijvingSysteem
{
    class Staplaats
    {
        string ID;
        string Huurpijs;
        string Rustigheid;

        public Staplaats(string id, string huurprijs, string rustigheid)
        {
            this.ID = id;
            this.Huurpijs = huurprijs;
            this.Rustigheid = rustigheid;
        }

    }
}
