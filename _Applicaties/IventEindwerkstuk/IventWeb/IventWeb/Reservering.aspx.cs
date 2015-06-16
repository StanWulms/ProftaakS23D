using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace IventWeb.ReservatieInhoud
{
    public partial class ReserveringPlaatsen : System.Web.UI.Page
    {
        //Database db;

        protected void Page_Load(object sender, EventArgs e)
        {
            //db = new Database();
        }

        protected void btnRegistreer_Click(object sender, EventArgs e)
        {
            lblValidation.Visible = true;
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                lblValidation.ForeColor = Color.Black;
                lblValidation.Text = "Uw gegevens zijn geldig.";
                /*db.ConnectDatabase("", "", "");
                if (db.AddData("add"))
                {

                }
                else
                {
                    lblValidation.Text = "Account(s) konden niet worden toegevoegd.";
                }*/
            }
            else
            {
                lblValidation.ForeColor = Color.Red;
                lblValidation.Text = "Uw gegevens zijn ongeldig!";
            }
        }

        protected void ddlAantal_TextChanged(object sender, EventArgs e)
        {
            lblAccount1.Visible = false; tbAccount1.Visible = false; rfvAccount1.Enabled = false; lblEmail1.Visible = false; tbEmail1.Visible = false; rfvEmail1.Enabled = false; revEmail1.Enabled = false;
            lblAccount2.Visible = false; tbAccount2.Visible = false; rfvAccount2.Enabled = false; lblEmail2.Visible = false; tbEmail2.Visible = false; rfvEmail2.Enabled = false; revEmail2.Enabled = false;
            lblAccount3.Visible = false; tbAccount3.Visible = false; rfvAccount3.Enabled = false; lblEmail3.Visible = false; tbEmail3.Visible = false; rfvEmail3.Enabled = false; revEmail3.Enabled = false;
            lblAccount4.Visible = false; tbAccount4.Visible = false; rfvAccount4.Enabled = false; lblEmail4.Visible = false; tbEmail4.Visible = false; rfvEmail4.Enabled = false; revEmail4.Enabled = false;
            lblAccount5.Visible = false; tbAccount5.Visible = false; rfvAccount5.Enabled = false; lblEmail5.Visible = false; tbEmail5.Visible = false; rfvEmail5.Enabled = false; revEmail5.Enabled = false;
            int aantal = Convert.ToInt32(ddlAantal.Text);
            for (int i = 0; i < aantal; i++)
            {
                if (i == 1) { lblAccount1.Visible = true; tbAccount1.Visible = true; rfvAccount1.Enabled = true; lblEmail1.Visible = true; tbEmail1.Visible = true; rfvEmail1.Enabled = true; revEmail1.Enabled = true; }
                if (i == 2) { lblAccount2.Visible = true; tbAccount2.Visible = true; rfvAccount2.Enabled = true; lblEmail2.Visible = true; tbEmail2.Visible = true; rfvEmail2.Enabled = true; revEmail2.Enabled = true; }
                if (i == 3) { lblAccount3.Visible = true; tbAccount3.Visible = true; rfvAccount3.Enabled = true; lblEmail3.Visible = true; tbEmail3.Visible = true; rfvEmail3.Enabled = true; revEmail3.Enabled = true; }
                if (i == 4) { lblAccount4.Visible = true; tbAccount4.Visible = true; rfvAccount4.Enabled = true; lblEmail4.Visible = true; tbEmail4.Visible = true; rfvEmail4.Enabled = true; revEmail4.Enabled = true; }
                if (i == 5) { lblAccount5.Visible = true; tbAccount5.Visible = true; rfvAccount5.Enabled = true; lblEmail5.Visible = true; tbEmail5.Visible = true; rfvEmail5.Enabled = true; revEmail5.Enabled = true; }
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

        }
    }
}