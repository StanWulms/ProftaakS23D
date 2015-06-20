using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections.Generic;
using System.Linq;

namespace adinasp
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchResult rs = (SearchResult)Session["directsearch"];
            if (rs.GetDirectoryEntry().Properties["samaccountname"].Value != null)
                lblUsernameDisplay.Text = "Username : " + rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();
            else
            {
                lblUsernameDisplay.Text = "Username : ";
            }

            if (rs.GetDirectoryEntry().Properties["givenName"].Value != null)
                lblFirstname.Text = "First Name : " + rs.GetDirectoryEntry().Properties["givenName"].Value.ToString();
            else
            {
                lblFirstname.Text = "First Name : ";
            }
            if (rs.GetDirectoryEntry().Properties["initials"].Value != null)
                lblMiddleName.Text = "Middle Name : " + rs.GetDirectoryEntry().Properties["initials"].Value.ToString();
            else
            {
                lblMiddleName.Text = "Middle Name : ";
            }
            if (rs.GetDirectoryEntry().Properties["sn"].Value != null)
                lblLastName.Text = "Last Name : " + rs.GetDirectoryEntry().Properties["sn"].Value.ToString();
            else
            {
                lblLastName.Text = "Last Name : ";
            }
            if (rs.GetDirectoryEntry().Properties["mail"].Value != null)
                lblEmailId.Text = "Email ID : " + rs.GetDirectoryEntry().Properties["mail"].Value.ToString();
            else
            {
                lblEmailId.Text = "Email ID : ";
            }
            if (rs.GetDirectoryEntry().Properties["title"].Value != null)
                lblTitle.Text = "Title : " + rs.GetDirectoryEntry().Properties["title"].Value.ToString();
            else
            {
                lblTitle.Text = "Title : ";
            }
            if (rs.GetDirectoryEntry().Properties["company"].Value != null)
                lblCompany.Text = "Company : " + rs.GetDirectoryEntry().Properties["company"].Value.ToString();
            else
            {
                lblCompany.Text = "Company : ";
            }
            if (rs.GetDirectoryEntry().Properties["l"].Value != null)
                lblCity.Text = "City : " + rs.GetDirectoryEntry().Properties["l"].Value.ToString();
            else
            {
                lblCity.Text = "City : ";
            }
            if (rs.GetDirectoryEntry().Properties["st"].Value != null)
                lblState.Text = "State : " + rs.GetDirectoryEntry().Properties["st"].Value.ToString();
            else
            {
                lblState.Text = "State : ";
            }
            if (rs.GetDirectoryEntry().Properties["co"].Value != null)
                lblCountry.Text = "Country : " + rs.GetDirectoryEntry().Properties["co"].Value.ToString();
            else
            {
                lblCountry.Text = "Country : ";
            }
            if (rs.GetDirectoryEntry().Properties["postalCode"].Value != null)
                lblPostal.Text = "Postal Code : " + rs.GetDirectoryEntry().Properties["postalCode"].Value.ToString();
            else
            {
                lblPostal.Text = "Postal Code : ";
            }
            if (rs.GetDirectoryEntry().Properties["telephoneNumber"].Value != null)
                lblTelephone.Text = "Telephone No. : " + rs.GetDirectoryEntry().Properties["telephoneNumber"].Value.ToString();
            else
            {
                lblTelephone.Text = "Telephone No. : ";
            }
        }

        protected void btnaanpas_Click(object sender, EventArgs e)
        {
            Response.Redirect("changestats.aspx");
        }
    }
}