using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class BijdrageBericht
    {
        public int BijdrageID { get; set; }
        public int BerichtID { get; set; }

        public BijdrageBericht(int bijdrageid, int berichtid)
        {
            this.BijdrageID = bijdrageid;
            this.BerichtID = berichtid;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}