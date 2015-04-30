using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Bericht
    {
        public int BerichtID { get; set; }
        public int BezoekerID { get; set; }
        public string BezoekerNaam { get; set; }
        public int PostID { get; set; }
        public string BerichtInhoud { get; set; }

        public Bericht(int berichtid, int bezoekerid, string bezoekerNaam, int postid, string bericht)
        {
            this.BerichtID = berichtid;
            this.BezoekerID = bezoekerid;
            this.BezoekerNaam = bezoekerNaam;
            this.PostID = postid;
            this.BerichtInhoud = bericht;
        }

        public override string ToString()
        {
            return BezoekerNaam + ":  " + BerichtInhoud;
        }
    }
}
