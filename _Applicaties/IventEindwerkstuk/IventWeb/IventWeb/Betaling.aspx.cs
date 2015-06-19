using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb
{
    public partial class Betaling : System.Web.UI.Page
    {
        Database db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new Database();
            Session["loadpage"] = "true";
            string laadpagina = (String)Session["loadpagebetaling"];
            if (laadpagina != "false")
            {
                List<Reservering> reserveringen = new List<Reservering>();
                List<Persoon> personen = new List<Persoon>();
                List<PlekSpecificatie> plekspeficaties = new List<PlekSpecificatie>();

                //alle reserveringen die nog NIET betaald zijn worden in de
                //Niet_betaald listbox (de linker) gezet.            
                reserveringen = db.GetDataReservering(@"SELECT * FROM reservering WHERE ""betaald"" = 0");
                foreach (Reservering r in reserveringen)
                {
                    //Ophalen van de persoon uitzoeken.
                    personen = db.GetDataPersoon(@"SELECT p.id, p.""voornaam"", p.""tussenvoegsel"", p.""achternaam"", p.""straat"", p.""huisnr"", p.""woonplaats"", p.""banknr"" FROM persoon p, reservering r WHERE p.id = r.""persoon_id"" AND p.id = " + r.PersoonID + " AND r.id =  " + r.ReserveringID);
                    lbNietBetaald.Items.Add(r.ToString() + " " + personen[0].ToString());
                }

                //alle reserveringen die al WEL betaald zijn worden in de
                //Wel_betaald listbox (de rechter) gezet.   
                reserveringen = db.GetDataReservering(@"SELECT * FROM reservering WHERE ""betaald"" = 1");
                foreach (Reservering r in reserveringen)
                {
                    //Ophalen van de persoon uitzoeken.
                    personen = db.GetDataPersoon(@"SELECT p.id, p.""voornaam"", p.""tussenvoegsel"", p.""achternaam"", p.""straat"", p.""huisnr"", p.""woonplaats"", p.""banknr"" FROM persoon p, reservering r WHERE p.id = r.""persoon_id"" AND p.id = " + r.PersoonID + " AND r.id =  " + r.ReserveringID);
                    lbWelBetaald.Items.Add(r.ToString() + " " + personen[0].ToString());
                }
                Session["loadpagebetaling"] = "false";
            }
        }

        protected void btnBetaal_Click(object sender, EventArgs e)
        {
            lblBetaalError.Visible = false;
            try
            {
                string r = lbNietBetaald.SelectedValue;
                r.Substring(4, r.IndexOf(":", 3) - 8);
                db.AddData(@"UPDATE reservering SET ""betaald"" = 1 WHERE id = " + r);
                Session["loadpagebetaling"] = "true";
                Response.Redirect("Betaling.aspx");
            }
            catch { lblBetaalError.Visible = true; }
        }
    }
}