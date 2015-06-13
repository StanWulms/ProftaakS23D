using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MateriaalVerhuurASP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVerhuur_Click(object sender, EventArgs e)
        {
            Response.Redirect("MateriaalVerhuren.aspx");
        }

        protected void btnTerugBrengen_Click(object sender, EventArgs e)
        {
            Response.Redirect("MateriaalTerugbrengen.aspx");
        }

        protected void btnToevoegen_Click(object sender, EventArgs e)
        {
            Response.Redirect("MateriaalToevoegen.aspx");
        }
    }
}