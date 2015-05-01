using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class ReserveringPolsbandje
    {
        public int ResPbID { get; set; }
        public int ReserveringID { get; set; }
        public int PolsBandjeID { get; set; }
        public int AccountID { get; set; }
        public int Aanwezig { get; set; }

        public ReserveringPolsbandje(int respbid, int reserveringid, int polsbandjeid, int accountid, int aanwezig)
        {
            this.ResPbID = respbid;
            this.ReserveringID = reserveringid;
            this.PolsBandjeID = polsbandjeid;
            this.AccountID = accountid;
            this.Aanwezig = aanwezig;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}