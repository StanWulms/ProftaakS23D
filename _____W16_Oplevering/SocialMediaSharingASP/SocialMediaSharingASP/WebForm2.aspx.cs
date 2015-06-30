using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocialMediaSharingASP
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        Categorie c;
        string dit;
        string tests;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["string"] = tbInvoer.Text;
            tests = (String)Session["string"];
            dit = (String)Session["test"];
            if (dit != "true")
            {
                Video.InnerHtml += @"<video width=""320""  height=""240"" controls=""controls"" autoplay=""autoplay"" runat=""server""> <source src=""..\Images\" + tests + ".mp4" + @" type=""video/mp4""></video>";
                Session["test"] = "true";
            }
        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
         
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Session["test"] = "true";
            Video.InnerHtml += @"<video width=""320""  height=""240"" controls=""controls"" autoplay=""autoplay"" runat=""server""> <source src=""..\Images\" + tests + ".mp4" + @" type=""video/mp4""></video>";
            Response.Redirect("WebForm2.aspx");
        }
    }
}