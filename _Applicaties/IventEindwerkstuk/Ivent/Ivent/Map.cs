using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Map
    {
        public int MapID { get; set; }
        public int BezoekerID { get; set; }
        public int SubmapID { get; set; }
        public string MapNaam { get; set; }

        public Map(int mapid, int bezoekerid, int submapid, string mapNaam)
        {
            this.MapID = mapid;
            this.BezoekerID = bezoekerid;
            this.SubmapID = submapid;
            this.MapNaam = mapNaam;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
