using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Plaats
    {
        public int PlaatsID { get; set; }
        public string PlaatsGrootte { get; set; }
        public int MaxAantal { get; set; }

        public Plaats(int plaatsid, string plaatsGrootte, int maxAantal)
        {
            this.PlaatsID = plaatsid;
            this.PlaatsGrootte = plaatsGrootte;
            this.MaxAantal = maxAantal;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
