using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Plek
    {
        public int PlekID { get; set; }
        public int LocatieID { get; set; }
        public string Nummer { get; set; }
        public int Capaciteit { get; set; }

        public Plek(int plekid, int locatieid, string nummer, int capaciteit)
        {
            this.PlekID = plekid;
            this.LocatieID = locatieid;
            this.Nummer = nummer;
            this.Capaciteit = capaciteit;
        }

        public override string ToString()
        {
            return Nummer.ToString();
        }
    }
}