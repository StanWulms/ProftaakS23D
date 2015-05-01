using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class Event
    {
        public int EventID { get; set; }
        public int LocatieID { get; set; }
        public string Naam { get; set; }
        public DateTime DatumStart { get; set; }
        public DateTime DatumEinde { get; set; }
        public int MaxBezoekers { get; set; }

        public Event(int eventid, int locatieid, string naam, DateTime datumStart, DateTime datumEinde, int maxBezoekers)
        {
            this.EventID = eventid;
            this.LocatieID = locatieid;
            this.Naam = naam;
            this.DatumStart = datumStart;
            this.DatumEinde = datumEinde;
            this.MaxBezoekers = maxBezoekers;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}