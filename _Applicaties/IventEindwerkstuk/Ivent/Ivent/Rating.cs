using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivent
{
    class Rating
    {
        public int BezoekerID { get; set; }
        public int PostID { get; set; }
        public bool PlusOne { get; set; }

        public Rating(int bezoekerid, int postid, int plusOne)
        {
            this.BezoekerID = bezoekerid;
            this.PostID = postid;
            if (plusOne == 1)
            {
                this.PlusOne = true;
            }
            else
            {
                this.PlusOne = false;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
