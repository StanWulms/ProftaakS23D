using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
namespace IventWeb.SysteembeheerInhoud
{
    public partial class GenereerAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnverstuur_Click(object sender, EventArgs e)
        {
            string activatiecode = "ajksdfhafdnja";
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(tbemail.Text);
                mailMessage.From = new MailAddress("balzak@boesbo35.com");//mail adres van de ingevoerde persoon
                mailMessage.Subject = "bevestigingscode ict4events";
                mailMessage.Body = "De activatiecode van het event is:" + activatiecode;//de activatiecode met de mail
                SmtpClient smtpClient = new SmtpClient("smtp.boesbo35.com", 25);
                smtpClient.Credentials = new System.Net.NetworkCredential("balzak@boesbo35.com", "Qunfong1");
                smtpClient.Send(mailMessage);                
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
        }
    }
}