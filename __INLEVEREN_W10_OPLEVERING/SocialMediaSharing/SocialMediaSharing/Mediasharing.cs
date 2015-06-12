using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaSharing
{
    class Mediasharing
    {
        public string gebruikersnaam { get; set; }
        public string wachtwoord { get; set; }
        public string mediaID { get; set; }
        public string soort { get; set; }

        List<String> mediaIds = new List<String>();

        public bool Download(string mediaID)
        {
            this.mediaID = mediaID;
            //if(mediaID in de list van mediaID's)
            //{
            // return true;
            //}
            return false;
        }

        public bool Upload(string mediaID, string soort)
        {
            this.mediaID = mediaID;
            this.soort = soort;
            return true;
        }

        public List<String> Zoeken(string mediaID)
        {
            this.mediaID = mediaID;
            return mediaIds;
        }

        public bool Inloggen(string gebruikersnaam, string wachtwoord)
        {
            this.gebruikersnaam = gebruikersnaam;
            this.wachtwoord = wachtwoord;
            //if()
            return true;
        }

        public bool Uitloggen()
        {
            return true;
        }
    }
}
