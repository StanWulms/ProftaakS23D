using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Bericht
    {
        public string mediaID { get; set; }
        public string tekst { get; set; }

        public bool BerichtPlaatsen(string mediaID, string tekst)
        {
            this.mediaID = mediaID;
            this.tekst = tekst;
            return true;
        }

        public bool Rapporteren(string mediaID)
        {
            this.mediaID = mediaID;
            return true;
        }

        public bool Liken(string mediaID)
        {
            this.mediaID = mediaID;
            return false;
        }
    }
}
