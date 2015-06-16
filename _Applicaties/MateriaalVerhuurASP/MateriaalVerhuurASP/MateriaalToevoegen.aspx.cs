using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MateriaalVerhuurASP
{
    public partial class MateriaalToevoegen : System.Web.UI.Page
    {
        Database database;
        protected void Page_Load(object sender, EventArgs e)
        {
            // haalt alle producten en categorieen op uit de database en zet deze in 2 dropdownlists een voor de categorieen en een voor de producten
            ddlSoort.Items.Clear();
            database = new Database();
            List<String> producten = database.getproducten();
            List<String> categorieen = database.getcategorieproduct();
            foreach(String product in producten)
            {
                ddlSoort.Items.Add(product);
            }
            foreach (String categorie in categorieen)
            {
                DDLproductcat.Items.Add(categorie);                  
            }
        }

        protected void btnToevoegen_Click(object sender, EventArgs e)
        {
            //hier wordt er het geselecteerde categorie om gezet na een nummer en met deze en alle ingevoerde waarde kan een nieuw product worden gemaakt
            int catid = DDLproductcat.SelectedIndex + 1;
            database.insertproduct(catid, tbMerk.Text, tbSoort.Text, Convert.ToInt32(Tbprijs.Text));
            Response.Redirect("WebForm1.aspx");          
        }

        protected void btnExemplaarToevoegen_Click(object sender, EventArgs e)
        {
            //hier wordt er het geselecteerde product om gezet na een nummer en hier wordt een nieuw exemplaar mee aangemaakt
            int productid = ddlSoort.SelectedIndex + 1;
            database.insertexemplaar(productid);
            
        }
    }
}