namespace RVIS
{
    partial class FrmSpecialSetting
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
            this.grpMESConfig = new System.Windows.Forms.GroupBox();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.lblAttachDBFilenameErr = new System.Windows.Forms.Label();
            this.lblDataSourceErr = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.txtAttachDBFilename = new System.Windows.Forms.TextBox();
            this.lblAttachDBFilename = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpMESConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMESConfig
            // 
            this.grpMESConfig.Controls.Add(this.txtDataSource);
            this.grpMESConfig.Controls.Add(this.lblAttachDBFilenameErr);
            this.grpMESConfig.Controls.Add(this.lblDataSourceErr);
            this.grpMESConfig.Controls.Add(this.lblDataSource);
            this.grpMESConfig.Controls.Add(this.txtAttachDBFilename);
            this.grpMESConfig.Controls.Add(this.lblAttachDBFilename);
            this.grpMESConfig.Location = new System.Drawing.Point(12, 12);
            this.grpMESConfig.Name = "grpMESConfig";
            this.grpMESConfig.Size = new System.Drawing.Size(560, 86);
            this.grpMESConfig.TabIndex = 1;
            this.grpMESConfig.TabStop = false;
            this.grpMESConfig.Text = "SQL Setting:";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(115, 25);
            this.txtDataSource.MaxLength = 3;
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(423, 20);
            this.txtDataSource.TabIndex = 1;
            // 
            // lblAttachDBFilenameErr
            // 
            this.lblAttachDBFilenameErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttachDBFilenameErr.ForeColor = System.Drawing.Color.Red;
            this.lblAttachDBFilenameErr.Location = new System.Drawing.Point(544, 52);
            this.lblAttachDBFilenameErr.Name = "lblAttachDBFilenameErr";
            this.lblAttachDBFilenameErr.Size = new System.Drawing.Size(10, 17);
            this.lblAttachDBFilenameErr.TabIndex = 10;
            this.lblAttachDBFilenameErr.Text = "*";
            // 
            // lblDataSourceErr
            // 
            this.lblDataSourceErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataSourceErr.ForeColor = System.Drawing.Color.Red;
            this.lblDataSourceErr.Location = new System.Drawing.Point(544, 26);
            this.lblDataSourceErr.Name = "lblDataSourceErr";
            this.lblDataSourceErr.Size = new System.Drawing.Size(10, 17);
            this.lblDataSourceErr.TabIndex = 10;
            this.lblDataSourceErr.Text = "*";
            // 
            // lblDataSource
            // 
            this.lblDataSource.Location = new System.Drawing.Point(7, 28);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(103, 17);
            this.lblDataSource.TabIndex = 10;
            this.lblDataSource.Text = "DataSource:";
            // 
            // txtAttachDBFilename
            // 
            this.txtAttachDBFilename.Location = new System.Drawing.Point(115, 51);
            this.txtAttachDBFilename.MaxLength = 15;
            this.txtAttachDBFilename.Name = "txtAttachDBFilename";
            this.txtAttachDBFilename.Size = new System.Drawing.Size(423, 20);
            this.txtAttachDBFilename.TabIndex = 2;
            // 
            // lblAttachDBFilename
            // 
            this.lblAttachDBFilename.Location = new System.Drawing.Point(7, 54);
            this.lblAttachDBFilename.Name = "lblAttachDBFilename";
            this.lblAttachDBFilename.Size = new System.Drawing.Size(103, 17);
            this.lblAttachDBFilename.TabIndex = 4;
            this.lblAttachDBFilename.Text = "AttachDBFilename:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(399, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(76, 104);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmSpecialSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 139);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpMESConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSpecialSetting";
            this.Text = "Special Setting";
            this.Load += new System.EventHandler(this.FrmSpecialSetting_Load);
            this.grpMESConfig.ResumeLayout(false);
            this.grpMESConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMESConfig;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label lblDataSource;
        private System.Windows.Forms.TextBox txtAttachDBFilename;
        private System.Windows.Forms.Label lblAttachDBFilename;
        private System.Windows.Forms.Label lblAttachDBFilenameErr;
        private System.Windows.Forms.Label lblDataSourceErr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}