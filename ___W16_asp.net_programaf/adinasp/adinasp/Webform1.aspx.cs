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

namespace adinasp
{
    public partial class ads_hit : System.Web.UI.Page
    {
        public DirectorySearcher dirSearch = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            lbldomein.Text = Convert.ToString(Domain.GetComputerDomain());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {     
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + lbldomein.Text, tbnaam.Text, tbwachtwoord.Text);
                object nativeObject = entry.NativeObject;
                authentic = true;
                Session["entry"] = entry;
                DirectorySearcher ds = new DirectorySearcher(entry);
                ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + tbnaam.Text + "))";

                ds.SearchScope = SearchScope.Subtree;
                ds.ServerTimeLimit = TimeSpan.FromSeconds(90);

                SearchResult userObject = ds.FindOne();
                if (authentic == true && userObject != null)
                {
                    Session["username"] = tbnaam.Text;
                    Session["directsearch"] = userObject;
                    Response.Redirect("eigenstats.aspx");
                }
            }
            catch (DirectoryServicesCOMException ex)
            {
                tbcheck.Text = ex.Message;
            }
            
        }

        protected void btnmail_Click(object sender, EventArgs e)
        {
            string activatiecode = "ajksdfhafdnja";
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add("klootzak@boesbo35.com");
                mailMessage.From = new MailAddress("balzak@boesbo35.com");//mail adres van de ingevoerde persoon
                mailMessage.Subject = "bevestigingscode ict4events";
                mailMessage.Body = "De activatiecode van het event is:"+ activatiecode;//de activatiecode met de mail
                SmtpClient smtpClient = new SmtpClient("smtp.boesbo35.com", 25);
                smtpClient.Credentials = new System.Net.NetworkCredential("balzak@boesbo35.com","Qunfong1");
                smtpClient.Send(mailMessage);
                Response.Write("E-mail sent!");
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
            
            
        }

        protected void btnmaakacc_Click(object sender, EventArgs e)
        {
            try
            {
                string oGUID = "";
                string connectionPrefix = "LDAP://" + lbldomein.Text;                
                DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix);                
                DirectoryEntry newUser = dirEntry.Children.Add
                    ("CN=" + tbnaamacc.Text, "user");                
                newUser.Properties["samAccountName"].Value = tbnaamacc.Text;                
                //newUser.CommitChanges();                
                oGUID = newUser.Guid.ToString();               
                newUser.Invoke("SetPassword", new object[] { tbwwacc.Text });
                //newUser.CommitChanges();
                dirEntry.Close();
                newUser.Close();            
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //DoSomethingwith --> E.Message.ToString();
                tbcheck.Text = E.Message;
                

            }
        }
        
    }
}