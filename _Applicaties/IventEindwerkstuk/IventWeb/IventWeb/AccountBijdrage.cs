using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IventWeb
{
    public class AccountBijdrage
    {
        public int AccountBijdrageID { get; set; }
        public int AccountID { get; set; }
        public int BijdrageID { get; set; }
        public int Like { get; set; }
        public int Ongewenst { get; set; }

        public AccountBijdrage(int accountbijdrageid, int accountid, int bijdrageid, int like, int ongewenst)
        {
            this.AccountBijdrageID = accountbijdrageid;
            this.AccountID = accountid;
            this.BijdrageID = bijdrageid;
            this.Like = like;
            this.Ongewenst = ongewenst;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}