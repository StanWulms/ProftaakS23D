using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb
{
    public partial class Plekbeheren : System.Web.UI.Page
    {
        Database db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
            ddlEvent.Items.Clear();
            List<Event> evenementen = db.GetDataEvent("SELECT * FROM event");
            foreach (Event evenement in evenementen)
            {
                ddlEvent.Items.Add(evenement.Naam);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblNummerError.Visible = false;
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                //Eerst kijk ik of het ingevulde nummer nog niet in gebruik is voor het
                //gekozen evenement.
                List<Event> evenementen = db.GetDataEvent(@"SELECT * FROM event WHERE ""naam"" = '" + ddlEvent.Text + "'");
                List<Plek> plekken = db.GetDataPlek(@"SELECT * FROM plek WHERE ""locatie_id"" = (SELECT id FROM locatie WHERE id = (SELECT ""locatie_id"" FROM event WHERE ""naam"" = '" + ddlEvent.Text + @"')) AND ""nummer"" = " + tbNummer.Text);
                bool uniekid = true;
                try { string test = plekken[0].PlekID.ToString(); }
                catch { uniekid = false; }
                if (!uniekid)
                {
                    //Eerst INSERT de nieuwe plaats
                    db.AddData(@"INSERT INTO plek (""locatie_id"", ""nummer"", ""capaciteit"") VALUES (" + evenementen[0].LocatieID + ",'" + tbNummer.Text + "'," + tbCapaciteit.Text + ")");
                    //Vervolgens de (eventuele) specificaties.
                    List<Plek> huidigeplek = db.GetDataPlek(@"SELECT * FROM plek WHERE id = (SELECT MAX(id) FROM plek)");
                    if (cbComfort.Checked) { db.AddData(@"INSERT INTO plek_specificatie (""specificatie_id"", ""plek_id"", ""waarde"") VALUES (2," + huidigeplek[0].PlekID + ",'ja')"); }
                    if (cbHandicap.Checked) { db.AddData(@"INSERT INTO plek_specificatie (""specificatie_id"", ""plek_id"", ""waarde"") VALUES (3," + huidigeplek[0].PlekID + ",'ja')"); }
                    if (cbKraanwater.Checked) { db.AddData(@"INSERT INTO plek_specificatie (""specificatie_id"", ""plek_id"", ""waarde"") VALUES (5," + huidigeplek[0].PlekID + ",'ja')"); }
                    else { db.AddData(@"INSERT INTO plek_specificatie (""specificatie_id"", ""plek_id"", ""waarde"") VALUES (5," + huidigeplek[0].PlekID + ",'nee')"); }                       
                }
                else
                {
                    lblNummerError.Visible = true;
                }
            }
        }
    }
}