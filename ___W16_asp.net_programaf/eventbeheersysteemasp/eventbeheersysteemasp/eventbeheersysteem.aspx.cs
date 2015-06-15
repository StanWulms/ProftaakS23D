using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eventbeheersysteemasp
{
    public partial class eventbeheersysteem : System.Web.UI.Page
    {
        List<eventmaken> events;
        Home home;
        protected void Page_Load(object sender, EventArgs e)
        {            
            home = new Home();
            events = home.getevents();
            geefeventsweer();
        }

        protected void btnaddloc_Click(object sender, EventArgs e)
        {
            home.insertlocation(tbnaamloc.Text, Tbstraat.Text, Tbnr.Text, Tbpostcode.Text, Tbplaats.Text);           
        }

        protected void btnAddEvent_click(object sender, EventArgs e)
        {
            home.insertevent(Tbnaame.Text, Tbnaamlocatie.Text, Tbdatumstart.Text, Tbdatumeind.Text, Tbmaxbezoeker.Text);
            Response.Redirect("eventbeheersysteem.aspx");
        }
        public void geefeventsweer()
        {
            ListBox1.Items.Clear();
            foreach (eventmaken eventje in events)
            {
                ListBox1.Items.Add("eventID:      " +Convert.ToString(eventje.eventid));
                ListBox1.Items.Add("eventnaam:    " + eventje.name);
                ListBox1.Items.Add("begindatum:   " + Convert.ToString(eventje.begindatum));
                ListBox1.Items.Add("einddatum:    " + Convert.ToString(eventje.einddatum));
                ListBox1.Items.Add("maxbezoekers: " + eventje.aantal);
                ListBox1.Items.Add("straat:       " + eventje.straat +" "+ eventje.huisnummer);                
                ListBox1.Items.Add("postcode:     " + eventje.postcode);
                ListBox1.Items.Add("plaats:       " + eventje.plaats);
                ListBox1.Items.Add("___________________________");
            }
        }
    }
}