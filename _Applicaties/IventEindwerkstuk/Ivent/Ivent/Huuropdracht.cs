using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Huuropdracht
    {
        public int HuuropdrachtID { get; set; }
        public int ExemplaarID { get; set; }
        public int BezoekerID { get; set; }
        public DateTime HuurDatum { get; set; }
        public DateTime InleverDatum { get; set; }
        public DateTime RetourDatum { get; set; }

        public Huuropdracht(int huuropdrachtid, int exemplaarid, int bezoekerid, DateTime huurDatum, DateTime inleverDatum, DateTime retourDatum)
        {
            this.HuuropdrachtID = huuropdrachtid;
            this.ExemplaarID = exemplaarid;
            this.BezoekerID = bezoekerid;
            this.HuurDatum = huurDatum;
            this.InleverDatum = inleverDatum;
            this.RetourDatum = retourDatum;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
