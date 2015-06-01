using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Bericht
    {
        public int BijdrageID { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }

        public Bericht(int bijdrageid, string titel, string inhoud)
        {
            this.BijdrageID = bijdrageid;
            this.Titel = titel;
            this.Inhoud = inhoud;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}