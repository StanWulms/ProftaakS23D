using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Bijdrage
    {
        public int BijdrageID { get; set; }
        public int AccountID { get; set; }
        public DateTime Datum { get; set; }
        public string Soort { get; set; }

        public Bijdrage(int bijdrageid, int accountid, DateTime datum, string soort)
        {
            this.BijdrageID = bijdrageid;
            this.AccountID = accountid;
            this.Datum = datum;
            this.Soort = soort;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}