namespace TestToolKit
{
    partial class FrmTestToolKit
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
            this.grpModbus = new System.Windows.Forms.GroupBox();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRobotX = new System.Windows.Forms.Button();
            this.btnToolRZ = new System.Windows.Forms.Button();
            this.btnRobotY = new System.Windows.Forms.Button();
            this.btnToolRY = new System.Windows.Forms.Button();
            this.btnRobotZ = new System.Windows.Forms.Button();
            this.btnToolRX = new System.Windows.Forms.Button();
            this.btnRobotRX = new System.Windows.Forms.Button();
            this.btnToolZ = new System.Windows.Forms.Button();
            this.btnRobotRY = new System.Windows.Forms.Button();
            this.btnToolY = new System.Windows.Forms.Button();
            this.btnRobotRZ = new System.Windows.Forms.Button();
            this.btnToolX = new System.Windows.Forms.Button();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnPauseSts = new System.Windows.Forms.Button();
            this.btnEditSts = new System.Windows.Forms.Button();
            this.btnRunSts = new System.Windows.Forms.Button();
            this.btnErrorSts = new System.Windows.Forms.Button();
            this.btnPermissionSts = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.bgwUIThread = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMESLog = new System.Windows.Forms.Label();
            this.rtxMESLog = new System.Windows.Forms.RichTextBox();
            this.grpTcpServer = new System.Windows.Forms.GroupBox();
            this.txtListenerPort = new System.Windows.Forms.TextBox();
            this.lblListenerIP = new System.Windows.Forms.Label();
            this.txtListenerIP = new System.Windows.Forms.TextBox();
            this.btnStartListener = new System.Windows.Forms.Button();
            this.rtxTcpServerLog = new System.Windows.Forms.RichTextBox();
            this.grpTcpClient = new System.Windows.Forms.GroupBox();
            this.btnSendToServer = new System.Windows.Forms.Button();
            this.txtClientMsg = new System.Windows.Forms.TextBox();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnConnectServer = new System.Windows.Forms.Button();
            this.rtxTcpClientLog = new System.Windows.Forms.RichTextBox();
            this.btnGetLastError = new System.Windows.Forms.Button();
            this.grpModbus.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpTcpServer.SuspendLayout();
            this.grpTcpClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpModbus
            // 
            this.grpModbus.Controls.Add(this.grpControl);
            this.grpModbus.Controls.Add(this.grpStatus);
            this.grpModbus.Controls.Add(this.label3);
            this.grpModbus.Controls.Add(this.lblOutput);
            this.grpModbus.Controls.Add(this.label1);
            this.grpModbus.Controls.Add(this.txtIP);
            this.grpModbus.Controls.Add(this.btnConnect);
            this.grpModbus.Location = new System.Drawing.Point(12, 12);
            this.grpModbus.Name = "grpModbus";
            this.grpModbus.Size = new System.Drawing.Size(360, 420);
            this.grpModbus.TabIndex = 0;
            this.grpModbus.TabStop = false;
            this.grpModbus.Text = "Modbus";
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btnStart);
            this.grpControl.Controls.Add(this.btnStop);
            this.grpControl.Controls.Add(this.btnPause);
            this.grpControl.Controls.Add(this.btnRobotX);
            this.grpControl.Controls.Add(this.btnToolRZ);
            this.grpControl.Controls.Add(this.btnRobotY);
            this.grpControl.Controls.Add(this.btnToolRY);
            this.grpControl.Controls.Add(this.btnRobotZ);
            this.grpControl.Controls.Add(this.btnToolRX);
            this.grpControl.Controls.Add(this.btnRobotRX);
            this.grpControl.Controls.Add(this.btnToolZ);
            this.grpControl.Controls.Add(this.btnRobotRY);
            this.grpControl.Controls.Add(this.btnToolY);
            this.grpControl.Controls.Add(this.btnRobotRZ);
            this.grpControl.Controls.Add(this.btnToolX);
            this.grpControl.Location = new System.Drawing.Point(6, 64);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(251, 183);
            this.grpControl.TabIndex = 22;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "Control:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(87, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(168, 19);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRobotX
            // 
            this.btnRobotX.Location = new System.Drawing.Point(6, 55);
            this.btnRobotX.Name = "btnRobotX";
            this.btnRobotX.Size = new System.Drawing.Size(75, 23);
            this.btnRobotX.TabIndex = 7;
            this.btnRobotX.Text = "Robot X";
            this.btnRobotX.UseVisualStyleBackColor = true;
            this.btnRobotX.Click += new System.EventHandler(this.btnRobotX_Click);
            // 
            // btnToolRZ
            // 
            this.btnToolRZ.Location = new System.Drawing.Point(168, 151);
            this.btnToolRZ.Name = "btnToolRZ";
            this.btnToolRZ.Size = new System.Drawing.Size(75, 23);
            this.btnToolRZ.TabIndex = 18;
            this.btnToolRZ.Text = "Tool RZ";
            this.btnToolRZ.UseVisualStyleBackColor = true;
            this.btnToolRZ.Click += new System.EventHandler(this.btnToolRZ_Click);
            // 
            // btnRobotY
            // 
            this.btnRobotY.Location = new System.Drawing.Point(87, 55);
            this.btnRobotY.Name = "btnRobotY";
            this.btnRobotY.Size = new System.Drawing.Size(75, 23);
            this.btnRobotY.TabIndex = 8;
            this.btnRobotY.Text = "Robot Y";
            this.btnRobotY.UseVisualStyleBackColor = true;
            this.btnRobotY.Click += new System.EventHandler(this.btnRobotY_Click);
            // 
            // btnToolRY
            // 
            this.btnToolRY.Location = new System.Drawing.Point(87, 151);
            this.btnToolRY.Name = "btnToolRY";
            this.btnToolRY.Size = new System.Drawing.Size(75, 23);
            this.btnToolRY.TabIndex = 17;
            this.btnToolRY.Text = "Tool RY";
            this.btnToolRY.UseVisualStyleBackColor = true;
            this.btnToolRY.Click += new System.EventHandler(this.btnToolRY_Click);
            // 
            // btnRobotZ
            // 
            this.btnRobotZ.Location = new System.Drawing.Point(168, 55);
            this.btnRobotZ.Name = "btnRobotZ";
            this.btnRobotZ.Size = new System.Drawing.Size(75, 23);
            this.btnRobotZ.TabIndex = 9;
            this.btnRobotZ.Text = "Robot Z";
            this.btnRobotZ.UseVisualStyleBackColor = true;
            this.btnRobotZ.Click += new System.EventHandler(this.btnRobotZ_Click);
            // 
            // btnToolRX
            // 
            this.btnToolRX.Location = new System.Drawing.Point(6, 151);
            this.btnToolRX.Name = "btnToolRX";
            this.btnToolRX.Size = new System.Drawing.Size(75, 23);
            this.btnToolRX.TabIndex = 16;
            this.btnToolRX.Text = "Tool RX";
            this.btnToolRX.UseVisualStyleBackColor = true;
            this.btnToolRX.Click += new System.EventHandler(this.btnToolRX_Click);
            // 
            // btnRobotRX
            // 
            this.btnRobotRX.Location = new System.Drawing.Point(6, 84);
            this.btnRobotRX.Name = "btnRobotRX";
            this.btnRobotRX.Size = new System.Drawing.Size(75, 23);
            this.btnRobotRX.TabIndex = 10;
            this.btnRobotRX.Text = "Robot RX";
            this.btnRobotRX.UseVisualStyleBackColor = true;
            this.btnRobotRX.Click += new System.EventHandler(this.btnRobotRX_Click);
            // 
            // btnToolZ
            // 
            this.btnToolZ.Location = new System.Drawing.Point(168, 122);
            this.btnToolZ.Name = "btnToolZ";
            this.btnToolZ.Size = new System.Drawing.Size(75, 23);
            this.btnToolZ.TabIndex = 15;
            this.btnToolZ.Text = "Tool Z";
            this.btnToolZ.UseVisualStyleBackColor = true;
            this.btnToolZ.Click += new System.EventHandler(this.btnToolZ_Click);
            // 
            // btnRobotRY
            // 
            this.btnRobotRY.Location = new System.Drawing.Point(87, 84);
            this.btnRobotRY.Name = "btnRobotRY";
            this.btnRobotRY.Size = new System.Drawing.Size(75, 23);
            this.btnRobotRY.TabIndex = 11;
            this.btnRobotRY.Text = "Robot RY";
            this.btnRobotRY.UseVisualStyleBackColor = true;
            this.btnRobotRY.Click += new System.EventHandler(this.btnRobotRY_Click);
            // 
            // btnToolY
            // 
            this.btnToolY.Location = new System.Drawing.Point(87, 122);
            this.btnToolY.Name = "btnToolY";
            this.btnToolY.Size = new System.Drawing.Size(75, 23);
            this.btnToolY.TabIndex = 14;
            this.btnToolY.Text = "Tool Y";
            this.btnToolY.UseVisualStyleBackColor = true;
            this.btnToolY.Click += new System.EventHandler(this.btnToolY_Click);
            // 
            // btnRobotRZ
            // 
            this.btnRobotRZ.Location = new System.Drawing.Point(168, 84);
            this.btnRobotRZ.Name = "btnRobotRZ";
            this.btnRobotRZ.Size = new System.Drawing.Size(75, 23);
            this.btnRobotRZ.TabIndex = 12;
            this.btnRobotRZ.Text = "Robot RZ";
            this.btnRobotRZ.UseVisualStyleBackColor = true;
            this.btnRobotRZ.Click += new System.EventHandler(this.btnRobotRZ_Click);
            // 
            // btnToolX
            // 
            this.btnToolX.Location = new System.Drawing.Point(6, 122);
            this.btnToolX.Name = "btnToolX";
            this.btnToolX.Size = new System.Drawing.Size(75, 23);
            this.btnToolX.TabIndex = 13;
            this.btnToolX.Text = "Tool X";
            this.btnToolX.UseVisualStyleBackColor = true;
            this.btnToolX.Click += new System.EventHandler(this.btnToolX_Click);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.btnGetLastError);
            this.grpStatus.Controls.Add(this.btnPauseSts);
            this.grpStatus.Controls.Add(this.btnEditSts);
            this.grpStatus.Controls.Add(this.btnRunSts);
            this.grpStatus.Controls.Add(this.btnErrorSts);
            this.grpStatus.Controls.Add(this.btnPermissionSts);
            this.grpStatus.Location = new System.Drawing.Point(263, 64);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(88, 197);
            this.grpStatus.TabIndex = 21;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status:";
            // 
            // btnPauseSts
            // 
            this.btnPauseSts.Location = new System.Drawing.Point(6, 106);
            this.btnPauseSts.Name = "btnPauseSts";
            this.btnPauseSts.Size = new System.Drawing.Size(75, 23);
            this.btnPauseSts.TabIndex = 10;
            this.btnPauseSts.Text = "Pause";
            this.btnPauseSts.UseVisualStyleBackColor = true;
            this.btnPauseSts.Click += new System.EventHandler(this.btnPauseSts_Click);
            // 
            // btnEditSts
            // 
            this.btnEditSts.Location = new System.Drawing.Point(6, 77);
            this.btnEditSts.Name = "btnEditSts";
            this.btnEditSts.Size = new System.Drawing.Size(75, 23);
            this.btnEditSts.TabIndex = 9;
            this.btnEditSts.Text = "Editting";
            this.btnEditSts.UseVisualStyleBackColor = true;
            this.btnEditSts.Click += new System.EventHandler(this.btnEditSts_Click);
            // 
            // btnRunSts
            // 
            this.btnRunSts.Location = new System.Drawing.Point(6, 48);
            this.btnRunSts.Name = "btnRunSts";
            this.btnRunSts.Size = new System.Drawing.Size(75, 23);
            this.btnRunSts.TabIndex = 8;
            this.btnRunSts.Text = "Running";
            this.btnRunSts.UseVisualStyleBackColor = true;
            this.btnRunSts.Click += new System.EventHandler(this.btnRunSts_Click);
            // 
            // btnErrorSts
            // 
            this.btnErrorSts.Location = new System.Drawing.Point(6, 19);
            this.btnErrorSts.Name = "btnErrorSts";
            this.btnErrorSts.Size = new System.Drawing.Size(75, 23);
            this.btnErrorSts.TabIndex = 7;
            this.btnErrorSts.Text = "Error";
            this.btnErrorSts.UseVisualStyleBackColor = true;
            this.btnErrorSts.Click += new System.EventHandler(this.btnErrorSts_Click);
            // 
            // btnPermissionSts
            // 
            this.btnPermissionSts.Location = new System.Drawing.Point(6, 135);
            this.btnPermissionSts.Name = "btnPermissionSts";
            this.btnPermissionSts.Size = new System.Drawing.Size(75, 23);
            this.btnPermissionSts.TabIndex = 6;
            this.btnPermissionSts.Text = "Permission";
            this.btnPermissionSts.UseVisualStyleBackColor = true;
            this.btnPermissionSts.Click += new System.EventHandler(this.btnPermissionSts_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Output:";
            // 
            // lblOutput
            // 
            this.lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOutput.Location = new System.Drawing.Point(6, 315);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(345, 96);
            this.lblOutput.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(73, 21);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(116, 20);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "179.217.14.101";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(195, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // bgwUIThread
            // 
            this.bgwUIThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwUIThread_DoWork);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMESLog);
            this.groupBox1.Controls.Add(this.rtxMESLog);
            this.groupBox1.Location = new System.Drawing.Point(396, 389);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 168);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MES Logger:";
            // 
            // lblMESLog
            // 
            this.lblMESLog.AutoSize = true;
            this.lblMESLog.Location = new System.Drawing.Point(6, 21);
            this.lblMESLog.Name = "lblMESLog";
            this.lblMESLog.Size = new System.Drawing.Size(28, 13);
            this.lblMESLog.TabIndex = 24;
            this.lblMESLog.Text = "Log:";
            // 
            // rtxMESLog
            // 
            this.rtxMESLog.BackColor = System.Drawing.Color.Black;
            this.rtxMESLog.DetectUrls = false;
            this.rtxMESLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtxMESLog.ForeColor = System.Drawing.Color.Yellow;
            this.rtxMESLog.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.rtxMESLog.Location = new System.Drawing.Point(9, 37);
            this.rtxMESLog.Name = "rtxMESLog";
            this.rtxMESLog.ReadOnly = true;
            this.rtxMESLog.Size = new System.Drawing.Size(370, 125);
            this.rtxMESLog.TabIndex = 22;
            this.rtxMESLog.Text = "";
            // 
            // grpTcpServer
            // 
            this.grpTcpServer.Controls.Add(this.txtListenerPort);
            this.grpTcpServer.Controls.Add(this.lblListenerIP);
            this.grpTcpServer.Controls.Add(this.txtListenerIP);
            this.grpTcpServer.Controls.Add(this.btnStartListener);
            this.grpTcpServer.Controls.Add(this.rtxTcpServerLog);
            this.grpTcpServer.Location = new System.Drawing.Point(396, 14);
            this.grpTcpServer.Name = "grpTcpServer";
            this.grpTcpServer.Size = new System.Drawing.Size(382, 180);
            this.grpTcpServer.TabIndex = 23;
            this.grpTcpServer.TabStop = false;
            this.grpTcpServer.Text = "TCP Server:";
            // 
            // txtListenerPort
            // 
            this.txtListenerPort.Location = new System.Drawing.Point(195, 23);
            this.txtListenerPort.Name = "txtListenerPort";
            this.txtListenerPort.Size = new System.Drawing.Size(93, 20);
            this.txtListenerPort.TabIndex = 27;
            this.txtListenerPort.Text = "19888";
            // 
            // lblListenerIP
            // 
            this.lblListenerIP.AutoSize = true;
            this.lblListenerIP.Location = new System.Drawing.Point(6, 26);
            this.lblListenerIP.Name = "lblListenerIP";
            this.lblListenerIP.Size = new System.Drawing.Size(60, 13);
            this.lblListenerIP.TabIndex = 26;
            this.lblListenerIP.Text = "Listener IP:";
            // 
            // txtListenerIP
            // 
            this.txtListenerIP.Location = new System.Drawing.Point(73, 23);
            this.txtListenerIP.Name = "txtListenerIP";
            this.txtListenerIP.Size = new System.Drawing.Size(116, 20);
            this.txtListenerIP.TabIndex = 25;
            this.txtListenerIP.Text = "179.217.14.100";
            // 
            // btnStartListener
            // 
            this.btnStartListener.Location = new System.Drawing.Point(294, 21);
            this.btnStartListener.Name = "btnStartListener";
            this.btnStartListener.Size = new System.Drawing.Size(75, 23);
            this.btnStartListener.TabIndex = 24;
            this.btnStartListener.Text = "Start";
            this.btnStartListener.UseVisualStyleBackColor = true;
            this.btnStartListener.Click += new System.EventHandler(this.btnStartListener_Click);
            // 
            // rtxTcpServerLog
            // 
            this.rtxTcpServerLog.BackColor = System.Drawing.Color.Black;
            this.rtxTcpServerLog.DetectUrls = false;
            this.rtxTcpServerLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtxTcpServerLog.ForeColor = System.Drawing.Color.Lime;
            this.rtxTcpServerLog.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.rtxTcpServerLog.Location = new System.Drawing.Point(9, 50);
            this.rtxTcpServerLog.Name = "rtxTcpServerLog";
            this.rtxTcpServerLog.ReadOnly = true;
            this.rtxTcpServerLog.Size = new System.Drawing.Size(367, 124);
            this.rtxTcpServerLog.TabIndex = 23;
            this.rtxTcpServerLog.Text = "";
            this.rtxTcpServerLog.TextChanged += new System.EventHandler(this.rtxTcpServerLog_TextChanged);
            // 
            // grpTcpClient
            // 
            this.grpTcpClient.Controls.Add(this.btnSendToServer);
            this.grpTcpClient.Controls.Add(this.txtClientMsg);
            this.grpTcpClient.Controls.Add(this.txtServerPort);
            this.grpTcpClient.Controls.Add(this.lblServerIP);
            this.grpTcpClient.Controls.Add(this.txtServerIP);
            this.grpTcpClient.Controls.Add(this.btnConnectServer);
            this.grpTcpClient.Controls.Add(this.rtxTcpClientLog);
            this.grpTcpClient.Location = new System.Drawing.Point(396, 198);
            this.grpTcpClient.Name = "grpTcpClient";
            this.grpTcpClient.Size = new System.Drawing.Size(382, 185);
            this.grpTcpClient.TabIndex = 24;
            this.grpTcpClient.TabStop = false;
            this.grpTcpClient.Text = "TCP Client:";
            // 
            // btnSendToServer
            // 
            this.btnSendToServer.Location = new System.Drawing.Point(304, 155);
            this.btnSendToServer.Name = "btnSendToServer";
            this.btnSendToServer.Size = new System.Drawing.Size(75, 23);
            this.btnSendToServer.TabIndex = 29;
            this.btnSendToServer.Text = "Send";
            this.btnSendToServer.UseVisualStyleBackColor = true;
            this.btnSendToServer.Click += new System.EventHandler(this.btnSendToServer_Click);
            // 
            // txtClientMsg
            // 
            this.txtClientMsg.Location = new System.Drawing.Point(9, 157);
            this.txtClientMsg.Name = "txtClientMsg";
            this.txtClientMsg.Size = new System.Drawing.Size(289, 20);
            this.txtClientMsg.TabIndex = 28;
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(195, 23);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(93, 20);
            this.txtServerPort.TabIndex = 27;
            this.txtServerPort.Text = "38888";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(6, 26);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(54, 13);
            this.lblServerIP.TabIndex = 26;
            this.lblServerIP.Text = "Server IP:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(73, 23);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(116, 20);
            this.txtServerIP.TabIndex = 25;
            this.txtServerIP.Text = "179.217.14.101";
            // 
            // btnConnectServer
            // 
            this.btnConnectServer.Location = new System.Drawing.Point(294, 21);
            this.btnConnectServer.Name = "btnConnectServer";
            this.btnConnectServer.Size = new System.Drawing.Size(75, 23);
            this.btnConnectServer.TabIndex = 24;
            this.btnConnectServer.Text = "Connect";
            this.btnConnectServer.UseVisualStyleBackColor = true;
            this.btnConnectServer.Click += new System.EventHandler(this.btnConnectServer_Click);
            // 
            // rtxTcpClientLog
            // 
            this.rtxTcpClientLog.BackColor = System.Drawing.Color.Black;
            this.rtxTcpClientLog.DetectUrls = false;
            this.rtxTcpClientLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtxTcpClientLog.ForeColor = System.Drawing.Color.Lime;
            this.rtxTcpClientLog.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.rtxTcpClientLog.Location = new System.Drawing.Point(9, 50);
            this.rtxTcpClientLog.Name = "rtxTcpClientLog";
            this.rtxTcpClientLog.ReadOnly = true;
            this.rtxTcpClientLog.Size = new System.Drawing.Size(367, 103);
            this.rtxTcpClientLog.TabIndex = 23;
            this.rtxTcpClientLog.Text = "";
            this.rtxTcpClientLog.TextChanged += new System.EventHandler(this.rtxTcpClientLog_TextChanged);
            // 
            // btnGetLastError
            // 
            this.btnGetLastError.Location = new System.Drawing.Point(6, 164);
            this.btnGetLastError.Name = "btnGetLastError";
            this.btnGetLastError.Size = new System.Drawing.Size(75, 23);
            this.btnGetLastError.TabIndex = 11;
            this.btnGetLastError.Text = "Last Error";
            this.btnGetLastError.UseVisualStyleBackColor = true;
            this.btnGetLastError.Click += new System.EventHandler(this.btnGetLastError_Click);
            // 
            // FrmTestToolKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grpTcpClient);
            this.Controls.Add(this.grpTcpServer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpModbus);
            this.Name = "FrmTestToolKit";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpModbus.ResumeLayout(false);
            this.grpModbus.PerformLayout();
            this.grpControl.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTcpServer.ResumeLayout(false);
            this.grpTcpServer.PerformLayout();
            this.grpTcpClient.ResumeLayout(false);
            this.grpTcpClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpModbus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnToolRZ;
        private System.Windows.Forms.Button btnToolRY;
        private System.Windows.Forms.Button btnToolRX;
        private System.Windows.Forms.Button btnToolZ;
        private System.Windows.Forms.Button btnToolY;
        private System.Windows.Forms.Button btnToolX;
        private System.Windows.Forms.Button btnRobotRZ;
        private System.Windows.Forms.Button btnRobotRY;
        private System.Windows.Forms.Button btnRobotRX;
        private System.Windows.Forms.Button btnRobotZ;
        private System.Windows.Forms.Button btnRobotY;
        private System.Windows.Forms.Button btnRobotX;
        private System.Windows.Forms.Button btnPermissionSts;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Button btnRunSts;
        private System.Windows.Forms.Button btnErrorSts;
        private System.Windows.Forms.Button btnPauseSts;
        private System.Windows.Forms.Button btnEditSts;
        private System.Windows.Forms.GroupBox grpControl;
        private System.ComponentModel.BackgroundWorker bgwUIThread;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMESLog;
        private System.Windows.Forms.RichTextBox rtxMESLog;
        private System.Windows.Forms.GroupBox grpTcpServer;
        private System.Windows.Forms.Label lblListenerIP;
        private System.Windows.Forms.TextBox txtListenerIP;
        private System.Windows.Forms.Button btnStartListener;
        private System.Windows.Forms.RichTextBox rtxTcpServerLog;
        private System.Windows.Forms.TextBox txtListenerPort;
        private System.Windows.Forms.GroupBox grpTcpClient;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Button btnConnectServer;
        private System.Windows.Forms.RichTextBox rtxTcpClientLog;
        private System.Windows.Forms.Button btnSendToServer;
        private System.Windows.Forms.TextBox txtClientMsg;
        private System.Windows.Forms.Button btnGetLastError;
    }
}

