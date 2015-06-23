using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace IventWeb
{
    public partial class SMSContent : System.Web.UI.Page
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
            //ACTIVE DIRECTORY
            SearchResult rs = (SearchResult)Session["directsearch"];
            if (rs.GetDirectoryEntry().Properties["samaccountname"].Value != null)
                lblDbNaam.Text = "Username : " + rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();
            //ACTIVE DIRECTORY
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
            Response.Redirect("SMSContent.aspx");

        }
        //Knop voor het toevoegen van nieuwe Categorieën
        protected void btnMapToevoegen_Click(object sender, EventArgs e)
        {
            Page.Validate("MapToevoegenValidators");
            if (Page.IsValid)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + "12" + " - " + "11" + "');</script>");
                c.InsertMap(tbMapNaam.Text);
                Response.Redirect("SMSContent.aspx");
            }         
        }

        //Knop om de bestanden - NOT
        protected void btnShowBestand_Click(object sender, EventArgs e)
        {
            try
            {
                //Selecteren van het bestand uit de listbox dat getoond dient te worden.
                int lengte = lbBestanden.SelectedItem.Text.IndexOf("-", 0);
                Session["bestand"] = lbBestanden.SelectedItem.Text.Substring(4, lengte - 4);

                string src = bs.GetBestandInhoud();
                if (src.IndexOf(".mp4") < 0)
                {
                    Image1.ImageUrl = src;
                }
                else
                {                    
                    video.InnerHtml = @"<video id=""Viodeoo"" width=""320"" height=""240"" controls=""controls"" autoplay=""autoplay"" runat=""server""><source id=""Videoo"" src=" + src + @" type=""video/mp4""></video>";
                }
            }
            catch
            {
                Response.Write("<script>alert('Selecteer eerst een bestand.')</script>");
            }
        }

        //Knop om de inhoud van hoofd bericht en reacties te zien..
        protected void btnShowBerichtInhoud_Click(object sender, EventArgs e)
        {
            try
            {
                lblBerichtInhoud.Text = "";
                lblBerichtReactie.Text = "";
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
                string reports = likereports.Substring(likereports.IndexOf(".") + 1);
                lblLike.Text = likes;
                lblReport.Text = reports;
            }
            catch
            {
                Response.Write("<script>alert('Selecteer eerst een bericht.')</script>");
            }
        }

        //Knop om een Bericht te plaatsen.
        protected void btnPlaatsBericht_Click(object sender, EventArgs e)
        {
            try
            {
                ber.InsertBericht(tbTitel.Text, tbInhoud.Text);
            }
            catch
            {
                Response.Write("<script>alert('U dient een Titel/Inhoud in te voeren.')</script>");
            }
            Server.Transfer("SMSContent.aspx");
        }

        //Knop om bestanden up te loaden.
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            //WEBCONFIG HTTP RUNTIME @KEES  --------------------------------------------------------
            //Neem de goede file vanaf de pc.
            StartUpload();

            //Bestanden in de database zetten.
            bs.InsertBestand(imgPath, imgSize);
            Response.Redirect("SMSContent.aspx");
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
            try
            {
                ab.GiveLike();
            }
            catch
            {
                Response.Write("<script>alert('U heeft al een Like/Report gegeven voor dit bericht.')</script>");
            }

            Server.Transfer("SMSContent.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                ab.GiveReport();
            }
            catch
            {
                //  Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image Saved!')", true);
                Response.Write("<script>alert('U heeft al een Like/Report gegeven voor dit bericht.')</script>");



            }
            Server.Transfer("SMSContent.aspx");
            //  Response.Redirect("SMSContent.aspx");
        }
    }
}