namespace RVIS
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mns1 = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddRemoveUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpecialSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.ssTMRobot = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssMES = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.picProductLogo = new System.Windows.Forms.PictureBox();
            this.picCustomerLogo = new System.Windows.Forms.PictureBox();
            this.rtxUnitResult = new System.Windows.Forms.RichTextBox();
            this.rtxLog = new System.Windows.Forms.RichTextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpTestYield = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRateVal = new System.Windows.Forms.Label();
            this.lblPassVal = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalVal = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.picTestImg = new System.Windows.Forms.PictureBox();
            this.picMasterImg = new System.Windows.Forms.PictureBox();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOperatorID = new System.Windows.Forms.Label();
            this.lblOperatorIDVal = new System.Windows.Forms.Label();
            this.grpSystemLog = new System.Windows.Forms.GroupBox();
            this.sfdUnitData = new System.Windows.Forms.SaveFileDialog();
            this.mns1.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCustomerLogo)).BeginInit();
            this.grpTestYield.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTestImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMasterImg)).BeginInit();
            this.grpUser.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpSystemLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // mns1
            // 
            this.mns1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiConnection,
            this.tsmiTools,
            this.tsmiService});
            this.mns1.Location = new System.Drawing.Point(0, 0);
            this.mns1.Name = "mns1";
            this.mns1.Size = new System.Drawing.Size(1264, 24);
            this.mns1.TabIndex = 0;
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLogin,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "File";
            // 
            // tsmiLogin
            // 
            this.tsmiLogin.Name = "tsmiLogin";
            this.tsmiLogin.Size = new System.Drawing.Size(104, 22);
            this.tsmiLogin.Text = "Login";
            this.tsmiLogin.Click += new System.EventHandler(this.tsmiLogin_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(101, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(104, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiConnection
            // 
            this.tsmiConnection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConnect,
            this.tsmiDisconnect});
            this.tsmiConnection.Name = "tsmiConnection";
            this.tsmiConnection.Size = new System.Drawing.Size(81, 20);
            this.tsmiConnection.Text = "Connection";
            // 
            // tsmiConnect
            // 
            this.tsmiConnect.Name = "tsmiConnect";
            this.tsmiConnect.Size = new System.Drawing.Size(133, 22);
            this.tsmiConnect.Text = "Connect";
            this.tsmiConnect.Click += new System.EventHandler(this.tsmiConnect_Click);
            // 
            // tsmiDisconnect
            // 
            this.tsmiDisconnect.Name = "tsmiDisconnect";
            this.tsmiDisconnect.Size = new System.Drawing.Size(133, 22);
            this.tsmiDisconnect.Text = "Disconnect";
            this.tsmiDisconnect.Click += new System.EventHandler(this.tsmiDisconnect_Click);
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddRemoveUser,
            this.tsmiSettings});
            this.tsmiTools.Name = "tsmiTools";
            this.tsmiTools.Size = new System.Drawing.Size(48, 20);
            this.tsmiTools.Text = "Tools";
            // 
            // tsmiAddRemoveUser
            // 
            this.tsmiAddRemoveUser.Name = "tsmiAddRemoveUser";
            this.tsmiAddRemoveUser.Size = new System.Drawing.Size(170, 22);
            this.tsmiAddRemoveUser.Text = "Add/Remove User";
            this.tsmiAddRemoveUser.Click += new System.EventHandler(this.tsmiAddRemoveUser_Click);
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(170, 22);
            this.tsmiSettings.Text = "Settings";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // tsmiService
            // 
            this.tsmiService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSpecialSetting});
            this.tsmiService.Name = "tsmiService";
            this.tsmiService.Size = new System.Drawing.Size(56, 20);
            this.tsmiService.Text = "Service";
            // 
            // tsmiSpecialSetting
            // 
            this.tsmiSpecialSetting.Name = "tsmiSpecialSetting";
            this.tsmiSpecialSetting.Size = new System.Drawing.Size(151, 22);
            this.tsmiSpecialSetting.Text = "Special Setting";
            this.tsmiSpecialSetting.Click += new System.EventHandler(this.tsmiSpecialSetting_Click);
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssTMRobot,
            this.ssMES,
            this.ssDateTime});
            this.ssStatus.Location = new System.Drawing.Point(0, 657);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(1264, 24);
            this.ssStatus.TabIndex = 1;
            // 
            // ssTMRobot
            // 
            this.ssTMRobot.BackColor = System.Drawing.Color.Red;
            this.ssTMRobot.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssTMRobot.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.ssTMRobot.Name = "ssTMRobot";
            this.ssTMRobot.Size = new System.Drawing.Size(108, 19);
            this.ssTMRobot.Text = "TM-Robot: Offline";
            // 
            // ssMES
            // 
            this.ssMES.BackColor = System.Drawing.Color.Red;
            this.ssMES.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ssMES.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.ssMES.Name = "ssMES";
            this.ssMES.Size = new System.Drawing.Size(76, 19);
            this.ssMES.Text = "MES: Offline";
            // 
            // ssDateTime
            // 
            this.ssDateTime.Name = "ssDateTime";
            this.ssDateTime.Size = new System.Drawing.Size(1065, 19);
            this.ssDateTime.Spring = true;
            this.ssDateTime.Text = "Date Time";
            this.ssDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrClock
            // 
            this.tmrClock.Interval = 1000;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.picProductLogo);
            this.panel1.Controls.Add(this.picCustomerLogo);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 68);
            this.panel1.TabIndex = 4;
            // 
            // picProductLogo
            // 
            this.picProductLogo.Image = ((System.Drawing.Image)(resources.GetObject("picProductLogo.Image")));
            this.picProductLogo.Location = new System.Drawing.Point(242, 5);
            this.picProductLogo.Name = "picProductLogo";
            this.picProductLogo.Size = new System.Drawing.Size(360, 60);
            this.picProductLogo.TabIndex = 4;
            this.picProductLogo.TabStop = false;
            // 
            // picCustomerLogo
            // 
            this.picCustomerLogo.Image = ((System.Drawing.Image)(resources.GetObject("picCustomerLogo.Image")));
            this.picCustomerLogo.Location = new System.Drawing.Point(3, 3);
            this.picCustomerLogo.Name = "picCustomerLogo";
            this.picCustomerLogo.Size = new System.Drawing.Size(125, 60);
            this.picCustomerLogo.TabIndex = 3;
            this.picCustomerLogo.TabStop = false;
            // 
            // rtxUnitResult
            // 
            this.rtxUnitResult.AcceptsTab = true;
            this.rtxUnitResult.BackColor = System.Drawing.Color.Black;
            this.rtxUnitResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxUnitResult.ForeColor = System.Drawing.Color.White;
            this.rtxUnitResult.Location = new System.Drawing.Point(824, 128);
            this.rtxUnitResult.Name = "rtxUnitResult";
            this.rtxUnitResult.ReadOnly = true;
            this.rtxUnitResult.Size = new System.Drawing.Size(322, 526);
            this.rtxUnitResult.TabIndex = 5;
            this.rtxUnitResult.TabStop = false;
            this.rtxUnitResult.Text = "";
            this.rtxUnitResult.TextChanged += new System.EventHandler(this.rtxUnitResult_TextChanged);
            // 
            // rtxLog
            // 
            this.rtxLog.Location = new System.Drawing.Point(6, 19);
            this.rtxLog.Name = "rtxLog";
            this.rtxLog.ReadOnly = true;
            this.rtxLog.Size = new System.Drawing.Size(538, 176);
            this.rtxLog.TabIndex = 5;
            this.rtxLog.TabStop = false;
            this.rtxLog.Text = "";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(1152, 128);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 60);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(1152, 194);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 60);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.SystemColors.Control;
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResult.Location = new System.Drawing.Point(824, 27);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(428, 68);
            this.lblResult.TabIndex = 7;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Good sample:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(414, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "Current Unit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(820, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Result:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(1152, 593);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 60);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpTestYield
            // 
            this.grpTestYield.Controls.Add(this.tableLayoutPanel1);
            this.grpTestYield.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTestYield.Location = new System.Drawing.Point(568, 521);
            this.grpTestYield.Name = "grpTestYield";
            this.grpTestYield.Size = new System.Drawing.Size(250, 133);
            this.grpTestYield.TabIndex = 12;
            this.grpTestYield.TabStop = false;
            this.grpTestYield.Text = "Test Yield:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel1.Controls.Add(this.lblRateVal, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPassVal, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblRate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalVal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPass, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblRateVal
            // 
            this.lblRateVal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRateVal.AutoSize = true;
            this.lblRateVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRateVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRateVal.Location = new System.Drawing.Point(111, 66);
            this.lblRateVal.Name = "lblRateVal";
            this.lblRateVal.Size = new System.Drawing.Size(124, 34);
            this.lblRateVal.TabIndex = 20;
            this.lblRateVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPassVal
            // 
            this.lblPassVal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassVal.AutoSize = true;
            this.lblPassVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPassVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassVal.Location = new System.Drawing.Point(111, 33);
            this.lblPassVal.Name = "lblPassVal";
            this.lblPassVal.Size = new System.Drawing.Size(124, 33);
            this.lblPassVal.TabIndex = 19;
            this.lblPassVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRate
            // 
            this.lblRate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.Location = new System.Drawing.Point(3, 66);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(102, 34);
            this.lblRate.TabIndex = 18;
            this.lblRate.Text = "Rate:";
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(3, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(102, 33);
            this.lblTotal.TabIndex = 17;
            this.lblTotal.Text = "Total:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalVal
            // 
            this.lblTotalVal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalVal.AutoSize = true;
            this.lblTotalVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVal.Location = new System.Drawing.Point(111, 0);
            this.lblTotalVal.Name = "lblTotalVal";
            this.lblTotalVal.Size = new System.Drawing.Size(124, 33);
            this.lblTotalVal.TabIndex = 16;
            this.lblTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPass
            // 
            this.lblPass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.Location = new System.Drawing.Point(3, 33);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(102, 33);
            this.lblPass.TabIndex = 15;
            this.lblPass.Text = "Passed:";
            this.lblPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picTestImg
            // 
            this.picTestImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picTestImg.Location = new System.Drawing.Point(418, 128);
            this.picTestImg.Name = "picTestImg";
            this.picTestImg.Size = new System.Drawing.Size(400, 300);
            this.picTestImg.TabIndex = 2;
            this.picTestImg.TabStop = false;
            // 
            // picMasterImg
            // 
            this.picMasterImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picMasterImg.Location = new System.Drawing.Point(12, 128);
            this.picMasterImg.Name = "picMasterImg";
            this.picMasterImg.Size = new System.Drawing.Size(400, 300);
            this.picMasterImg.TabIndex = 2;
            this.picMasterImg.TabStop = false;
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.tableLayoutPanel2);
            this.grpUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUser.Location = new System.Drawing.Point(568, 448);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(250, 67);
            this.grpUser.TabIndex = 13;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "User:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel2.Controls.Add(this.lblOperatorID, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblOperatorIDVal, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(238, 32);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblOperatorID
            // 
            this.lblOperatorID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperatorID.AutoSize = true;
            this.lblOperatorID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperatorID.Location = new System.Drawing.Point(3, 0);
            this.lblOperatorID.Name = "lblOperatorID";
            this.lblOperatorID.Size = new System.Drawing.Size(102, 32);
            this.lblOperatorID.TabIndex = 15;
            this.lblOperatorID.Text = "Operator ID:";
            this.lblOperatorID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOperatorIDVal
            // 
            this.lblOperatorIDVal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperatorIDVal.AutoSize = true;
            this.lblOperatorIDVal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOperatorIDVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperatorIDVal.Location = new System.Drawing.Point(111, 0);
            this.lblOperatorIDVal.Name = "lblOperatorIDVal";
            this.lblOperatorIDVal.Size = new System.Drawing.Size(124, 32);
            this.lblOperatorIDVal.TabIndex = 19;
            this.lblOperatorIDVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpSystemLog
            // 
            this.grpSystemLog.Controls.Add(this.rtxLog);
            this.grpSystemLog.Location = new System.Drawing.Point(12, 448);
            this.grpSystemLog.Name = "grpSystemLog";
            this.grpSystemLog.Size = new System.Drawing.Size(550, 205);
            this.grpSystemLog.TabIndex = 14;
            this.grpSystemLog.TabStop = false;
            this.grpSystemLog.Text = "System Log:";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.grpSystemLog);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.grpTestYield);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.rtxUnitResult);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picTestImg);
            this.Controls.Add(this.picMasterImg);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.mns1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mns1;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robotic Vision Inspection System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.mns1.ResumeLayout(false);
            this.mns1.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProductLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCustomerLogo)).EndInit();
            this.grpTestYield.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTestImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMasterImg)).EndInit();
            this.grpUser.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpSystemLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mns1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnection;
        private System.Windows.Forms.ToolStripMenuItem tsmiTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisconnect;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.Timer tmrClock;
        private System.Windows.Forms.ToolStripStatusLabel ssTMRobot;
        private System.Windows.Forms.ToolStripStatusLabel ssDateTime;
        private System.Windows.Forms.PictureBox picMasterImg;
        private System.Windows.Forms.PictureBox picTestImg;
        private System.Windows.Forms.PictureBox picCustomerLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picProductLogo;
        private System.Windows.Forms.RichTextBox rtxUnitResult;
        private System.Windows.Forms.RichTextBox rtxLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpTestYield;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblRateVal;
        private System.Windows.Forms.Label lblPassVal;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalVal;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblOperatorID;
        private System.Windows.Forms.Label lblOperatorIDVal;
        private System.Windows.Forms.ToolStripStatusLabel ssMES;
        private System.Windows.Forms.GroupBox grpSystemLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRemoveUser;
        private System.Windows.Forms.SaveFileDialog sfdUnitData;
        private System.Windows.Forms.ToolStripMenuItem tsmiService;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpecialSetting;
    }
}

