namespace E911_Tools
{
    partial class frmReverseGeocode
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
            this.cboChooseLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReverseGeocode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchRadius = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAddressField = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboChooseAddressNumber = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboChooseHwyName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioHWYNAME = new System.Windows.Forms.RadioButton();
            this.radioSTREETNAME = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAssignCityCD = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboChooseLayer
            // 
            this.cboChooseLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseLayer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboChooseLayer.FormattingEnabled = true;
            this.cboChooseLayer.Location = new System.Drawing.Point(10, 44);
            this.cboChooseLayer.Name = "cboChooseLayer";
            this.cboChooseLayer.Size = new System.Drawing.Size(240, 21);
            this.cboChooseLayer.TabIndex = 0;
            this.cboChooseLayer.SelectedIndexChanged += new System.EventHandler(this.cboChooseLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose Layer to Reverse Geocode";
            // 
            // btnReverseGeocode
            // 
            this.btnReverseGeocode.Location = new System.Drawing.Point(48, 253);
            this.btnReverseGeocode.Name = "btnReverseGeocode";
            this.btnReverseGeocode.Size = new System.Drawing.Size(160, 23);
            this.btnReverseGeocode.TabIndex = 2;
            this.btnReverseGeocode.Text = "Reverse Geocode";
            this.btnReverseGeocode.UseVisualStyleBackColor = true;
            this.btnReverseGeocode.Click += new System.EventHandler(this.btnReverseGeocode_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Search Radius";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtSearchRadius
            // 
            this.txtSearchRadius.Location = new System.Drawing.Point(22, 42);
            this.txtSearchRadius.Name = "txtSearchRadius";
            this.txtSearchRadius.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearchRadius.Size = new System.Drawing.Size(85, 20);
            this.txtSearchRadius.TabIndex = 4;
            this.txtSearchRadius.Text = "10000";
            this.txtSearchRadius.TextChanged += new System.EventHandler(this.txtSearchRadius_TextChanged);
            this.txtSearchRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchRadius_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Choose Field to Write Entire Result";
            // 
            // cboAddressField
            // 
            this.cboAddressField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAddressField.FormattingEnabled = true;
            this.cboAddressField.Location = new System.Drawing.Point(22, 96);
            this.cboAddressField.Name = "cboAddressField";
            this.cboAddressField.Size = new System.Drawing.Size(227, 21);
            this.cboAddressField.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(222, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Choose Field to Write Result Address Number";
            // 
            // cboChooseAddressNumber
            // 
            this.cboChooseAddressNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseAddressNumber.FormattingEnabled = true;
            this.cboChooseAddressNumber.Location = new System.Drawing.Point(22, 154);
            this.cboChooseAddressNumber.Name = "cboChooseAddressNumber";
            this.cboChooseAddressNumber.Size = new System.Drawing.Size(227, 21);
            this.cboChooseAddressNumber.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Choose Field to Write Result Street Name";
            // 
            // cboChooseHwyName
            // 
            this.cboChooseHwyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseHwyName.FormattingEnabled = true;
            this.cboChooseHwyName.Location = new System.Drawing.Point(22, 212);
            this.cboChooseHwyName.Name = "cboChooseHwyName";
            this.cboChooseHwyName.Size = new System.Drawing.Size(227, 21);
            this.cboChooseHwyName.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Run this tool twice - once for each Geocoder:";
            // 
            // radioHWYNAME
            // 
            this.radioHWYNAME.AutoSize = true;
            this.radioHWYNAME.Location = new System.Drawing.Point(36, 42);
            this.radioHWYNAME.Name = "radioHWYNAME";
            this.radioHWYNAME.Size = new System.Drawing.Size(132, 17);
            this.radioHWYNAME.TabIndex = 12;
            this.radioHWYNAME.Text = "HWYNAME Geocoder";
            this.radioHWYNAME.UseVisualStyleBackColor = true;
            // 
            // radioSTREETNAME
            // 
            this.radioSTREETNAME.AutoSize = true;
            this.radioSTREETNAME.Location = new System.Drawing.Point(36, 66);
            this.radioSTREETNAME.Name = "radioSTREETNAME";
            this.radioSTREETNAME.Size = new System.Drawing.Size(149, 17);
            this.radioSTREETNAME.TabIndex = 13;
            this.radioSTREETNAME.Text = "STREETNAME Geocoder";
            this.radioSTREETNAME.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Feet";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.radioHWYNAME);
            this.groupBox1.Controls.Add(this.radioSTREETNAME);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 103);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choice Geocoder";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSearchRadius);
            this.groupBox2.Controls.Add(this.btnReverseGeocode);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cboChooseHwyName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboAddressField);
            this.groupBox2.Controls.Add(this.cboChooseAddressNumber);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 308);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reverse Geocode";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAssignCityCD);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(12, 555);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 117);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Assign City Codes";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cboChooseLayer);
            this.groupBox4.Location = new System.Drawing.Point(13, 122);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 91);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Choose Map Layer";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(215, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "This button will assign the city code from the";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(219, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Dispatch Center\'s City Polygon to the chosen";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "map layer\'s CITYCD field.";
            // 
            // btnAssignCityCD
            // 
            this.btnAssignCityCD.Location = new System.Drawing.Point(49, 81);
            this.btnAssignCityCD.Name = "btnAssignCityCD";
            this.btnAssignCityCD.Size = new System.Drawing.Size(160, 23);
            this.btnAssignCityCD.TabIndex = 3;
            this.btnAssignCityCD.Text = "Assign CITYCD";
            this.btnAssignCityCD.UseVisualStyleBackColor = true;
            this.btnAssignCityCD.Click += new System.EventHandler(this.btnAssignCityCD_Click);
            // 
            // frmReverseGeocode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 690);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmReverseGeocode";
            this.ShowIcon = false;
            this.Text = "Reverse Geocode Mile Markers";
            this.Load += new System.EventHandler(this.frmReverseGeocode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboChooseLayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReverseGeocode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchRadius;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboAddressField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboChooseAddressNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboChooseHwyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioHWYNAME;
        private System.Windows.Forms.RadioButton radioSTREETNAME;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAssignCityCD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}