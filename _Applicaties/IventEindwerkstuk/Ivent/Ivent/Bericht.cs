using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    //TestGitHub
    class Bericht
    {
        public int BerichtID { get; set; }
        public int PostID { get; set; }
        public int BezoekerID { get; set; }
        public string BerichtInhoud { get; set; }

        public Bericht(int berichtid, int postid, int bezoekerid, string berichtInhoud)
        {
            this.BerichtID = berichtid;
            this.PostID = postid;
            this.BezoekerID = bezoekerid;
            this.BerichtInhoud = berichtInhoud;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
