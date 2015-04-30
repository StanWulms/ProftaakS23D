using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Staplaats
    {
        public int PlaatsID { get; set; }
        public double PlaatsHuurPrijs { get; set; }
        public int RustigheidsFactor { get; set; }

        public Staplaats(int plaatsid, double plaatsHuurPrijs, int rustigheidsFactor)
        {
            this.PlaatsID = plaatsid;
            this.PlaatsHuurPrijs = plaatsHuurPrijs;
            this.RustigheidsFactor = rustigheidsFactor;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
