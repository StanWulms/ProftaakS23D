using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

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
            lblError.Text = "";
            lblError.ForeColor = Color.Red;
            string tag = tbtag.Text;
            if (tag != "")
            {
                try 
                { 
                    Int64 tagnumber = Convert.ToInt64(tag);
                    string naam = db.Tagger(tag);
                    if (naam.Substring(0, 4) == "FOUT") 
                    {
                        lblError.Text = naam;                       
                    }
                    else
                    {
                        lblError.ForeColor = Color.Green;
                        lblError.Text = "Geldige barcode en het kaartje is betaald.";
                        if (naam == "Bezoeker is uitgecheckt") { Tbnaam.Text = naam; }
                        else { Tbnaam.Text = naam + ": is ingecheckt."; }
                    }
                }
                catch { lblError.Text = "Vul alleen getallen in!"; }
            }
            else { lblError.Text = "Scan eerst de barcode"; }
        }
    }
}