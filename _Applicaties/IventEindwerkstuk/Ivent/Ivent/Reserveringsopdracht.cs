using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Reserveringsopdracht
    {
        public int ReserveringsopdrachtID { get; set; }
        public int VoorwerpID { get; set; }
        public int BezoekerID { get; set; }
        public int Aantal { get; set; }

        public Reserveringsopdracht(int reserveringsopdrachtid, int voorwerpid, int bezoekerid, int aantal)
        {
            this.ReserveringsopdrachtID = reserveringsopdrachtid;
            this.VoorwerpID = voorwerpid;
            this.BezoekerID = bezoekerid;
            this.Aantal = aantal;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
