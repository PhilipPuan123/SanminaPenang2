namespace RVIS
{
    partial class OverallSts
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
            this.lblOverallResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOverallResult
            // 
            this.lblOverallResult.BackColor = System.Drawing.SystemColors.Control;
            this.lblOverallResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOverallResult.Font = new System.Drawing.Font("Impact", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverallResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOverallResult.Location = new System.Drawing.Point(5, 8);
            this.lblOverallResult.Name = "lblOverallResult";
            this.lblOverallResult.Size = new System.Drawing.Size(700, 235);
            this.lblOverallResult.TabIndex = 8;
            this.lblOverallResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OverallSts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 252);
            this.Controls.Add(this.lblOverallResult);
            this.Name = "OverallSts";
            this.Opacity = 0.7D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OverallSts";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblOverallResult;
    }
}