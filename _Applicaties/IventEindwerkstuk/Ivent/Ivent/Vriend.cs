using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Vriend
    {
        public int BezoekerID { get; set; }
        public int VriendID { get; set; }

        public Vriend(int bezoekerid, int vriendid)
        {
            this.BezoekerID = bezoekerid;
            this.VriendID = vriendid;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
