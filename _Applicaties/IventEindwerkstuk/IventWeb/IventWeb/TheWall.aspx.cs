using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IventWeb.SMSInhoud
{
    public partial class TheWall : System.Web.UI.Page
    {
        Categorie c;
        int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            //  if (Session["i"] != "0")
            //   if(i ==0)

            c = new Categorie();
            c.getCategorie();
            foreach (Categorie cat in c.categorieen)
            {
                Button ButtonChange = new Button();
                ButtonChange.Height = 100;
                ButtonChange.Width = 100;

                ButtonChange.Text = cat.Naam;
                ButtonChange.ID = "id" + cat.BijdrageID.ToString();
                ButtonChange.Font.Size = FontUnit.Point(7);
                ButtonChange.ControlStyle.CssClass = "button";
                ButtonChange.Click += new EventHandler(wbtn_Click);
                //  ButtonChange.OnClientClick = "return false";
                // ButtonChange.CausesValidation = false;

                pnlMappen.Controls.Add(ButtonChange);
            }
        }

        public void wbtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;


            Session["categorie"] = (String)button.ID.Substring(2);
            Response.Redirect("SMSContent.aspx");

        }

        public void wbtn2_Click(object sender, EventArgs e)
        {
            pnlMappen.Controls.Clear();
            Button button = sender as Button;
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + button.ID + " - " + button.ID.Substring(2) + "');</script>");



            c = new Categorie();
            Session["categorie"] = (String)button.ID.Substring(2);
            c.getSUBCategorie();
            foreach (Categorie cat in c.categorieen)
            {
                Button ButtonChange = new Button();
                ButtonChange.Height = 100;
                ButtonChange.Width = 120;

                ButtonChange.Text = cat.Naam;
                ButtonChange.ID = "id" + cat.BijdrageID.ToString();
                ButtonChange.Font.Size = FontUnit.Point(7);
                ButtonChange.ControlStyle.CssClass = "button";
                ButtonChange.Click += new EventHandler(wbtn_Click);
                //  ButtonChange.OnClientClick = "return false";
                // ButtonChange.CausesValidation = false;

                pnlMappen.Controls.Add(ButtonChange);
            }
        }
    }
}