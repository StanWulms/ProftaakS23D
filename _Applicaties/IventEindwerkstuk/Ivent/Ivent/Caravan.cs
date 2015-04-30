using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Caravan
    {
        public int PlaatsID { get; set; }
        public double CaravanHuurprijs { get; set; }

        public Caravan(int plaatsid, double caravanHuurprijs)
        {
            this.PlaatsID = plaatsid;
            this.CaravanHuurprijs = caravanHuurprijs;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
