using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Reservering
    {
        public int ReserveringID { get; set; }
        public int PersoonID { get; set; }
        public DateTime DatumStart { get; set; }
        public DateTime DatumEinde { get; set; }
        public int Betaald { get; set; }

        public Reservering(int reserveringid, int persoonid, DateTime datumStart, DateTime datumEinde, int betaald)
        {
            this.ReserveringID = reserveringid;
            this.PersoonID = persoonid;
            this.DatumStart = datumStart;
            this.DatumEinde = datumEinde;
            this.Betaald = betaald;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}