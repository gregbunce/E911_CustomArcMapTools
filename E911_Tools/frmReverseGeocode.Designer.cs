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
            this.SuspendLayout();
            // 
            // cboChooseLayer
            // 
            this.cboChooseLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseLayer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboChooseLayer.FormattingEnabled = true;
            this.cboChooseLayer.Location = new System.Drawing.Point(17, 44);
            this.cboChooseLayer.Name = "cboChooseLayer";
            this.cboChooseLayer.Size = new System.Drawing.Size(208, 21);
            this.cboChooseLayer.TabIndex = 0;
            this.cboChooseLayer.SelectedIndexChanged += new System.EventHandler(this.cboChooseLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose Layer to Reverse Geocode";
            // 
            // btnReverseGeocode
            // 
            this.btnReverseGeocode.Location = new System.Drawing.Point(48, 323);
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
            this.label2.Location = new System.Drawing.Point(22, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Search Radius";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtSearchRadius
            // 
            this.txtSearchRadius.Location = new System.Drawing.Point(17, 97);
            this.txtSearchRadius.Name = "txtSearchRadius";
            this.txtSearchRadius.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearchRadius.Size = new System.Drawing.Size(85, 20);
            this.txtSearchRadius.TabIndex = 4;
            this.txtSearchRadius.Text = "1000";
            this.txtSearchRadius.TextChanged += new System.EventHandler(this.txtSearchRadius_TextChanged);
            this.txtSearchRadius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchRadius_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Choose Field to Write Address";
            // 
            // cboAddressField
            // 
            this.cboAddressField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAddressField.FormattingEnabled = true;
            this.cboAddressField.Location = new System.Drawing.Point(17, 151);
            this.cboAddressField.Name = "cboAddressField";
            this.cboAddressField.Size = new System.Drawing.Size(208, 21);
            this.cboAddressField.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Choose Field to Write Address Number";
            // 
            // cboChooseAddressNumber
            // 
            this.cboChooseAddressNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseAddressNumber.FormattingEnabled = true;
            this.cboChooseAddressNumber.Location = new System.Drawing.Point(17, 209);
            this.cboChooseAddressNumber.Name = "cboChooseAddressNumber";
            this.cboChooseAddressNumber.Size = new System.Drawing.Size(208, 21);
            this.cboChooseAddressNumber.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Choose Field to Write HWY Number";
            // 
            // cboChooseHwyName
            // 
            this.cboChooseHwyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseHwyName.FormattingEnabled = true;
            this.cboChooseHwyName.Location = new System.Drawing.Point(17, 267);
            this.cboChooseHwyName.Name = "cboChooseHwyName";
            this.cboChooseHwyName.Size = new System.Drawing.Size(208, 21);
            this.cboChooseHwyName.TabIndex = 10;
            // 
            // frmReverseGeocode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 367);
            this.Controls.Add(this.cboChooseHwyName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboChooseAddressNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboAddressField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSearchRadius);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReverseGeocode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboChooseLayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmReverseGeocode";
            this.ShowIcon = false;
            this.Text = "Reverse Geocode Mile Markers";
            this.Load += new System.EventHandler(this.frmReverseGeocode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}