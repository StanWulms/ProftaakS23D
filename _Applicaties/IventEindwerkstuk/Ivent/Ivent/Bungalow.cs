using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Bungalow
    {
        public int plaatsID { get; set; }
        public double HuurTarief { get; set; }

        public Bungalow(int plaatsid, double huurTarief)
        {
            this.plaatsID = plaatsid;
            this.HuurTarief = huurTarief;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
