using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace adinasp
{
    public partial class changestats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblnaam.Text = (string)Session["username"];   
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + Convert.ToString(Domain.GetComputerDomain()));            
            DirectoryEntry group = entry.Children.Find("CN=" + lblnaam.Text);            
            if (tbfirstname.Text != "")
            {
                group.Properties["givenName"].Value = tbfirstname.Text;
            }
            if (tbmiddlename.Text != "")
            {
                group.Properties["initials"].Value = tbmiddlename.Text;
            }
            if (tblastname.Text != "")
            {
                group.Properties["sn"].Value = tblastname.Text;
            }
            if (tbemail.Text != "")
            {
                group.Properties["mail"].Value = tbemail.Text;
            }
            if (tbtitle.Text != "")
            {
                group.Properties["title"].Value = tbtitle.Text;
            }
            if (tbcompany.Text != "")
            {
                group.Properties["company"].Value = tbcompany.Text;
            }
            if (tbcity.Text != "")
            {
                group.Properties["l"].Value = tbcity.Text;
            }
            if (tbstate.Text != "")
            {
                group.Properties["st"].Value = tbstate.Text;
            }
            if (tbcountry.Text != "")
            {
                group.Properties["co"].Value = tbcountry.Text;
            }
            if (tbpostalcode.Text != "")
            {
                group.Properties["postalCode"].Value = tbpostalcode.Text;
            }
            if (tbtelefoon.Text != "")
            {
                group.Properties["telephoneNumber"].Value = tbtelefoon.Text;
            }
            group.CommitChanges();
            Response.Redirect("eigenstats.aspx");
        }        
    }
}