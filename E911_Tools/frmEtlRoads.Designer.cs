namespace E911_Tools
{
    partial class frmEtlRoads
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
            this.cboPSAPname = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnETLtoPSAP = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.lblProgressBar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboPSAPname
            // 
            this.cboPSAPname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPSAPname.FormattingEnabled = true;
            this.cboPSAPname.Items.AddRange(new object[] {
            "Beaver",
            "BoxElder",
            "Ceder",
            "Richfield",
            "SanJuan",
            "StGeorge",
            "TOC",
            "UintahBasin",
            "Weber"});
            this.cboPSAPname.Location = new System.Drawing.Point(19, 57);
            this.cboPSAPname.Name = "cboPSAPname";
            this.cboPSAPname.Size = new System.Drawing.Size(273, 21);
            this.cboPSAPname.TabIndex = 0;
            this.cboPSAPname.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select PSAP for ETL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(470, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "This tool extracts roads data from UTRANS database and loads them into selected P" +
    "SAP schema.";
            // 
            // btnETLtoPSAP
            // 
            this.btnETLtoPSAP.Location = new System.Drawing.Point(360, 57);
            this.btnETLtoPSAP.Name = "btnETLtoPSAP";
            this.btnETLtoPSAP.Size = new System.Drawing.Size(98, 23);
            this.btnETLtoPSAP.TabIndex = 3;
            this.btnETLtoPSAP.Text = "ETL PSAP";
            this.btnETLtoPSAP.UseVisualStyleBackColor = true;
            this.btnETLtoPSAP.Click += new System.EventHandler(this.btnETLtoPSAP_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(19, 113);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(436, 23);
            this.pBar.TabIndex = 4;
            this.pBar.Visible = false;
            // 
            // lblProgressBar
            // 
            this.lblProgressBar.AutoSize = true;
            this.lblProgressBar.Location = new System.Drawing.Point(20, 96);
            this.lblProgressBar.Name = "lblProgressBar";
            this.lblProgressBar.Size = new System.Drawing.Size(65, 13);
            this.lblProgressBar.TabIndex = 5;
            this.lblProgressBar.Text = "Working .....";
            this.lblProgressBar.Visible = false;
            // 
            // frmEtlRoads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 152);
            this.Controls.Add(this.lblProgressBar);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnETLtoPSAP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPSAPname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmEtlRoads";
            this.ShowIcon = false;
            this.Text = "ETL Roads for PSAP";
            this.Load += new System.EventHandler(this.GetRoadsData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPSAPname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnETLtoPSAP;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label lblProgressBar;
    }
}