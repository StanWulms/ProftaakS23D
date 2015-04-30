using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Exemplaar
    {
        public int ExemplaarID { get; set; }
        public int VoorwerpID { get; set; }
        public string ExemplaarStaat { get; set; }

        public Exemplaar(int exemplaarid, int voorwerpid, string exemplaarStaat)
        {
            this.ExemplaarID = exemplaarid;
            this.VoorwerpID = voorwerpid;
            this.ExemplaarStaat = exemplaarStaat;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
