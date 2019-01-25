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
            this.grpSoftwareDir = new System.Windows.Forms.GroupBox();
            this.txtTMImageSavePath = new System.Windows.Forms.TextBox();
            this.lblTMImageSavePathErr = new System.Windows.Forms.Label();
            this.lblTMImageSavePath = new System.Windows.Forms.Label();
            this.txtUIImageLoadPath = new System.Windows.Forms.TextBox();
            this.lblUIImageLoadPathErr = new System.Windows.Forms.Label();
            this.lblUIImageLoadPath = new System.Windows.Forms.Label();
            this.grpMESConfig.SuspendLayout();
            this.grpSoftwareDir.SuspendLayout();
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
            this.grpMESConfig.Size = new System.Drawing.Size(695, 86);
            this.grpMESConfig.TabIndex = 1;
            this.grpMESConfig.TabStop = false;
            this.grpMESConfig.Text = "SQL Setting:";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(163, 25);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(510, 20);
            this.txtDataSource.TabIndex = 1;
            // 
            // lblAttachDBFilenameErr
            // 
            this.lblAttachDBFilenameErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttachDBFilenameErr.ForeColor = System.Drawing.Color.Red;
            this.lblAttachDBFilenameErr.Location = new System.Drawing.Point(679, 52);
            this.lblAttachDBFilenameErr.Name = "lblAttachDBFilenameErr";
            this.lblAttachDBFilenameErr.Size = new System.Drawing.Size(10, 17);
            this.lblAttachDBFilenameErr.TabIndex = 10;
            this.lblAttachDBFilenameErr.Text = "*";
            // 
            // lblDataSourceErr
            // 
            this.lblDataSourceErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataSourceErr.ForeColor = System.Drawing.Color.Red;
            this.lblDataSourceErr.Location = new System.Drawing.Point(679, 26);
            this.lblDataSourceErr.Name = "lblDataSourceErr";
            this.lblDataSourceErr.Size = new System.Drawing.Size(10, 17);
            this.lblDataSourceErr.TabIndex = 10;
            this.lblDataSourceErr.Text = "*";
            // 
            // lblDataSource
            // 
            this.lblDataSource.Location = new System.Drawing.Point(7, 28);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(150, 17);
            this.lblDataSource.TabIndex = 10;
            this.lblDataSource.Text = "DataSource:";
            // 
            // txtAttachDBFilename
            // 
            this.txtAttachDBFilename.Location = new System.Drawing.Point(163, 51);
            this.txtAttachDBFilename.Name = "txtAttachDBFilename";
            this.txtAttachDBFilename.Size = new System.Drawing.Size(510, 20);
            this.txtAttachDBFilename.TabIndex = 2;
            // 
            // lblAttachDBFilename
            // 
            this.lblAttachDBFilename.Location = new System.Drawing.Point(7, 54);
            this.lblAttachDBFilename.Name = "lblAttachDBFilename";
            this.lblAttachDBFilename.Size = new System.Drawing.Size(150, 17);
            this.lblAttachDBFilename.TabIndex = 4;
            this.lblAttachDBFilename.Text = "AttachDBFilename:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(464, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(141, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpSoftwareDir
            // 
            this.grpSoftwareDir.Controls.Add(this.txtTMImageSavePath);
            this.grpSoftwareDir.Controls.Add(this.lblTMImageSavePathErr);
            this.grpSoftwareDir.Controls.Add(this.lblTMImageSavePath);
            this.grpSoftwareDir.Controls.Add(this.txtUIImageLoadPath);
            this.grpSoftwareDir.Controls.Add(this.lblUIImageLoadPathErr);
            this.grpSoftwareDir.Controls.Add(this.lblUIImageLoadPath);
            this.grpSoftwareDir.Location = new System.Drawing.Point(12, 104);
            this.grpSoftwareDir.Name = "grpSoftwareDir";
            this.grpSoftwareDir.Size = new System.Drawing.Size(695, 84);
            this.grpSoftwareDir.TabIndex = 5;
            this.grpSoftwareDir.TabStop = false;
            this.grpSoftwareDir.Text = "Software Directory:";
            // 
            // txtTMImageSavePath
            // 
            this.txtTMImageSavePath.Location = new System.Drawing.Point(163, 53);
            this.txtTMImageSavePath.Name = "txtTMImageSavePath";
            this.txtTMImageSavePath.Size = new System.Drawing.Size(510, 20);
            this.txtTMImageSavePath.TabIndex = 11;
            // 
            // lblTMImageSavePathErr
            // 
            this.lblTMImageSavePathErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTMImageSavePathErr.ForeColor = System.Drawing.Color.Red;
            this.lblTMImageSavePathErr.Location = new System.Drawing.Point(678, 53);
            this.lblTMImageSavePathErr.Name = "lblTMImageSavePathErr";
            this.lblTMImageSavePathErr.Size = new System.Drawing.Size(10, 17);
            this.lblTMImageSavePathErr.TabIndex = 12;
            this.lblTMImageSavePathErr.Text = "*";
            // 
            // lblTMImageSavePath
            // 
            this.lblTMImageSavePath.Location = new System.Drawing.Point(6, 55);
            this.lblTMImageSavePath.Name = "lblTMImageSavePath";
            this.lblTMImageSavePath.Size = new System.Drawing.Size(150, 17);
            this.lblTMImageSavePath.TabIndex = 13;
            this.lblTMImageSavePath.Text = "TM Image Save Path:";
            // 
            // txtUIImageLoadPath
            // 
            this.txtUIImageLoadPath.Location = new System.Drawing.Point(164, 26);
            this.txtUIImageLoadPath.Name = "txtUIImageLoadPath";
            this.txtUIImageLoadPath.Size = new System.Drawing.Size(510, 20);
            this.txtUIImageLoadPath.TabIndex = 1;
            // 
            // lblUIImageLoadPathErr
            // 
            this.lblUIImageLoadPathErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUIImageLoadPathErr.ForeColor = System.Drawing.Color.Red;
            this.lblUIImageLoadPathErr.Location = new System.Drawing.Point(679, 26);
            this.lblUIImageLoadPathErr.Name = "lblUIImageLoadPathErr";
            this.lblUIImageLoadPathErr.Size = new System.Drawing.Size(10, 17);
            this.lblUIImageLoadPathErr.TabIndex = 10;
            this.lblUIImageLoadPathErr.Text = "*";
            // 
            // lblUIImageLoadPath
            // 
            this.lblUIImageLoadPath.Location = new System.Drawing.Point(7, 28);
            this.lblUIImageLoadPath.Name = "lblUIImageLoadPath";
            this.lblUIImageLoadPath.Size = new System.Drawing.Size(150, 17);
            this.lblUIImageLoadPath.TabIndex = 10;
            this.lblUIImageLoadPath.Text = "UI Image Load Path:";
            // 
            // FrmSpecialSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 225);
            this.Controls.Add(this.grpSoftwareDir);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpMESConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSpecialSetting";
            this.Text = "Special Setting";
            this.Load += new System.EventHandler(this.FrmSpecialSetting_Load);
            this.grpMESConfig.ResumeLayout(false);
            this.grpMESConfig.PerformLayout();
            this.grpSoftwareDir.ResumeLayout(false);
            this.grpSoftwareDir.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpSoftwareDir;
        private System.Windows.Forms.TextBox txtUIImageLoadPath;
        private System.Windows.Forms.Label lblUIImageLoadPathErr;
        private System.Windows.Forms.Label lblUIImageLoadPath;
        private System.Windows.Forms.TextBox txtTMImageSavePath;
        private System.Windows.Forms.Label lblTMImageSavePathErr;
        private System.Windows.Forms.Label lblTMImageSavePath;
    }
}