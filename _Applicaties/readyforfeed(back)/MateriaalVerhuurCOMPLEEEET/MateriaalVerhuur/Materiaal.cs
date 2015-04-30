using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateriaalVerhuur
{
    class Materiaal
    {
        public List<Voorwerp> VerhuurbareVoorwerpen { get; set; }
        public List<Voorwerp> TerugBrengVoorwerpen { get; set; }
   
        public Materiaal()
        {
            
        }

        public void VoorwerpVerhuren(Voorwerp v)
        {
            VerhuurbareVoorwerpen = new List<Voorwerp>();
            VerhuurbareVoorwerpen.Add(v);
        }

        public void VoorwerpTerugbrengen(Voorwerp v)
        {
            TerugBrengVoorwerpen = new List<Voorwerp>();
            TerugBrengVoorwerpen.Add(v);
        }

        public void VoorwerpToevoegen(Voorwerp v)
        {
            VerhuurbareVoorwerpen = new List<Voorwerp>();
            VerhuurbareVoorwerpen.Add(v);
        }
    }
}
