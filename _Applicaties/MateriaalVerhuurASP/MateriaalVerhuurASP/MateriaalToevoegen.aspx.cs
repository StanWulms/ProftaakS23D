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
            int catid = DDLproductcat.SelectedIndex + 1;
            database.insertproduct(catid, tbMerk.Text, tbSoort.Text, Convert.ToInt32(Tbprijs.Text));
            Response.Redirect("WebForm1.aspx");          
        }

        protected void btnExemplaarToevoegen_Click(object sender, EventArgs e)
        {
            int productid = ddlSoort.SelectedIndex + 1;
            database.insertexemplaar(productid);
            
        }
    }
}