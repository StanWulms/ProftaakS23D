using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;

namespace IventWeb
{
    public class Bijdrage
    {
        public int BijdrageID { get; set; }
        public int AccountID { get; set; }
        public DateTime Datum { get; set; }
        public string Soort { get; set; }
        public List<Bijdrage> bijdragenBestand { get; set; }

        Bijdrage b;

        public Bijdrage(int bijdrageid, int accountid, DateTime datum, string soort)
        {
            this.BijdrageID = bijdrageid;
            this.AccountID = accountid;
            this.Datum = datum;
            this.Soort = soort;
            bijdragenBestand = new List<Bijdrage>();
        }

        public Bijdrage()
        {
            bijdragenBestand = new List<Bijdrage>();
        }



        public override string ToString()
        {
            return base.ToString();
        }
    }
}