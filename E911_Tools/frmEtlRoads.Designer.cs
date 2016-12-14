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
            this.btnETLtoPSAP = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.lblProgressBar = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBuffer = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnReproject = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLengthMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSelectLawZone = new System.Windows.Forms.ComboBox();
            this.btnSplitSelectSeg = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectIntersectSegs = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboPSAPname
            // 
            this.cboPSAPname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPSAPname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cboPSAPname.Location = new System.Drawing.Point(15, 19);
            this.cboPSAPname.Name = "cboPSAPname";
            this.cboPSAPname.Size = new System.Drawing.Size(273, 21);
            this.cboPSAPname.TabIndex = 0;
            this.cboPSAPname.SelectedIndexChanged += new System.EventHandler(this.cboPSAPname_SelectedIndexChanged);
            // 
            // btnETLtoPSAP
            // 
            this.btnETLtoPSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnETLtoPSAP.Location = new System.Drawing.Point(336, 24);
            this.btnETLtoPSAP.Name = "btnETLtoPSAP";
            this.btnETLtoPSAP.Size = new System.Drawing.Size(120, 23);
            this.btnETLtoPSAP.TabIndex = 3;
            this.btnETLtoPSAP.Text = "ETL Roads";
            this.btnETLtoPSAP.UseVisualStyleBackColor = true;
            this.btnETLtoPSAP.Click += new System.EventHandler(this.btnETLtoPSAP_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(19, 65);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(430, 23);
            this.pBar.TabIndex = 4;
            // 
            // lblProgressBar
            // 
            this.lblProgressBar.AutoSize = true;
            this.lblProgressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressBar.Location = new System.Drawing.Point(20, 48);
            this.lblProgressBar.Name = "lblProgressBar";
            this.lblProgressBar.Size = new System.Drawing.Size(94, 13);
            this.lblProgressBar.TabIndex = 5;
            this.lblProgressBar.Text = "ETL Status Bar.....";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBuffer);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblProgressBar);
            this.groupBox1.Controls.Add(this.pBar);
            this.groupBox1.Controls.Add(this.btnETLtoPSAP);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ETL Roads from UTRANS and Custom Segments to FGDB";
            // 
            // txtBuffer
            // 
            this.txtBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuffer.Location = new System.Drawing.Point(248, 26);
            this.txtBuffer.Name = "txtBuffer";
            this.txtBuffer.Size = new System.Drawing.Size(46, 20);
            this.txtBuffer.TabIndex = 7;
            this.txtBuffer.Text = "0";
            this.txtBuffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuffer_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(43, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Apply buffer to UTRANS roads in miles:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 355);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Post-ETL Processing for Spillman System";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnReproject);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(15, 278);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(454, 62);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Reproject the File Geodatabase";
            // 
            // btnReproject
            // 
            this.btnReproject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReproject.Location = new System.Drawing.Point(334, 17);
            this.btnReproject.Name = "btnReproject";
            this.btnReproject.Size = new System.Drawing.Size(103, 23);
            this.btnReproject.TabIndex = 1;
            this.btnReproject.Text = "Reproject";
            this.btnReproject.UseVisualStyleBackColor = true;
            this.btnReproject.Click += new System.EventHandler(this.btnReproject_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(306, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Reproject the File GeoDatabase to WGS84 for Spillman System";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.txtLengthMin);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.cboSelectLawZone);
            this.groupBox6.Controls.Add(this.btnSplitSelectSeg);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(15, 151);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(454, 109);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Split the Selected Segments that Cross Polygon Boundaries";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(233, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "meters.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Split lines if length is greater than";
            // 
            // txtLengthMin
            // 
            this.txtLengthMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLengthMin.Location = new System.Drawing.Point(189, 21);
            this.txtLengthMin.Name = "txtLengthMin";
            this.txtLengthMin.Size = new System.Drawing.Size(38, 20);
            this.txtLengthMin.TabIndex = 3;
            this.txtLengthMin.Text = "20";
            this.txtLengthMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLengthMin.TextChanged += new System.EventHandler(this.txtLengthMin_TextChanged);
            this.txtLengthMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLengthMin_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(373, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Select polygon boundary to base split on (based on selected dispatch above):";
            // 
            // cboSelectLawZone
            // 
            this.cboSelectLawZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectLawZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSelectLawZone.FormattingEnabled = true;
            this.cboSelectLawZone.Items.AddRange(new object[] {
            "City CD",
            "EMS Zones",
            "Fire Zones",
            "Law Zones"});
            this.cboSelectLawZone.Location = new System.Drawing.Point(21, 73);
            this.cboSelectLawZone.Name = "cboSelectLawZone";
            this.cboSelectLawZone.Size = new System.Drawing.Size(275, 21);
            this.cboSelectLawZone.TabIndex = 1;
            // 
            // btnSplitSelectSeg
            // 
            this.btnSplitSelectSeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplitSelectSeg.Location = new System.Drawing.Point(312, 72);
            this.btnSplitSelectSeg.Name = "btnSplitSelectSeg";
            this.btnSplitSelectSeg.Size = new System.Drawing.Size(128, 23);
            this.btnSplitSelectSeg.TabIndex = 0;
            this.btnSplitSelectSeg.Text = "Split Selected";
            this.btnSplitSelectSeg.UseVisualStyleBackColor = true;
            this.btnSplitSelectSeg.Click += new System.EventHandler(this.btnSplitSelectSeg_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.btnSelectIntersectSegs);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(15, 29);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(454, 107);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Select the Segments that Cross Polygon Boundaries";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(237, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "2: Target = Roads;   Source = Polygon Boundary";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "4: Use Shift-Select to exclude segments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(357, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "3: Spatial Selection = are crossed by the outline of the source layer feature";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "1: Click button to open ArcMap\'s Select By Location tool";
            // 
            // btnSelectIntersectSegs
            // 
            this.btnSelectIntersectSegs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectIntersectSegs.Location = new System.Drawing.Point(302, 22);
            this.btnSelectIntersectSegs.Name = "btnSelectIntersectSegs";
            this.btnSelectIntersectSegs.Size = new System.Drawing.Size(138, 23);
            this.btnSelectIntersectSegs.TabIndex = 0;
            this.btnSelectIntersectSegs.Text = "Select by Location";
            this.btnSelectIntersectSegs.UseVisualStyleBackColor = true;
            this.btnSelectIntersectSegs.Click += new System.EventHandler(this.btnSelectIntersectSegs_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboPSAPname);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 53);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Dispatch Center";
            // 
            // frmEtlRoads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 584);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmEtlRoads";
            this.ShowIcon = false;
            this.Text = "ETL Roads Data for Dispatch Center";
            this.Load += new System.EventHandler(this.GetRoadsData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPSAPname;
        private System.Windows.Forms.Button btnETLtoPSAP;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label lblProgressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSelectIntersectSegs;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSplitSelectSeg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnReproject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSelectLawZone;
        private System.Windows.Forms.TextBox txtLengthMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuffer;
        private System.Windows.Forms.Label label9;
    }
}