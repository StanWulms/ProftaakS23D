namespace DBconnectietest
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
            this.btn_dbconnect = new System.Windows.Forms.Button();
            this.btn_query = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dbconnect
            // 
            this.btn_dbconnect.Location = new System.Drawing.Point(87, 78);
            this.btn_dbconnect.Name = "btn_dbconnect";
            this.btn_dbconnect.Size = new System.Drawing.Size(75, 23);
            this.btn_dbconnect.TabIndex = 0;
            this.btn_dbconnect.Text = "DBconnect";
            this.btn_dbconnect.UseVisualStyleBackColor = true;
            this.btn_dbconnect.Click += new System.EventHandler(this.btn_dbconnect_Click);
            // 
            // btn_query
            // 
            this.btn_query.Location = new System.Drawing.Point(12, 246);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(130, 23);
            this.btn_query.TabIndex = 1;
            this.btn_query.Text = "Voer Query uit";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(430, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(171, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "voeg iets toe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 281);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_query);
            this.Controls.Add(this.btn_dbconnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_dbconnect;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

