namespace RVIS
{
    partial class FrmSetting
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
            this.lblLocalServerPathErr = new System.Windows.Forms.Label();
            this.txtLocalServerPath = new System.Windows.Forms.TextBox();
            this.lblLocalServerPath = new System.Windows.Forms.Label();
            this.txtMESDevNum = new System.Windows.Forms.TextBox();
            this.lblMESPortErr = new System.Windows.Forms.Label();
            this.lblMESIPErr = new System.Windows.Forms.Label();
            this.lblMESDevNumErr = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMESPort = new System.Windows.Forms.TextBox();
            this.txtMESIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpTMConfig = new System.Windows.Forms.GroupBox();
            this.lblTMModbusPortErr = new System.Windows.Forms.Label();
            this.lblTMIPErr = new System.Windows.Forms.Label();
            this.txtTMModbusPort = new System.Windows.Forms.TextBox();
            this.txtTMIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlDataResetFreq = new System.Windows.Forms.Panel();
            this.rdoHourly = new System.Windows.Forms.RadioButton();
            this.rdoDaily = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPCServerPortErr = new System.Windows.Forms.Label();
            this.lblPCServerIPErr = new System.Windows.Forms.Label();
            this.txtPCServerPort = new System.Windows.Forms.TextBox();
            this.txtPCServerIP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grpMESConfig.SuspendLayout();
            this.grpTMConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlDataResetFreq.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMESConfig
            // 
            this.grpMESConfig.Controls.Add(this.lblLocalServerPathErr);
            this.grpMESConfig.Controls.Add(this.txtLocalServerPath);
            this.grpMESConfig.Controls.Add(this.lblLocalServerPath);
            this.grpMESConfig.Controls.Add(this.txtMESDevNum);
            this.grpMESConfig.Controls.Add(this.lblMESPortErr);
            this.grpMESConfig.Controls.Add(this.lblMESIPErr);
            this.grpMESConfig.Controls.Add(this.lblMESDevNumErr);
            this.grpMESConfig.Controls.Add(this.label5);
            this.grpMESConfig.Controls.Add(this.txtMESPort);
            this.grpMESConfig.Controls.Add(this.txtMESIP);
            this.grpMESConfig.Controls.Add(this.label2);
            this.grpMESConfig.Controls.Add(this.label1);
            this.grpMESConfig.Location = new System.Drawing.Point(12, 12);
            this.grpMESConfig.Name = "grpMESConfig";
            this.grpMESConfig.Size = new System.Drawing.Size(290, 192);
            this.grpMESConfig.TabIndex = 4;
            this.grpMESConfig.TabStop = false;
            this.grpMESConfig.Text = "42Q Configuration:";
            // 
            // lblLocalServerPathErr
            // 
            this.lblLocalServerPathErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalServerPathErr.ForeColor = System.Drawing.Color.Red;
            this.lblLocalServerPathErr.Location = new System.Drawing.Point(274, 105);
            this.lblLocalServerPathErr.Name = "lblLocalServerPathErr";
            this.lblLocalServerPathErr.Size = new System.Drawing.Size(10, 17);
            this.lblLocalServerPathErr.TabIndex = 13;
            this.lblLocalServerPathErr.Text = "*";
            // 
            // txtLocalServerPath
            // 
            this.txtLocalServerPath.Location = new System.Drawing.Point(115, 104);
            this.txtLocalServerPath.Name = "txtLocalServerPath";
            this.txtLocalServerPath.Size = new System.Drawing.Size(153, 20);
            this.txtLocalServerPath.TabIndex = 11;
            // 
            // lblLocalServerPath
            // 
            this.lblLocalServerPath.Location = new System.Drawing.Point(6, 107);
            this.lblLocalServerPath.Name = "lblLocalServerPath";
            this.lblLocalServerPath.Size = new System.Drawing.Size(103, 17);
            this.lblLocalServerPath.TabIndex = 12;
            this.lblLocalServerPath.Text = "Local Server Path:";
            // 
            // txtMESDevNum
            // 
            this.txtMESDevNum.Location = new System.Drawing.Point(115, 25);
            this.txtMESDevNum.MaxLength = 3;
            this.txtMESDevNum.Name = "txtMESDevNum";
            this.txtMESDevNum.Size = new System.Drawing.Size(153, 20);
            this.txtMESDevNum.TabIndex = 1;
            // 
            // lblMESPortErr
            // 
            this.lblMESPortErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESPortErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESPortErr.Location = new System.Drawing.Point(274, 79);
            this.lblMESPortErr.Name = "lblMESPortErr";
            this.lblMESPortErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESPortErr.TabIndex = 10;
            this.lblMESPortErr.Text = "*";
            // 
            // lblMESIPErr
            // 
            this.lblMESIPErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESIPErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESIPErr.Location = new System.Drawing.Point(274, 52);
            this.lblMESIPErr.Name = "lblMESIPErr";
            this.lblMESIPErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESIPErr.TabIndex = 10;
            this.lblMESIPErr.Text = "*";
            // 
            // lblMESDevNumErr
            // 
            this.lblMESDevNumErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESDevNumErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESDevNumErr.Location = new System.Drawing.Point(274, 26);
            this.lblMESDevNumErr.Name = "lblMESDevNumErr";
            this.lblMESDevNumErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESDevNumErr.TabIndex = 10;
            this.lblMESDevNumErr.Text = "*";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Device Num:";
            // 
            // txtMESPort
            // 
            this.txtMESPort.Location = new System.Drawing.Point(115, 78);
            this.txtMESPort.MaxLength = 5;
            this.txtMESPort.Name = "txtMESPort";
            this.txtMESPort.Size = new System.Drawing.Size(153, 20);
            this.txtMESPort.TabIndex = 3;
            // 
            // txtMESIP
            // 
            this.txtMESIP.Location = new System.Drawing.Point(115, 51);
            this.txtMESIP.MaxLength = 15;
            this.txtMESIP.Name = "txtMESIP";
            this.txtMESIP.Size = new System.Drawing.Size(153, 20);
            this.txtMESIP.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address:";
            // 
            // grpTMConfig
            // 
            this.grpTMConfig.Controls.Add(this.lblTMModbusPortErr);
            this.grpTMConfig.Controls.Add(this.lblTMIPErr);
            this.grpTMConfig.Controls.Add(this.txtTMModbusPort);
            this.grpTMConfig.Controls.Add(this.txtTMIP);
            this.grpTMConfig.Controls.Add(this.label3);
            this.grpTMConfig.Controls.Add(this.label4);
            this.grpTMConfig.Location = new System.Drawing.Point(308, 12);
            this.grpTMConfig.Name = "grpTMConfig";
            this.grpTMConfig.Size = new System.Drawing.Size(290, 80);
            this.grpTMConfig.TabIndex = 5;
            this.grpTMConfig.TabStop = false;
            this.grpTMConfig.Text = "TM Configuration:";
            // 
            // lblTMModbusPortErr
            // 
            this.lblTMModbusPortErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTMModbusPortErr.ForeColor = System.Drawing.Color.Red;
            this.lblTMModbusPortErr.Location = new System.Drawing.Point(274, 51);
            this.lblTMModbusPortErr.Name = "lblTMModbusPortErr";
            this.lblTMModbusPortErr.Size = new System.Drawing.Size(10, 17);
            this.lblTMModbusPortErr.TabIndex = 11;
            this.lblTMModbusPortErr.Text = "*";
            // 
            // lblTMIPErr
            // 
            this.lblTMIPErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTMIPErr.ForeColor = System.Drawing.Color.Red;
            this.lblTMIPErr.Location = new System.Drawing.Point(274, 25);
            this.lblTMIPErr.Name = "lblTMIPErr";
            this.lblTMIPErr.Size = new System.Drawing.Size(10, 17);
            this.lblTMIPErr.TabIndex = 12;
            this.lblTMIPErr.Text = "*";
            // 
            // txtTMModbusPort
            // 
            this.txtTMModbusPort.Location = new System.Drawing.Point(115, 50);
            this.txtTMModbusPort.MaxLength = 5;
            this.txtTMModbusPort.Name = "txtTMModbusPort";
            this.txtTMModbusPort.Size = new System.Drawing.Size(153, 20);
            this.txtTMModbusPort.TabIndex = 5;
            // 
            // txtTMIP
            // 
            this.txtTMIP.Location = new System.Drawing.Point(115, 24);
            this.txtTMIP.MaxLength = 15;
            this.txtTMIP.Name = "txtTMIP";
            this.txtTMIP.Size = new System.Drawing.Size(153, 20);
            this.txtTMIP.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Modbus Port:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "IP Address:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(423, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(113, 210);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlDataResetFreq);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblPCServerPortErr);
            this.groupBox1.Controls.Add(this.lblPCServerIPErr);
            this.groupBox1.Controls.Add(this.txtPCServerPort);
            this.groupBox1.Controls.Add(this.txtPCServerIP);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(308, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 106);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PC Configuration:";
            // 
            // pnlDataResetFreq
            // 
            this.pnlDataResetFreq.Controls.Add(this.rdoHourly);
            this.pnlDataResetFreq.Controls.Add(this.rdoDaily);
            this.pnlDataResetFreq.Location = new System.Drawing.Point(115, 76);
            this.pnlDataResetFreq.Name = "pnlDataResetFreq";
            this.pnlDataResetFreq.Size = new System.Drawing.Size(153, 24);
            this.pnlDataResetFreq.TabIndex = 8;
            // 
            // rdoHourly
            // 
            this.rdoHourly.AutoSize = true;
            this.rdoHourly.Location = new System.Drawing.Point(57, 3);
            this.rdoHourly.Name = "rdoHourly";
            this.rdoHourly.Size = new System.Drawing.Size(55, 17);
            this.rdoHourly.TabIndex = 1;
            this.rdoHourly.TabStop = true;
            this.rdoHourly.Text = "Hourly";
            this.rdoHourly.UseVisualStyleBackColor = true;
            // 
            // rdoDaily
            // 
            this.rdoDaily.AutoSize = true;
            this.rdoDaily.Location = new System.Drawing.Point(3, 3);
            this.rdoDaily.Name = "rdoDaily";
            this.rdoDaily.Size = new System.Drawing.Size(48, 17);
            this.rdoDaily.TabIndex = 0;
            this.rdoDaily.TabStop = true;
            this.rdoDaily.Text = "Daily";
            this.rdoDaily.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Data Reset Freq.:";
            // 
            // lblPCServerPortErr
            // 
            this.lblPCServerPortErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPCServerPortErr.ForeColor = System.Drawing.Color.Red;
            this.lblPCServerPortErr.Location = new System.Drawing.Point(274, 51);
            this.lblPCServerPortErr.Name = "lblPCServerPortErr";
            this.lblPCServerPortErr.Size = new System.Drawing.Size(10, 17);
            this.lblPCServerPortErr.TabIndex = 13;
            this.lblPCServerPortErr.Text = "*";
            // 
            // lblPCServerIPErr
            // 
            this.lblPCServerIPErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPCServerIPErr.ForeColor = System.Drawing.Color.Red;
            this.lblPCServerIPErr.Location = new System.Drawing.Point(274, 25);
            this.lblPCServerIPErr.Name = "lblPCServerIPErr";
            this.lblPCServerIPErr.Size = new System.Drawing.Size(10, 17);
            this.lblPCServerIPErr.TabIndex = 14;
            this.lblPCServerIPErr.Text = "*";
            // 
            // txtPCServerPort
            // 
            this.txtPCServerPort.Location = new System.Drawing.Point(115, 50);
            this.txtPCServerPort.MaxLength = 5;
            this.txtPCServerPort.Name = "txtPCServerPort";
            this.txtPCServerPort.Size = new System.Drawing.Size(153, 20);
            this.txtPCServerPort.TabIndex = 7;
            // 
            // txtPCServerIP
            // 
            this.txtPCServerIP.Location = new System.Drawing.Point(115, 24);
            this.txtPCServerIP.MaxLength = 15;
            this.txtPCServerIP.Name = "txtPCServerIP";
            this.txtPCServerIP.Size = new System.Drawing.Size(153, 20);
            this.txtPCServerIP.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Server Port:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 17);
            this.label10.TabIndex = 4;
            this.label10.Text = "IP Address:";
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 245);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpTMConfig);
            this.Controls.Add(this.grpMESConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSetting";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.grpMESConfig.ResumeLayout(false);
            this.grpMESConfig.PerformLayout();
            this.grpTMConfig.ResumeLayout(false);
            this.grpTMConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlDataResetFreq.ResumeLayout(false);
            this.pnlDataResetFreq.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMESConfig;
        private System.Windows.Forms.TextBox txtMESDevNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMESPort;
        private System.Windows.Forms.TextBox txtMESIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpTMConfig;
        private System.Windows.Forms.TextBox txtTMModbusPort;
        private System.Windows.Forms.TextBox txtTMIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPCServerPort;
        private System.Windows.Forms.TextBox txtPCServerIP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblMESPortErr;
        private System.Windows.Forms.Label lblMESIPErr;
        private System.Windows.Forms.Label lblMESDevNumErr;
        private System.Windows.Forms.Label lblTMModbusPortErr;
        private System.Windows.Forms.Label lblTMIPErr;
        private System.Windows.Forms.Label lblPCServerPortErr;
        private System.Windows.Forms.Label lblPCServerIPErr;
        private System.Windows.Forms.Panel pnlDataResetFreq;
        private System.Windows.Forms.RadioButton rdoHourly;
        private System.Windows.Forms.RadioButton rdoDaily;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLocalServerPathErr;
        private System.Windows.Forms.TextBox txtLocalServerPath;
        private System.Windows.Forms.Label lblLocalServerPath;
    }
}