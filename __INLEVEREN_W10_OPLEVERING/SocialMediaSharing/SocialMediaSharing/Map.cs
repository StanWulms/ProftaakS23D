using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Map
    {
        public int MapID { get; set; }

        public int SubmapID { get; set; }

        public string MapNaam { get; set; }

        public int BezoekerID { get; set; }

        public Map(int mapID, string mapNaam)
        {
            this.MapID = mapID;
            this.MapNaam = mapNaam;
        }
        public Map(int bezoekerID, int mapID, string mapNaam)
        {
            this.BezoekerID = bezoekerID;
            this.MapID = mapID;
            this.MapNaam = mapNaam;
        }
    }
}
