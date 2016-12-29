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
            this.SuspendLayout();
            // 
            // cboChooseLayer
            // 
            this.cboChooseLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseLayer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboChooseLayer.FormattingEnabled = true;
            this.cboChooseLayer.Location = new System.Drawing.Point(13, 54);
            this.cboChooseLayer.Name = "cboChooseLayer";
            this.cboChooseLayer.Size = new System.Drawing.Size(311, 21);
            this.cboChooseLayer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose Layer to Reverse Geocode";
            // 
            // btnReverseGeocode
            // 
            this.btnReverseGeocode.Location = new System.Drawing.Point(84, 238);
            this.btnReverseGeocode.Name = "btnReverseGeocode";
            this.btnReverseGeocode.Size = new System.Drawing.Size(160, 23);
            this.btnReverseGeocode.TabIndex = 2;
            this.btnReverseGeocode.Text = "Reverse Geocode";
            this.btnReverseGeocode.UseVisualStyleBackColor = true;
            this.btnReverseGeocode.Click += new System.EventHandler(this.btnReverseGeocode_Click);
            // 
            // frmReverseGeocode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 273);
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
    }
}