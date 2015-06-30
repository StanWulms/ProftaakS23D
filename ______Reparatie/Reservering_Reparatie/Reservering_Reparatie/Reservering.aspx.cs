using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reservering_Reparatie
{
    public partial class Reservering : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Als alle verplichte velden zijn ingevuld wordt er een SP in de 
        /// database aangeroepen die de ingevulde waardes controleert op correctheid.
        /// Zijn de gegevens geldig, dan worden deze toegevoegd aan de database.
        /// Zo niet, dan wordt er een string terug gestuurd met een melding van de desbetreffende fout.
        /// </summary>
        protected void btnRegistreer_Click(object sender, EventArgs e)
        {
            Page.Validate("AllValidators");
            if (Page.IsValid)
            {
                lblValidation.Text = "Goeie ouwe";
                lblValidation.Visible = true;
            }
        }

        /// <summary>
        /// Iedere keer dat er een ander aantal extra bezoekers wordt geselecteerd
        /// dienen de juiste velden zichtbaar te worden. De textboxen hebben standaard waardes -1 t/m -5.
        /// Dit doe ik omdat ik kijk of er geen van de velden gelijk zijn aan elkaar.
        /// Als de textboxes leeg waren zou deze check altijd 'true' geven.
        /// </summary>
        protected void ddlAantal_TextChanged(object sender, EventArgs e)
        {
            lblAccount1.Visible = false; tbAccount1.Visible = false; tbAccount1.Text = "-1"; rfvAccount1.Enabled = false; lblEmail1.Visible = false; tbEmail1.Visible = false; tbEmail1.Text = "-1"; rfvEmail1.Enabled = false;
            lblAccount2.Visible = false; tbAccount2.Visible = false; tbAccount2.Text = "-2"; rfvAccount2.Enabled = false; lblEmail2.Visible = false; tbEmail2.Visible = false; tbEmail2.Text = "-2"; rfvEmail2.Enabled = false;
            lblAccount3.Visible = false; tbAccount3.Visible = false; tbAccount3.Text = "-3"; rfvAccount3.Enabled = false; lblEmail3.Visible = false; tbEmail3.Visible = false; tbEmail3.Text = "-3"; rfvEmail3.Enabled = false;
            lblAccount4.Visible = false; tbAccount4.Visible = false; tbAccount4.Text = "-4"; rfvAccount4.Enabled = false; lblEmail4.Visible = false; tbEmail4.Visible = false; tbEmail4.Text = "-4"; rfvEmail4.Enabled = false;
            lblAccount5.Visible = false; tbAccount5.Visible = false; tbAccount5.Text = "-5"; rfvAccount5.Enabled = false; lblEmail5.Visible = false; tbEmail5.Visible = false; tbEmail5.Text = "-5"; rfvEmail5.Enabled = false;
            int aantal = Convert.ToInt32(ddlAantal.Text);
            for (int i = 0; i < aantal; i++)
            {
                if (i == 1) { lblAccount1.Visible = true; tbAccount1.Visible = true; tbAccount1.Text = ""; rfvAccount1.Enabled = true; lblEmail1.Visible = true; tbEmail1.Visible = true; tbEmail1.Text = ""; rfvEmail1.Enabled = true; }
                if (i == 2) { lblAccount2.Visible = true; tbAccount2.Visible = true; tbAccount2.Text = ""; rfvAccount2.Enabled = true; lblEmail2.Visible = true; tbEmail2.Visible = true; tbEmail2.Text = ""; rfvEmail2.Enabled = true; }
                if (i == 3) { lblAccount3.Visible = true; tbAccount3.Visible = true; tbAccount3.Text = ""; rfvAccount3.Enabled = true; lblEmail3.Visible = true; tbEmail3.Visible = true; tbEmail3.Text = ""; rfvEmail3.Enabled = true; }
                if (i == 4) { lblAccount4.Visible = true; tbAccount4.Visible = true; tbAccount4.Text = ""; rfvAccount4.Enabled = true; lblEmail4.Visible = true; tbEmail4.Visible = true; tbEmail4.Text = ""; rfvEmail4.Enabled = true; }
                if (i == 5) { lblAccount5.Visible = true; tbAccount5.Visible = true; tbAccount5.Text = ""; rfvAccount5.Enabled = true; lblEmail5.Visible = true; tbEmail5.Visible = true; tbEmail5.Text = ""; rfvEmail5.Enabled = true; }
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

        }
    }
}