using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.SysteembeheerInhoud
{
    public partial class EventBeheren : System.Web.UI.Page
    {
        List<DataBaseKlassen.EventAanmaken> events;
        Database db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
            events = db.getevents();
            geefeventsweer();
        }

        protected void btnaddloc_Click(object sender, EventArgs e)
        {
            db.insertlocation(tbnaamloc.Text, Tbstraat.Text, Tbnr.Text, Tbpostcode.Text, Tbplaats.Text);
        }

        protected void btnAddEvent_click(object sender, EventArgs e)
        {
            db.insertevent(Tbnaame.Text, Tbnaamlocatie.Text, Tbdatumstart.Text, Tbdatumeind.Text, Tbmaxbezoeker.Text);
            Response.Redirect("eventbeheersysteem.aspx");
        }
        public void geefeventsweer()
        {
            ListBox1.Items.Clear();
            foreach (DataBaseKlassen.EventAanmaken eventje in events)
            {
                ListBox1.Items.Add("eventID:      " + Convert.ToString(eventje.EventID));
                ListBox1.Items.Add("eventnaam:    " + eventje.Name);
                ListBox1.Items.Add("begindatum:   " + Convert.ToString(eventje.BeginDatum));
                ListBox1.Items.Add("einddatum:    " + Convert.ToString(eventje.EindDatum));
                ListBox1.Items.Add("maxbezoekers: " + eventje.Aantal);
                ListBox1.Items.Add("straat:       " + eventje.Straat + " " + eventje.HuisNummer);
                ListBox1.Items.Add("postcode:     " + eventje.Postcode);
                ListBox1.Items.Add("plaats:       " + eventje.Plaats);
                ListBox1.Items.Add("___________________________");
            }
        }
    }
}