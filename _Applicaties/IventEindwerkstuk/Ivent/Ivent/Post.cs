using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Post
    {
        public int PostID { get; set; }
        public int BezoekerID { get; set; }
        public int MapID { get; set; }
        public string PostNaam { get; set; }
        public string Bericht { get; set; }
        public string Bestand { get; set; }

        public Post(int postID, int bezoekerID, int mapID, string postNaam, string bericht, string bestand)
        {
            this.PostID = postID;
            this.BezoekerID = bezoekerID;
            this.MapID = mapID;
            this.PostNaam = postNaam;
            this.Bericht = bericht;
            this.Bestand = bestand;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
