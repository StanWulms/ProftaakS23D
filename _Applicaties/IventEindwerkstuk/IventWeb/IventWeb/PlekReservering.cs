using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class PlekReservering
    {
        public int PlekReserveringID { get; set; }
        public int PlekID { get; set; }
        public int ReserveringID { get; set; }

        public PlekReservering(int plekreserveringid, int plekid, int reserveringid)
        {
            this.PlekReserveringID = plekreserveringid;
            this.PlekID = plekid;
            this.ReserveringID = reserveringid;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}