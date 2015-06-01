using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class PlekSpecificatie
    {
        public int PlekSpecificatieID { get; set; }
        public int SpecificatieID { get; set; }
        public int PlekID { get; set; }
        public string Waarde { get; set; }

        public PlekSpecificatie(int plekspecificatieid, int specificatieid, int plekid, string waarde)
        {
            this.PlekSpecificatieID = plekspecificatieid;
            this.SpecificatieID = specificatieid;
            this.PlekID = plekid;
            this.Waarde = waarde;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}