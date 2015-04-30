namespace event_beheer_systeem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
                  /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.btnInloggen = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lbPassWord = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.gbaanmaak = new System.Windows.Forms.GroupBox();
            this.btnmaakaan = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbplaats = new System.Windows.Forms.TextBox();
            this.tbtoevoeg = new System.Windows.Forms.TextBox();
            this.tbhuisn = new System.Windows.Forms.TextBox();
            this.tbstraatnaam = new System.Windows.Forms.TextBox();
            this.tbalfa = new System.Windows.Forms.TextBox();
            this.tbnumeriek = new System.Windows.Forms.TextBox();
            this.dtpeind = new System.Windows.Forms.DateTimePicker();
            this.dtpbegin = new System.Windows.Forms.DateTimePicker();
            this.btnaanpassen = new System.Windows.Forms.Button();
            this.tbaanpassen = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gbaanpassen = new System.Windows.Forms.GroupBox();
            this.tbaanpassingen = new System.Windows.Forms.TextBox();
            this.gbLogin.SuspendLayout();
            this.gbaanmaak.SuspendLayout();
            this.gbaanpassen.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.btnInloggen);
            this.gbLogin.Controls.Add(this.tbPassword);
            this.gbLogin.Controls.Add(this.tbUsername);
            this.gbLogin.Controls.Add(this.lbPassWord);
            this.gbLogin.Controls.Add(this.lbUserName);
            this.gbLogin.Location = new System.Drawing.Point(405, 222);
            this.gbLogin.Margin = new System.Windows.Forms.Padding(2);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Padding = new System.Windows.Forms.Padding(2);
            this.gbLogin.Size = new System.Drawing.Size(368, 262);
            this.gbLogin.TabIndex = 0;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Login";
            // 
            // btnInloggen
            // 
            this.btnInloggen.Location = new System.Drawing.Point(144, 214);
            this.btnInloggen.Margin = new System.Windows.Forms.Padding(2);
            this.btnInloggen.Name = "btnInloggen";
            this.btnInloggen.Size = new System.Drawing.Size(80, 28);
            this.btnInloggen.TabIndex = 1;
            this.btnInloggen.Text = "Inloggen";
            this.btnInloggen.UseVisualStyleBackColor = true;
            this.btnInloggen.Click += new System.EventHandler(this.btnInloggen_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(144, 143);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(174, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(144, 61);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(2);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(174, 20);
            this.tbUsername.TabIndex = 2;
            // 
            // lbPassWord
            // 
            this.lbPassWord.AutoSize = true;
            this.lbPassWord.Location = new System.Drawing.Point(5, 143);
            this.lbPassWord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPassWord.Name = "lbPassWord";
            this.lbPassWord.Size = new System.Drawing.Size(71, 13);
            this.lbPassWord.TabIndex = 1;
            this.lbPassWord.Text = "Wachtwoord:";
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(5, 61);
            this.lbUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(87, 13);
            this.lbUserName.TabIndex = 0;
            this.lbUserName.Text = "Gebruikersnaam:";
            // 
            // gbaanmaak
            // 
            this.gbaanmaak.Controls.Add(this.btnmaakaan);
            this.gbaanmaak.Controls.Add(this.label10);
            this.gbaanmaak.Controls.Add(this.label6);
            this.gbaanmaak.Controls.Add(this.label5);
            this.gbaanmaak.Controls.Add(this.label4);
            this.gbaanmaak.Controls.Add(this.label3);
            this.gbaanmaak.Controls.Add(this.label2);
            this.gbaanmaak.Controls.Add(this.tbplaats);
            this.gbaanmaak.Controls.Add(this.tbtoevoeg);
            this.gbaanmaak.Controls.Add(this.tbhuisn);
            this.gbaanmaak.Controls.Add(this.tbstraatnaam);
            this.gbaanmaak.Controls.Add(this.tbalfa);
            this.gbaanmaak.Controls.Add(this.tbnumeriek);
            this.gbaanmaak.Controls.Add(this.dtpeind);
            this.gbaanmaak.Controls.Add(this.dtpbegin);
            this.gbaanmaak.Location = new System.Drawing.Point(796, 283);
            this.gbaanmaak.Name = "gbaanmaak";
            this.gbaanmaak.Size = new System.Drawing.Size(296, 266);
            this.gbaanmaak.TabIndex = 6;
            this.gbaanmaak.TabStop = false;
            this.gbaanmaak.Text = "event aanmaken:";
            this.gbaanmaak.Visible = false;
            // 
            // btnmaakaan
            // 
            this.btnmaakaan.Location = new System.Drawing.Point(22, 210);
            this.btnmaakaan.Name = "btnmaakaan";
            this.btnmaakaan.Size = new System.Drawing.Size(109, 50);
            this.btnmaakaan.TabIndex = 17;
            this.btnmaakaan.Text = "maak event aan";
            this.btnmaakaan.UseVisualStyleBackColor = true;
            this.btnmaakaan.Click += new System.EventHandler(this.btnmaakaan_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "straatnaam";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "huisnummer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "begin datum:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "plaats:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "postcode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "eind datum:";
            // 
            // tbplaats
            // 
            this.tbplaats.Location = new System.Drawing.Point(106, 86);
            this.tbplaats.Name = "tbplaats";
            this.tbplaats.Size = new System.Drawing.Size(100, 20);
            this.tbplaats.TabIndex = 7;
            // 
            // tbtoevoeg
            // 
            this.tbtoevoeg.Location = new System.Drawing.Point(160, 164);
            this.tbtoevoeg.MaxLength = 1;
            this.tbtoevoeg.Name = "tbtoevoeg";
            this.tbtoevoeg.Size = new System.Drawing.Size(19, 20);
            this.tbtoevoeg.TabIndex = 6;
            // 
            // tbhuisn
            // 
            this.tbhuisn.Location = new System.Drawing.Point(106, 164);
            this.tbhuisn.MaxLength = 4;
            this.tbhuisn.Name = "tbhuisn";
            this.tbhuisn.Size = new System.Drawing.Size(48, 20);
            this.tbhuisn.TabIndex = 5;
            // 
            // tbstraatnaam
            // 
            this.tbstraatnaam.Location = new System.Drawing.Point(106, 138);
            this.tbstraatnaam.MaxLength = 35;
            this.tbstraatnaam.Name = "tbstraatnaam";
            this.tbstraatnaam.Size = new System.Drawing.Size(100, 20);
            this.tbstraatnaam.TabIndex = 4;
            // 
            // tbalfa
            // 
            this.tbalfa.Location = new System.Drawing.Point(160, 112);
            this.tbalfa.MaxLength = 2;
            this.tbalfa.Name = "tbalfa";
            this.tbalfa.Size = new System.Drawing.Size(35, 20);
            this.tbalfa.TabIndex = 3;
            // 
            // tbnumeriek
            // 
            this.tbnumeriek.Location = new System.Drawing.Point(106, 112);
            this.tbnumeriek.MaxLength = 4;
            this.tbnumeriek.Name = "tbnumeriek";
            this.tbnumeriek.Size = new System.Drawing.Size(48, 20);
            this.tbnumeriek.TabIndex = 2;
            // 
            // dtpeind
            // 
            this.dtpeind.Location = new System.Drawing.Point(93, 60);
            this.dtpeind.Name = "dtpeind";
            this.dtpeind.Size = new System.Drawing.Size(183, 20);
            this.dtpeind.TabIndex = 1;
            // 
            // dtpbegin
            // 
            this.dtpbegin.Location = new System.Drawing.Point(93, 34);
            this.dtpbegin.Name = "dtpbegin";
            this.dtpbegin.Size = new System.Drawing.Size(183, 20);
            this.dtpbegin.TabIndex = 0;
            // 
            // btnaanpassen
            // 
            this.btnaanpassen.Location = new System.Drawing.Point(117, 43);
            this.btnaanpassen.Name = "btnaanpassen";
            this.btnaanpassen.Size = new System.Drawing.Size(75, 23);
            this.btnaanpassen.TabIndex = 18;
            this.btnaanpassen.Text = "aanpassen";
            this.btnaanpassen.UseVisualStyleBackColor = true;
            this.btnaanpassen.Click += new System.EventHandler(this.btnaanpassen_Click);
            // 
            // tbaanpassen
            // 
            this.tbaanpassen.Location = new System.Drawing.Point(6, 46);
            this.tbaanpassen.Name = "tbaanpassen";
            this.tbaanpassen.Size = new System.Drawing.Size(100, 20);
            this.tbaanpassen.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "eventid";
            // 
            // gbaanpassen
            // 
            this.gbaanpassen.Controls.Add(this.label7);
            this.gbaanpassen.Controls.Add(this.btnaanpassen);
            this.gbaanpassen.Controls.Add(this.tbaanpassen);
            this.gbaanpassen.Location = new System.Drawing.Point(818, 117);
            this.gbaanpassen.Name = "gbaanpassen";
            this.gbaanpassen.Size = new System.Drawing.Size(200, 100);
            this.gbaanpassen.TabIndex = 21;
            this.gbaanpassen.TabStop = false;
            this.gbaanpassen.Text = "aanpassen eventgegevens";
            this.gbaanpassen.Visible = false;
            // 
            // tbaanpassingen
            // 
            this.tbaanpassingen.Location = new System.Drawing.Point(917, 223);
            this.tbaanpassingen.Name = "tbaanpassingen";
            this.tbaanpassingen.Size = new System.Drawing.Size(100, 20);
            this.tbaanpassingen.TabIndex = 23;
            this.tbaanpassingen.Visible = false;
            this.tbaanpassingen.TextChanged += new System.EventHandler(this.tbaanpassingen_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 711);
            this.Controls.Add(this.tbaanpassingen);
            this.Controls.Add(this.gbaanpassen);
            this.Controls.Add(this.gbaanmaak);
            this.Controls.Add(this.gbLogin);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "event beheer systeem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.gbaanmaak.ResumeLayout(false);
            this.gbaanmaak.PerformLayout();
            this.gbaanpassen.ResumeLayout(false);
            this.gbaanpassen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.Button btnInloggen;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lbPassWord;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.GroupBox gbaanmaak;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbplaats;
        private System.Windows.Forms.TextBox tbtoevoeg;
        private System.Windows.Forms.TextBox tbhuisn;
        private System.Windows.Forms.TextBox tbstraatnaam;
        private System.Windows.Forms.TextBox tbalfa;
        private System.Windows.Forms.TextBox tbnumeriek;
        private System.Windows.Forms.DateTimePicker dtpeind;
        private System.Windows.Forms.DateTimePicker dtpbegin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnmaakaan;
        private System.Windows.Forms.Button btnaanpassen;
        private System.Windows.Forms.TextBox tbaanpassen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbaanpassen;
        private System.Windows.Forms.TextBox tbaanpassingen;
    }
}

