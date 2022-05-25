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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMESTypeErr = new System.Windows.Forms.Label();
            this.txtMESType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMESConduitStationErr = new System.Windows.Forms.Label();
            this.txtMESConduitStation = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lblMESConduitClientIdErr = new System.Windows.Forms.Label();
            this.txtMESConduitClientId = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.lblMESConduitURLErr = new System.Windows.Forms.Label();
            this.txtMESConduitURL = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.grpMeasurementConfig = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMESMeasureRevision = new System.Windows.Forms.Label();
            this.label5lblMESMeasureToolingId = new System.Windows.Forms.Label();
            this.txtMESMeasureRevision = new System.Windows.Forms.TextBox();
            this.txtMESMeasureToolingId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMESMeasureResourceNameErr = new System.Windows.Forms.Label();
            this.txtMESMeasureResourceName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.lblMESMeasureTestIdErr = new System.Windows.Forms.Label();
            this.lblMESMeasureProcessNameErr = new System.Windows.Forms.Label();
            this.lblMESMeasureStationErr = new System.Windows.Forms.Label();
            this.lblMESMeasureClientIdErr = new System.Windows.Forms.Label();
            this.lblMESMeasureSecretErr = new System.Windows.Forms.Label();
            this.lblMESMeasureServiceErr = new System.Windows.Forms.Label();
            this.txtMESMeasureProcessName = new System.Windows.Forms.TextBox();
            this.txtMESMeasureStation = new System.Windows.Forms.TextBox();
            this.txtMESMeasureClientId = new System.Windows.Forms.TextBox();
            this.txtMESMeasureSecret = new System.Windows.Forms.TextBox();
            this.txtMESMeasureService = new System.Windows.Forms.TextBox();
            this.txtMESMeasureTestId = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblMESMeasureURLErr = new System.Windows.Forms.Label();
            this.txtMESMeasureURL = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpTMConfig = new System.Windows.Forms.GroupBox();
            this.lblTMModbusPortErr = new System.Windows.Forms.Label();
            this.lblTMIPErr = new System.Windows.Forms.Label();
            this.txtTMModbusPort = new System.Windows.Forms.TextBox();
            this.txtTMIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpPCconfig = new System.Windows.Forms.GroupBox();
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
            this.grpSavePath = new System.Windows.Forms.GroupBox();
            this.lblLocalServerPathErr = new System.Windows.Forms.Label();
            this.txtLocalServerPath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdo42Qon = new System.Windows.Forms.RadioButton();
            this.rdo42Qoff = new System.Windows.Forms.RadioButton();
            this.grpMESConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpMeasurementConfig.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpTMConfig.SuspendLayout();
            this.grpPCconfig.SuspendLayout();
            this.pnlDataResetFreq.SuspendLayout();
            this.grpSavePath.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMESConfig
            // 
            this.grpMESConfig.Controls.Add(this.groupBox1);
            this.grpMESConfig.Controls.Add(this.grpMeasurementConfig);
            this.grpMESConfig.Location = new System.Drawing.Point(12, 12);
            this.grpMESConfig.Name = "grpMESConfig";
            this.grpMESConfig.Size = new System.Drawing.Size(631, 419);
            this.grpMESConfig.TabIndex = 4;
            this.grpMESConfig.TabStop = false;
            this.grpMESConfig.Text = "42Q Configuration:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMESTypeErr);
            this.groupBox1.Controls.Add(this.txtMESType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblMESConduitStationErr);
            this.groupBox1.Controls.Add(this.txtMESConduitStation);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.lblMESConduitClientIdErr);
            this.groupBox1.Controls.Add(this.txtMESConduitClientId);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.lblMESConduitURLErr);
            this.groupBox1.Controls.Add(this.txtMESConduitURL);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Location = new System.Drawing.Point(8, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 165);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conduit Configuration:";
            // 
            // lblMESTypeErr
            // 
            this.lblMESTypeErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESTypeErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESTypeErr.Location = new System.Drawing.Point(264, 128);
            this.lblMESTypeErr.Name = "lblMESTypeErr";
            this.lblMESTypeErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESTypeErr.TabIndex = 43;
            this.lblMESTypeErr.Text = "*";
            // 
            // txtMESType
            // 
            this.txtMESType.Location = new System.Drawing.Point(105, 128);
            this.txtMESType.MaxLength = 20;
            this.txtMESType.Name = "txtMESType";
            this.txtMESType.Size = new System.Drawing.Size(153, 20);
            this.txtMESType.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 41;
            this.label8.Text = "Type:";
            // 
            // lblMESConduitStationErr
            // 
            this.lblMESConduitStationErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESConduitStationErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESConduitStationErr.Location = new System.Drawing.Point(264, 91);
            this.lblMESConduitStationErr.Name = "lblMESConduitStationErr";
            this.lblMESConduitStationErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESConduitStationErr.TabIndex = 40;
            this.lblMESConduitStationErr.Text = "*";
            // 
            // txtMESConduitStation
            // 
            this.txtMESConduitStation.Location = new System.Drawing.Point(105, 91);
            this.txtMESConduitStation.MaxLength = 20;
            this.txtMESConduitStation.Name = "txtMESConduitStation";
            this.txtMESConduitStation.Size = new System.Drawing.Size(153, 20);
            this.txtMESConduitStation.TabIndex = 39;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(6, 94);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 17);
            this.label23.TabIndex = 38;
            this.label23.Text = "Station:";
            // 
            // lblMESConduitClientIdErr
            // 
            this.lblMESConduitClientIdErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESConduitClientIdErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESConduitClientIdErr.Location = new System.Drawing.Point(264, 54);
            this.lblMESConduitClientIdErr.Name = "lblMESConduitClientIdErr";
            this.lblMESConduitClientIdErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESConduitClientIdErr.TabIndex = 37;
            this.lblMESConduitClientIdErr.Text = "*";
            // 
            // txtMESConduitClientId
            // 
            this.txtMESConduitClientId.Location = new System.Drawing.Point(105, 54);
            this.txtMESConduitClientId.MaxLength = 20;
            this.txtMESConduitClientId.Name = "txtMESConduitClientId";
            this.txtMESConduitClientId.Size = new System.Drawing.Size(153, 20);
            this.txtMESConduitClientId.TabIndex = 36;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(6, 57);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 17);
            this.label22.TabIndex = 35;
            this.label22.Text = "Client Id:";
            // 
            // lblMESConduitURLErr
            // 
            this.lblMESConduitURLErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESConduitURLErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESConduitURLErr.Location = new System.Drawing.Point(264, 23);
            this.lblMESConduitURLErr.Name = "lblMESConduitURLErr";
            this.lblMESConduitURLErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESConduitURLErr.TabIndex = 20;
            this.lblMESConduitURLErr.Text = "*";
            // 
            // txtMESConduitURL
            // 
            this.txtMESConduitURL.Location = new System.Drawing.Point(105, 21);
            this.txtMESConduitURL.MaxLength = 200;
            this.txtMESConduitURL.Name = "txtMESConduitURL";
            this.txtMESConduitURL.Size = new System.Drawing.Size(153, 20);
            this.txtMESConduitURL.TabIndex = 19;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(6, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(103, 17);
            this.label21.TabIndex = 18;
            this.label21.Text = "URL:";
            // 
            // grpMeasurementConfig
            // 
            this.grpMeasurementConfig.Controls.Add(this.groupBox2);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureResourceNameErr);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureResourceName);
            this.grpMeasurementConfig.Controls.Add(this.label24);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureTestIdErr);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureProcessNameErr);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureStationErr);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureClientIdErr);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureSecretErr);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureServiceErr);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureProcessName);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureStation);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureClientId);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureSecret);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureService);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureTestId);
            this.grpMeasurementConfig.Controls.Add(this.label19);
            this.grpMeasurementConfig.Controls.Add(this.label17);
            this.grpMeasurementConfig.Controls.Add(this.label16);
            this.grpMeasurementConfig.Controls.Add(this.label15);
            this.grpMeasurementConfig.Controls.Add(this.label14);
            this.grpMeasurementConfig.Controls.Add(this.label13);
            this.grpMeasurementConfig.Controls.Add(this.lblMESMeasureURLErr);
            this.grpMeasurementConfig.Controls.Add(this.txtMESMeasureURL);
            this.grpMeasurementConfig.Controls.Add(this.label12);
            this.grpMeasurementConfig.Location = new System.Drawing.Point(8, 30);
            this.grpMeasurementConfig.Name = "grpMeasurementConfig";
            this.grpMeasurementConfig.Size = new System.Drawing.Size(598, 212);
            this.grpMeasurementConfig.TabIndex = 12;
            this.grpMeasurementConfig.TabStop = false;
            this.grpMeasurementConfig.Text = "Palindrome Configuration for Measurement:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMESMeasureRevision);
            this.groupBox2.Controls.Add(this.label5lblMESMeasureToolingId);
            this.groupBox2.Controls.Add(this.txtMESMeasureRevision);
            this.groupBox2.Controls.Add(this.txtMESMeasureToolingId);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(313, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 74);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fixture:";
            // 
            // lblMESMeasureRevision
            // 
            this.lblMESMeasureRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureRevision.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureRevision.Location = new System.Drawing.Point(259, 47);
            this.lblMESMeasureRevision.Name = "lblMESMeasureRevision";
            this.lblMESMeasureRevision.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureRevision.TabIndex = 43;
            this.lblMESMeasureRevision.Text = "*";
            // 
            // label5lblMESMeasureToolingId
            // 
            this.label5lblMESMeasureToolingId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5lblMESMeasureToolingId.ForeColor = System.Drawing.Color.Red;
            this.label5lblMESMeasureToolingId.Location = new System.Drawing.Point(259, 22);
            this.label5lblMESMeasureToolingId.Name = "label5lblMESMeasureToolingId";
            this.label5lblMESMeasureToolingId.Size = new System.Drawing.Size(10, 17);
            this.label5lblMESMeasureToolingId.TabIndex = 43;
            this.label5lblMESMeasureToolingId.Text = "*";
            // 
            // txtMESMeasureRevision
            // 
            this.txtMESMeasureRevision.Location = new System.Drawing.Point(106, 44);
            this.txtMESMeasureRevision.MaxLength = 20;
            this.txtMESMeasureRevision.Name = "txtMESMeasureRevision";
            this.txtMESMeasureRevision.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureRevision.TabIndex = 45;
            // 
            // txtMESMeasureToolingId
            // 
            this.txtMESMeasureToolingId.Location = new System.Drawing.Point(106, 21);
            this.txtMESMeasureToolingId.MaxLength = 20;
            this.txtMESMeasureToolingId.Name = "txtMESMeasureToolingId";
            this.txtMESMeasureToolingId.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureToolingId.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 44;
            this.label2.Text = "Revision:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "Tooling ID:";
            // 
            // lblMESMeasureResourceNameErr
            // 
            this.lblMESMeasureResourceNameErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureResourceNameErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureResourceNameErr.Location = new System.Drawing.Point(264, 56);
            this.lblMESMeasureResourceNameErr.Name = "lblMESMeasureResourceNameErr";
            this.lblMESMeasureResourceNameErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureResourceNameErr.TabIndex = 41;
            this.lblMESMeasureResourceNameErr.Text = "*";
            // 
            // txtMESMeasureResourceName
            // 
            this.txtMESMeasureResourceName.Location = new System.Drawing.Point(105, 56);
            this.txtMESMeasureResourceName.MaxLength = 200;
            this.txtMESMeasureResourceName.Name = "txtMESMeasureResourceName";
            this.txtMESMeasureResourceName.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureResourceName.TabIndex = 40;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(6, 58);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(103, 17);
            this.label24.TabIndex = 39;
            this.label24.Text = "Resource Name:";
            // 
            // lblMESMeasureTestIdErr
            // 
            this.lblMESMeasureTestIdErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureTestIdErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureTestIdErr.Location = new System.Drawing.Point(578, 25);
            this.lblMESMeasureTestIdErr.Name = "lblMESMeasureTestIdErr";
            this.lblMESMeasureTestIdErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureTestIdErr.TabIndex = 37;
            this.lblMESMeasureTestIdErr.Text = "*";
            // 
            // lblMESMeasureProcessNameErr
            // 
            this.lblMESMeasureProcessNameErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureProcessNameErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureProcessNameErr.Location = new System.Drawing.Point(578, 97);
            this.lblMESMeasureProcessNameErr.Name = "lblMESMeasureProcessNameErr";
            this.lblMESMeasureProcessNameErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureProcessNameErr.TabIndex = 36;
            this.lblMESMeasureProcessNameErr.Text = "*";
            // 
            // lblMESMeasureStationErr
            // 
            this.lblMESMeasureStationErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureStationErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureStationErr.Location = new System.Drawing.Point(578, 58);
            this.lblMESMeasureStationErr.Name = "lblMESMeasureStationErr";
            this.lblMESMeasureStationErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureStationErr.TabIndex = 35;
            this.lblMESMeasureStationErr.Text = "*";
            // 
            // lblMESMeasureClientIdErr
            // 
            this.lblMESMeasureClientIdErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureClientIdErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureClientIdErr.Location = new System.Drawing.Point(264, 158);
            this.lblMESMeasureClientIdErr.Name = "lblMESMeasureClientIdErr";
            this.lblMESMeasureClientIdErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureClientIdErr.TabIndex = 34;
            this.lblMESMeasureClientIdErr.Text = "*";
            // 
            // lblMESMeasureSecretErr
            // 
            this.lblMESMeasureSecretErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureSecretErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureSecretErr.Location = new System.Drawing.Point(264, 129);
            this.lblMESMeasureSecretErr.Name = "lblMESMeasureSecretErr";
            this.lblMESMeasureSecretErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureSecretErr.TabIndex = 33;
            this.lblMESMeasureSecretErr.Text = "*";
            // 
            // lblMESMeasureServiceErr
            // 
            this.lblMESMeasureServiceErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureServiceErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureServiceErr.Location = new System.Drawing.Point(264, 94);
            this.lblMESMeasureServiceErr.Name = "lblMESMeasureServiceErr";
            this.lblMESMeasureServiceErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureServiceErr.TabIndex = 32;
            this.lblMESMeasureServiceErr.Text = "*";
            // 
            // txtMESMeasureProcessName
            // 
            this.txtMESMeasureProcessName.Location = new System.Drawing.Point(419, 97);
            this.txtMESMeasureProcessName.MaxLength = 20;
            this.txtMESMeasureProcessName.Name = "txtMESMeasureProcessName";
            this.txtMESMeasureProcessName.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureProcessName.TabIndex = 30;
            // 
            // txtMESMeasureStation
            // 
            this.txtMESMeasureStation.Location = new System.Drawing.Point(419, 59);
            this.txtMESMeasureStation.MaxLength = 20;
            this.txtMESMeasureStation.Name = "txtMESMeasureStation";
            this.txtMESMeasureStation.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureStation.TabIndex = 29;
            // 
            // txtMESMeasureClientId
            // 
            this.txtMESMeasureClientId.Location = new System.Drawing.Point(105, 158);
            this.txtMESMeasureClientId.MaxLength = 20;
            this.txtMESMeasureClientId.Name = "txtMESMeasureClientId";
            this.txtMESMeasureClientId.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureClientId.TabIndex = 28;
            // 
            // txtMESMeasureSecret
            // 
            this.txtMESMeasureSecret.Location = new System.Drawing.Point(105, 129);
            this.txtMESMeasureSecret.MaxLength = 20;
            this.txtMESMeasureSecret.Name = "txtMESMeasureSecret";
            this.txtMESMeasureSecret.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureSecret.TabIndex = 27;
            // 
            // txtMESMeasureService
            // 
            this.txtMESMeasureService.Location = new System.Drawing.Point(105, 94);
            this.txtMESMeasureService.MaxLength = 20;
            this.txtMESMeasureService.Name = "txtMESMeasureService";
            this.txtMESMeasureService.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureService.TabIndex = 26;
            // 
            // txtMESMeasureTestId
            // 
            this.txtMESMeasureTestId.Location = new System.Drawing.Point(419, 23);
            this.txtMESMeasureTestId.MaxLength = 20;
            this.txtMESMeasureTestId.Name = "txtMESMeasureTestId";
            this.txtMESMeasureTestId.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureTestId.TabIndex = 25;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(310, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 17);
            this.label19.TabIndex = 24;
            this.label19.Text = "Test Id:";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(310, 94);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 17);
            this.label17.TabIndex = 22;
            this.label17.Text = "Process Name:";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(310, 59);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 17);
            this.label16.TabIndex = 21;
            this.label16.Text = "Station:";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 161);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 17);
            this.label15.TabIndex = 20;
            this.label15.Text = "Client:";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 17);
            this.label14.TabIndex = 19;
            this.label14.Text = "Secret:";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 17);
            this.label13.TabIndex = 18;
            this.label13.Text = "Service:";
            // 
            // lblMESMeasureURLErr
            // 
            this.lblMESMeasureURLErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMESMeasureURLErr.ForeColor = System.Drawing.Color.Red;
            this.lblMESMeasureURLErr.Location = new System.Drawing.Point(264, 23);
            this.lblMESMeasureURLErr.Name = "lblMESMeasureURLErr";
            this.lblMESMeasureURLErr.Size = new System.Drawing.Size(10, 17);
            this.lblMESMeasureURLErr.TabIndex = 17;
            this.lblMESMeasureURLErr.Text = "*";
            // 
            // txtMESMeasureURL
            // 
            this.txtMESMeasureURL.Location = new System.Drawing.Point(105, 21);
            this.txtMESMeasureURL.MaxLength = 200;
            this.txtMESMeasureURL.Name = "txtMESMeasureURL";
            this.txtMESMeasureURL.Size = new System.Drawing.Size(153, 20);
            this.txtMESMeasureURL.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "URL:";
            // 
            // grpTMConfig
            // 
            this.grpTMConfig.Controls.Add(this.lblTMModbusPortErr);
            this.grpTMConfig.Controls.Add(this.lblTMIPErr);
            this.grpTMConfig.Controls.Add(this.txtTMModbusPort);
            this.grpTMConfig.Controls.Add(this.txtTMIP);
            this.grpTMConfig.Controls.Add(this.label3);
            this.grpTMConfig.Controls.Add(this.label4);
            this.grpTMConfig.Location = new System.Drawing.Point(649, 23);
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
            this.btnCancel.Location = new System.Drawing.Point(767, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(90, 437);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpPCconfig
            // 
            this.grpPCconfig.Controls.Add(this.pnlDataResetFreq);
            this.grpPCconfig.Controls.Add(this.label6);
            this.grpPCconfig.Controls.Add(this.lblPCServerPortErr);
            this.grpPCconfig.Controls.Add(this.lblPCServerIPErr);
            this.grpPCconfig.Controls.Add(this.txtPCServerPort);
            this.grpPCconfig.Controls.Add(this.txtPCServerIP);
            this.grpPCconfig.Controls.Add(this.label9);
            this.grpPCconfig.Controls.Add(this.label10);
            this.grpPCconfig.Location = new System.Drawing.Point(649, 109);
            this.grpPCconfig.Name = "grpPCconfig";
            this.grpPCconfig.Size = new System.Drawing.Size(290, 106);
            this.grpPCconfig.TabIndex = 6;
            this.grpPCconfig.TabStop = false;
            this.grpPCconfig.Text = "PC Configuration:";
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
            // grpSavePath
            // 
            this.grpSavePath.Controls.Add(this.lblLocalServerPathErr);
            this.grpSavePath.Controls.Add(this.txtLocalServerPath);
            this.grpSavePath.Controls.Add(this.label11);
            this.grpSavePath.Location = new System.Drawing.Point(649, 221);
            this.grpSavePath.Name = "grpSavePath";
            this.grpSavePath.Size = new System.Drawing.Size(290, 57);
            this.grpSavePath.TabIndex = 11;
            this.grpSavePath.TabStop = false;
            this.grpSavePath.Text = "Save Path:";
            // 
            // lblLocalServerPathErr
            // 
            this.lblLocalServerPathErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalServerPathErr.ForeColor = System.Drawing.Color.Red;
            this.lblLocalServerPathErr.Location = new System.Drawing.Point(274, 26);
            this.lblLocalServerPathErr.Name = "lblLocalServerPathErr";
            this.lblLocalServerPathErr.Size = new System.Drawing.Size(10, 17);
            this.lblLocalServerPathErr.TabIndex = 15;
            this.lblLocalServerPathErr.Text = "*";
            // 
            // txtLocalServerPath
            // 
            this.txtLocalServerPath.Location = new System.Drawing.Point(115, 26);
            this.txtLocalServerPath.Name = "txtLocalServerPath";
            this.txtLocalServerPath.Size = new System.Drawing.Size(153, 20);
            this.txtLocalServerPath.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "Local Server Path:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdo42Qoff);
            this.groupBox3.Controls.Add(this.rdo42Qon);
            this.groupBox3.Location = new System.Drawing.Point(649, 285);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 49);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "42Q Controller:";
            // 
            // rdo42Qon
            // 
            this.rdo42Qon.AutoSize = true;
            this.rdo42Qon.Location = new System.Drawing.Point(25, 19);
            this.rdo42Qon.Name = "rdo42Qon";
            this.rdo42Qon.Size = new System.Drawing.Size(41, 17);
            this.rdo42Qon.TabIndex = 0;
            this.rdo42Qon.TabStop = true;
            this.rdo42Qon.Text = "ON";
            this.rdo42Qon.UseVisualStyleBackColor = true;
            // 
            // rdo42Qoff
            // 
            this.rdo42Qoff.AutoSize = true;
            this.rdo42Qoff.Location = new System.Drawing.Point(118, 19);
            this.rdo42Qoff.Name = "rdo42Qoff";
            this.rdo42Qoff.Size = new System.Drawing.Size(45, 17);
            this.rdo42Qoff.TabIndex = 1;
            this.rdo42Qoff.TabStop = true;
            this.rdo42Qoff.Text = "OFF";
            this.rdo42Qoff.UseVisualStyleBackColor = true;
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 472);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpSavePath);
            this.Controls.Add(this.grpPCconfig);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMeasurementConfig.ResumeLayout(false);
            this.grpMeasurementConfig.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpTMConfig.ResumeLayout(false);
            this.grpTMConfig.PerformLayout();
            this.grpPCconfig.ResumeLayout(false);
            this.grpPCconfig.PerformLayout();
            this.pnlDataResetFreq.ResumeLayout(false);
            this.pnlDataResetFreq.PerformLayout();
            this.grpSavePath.ResumeLayout(false);
            this.grpSavePath.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMESConfig;
        private System.Windows.Forms.GroupBox grpTMConfig;
        private System.Windows.Forms.TextBox txtTMModbusPort;
        private System.Windows.Forms.TextBox txtTMIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpPCconfig;
        private System.Windows.Forms.TextBox txtPCServerPort;
        private System.Windows.Forms.TextBox txtPCServerIP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTMModbusPortErr;
        private System.Windows.Forms.Label lblTMIPErr;
        private System.Windows.Forms.Label lblPCServerPortErr;
        private System.Windows.Forms.Label lblPCServerIPErr;
        private System.Windows.Forms.Panel pnlDataResetFreq;
        private System.Windows.Forms.RadioButton rdoHourly;
        private System.Windows.Forms.RadioButton rdoDaily;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpSavePath;
        private System.Windows.Forms.Label lblLocalServerPathErr;
        private System.Windows.Forms.TextBox txtLocalServerPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox grpMeasurementConfig;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblMESMeasureURLErr;
        private System.Windows.Forms.TextBox txtMESMeasureURL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMESTypeErr;
        private System.Windows.Forms.TextBox txtMESType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMESConduitStationErr;
        private System.Windows.Forms.TextBox txtMESConduitStation;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblMESConduitClientIdErr;
        private System.Windows.Forms.TextBox txtMESConduitClientId;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblMESConduitURLErr;
        private System.Windows.Forms.TextBox txtMESConduitURL;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblMESMeasureTestIdErr;
        private System.Windows.Forms.Label lblMESMeasureProcessNameErr;
        private System.Windows.Forms.Label lblMESMeasureStationErr;
        private System.Windows.Forms.Label lblMESMeasureClientIdErr;
        private System.Windows.Forms.Label lblMESMeasureSecretErr;
        private System.Windows.Forms.Label lblMESMeasureServiceErr;
        private System.Windows.Forms.TextBox txtMESMeasureProcessName;
        private System.Windows.Forms.TextBox txtMESMeasureStation;
        private System.Windows.Forms.TextBox txtMESMeasureClientId;
        private System.Windows.Forms.TextBox txtMESMeasureSecret;
        private System.Windows.Forms.TextBox txtMESMeasureService;
        private System.Windows.Forms.TextBox txtMESMeasureTestId;
        private System.Windows.Forms.Label lblMESMeasureResourceNameErr;
        private System.Windows.Forms.TextBox txtMESMeasureResourceName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMESMeasureRevision;
        private System.Windows.Forms.Label label5lblMESMeasureToolingId;
        private System.Windows.Forms.TextBox txtMESMeasureRevision;
        private System.Windows.Forms.TextBox txtMESMeasureToolingId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdo42Qoff;
        private System.Windows.Forms.RadioButton rdo42Qon;
    }
}