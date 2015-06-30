using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocialMediaSharingASP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        AccountBijdrage ab;
        Bijdrage b;
        Bestand bs;
        Bericht ber;
        Categorie c;

        string imgPath = "";
        int imgSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ab = new AccountBijdrage();
            b = new Bijdrage();
            bs = new Bestand();
            ber = new Bericht();
            c = new Categorie();

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
            if (!IsPostBack)
            {
                //Toevoegen van de bestand namen in de listbox
                bs.GetBestand();

                foreach (Bestand bstnd in bs.bestanden)
                {
                    lbBestanden.Items.Add(bstnd.ToString());
                }

                ber.GetBericht();

                foreach (Bericht brcht in ber.Berichten)
                {
                    //BUG: Berichten worden opnieuw ingeladen als er op de knop gedrukt wordt.
                    //int eindPos = brcht.ToString().IndexOf("*", 0);
                    //lbPosts.Items.Add(brcht.ToString().Substring(0,eindPos));
                    lbPosts.Items.Add(brcht.ToString());
                }
            }
        }
        //Knop voor Categorieën
        public void wbtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;


            Session["categorie"] = (String)button.ID.Substring(2);
            Response.Redirect("WebForm1.aspx");

        }
        //Knop voor het toevoegen van nieuwe Categorieën
        protected void btnMapToevoegen_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + "12" + " - " + "11" + "');</script>");
            c.InsertMap(tbMapNaam.Text);
            Response.Redirect("WebForm1.aspx");
        }

        //Knop om de bestanden - NOT
        protected void btnShowBestand_Click(object sender, EventArgs e)
        {
            //Selecteren van het bestand uit de listbox dat getoond dient te worden.
            int lengte = lbBestanden.SelectedItem.Text.IndexOf("-", 0);
            Session["bestand"] = lbBestanden.SelectedItem.Text.Substring(4, lengte - 4);

            string src = bs.GetBestandInhoud();
            Image1.ImageUrl = src;
        }

        //Knop om de inhoud van hoofd bericht en reacties te zien..
        protected void btnShowBerichtInhoud_Click(object sender, EventArgs e)
        {
            lblPlaatsReactie.Text = "Plaats reactie op bericht: ";
            lblTitel.Visible = false;
            tbTitel.Visible = false;
           // int str = lbPosts.SelectedItem.Text.ToString().IndexOf("-", 0, 1);
            int lengte = lbPosts.SelectedItem.Text.IndexOf(":", 0);
            //lblBerichtInhoud.Text = lbPosts.SelectedItem.Text.Substring(startPos);
            Session["bijdrage"] = lbPosts.SelectedItem.Text.Substring(0, lengte);

            ber.GetInhoud();
            foreach (Bericht b in ber.Inhouden)
            {
                lblBerichtInhoud.Text += b.Inhoud;
            }

            //Alle reacties op het aangeklikte bericht
            ber.GetReacties();
            foreach (Bericht bericht in ber.Reacties)
            {
                lblBerichtReactie.Text += bericht.Inhoud + "<br /><br />";
            }
            btnLike.Visible = true;
            btnReport.Visible = true;
            string likereports = ab.getLikeReports();
            string likes = likereports.Substring(0, likereports.IndexOf("."));
            string reports = likereports.Substring(likereports.IndexOf(".")+1);
            lblLike.Text = likes;
            lblReport.Text = reports;
        }

        //Knop om een Bericht te plaatsen.
        protected void btnPlaatsBericht_Click(object sender, EventArgs e)
        {
            ber.InsertBericht(tbTitel.Text, tbInhoud.Text);
            //Redirect naar zichzelf zodat de bijgevoegde waardes op het scherm weergegeven worden.
            Response.Redirect("WebForm1.aspx");
        }

        //Knop om bestanden up te loaden.
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            //WEBCONFIG HTTP RUNTIME @KEES  --------------------------------------------------------
            //Neem de goede file vanaf de pc.
            StartUpload();

            //Bestanden in de database zetten.
            bs.InsertBestand(imgPath,imgSize);
            Response.Redirect("WebForm1.aspx");
        }

        //Methode om files up te loaden. Zie btnFileUpload_Click
        private void StartUpload()
        {
            //Get the file name of the posted image
            string imgName = FileUpload1.FileName;

            //Sets the image Path
            imgPath = "Images/" + imgName;

            //get the size in bytes
            imgSize = FileUpload1.PostedFile.ContentLength;

            //Validates the posted file before saving
            if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
            {
                //Then save the file in the Folder
                FileUpload1.SaveAs(Server.MapPath(imgPath));
                //Image1.ImageUrl = "~/Images/" + imgName;
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image Saved!')", true);
            }
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {
            ab.GiveLike();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

        }
    }
}