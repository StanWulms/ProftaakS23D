using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toegangscontrole
{
    [Serializable]
    class bezoekers
    {
        private string naam;
        private bool bob;
        public bool Bob
        {
          get{return bob;} 
          set{bob = value;} 
        }
        public string Naam
        {
            get { return naam; }
        }
        public bezoekers(string naam)
        {
            this.naam = naam;
        }
    }
}
