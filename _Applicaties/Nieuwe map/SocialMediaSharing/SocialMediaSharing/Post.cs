using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Post
    {
        public int PostID { get; set; }
        public int BezoekerID { get; set; }
        public string Gebruikersnaam { get; set; }
        public int  MapID { get; set; }
        public string PostNaam { get; set; }
        public string Bericht { get; set; }
        public string Path { get; set; }

        public Post()
        {

        }
        public Post(/*int postID,*/ int bezoekerID, string gebruikersnaam, int mapID, string postNaam, string bericht, string path)
        {
            /*this.PostID = postID;*/
            this.BezoekerID = bezoekerID;
            this.Gebruikersnaam = gebruikersnaam;
            this.MapID = mapID;
            this.PostNaam = postNaam;
            this.Bericht = bericht;
            this.Path = path;
        }

        public override string ToString()
        {
            return Gebruikersnaam + ":\t\t" + PostNaam;           
        }
    }
}
