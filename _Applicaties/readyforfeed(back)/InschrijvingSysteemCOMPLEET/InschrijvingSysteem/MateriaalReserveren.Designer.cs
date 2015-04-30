namespace InschrijvingSysteem
{
    partial class txtbox_rfid
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
            this.lb_voorwerplist = new System.Windows.Forms.ListBox();
            this.txtbox_voorwerp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_reserveer = new System.Windows.Forms.Button();
            this.txtbox_begindatum = new System.Windows.Forms.DateTimePicker();
            this.txtbox_einddatum = new System.Windows.Forms.DateTimePicker();
            this.txtbox_accountnaam = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_voorwerplist
            // 
            this.lb_voorwerplist.FormattingEnabled = true;
            this.lb_voorwerplist.Location = new System.Drawing.Point(265, 48);
            this.lb_voorwerplist.Name = "lb_voorwerplist";
            this.lb_voorwerplist.Size = new System.Drawing.Size(340, 589);
            this.lb_voorwerplist.TabIndex = 0;
            this.lb_voorwerplist.SelectedIndexChanged += new System.EventHandler(this.lb_voorwerplist_SelectedIndexChanged);
            // 
            // txtbox_voorwerp
            // 
            this.txtbox_voorwerp.Location = new System.Drawing.Point(680, 285);
            this.txtbox_voorwerp.Name = "txtbox_voorwerp";
            this.txtbox_voorwerp.Size = new System.Drawing.Size(100, 20);
            this.txtbox_voorwerp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(608, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "VoorwerpID:";
            // 
            // btn_reserveer
            // 
            this.btn_reserveer.Location = new System.Drawing.Point(805, 365);
            this.btn_reserveer.Name = "btn_reserveer";
            this.btn_reserveer.Size = new System.Drawing.Size(75, 23);
            this.btn_reserveer.TabIndex = 3;
            this.btn_reserveer.Text = "Reserveer";
            this.btn_reserveer.UseVisualStyleBackColor = true;
            this.btn_reserveer.Click += new System.EventHandler(this.btn_reserveer_Click);
            // 
            // txtbox_begindatum
            // 
            this.txtbox_begindatum.Location = new System.Drawing.Point(680, 312);
            this.txtbox_begindatum.Name = "txtbox_begindatum";
            this.txtbox_begindatum.Size = new System.Drawing.Size(200, 20);
            this.txtbox_begindatum.TabIndex = 4;
            // 
            // txtbox_einddatum
            // 
            this.txtbox_einddatum.Location = new System.Drawing.Point(680, 339);
            this.txtbox_einddatum.Name = "txtbox_einddatum";
            this.txtbox_einddatum.Size = new System.Drawing.Size(200, 20);
            this.txtbox_einddatum.TabIndex = 5;
            // 
            // txtbox_accountnaam
            // 
            this.txtbox_accountnaam.Location = new System.Drawing.Point(611, 69);
            this.txtbox_accountnaam.Name = "txtbox_accountnaam";
            this.txtbox_accountnaam.Size = new System.Drawing.Size(100, 20);
            this.txtbox_accountnaam.TabIndex = 6;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(611, 145);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(204, 20);
            this.textBox3.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(608, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Accountnaam:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(608, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Scan RFID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "OF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(608, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Begindatum:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(608, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Einddatum:";
            // 
            // txtbox_rfid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtbox_accountnaam);
            this.Controls.Add(this.txtbox_einddatum);
            this.Controls.Add(this.txtbox_begindatum);
            this.Controls.Add(this.btn_reserveer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbox_voorwerp);
            this.Controls.Add(this.lb_voorwerplist);
            this.Name = "txtbox_rfid";
            this.Text = "MateriaalReserveren";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_voorwerplist;
        private System.Windows.Forms.TextBox txtbox_voorwerp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_reserveer;
        private System.Windows.Forms.DateTimePicker txtbox_begindatum;
        private System.Windows.Forms.DateTimePicker txtbox_einddatum;
        private System.Windows.Forms.TextBox txtbox_accountnaam;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}