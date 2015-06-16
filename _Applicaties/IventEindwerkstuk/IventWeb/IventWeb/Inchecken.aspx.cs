using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.ToegangscontroleInhoud
{
    public partial class Inchecken : System.Web.UI.Page
    {
        Database db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
        }

        /// <summary>
        /// Door op de enter toets te klikken wordt de ingevulde barcode
        /// geactiveerd. Het bijhorende (nog inactieve) account wordt daarbij opgehaald
        /// en het de tabel POLSBANDJE van de bijhorende barcode wordt op 1 (true) gezet.
        /// </summary>
        protected void btnenter_Click(object sender, EventArgs e)
        {
            string tag = tbtag.Text;
            string naam = db.Tagger(tag);
            Tbnaam.Text = naam;
        }
    }
}