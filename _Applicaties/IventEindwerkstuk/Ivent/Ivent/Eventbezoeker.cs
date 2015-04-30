using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Eventbezoeker
    {
        public int EventID { get; set; }
        public int BezoekerID { get; set; }

        public Eventbezoeker(int eventid, int bezoekerid)
        {
            this.EventID = eventid;
            this.BezoekerID = bezoekerid;   
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
