using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Afbeelding
    {
         public int  MapID { get; set; }
         public string Bestand { get; set; }

        public Afbeelding(int mapID, string bestand)
        {
            this.MapID = mapID;
            this.Bestand = bestand;
        }
    }
}
