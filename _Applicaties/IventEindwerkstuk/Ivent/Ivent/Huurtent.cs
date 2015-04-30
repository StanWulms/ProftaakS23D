using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Huurtent
    {
        public int PlaatsID { get; set; }
        public double HuurtentHuurprijs { get; set; }

        public Huurtent(int plaatsid, double huurtentHuurprijs)
        {
            this.PlaatsID = plaatsid;
            this.HuurtentHuurprijs = huurtentHuurprijs;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
