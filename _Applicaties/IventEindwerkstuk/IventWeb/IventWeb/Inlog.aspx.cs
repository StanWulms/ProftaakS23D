using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Net.Mail;
using System.ComponentModel;

namespace IventWeb
{
    public partial class Inlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["naambezoeker"] = "";
            Session["loadpage"] = "true";
            Session["loadpageadditem"] = "true";
            Session["itemsbezoeker"] = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + Convert.ToString(Domain.GetComputerDomain()), tbUsername.Text, tbPassword.Text);
                object nativeObject = entry.NativeObject;
                authentic = true;
                Session["entry"] = entry;
                DirectorySearcher ds = new DirectorySearcher(entry);
                ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + tbUsername.Text + "))";

                ds.SearchScope = SearchScope.Subtree;
                ds.ServerTimeLimit = TimeSpan.FromSeconds(90);

                SearchResult userObject = ds.FindOne();
                if (authentic == true && userObject != null)
                {                    
                    Session["username"] = tbUsername.Text;
                    Session["directsearch"] = userObject;
                    int pagina = ddlApplicatie.SelectedIndex;
                    switch (pagina)
                    {
                        case 1: Response.Redirect("Systeembeheer.aspx");
                            break;
                        case 2: Response.Redirect("Verhuur.aspx");
                            break;
                        case 3: Response.Redirect("Reservatie.aspx");
                            break;
                        case 4: Response.Redirect("SMS.aspx");
                            break;
                        case 5: Response.Redirect("Toegangscontrole.aspx");
                            break;
                        default: Response.Redirect("Home.aspx");
                            break;
                    }
                }
            }
            catch (DirectoryServicesCOMException)
            {
                
            }
        }
    }
}