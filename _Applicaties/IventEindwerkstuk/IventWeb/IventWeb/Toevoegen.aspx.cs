using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.VerhuurInhoud
{
    public partial class Toevoegen : System.Web.UI.Page
    {
        Database database;

        protected void Page_Load(object sender, EventArgs e)
        {
            database = new Database();
            string laadpagina = (String)Session["loadpageadditem"];
            if (laadpagina != "false")
            {
                ddlSoort.Items.Clear();
                DDLproductcat.Items.Clear();
                List<String> producten = database.getproducten();
                List<String> categorieen = database.getcategorieproduct();
                foreach (String product in producten)
                {
                    ddlSoort.Items.Add(product);
                }
                foreach (String categorie in categorieen)
                {
                    DDLproductcat.Items.Add(categorie);
                }
                Session["loadpageadditem"] = "false";
            }
        }

        protected void btnToevoegen_Click(object sender, EventArgs e)
        {
            Page.Validate("VoorwerpToevoegenValidators");
            if (Page.IsValid)
            {
                int catid = DDLproductcat.SelectedIndex + 1;
                lblBeschrijving.Text = Convert.ToString(catid);
                database.insertproduct(catid, tbMerk.Text, tbSoort.Text, Convert.ToInt32(Tbprijs.Text));
                Session["loadpageadditem"] = "true";
                Response.Redirect("Toevoegen.aspx");
            }
        }

        protected void btnExemplaarToevoegen_Click(object sender, EventArgs e)
        {
            Page.Validate("ExemplaarToevoegenValidators");
            if (Page.IsValid)
            {
                int productid = ddlSoort.SelectedIndex + 1;
                lblBeschrijving.Text = Convert.ToString(productid);
                database.insertexemplaar(productid);
                Session["loadpageadditem"] = "true";
                Response.Redirect("Toevoegen.aspx");
            }
        }
    }
}