using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Reservering
    {
        public int ReserveringID { get; set; }
        public int BezoekerID { get; set; }
        public int PlaatsID { get; set; }
        public DateTime ReserveringBeginDatum { get; set; }
        public DateTime ReserveringEindDatum { get; set; }
        public int Reserveerder { get; set; }

        public Reservering(int reserveringid, int bezoekerid, int plaatsid, DateTime reserveringBeginDatum, DateTime reserveringEindDatum, int reserveerder)
        {
            this.ReserveringID = reserveringid;
            this.BezoekerID = bezoekerid;
            this.PlaatsID = plaatsid;
            this.ReserveringBeginDatum = reserveringBeginDatum;
            this.ReserveringEindDatum = reserveringEindDatum;
            this.Reserveerder = reserveerder;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
