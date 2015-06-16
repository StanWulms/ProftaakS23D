using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace toegangscontrole_asp
{
    public partial class toegangsform : System.Web.UI.Page
    {
        
        Home home;
        protected void Page_Load(object sender, EventArgs e)
        {
                home = new Home();                                    
        }       

        protected void btnenter_Click(object sender, EventArgs e)
        {
            
            string tag = tbtag.Text;
            string naam = home.tagger(tag);
            Tbnaam.Text = naam;            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lbaanwezig.Items.Clear();
            lbnietaanwezig.Items.Clear();
            foreach(bezoekers bezoeker in home.getbezoekers(Tbsearch.Text))

                if(bezoeker.aanwezig == 0)
                {
                    lbnietaanwezig.Items.Add(bezoeker.naam);
                }
                else
                {
                    lbaanwezig.Items.Add(bezoeker.naam);
                }
                
            }
        }
    }
