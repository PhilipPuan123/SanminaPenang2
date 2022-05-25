using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using System.IO;
//using MesIF;
using TcpIF;
using TMModbusIF;
using RVISMMC;
using RVISData;
using System.Collections;
using SystemLog;
using System.Threading;


namespace RVIS
{
    public partial class FrmMain : Form
    {
        #region Declaration
        #region Constant
        private const int MESLOG_MAX_LINES = 100;
        private const string LINE_SEPARATOR = "***********************************************************";
        #endregion Constant

        private enum OverallStatus
        {
            ERROR = 0,
            READY,
            TESTING,
            PASS,
            FAIL,
            LOADING
        }

        /* Form Title */
        private static AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute));
        private static Version appversion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
        private string MAIN_TITLE = titleAttribute.Title + " V" + appversion;
        /* Test Data */
        //private
        public static DateTime resetTime;
        private bool isInspecting           = false;    // flag for inspection in progress
        private bool isLogOffNeeded         = false;    // flag for required to log-off 
        private bool isResetTestYieldNeeded = false;    // flag for required to reset test yield
        private CONNECTION_STS tmConnectionSts = CONNECTION_STS.Disconnected;
        /* Classes */
        private RVISML rvisMMC = new RVISML();
        FileLogger fileLog = new FileLogger();
        OverallSts finalSts = new OverallSts();
        private int resultNum = 0;
        double barValue = 0.0;
        private bool printOverallSts = true;
        private bool stsReporting = true;
        public bool sysWaitFlag { get; set; }
        /* Data */
        BitArray lightDigitalOutputSts;
        #endregion Declaration

        #region Form Controls
        public FrmMain()
        {
            InitializeComponent();
            UpgradeConfigFile();
            /* Enable key preview to track Alt+F4 to prevent form closing */
            this.KeyPreview = true;
            DataUtility.UpdateSettingDataFromConfig();
            DataUtility.UpdateSpecialSettingDataFromConfig();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = MAIN_TITLE;
            InitializeTmrClock();
            ShowDateTime();
            EnableAccess(UserData.UserAccess);
            /* Start backgroundWorker */
            InitializeBackgroundWorker();
            bgwUIThread.RunWorkerAsync();
            RVISData.GuiDataExchange.prjStillrunSts = false;
            Log.SaveLogFlag = false; // reset flag whether to save log file.
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAllConnectionAndBackgroundTask();

            if (bgwUIThread.WorkerSupportsCancellation)
            {
                this.bgwUIThread.CancelAsync();
            }
            Log.IsSaveLog(); //Initiate save file or not based on error flag
        }
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            /* Disable Alt+F4 to close form */
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
            }
        }

        #region Form Controls-Overall Status
        private void SetOverallStatus(OverallStatus status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<OverallStatus>(SetOverallStatus), new object[] { status });
            }
            else
            {
                string dispTxt = "";
                Color backColor = Color.Orange;
                Color fontColor = Color.Black;
                switch (status)
                {
                    case OverallStatus.READY:
                        dispTxt = "READY";
                        backColor = Color.Gold;
                        break;
                    case OverallStatus.PASS:
                        dispTxt = "PASS";
                        backColor = Color.Lime;
                        break;
                    case OverallStatus.FAIL:
                        dispTxt = "FAIL";
                        backColor = Color.Red;
                        break;
                    case OverallStatus.TESTING:
                        dispTxt = "TESTING";
                        backColor = Color.Yellow;
                        break;
                    //A-0023 s
                    case OverallStatus.LOADING:
                        dispTxt = "LOADING....";
                        backColor = Color.Cyan;
                        break;
                    //A-0023 e
                    case OverallStatus.ERROR:
                    default:
                        dispTxt = "ERROR";
                        backColor = Color.Red;
                        fontColor = Color.White;
                        break;
                }

                lblResult.Text = dispTxt;
                lblResult.BackColor = backColor;
                lblResult.ForeColor = fontColor;
            }
        }
        #endregion

        #region Form Controls-Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (sysWaitFlag == false)
            {
                MAIN_START();
                ModbusControl.SetControlBox_DO2(true);
            }
            else
            {
                MessageBox.Show("System is not yet ready. Please wait.");
            }
        }
        public void MAIN_START()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(MAIN_START), new object[] { });
            }
            else
            {
                if (RVISData.GuiDataExchange.prjStillrunSts == false)
                {
                    if (isResetTestYieldNeeded == false && isLogOffNeeded == false)
                    {
                    /* Set overall status */
                    SetOverallStatus(OverallStatus.TESTING);
                    /* Start new unit test result */
                    StartNewUnitResult();
                    rvisMMC.OnSerialResultPub -= RvisMMC_OnSerialResultPub;
                    rvisMMC.OnResultStringPub -= RvisMMC_OnResultStringPub;
                    rvisMMC.OnFinishInspecPub -= RvisMMC_OnFinishInspecPub;
                    rvisMMC.OnCoverResultPub -= RvisMMC_OnCoverResultPub;
                    rvisMMC.OnRobotRunStatusPub -= RvisMMC_OnRobotRunStatusPub;
                    rvisMMC.OnRobotPauseStatusPub -= RvisMMC_OnRobotPauseStatusPub;
                    rvisMMC.OnRobotErrStatusPub -= RvisMMC_OnRobotErrStatusPub;
                    rvisMMC.OnMESConduitResponsePub -= RvisMMC_OnMESConduitResponsePub;
                    rvisMMC.OnMESMeasurementResponsePub -= RvisMMC_OnMESMeasurementResponsePub;
                    rvisMMC.OnMESSerialSubmitPub -= RvisMMC_OnMESSerialSubmitPub;
                    /* Subscribe to MMC event */
                    rvisMMC.OnSerialResultPub += RvisMMC_OnSerialResultPub;
                    rvisMMC.OnResultStringPub += RvisMMC_OnResultStringPub;
                    rvisMMC.OnFinishInspecPub += RvisMMC_OnFinishInspecPub;
                    rvisMMC.OnCoverResultPub += RvisMMC_OnCoverResultPub;
                    rvisMMC.OnMESConduitResponsePub += RvisMMC_OnMESConduitResponsePub;
                    rvisMMC.OnMESMeasurementResponsePub += RvisMMC_OnMESMeasurementResponsePub;
                    rvisMMC.OnMESSerialSubmitPub += RvisMMC_OnMESSerialSubmitPub;
                    /* Start MMC */
                    rvisMMC.Start();
                    isInspecting = true;
                    RVISData.GuiDataExchange.prjStillrunSts = true;
                    fileLog.Logging("System start inspection");
                    PrintToSystemLog("Checking cover");
                    progressBarSts.Value = 0;
                    lblProgressBar.Text = "0%";
                    resultNum = 0;
                  }
                  else
                  {
                   if(isResetTestYieldNeeded)
                   {
                    ResetTestYieldData();
                    isResetTestYieldNeeded = false;
                   }
                   if(isLogOffNeeded)
                   {
                     LoadLoginForm(null, null);
                      isLogOffNeeded = false;
                   }
                  }
                }
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            ProjectStop();
        }
        public void ProjectStop()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ProjectStop), new object[] { });
            }
            else
            {
                /* Set overall status */
                SetOverallStatus(OverallStatus.ERROR);
                /* Subscribe to MMC event */
                rvisMMC.OnSerialResultPub -= RvisMMC_OnSerialResultPub;
                rvisMMC.OnResultStringPub -= RvisMMC_OnResultStringPub;
                rvisMMC.OnFinishInspecPub -= RvisMMC_OnFinishInspecPub;
                rvisMMC.OnRobotRunStatusPub -= RvisMMC_OnRobotRunStatusPub;
                rvisMMC.OnRobotPauseStatusPub -= RvisMMC_OnRobotPauseStatusPub;
                rvisMMC.OnRobotErrStatusPub -= RvisMMC_OnRobotErrStatusPub;
                rvisMMC.OnRobotStopStatusPub -= RvisMMC_OnRobotStopStatusPub;
                rvisMMC.OnSystemErrStatusPub -= RvisMMC_OnSystemErrStatusPub;
                rvisMMC.OnSystemReadyStartStatusPub -= RvisMMC_OnSystemReadyStartStatusPub;
                rvisMMC.OnMESConduitResponsePub -= RvisMMC_OnMESConduitResponsePub;
                rvisMMC.OnMESMeasurementResponsePub -= RvisMMC_OnMESMeasurementResponsePub;
                rvisMMC.OnMESSerialSubmitPub -= RvisMMC_OnMESSerialSubmitPub;
                rvisMMC.Stop();
                isInspecting = false;
                RVISData.GuiDataExchange.prjStillrunSts = false;
                StopAllConnectionAndBackgroundTask();
                /* Disable button */
                tsmiConnect.Enabled = true;
                tsmiDisconnect.Enabled = false;
            }
        }
        public void ProjectHalt()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ProjectHalt), new object[] { });
            }
            else
            {
                RVISData.GuiDataExchange.prjStillrunSts = false; //to enable later start again
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            /* Set save file dialog settings */
            sfdUnitData.OverwritePrompt = true;
            sfdUnitData.FileName = "TestData";
            sfdUnitData.Filter = "Text files (*.txt)|*.txt|Rich Text Format files|*.rtf";
            sfdUnitData.Title = "Save Test Data";
            sfdUnitData.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfdUnitData.RestoreDirectory = false;
            /* Show dialog */
            DialogResult result = sfdUnitData.ShowDialog();

            /* If Save button is pressed and filename is not empty */
            if (result == DialogResult.OK && sfdUnitData.FileName.Length > 0)
            {
                /* Save test data to selected file format */
                switch (sfdUnitData.FilterIndex)
                {
                    case 1:     // *.txt
                        rtxUnitResult.SaveFile(sfdUnitData.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 2:     // *.rtf
                        rtxUnitResult.SaveFile(sfdUnitData.FileName);
                        break;
                }
            }
        }
        private void btnLighting_Click(object sender, EventArgs e)
        {
            //** Note : DO5-ON + DO4-OFF (Light OFF) , DO5-OFF + DO4-ON (Light ON), DO5-OFF + DO4-OFF (Light control by PLC door)
            ModbusControl.GetControlBoxAllDigitalOutputs(ref lightDigitalOutputSts);
            if((lightDigitalOutputSts[5] == true)&&(lightDigitalOutputSts[4] == false)) //if lighting is off
            {
                ModbusControl.SetControlBox_DO4(true); //lighting is on
                ModbusControl.SetControlBox_DO5(false); 
                btnLighting.Text = "ON";
                btnLighting.BackColor = Color.Green;
            }
            else if ((lightDigitalOutputSts[5] == false)&&(lightDigitalOutputSts[4] == true))
            {
                ModbusControl.SetControlBox_DO4(false);
                ModbusControl.SetControlBox_DO5(true); //lighting is off
                btnLighting.Text = "OFF";
                btnLighting.BackColor = Color.Empty;
            }

        }
        private void chckBoxLight_CheckedChanged(object sender, EventArgs e)
        {
            if(btnLighting.Enabled == true)
            {
                ModbusControl.SetControlBox_DO4(false);
                ModbusControl.SetControlBox_DO5(false);
                btnLighting.Enabled = false;
                btnLighting.Text = "OFF";
                btnLighting.BackColor = Color.Empty;
            }
            else
            {
                btnLighting.Enabled = true;
                ModbusControl.SetControlBox_DO4(false);
                ModbusControl.SetControlBox_DO5(true);
            }
        }
        #endregion Form Controls-Buttons

        #region Form Controls-BackgroundWorker
        private void InitializeBackgroundWorker()
        {
            /* UI Thread */
            bgwUIThread.DoWork += BgwUIThread_DoWork;
            bgwUIThread.ProgressChanged += BgwUIThread_ProgressChanged;
            bgwUIThread.RunWorkerCompleted += BgwUIThread_RunWorkerCompleted;
            bgwUIThread.WorkerReportsProgress = true;
            bgwUIThread.WorkerSupportsCancellation = false;
        }

        private void BgwUIThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (bgwUIThread.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    /* Check TM Connection status */
                    if (tmConnectionSts == CONNECTION_STS.Connected &&
                        IsTMConnected() != true)
                    {
                        tmConnectionSts = CONNECTION_STS.Disconnected;
                    }
                    worker.ReportProgress(0);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
        private void BgwUIThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateDisplay();
        }
        private void BgwUIThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                //Do nothing
            }
            else
            {
                //Do nothing
            }
        }
        #endregion Form Controls-BackgroundWorker

        #region Form Controls-Timer
        private void InitializeTmrClock()
        {
            tmrClock.Enabled = true;
            tmrClock.Interval = 1000;
        }
        private void tmrClock_Tick(object sender, EventArgs e)
        {
            /* Check if need to logoff and reset test yield */
            if (ShouldResetNow())
            {
                isLogOffNeeded = true;
                isResetTestYieldNeeded = true;
            }
            /* Update current time on status strip */
            ShowDateTime();
        }
        private bool ShouldResetNow()
        {
            DateTime now = DateTime.Now;
            if(now > resetTime)
            {
                return true;
            }
            return false;
        }
        private void SetResetTime()
        {
            DateTime now = DateTime.Now;
            isLogOffNeeded = false;
            isResetTestYieldNeeded = false;
            switch (SettingData.DataResetFreq)
            {
                case "Hourly":
                    resetTime = now.AddHours(1);
                    break;
                case "Daily":
                default:
                    resetTime = now.AddDays(1);
                    break;
            }
        }
        #endregion Form Controls-Timer

        #region Form Controls-MenuStrips
        #region MenuStrips-File
        private void tsmiLogin_Click(object sender, EventArgs e)
        {
            LoadLoginForm(sender, e);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion MenuStrips-File

        #region MenuStrips-Connection
        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            /* Set status strip to Connecting */
            tmConnectionSts = CONNECTION_STS.Connecting;
            Task.Run(() => { StartConnectionTask(); });
        }

        private void StartConnectionTask()
        {
            string serverIP     = SettingData.PcServerIP;
            string serverPort   = SettingData.PcServerPort;
            string tmIP         = SettingData.TmIP;
            string tmModbusPort = SettingData.TmModbusPort;
            int listenerError, modbusError;
            /* Start Listerner */
            listenerError = StartListener(serverIP, serverPort);
            if (listenerError != 0)
            {
                MessageBox.Show("ErrorCode: " + listenerError,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            /* Connect TM Modbus */
            modbusError = ModbusControl.Connect(tmIP, tmModbusPort);
            if (modbusError != 0)
            {
                MessageBox.Show("ErrorCode: " + modbusError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            /* If error */
            if (listenerError != 0 || modbusError != 0)
            {
                StopAllConnectionAndBackgroundTask();
                /* Set status strip to Disconnected */
                tmConnectionSts = CONNECTION_STS.Disconnected;
                 PrintToSystemLog("Unable to establish connection with system. Please check connection");
                 SystemLog.Log.SaveLogFlag = true;
                 fileLog.Logging("Unable to establish connection with system. Please check connection"); //write to log file
            }
            else
            {
                /* Start background task for status checking */
                Task.Factory.StartNew(async () => { await rvisMMC.BackgroundSysCheck(); }, TaskCreationOptions.LongRunning);
                /* Set status strip to Connected */
                tmConnectionSts = CONNECTION_STS.Connected;
                ModbusControl.StartOrPauseProject();
                /* Start MMC */
                rvisMMC.Start();
                PrintToSystemLog("System Successfully Connected");
                fileLog.Logging("System Successfully Connected");
                rvisMMC.OnPhysicalStartButtonPub += RvisMMC_OnPhysicalStartButtonPub;
                rvisMMC.OnRobotRunStatusPub += RvisMMC_OnRobotRunStatusPub;
                rvisMMC.OnRobotPauseStatusPub += RvisMMC_OnRobotPauseStatusPub;
                rvisMMC.OnRobotErrStatusPub += RvisMMC_OnRobotErrStatusPub;
                rvisMMC.OnRobotStopStatusPub += RvisMMC_OnRobotStopStatusPub;
                rvisMMC.OnSystemErrStatusPub += RvisMMC_OnSystemErrStatusPub;
                rvisMMC.OnSystemReadyStartStatusPub += RvisMMC_OnSystemReadyStartStatusPub;
                sysWaitFlag = true;
                SetOverallStatus(OverallStatus.LOADING);
                PrintToSystemLog("Initializing test sequence");
            }
        }
        private void tsmiDisconnect_Click(object sender, EventArgs e)
        {
            StopAllConnectionAndBackgroundTask();
            /* Disable button */
            tsmiConnect.Enabled = true;
            tsmiDisconnect.Enabled = false;
        }
        #endregion MenuStrips-Connection

        #region MenuStrips-Tools
        private void tsmiAddRemoveUser_Click(object sender, EventArgs e)
        {
            FrmAddRemoveUser frmAddRemoveUser = new FrmAddRemoveUser();
            frmAddRemoveUser.ShowDialog();
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            FrmSetting frmSetting = new FrmSetting();
            frmSetting.ShowDialog();
        }
        #endregion MenuStrips-Tools

        #region MenuStrips-Service
        private void tsmiSpecialSetting_Click(object sender, EventArgs e)
        {
            FrmSpecialSetting frmSpecialSetting = new FrmSpecialSetting();
            frmSpecialSetting.ShowDialog();
        }
        #endregion MenuStrips-Service
        #endregion Form Controls-MenuStrips

        #region StatusStrip
        /// <summary>Update date and time on UI.</summary>
        private void ShowDateTime()
        {
            ssDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy  hh:mm:ss tt");
        }
        #endregion StatusStrip

        #region RichTextBox
        private void rtxUnitResult_TextChanged(object sender, EventArgs e)
        {
            SetCursorToEnd(sender, EventArgs.Empty);
        }
        #endregion RichTextBox
        #endregion Form Controls

        #region Form Function
        private void LoadLoginForm(object sender, EventArgs e)
        {
            rvisMMC.OnMESMeasurementResponsePub += RvisMMC_OnMESMeasurementResponsePub;
            using (FrmLogin frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    UserData.ID = frmLogin.UserID;
                    UserData.Password = frmLogin.Password;
                    UserData.UserAccess = frmLogin.UserAccess;
                    PrintToSystemLog("Login Successful");
                    fileLog.Logging("Login Successful");
                }
                lblOperatorIDVal.Text = UserData.ID;
                EnableAccess(UserData.UserAccess);
                SetResetTime();
            }
        }

        private void EnableAccess(AccessLevel accessLevel)
        {
            switch (accessLevel)
            {
                case AccessLevel.Service:
                    tsmiTools.Visible = true;
                    tsmiService.Visible = true;
                    lblOperatorIDVal.BackColor = Color.Yellow;
                    //A-0031 s
                    lightingGroup.Visible = true;
                    //A-0031 e
                    break;
                case AccessLevel.Admin:
                    tsmiTools.Visible = true;
                    tsmiService.Visible = false;
                    lblOperatorIDVal.BackColor = Color.Cyan;
                    //A-0031 s
                    lightingGroup.Visible = true;
                    //A-0031 e
                    break;
                case AccessLevel.User:
                default:
                    tsmiTools.Visible = false;
                    tsmiService.Visible = false;
                    lblOperatorIDVal.BackColor = SystemColors.Control;
                    //A-0031 s
                    lightingGroup.Visible = false;
                    //A-0031 e
                    break;
            }
        }
        
        #region Update Display
        private void UpdateDisplay()
        {
            /* File */
            UpdateFileMenu();
            /* Connection */
            UpdateConnectionMenu();
            /* Tools */
            UpdateToolsMenu();
            /* Service */
            UpdateServiceMenu();
            /* Buttons */
            UpdateStartButton();
            UpdateStopButton();
            UpdateSaveButton();
            //A-0001 insert s dominik add lighting button control 10-02-2019
            UpdateLightingControlButton();
            //insert e dominik add lighting button control 10-02-2019
            /* Test Yield */
            UpdateTestYieldData();
            /* Status strip */
            UpdateStatusStrip();
            /* Progress bar*/
            UpdateProgressBar();
        }
        private void UpdateProgressBar()
        {
            progressBarSts.Value = resultNum;
            barValue = resultNum;
            barValue = (barValue / 34) * 100;
            barValue = Math.Round(barValue, 2);
            //lblProgressBar.Text = (resultNum).ToString() + "/33";
            lblProgressBar.Text = (barValue).ToString() + "%";
        }

        #region Update Display-File
        private void UpdateFileMenu()
        {
            if (tmConnectionSts != CONNECTION_STS.Connecting)
            {
                tsmiFile.Enabled = true;
                UpdateLoginButton();
                UpdateExitButton();
            }
            else
            {
                tsmiFile.Enabled = false;
            }
        }

        private void UpdateLoginButton()
        {
            /* Conditions to enable button */
            if (isInspecting == false)
            {
                tsmiLogin.Enabled = true;
            }
            else
            {
                tsmiLogin.Enabled = false;
            }
        }

        private void UpdateExitButton()
        {
            /* Conditions to enable button */
            if (isInspecting == false)
            {
                tsmiExit.Enabled = true;
            }
            else
            {
                tsmiExit.Enabled = false;
            }
        }
        #endregion Update Display-File

        #region Update Display-Connection
        private void UpdateConnectionMenu()
        {
            /* Conditions to enable button */
            if (IsLoggedIn() == true    &&
                isInspecting == false)
            {
                tsmiConnection.Enabled = true;

                UpdateConnectButton();
                UpdateDisconnectButton();
            }
            else
            {
                tsmiConnection.Enabled = false;
            }
        }

        private void UpdateConnectButton()
        {
            /* Conditions to enable button */
            if (tmConnectionSts == CONNECTION_STS.Disconnected  &&
                IsLoggedIn()    == true                         &&
                IsTMConnected() == false                        &&
                isInspecting    == false)
            {
                tsmiConnect.Enabled = true;
            }
            else
            {
                tsmiConnect.Enabled = false;
            }
        }

        private void UpdateDisconnectButton()
        {
            /* Conditions to enable button */
            if(IsLoggedIn()     == true &&
                IsTMConnected() == true &&
                isInspecting    == false)
            {
                tsmiDisconnect.Enabled = true;
            }
            else
            {
                tsmiDisconnect.Enabled = false;
            }
        }
        #endregion Update Display-Connection

        #region Update Display-Tools
        private void UpdateToolsMenu()
        {
            /* Conditions to enable button */
            if (tmConnectionSts != CONNECTION_STS.Connecting    &&
                IsLoggedIn()    == true                         &&
                IsTMConnected() == false                        && 
                isInspecting    == false)
            {
                tsmiTools.Enabled = true;
            }
            else
            {
                tsmiTools.Enabled = false;
            }
        }
        #endregion UpdateDisplay-Tools

        #region Update Display-Service
        private void UpdateServiceMenu()
        {
            if (tmConnectionSts != CONNECTION_STS.Connecting    &&
                IsTMConnected() == false                        &&
                isInspecting == false)
            {
                tsmiService.Enabled = true;
            }
            else
            {
                tsmiService.Enabled = false;
            }
        }
        #endregion Update Display-Service

        #region Update Display-Buttons
        private void UpdateStartButton()
        {
            /* Conditions to enable button */
            if (IsLoggedIn()    == true     &&
                IsTMConnected() == true     &&
                isInspecting    == false)
            {
                btnStart.Enabled = true;
            }
            else
            {
                btnStart.Enabled = true;
            }
        }

        private void UpdateStopButton()
        {
            /* Conditions to enable button */
            if (isInspecting == true)
            {
                btnStop.Enabled = true;
            }
            else
            {
                btnStop.Enabled = true;
            }
        }

        private void UpdateSaveButton()
        {
            /* Conditions to enable button */
            if (IsLoggedIn()    == true     &&
                isInspecting    == false    &&
                rtxUnitResult.TextLength > 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private void UpdateLightingControlButton()
        {
            if (IsLoggedIn() == true && IsTMConnected() == true )
            {
                lightingGroup.Enabled = true;
            }
            else
            {
                lightingGroup.Enabled = false;
            }
        }
        #endregion Update Display-Buttons

        #region Update Display-Status Strips
        private void UpdateStatusStrip()
        {
            UpdateTMConnectionStatus(tmConnectionSts);
            Update42QConnectionStatus();
        }
        #endregion Update Display-Status Strips
        
        private bool IsLoggedIn()
        {
            if (String.IsNullOrEmpty(UserData.ID))
            {
                return false;
            }
            return true;
        }

        private bool IsTMConnected()
        {
            if (!rvisMMC.tcpServer.IsRunning || !ModbusControl.IsRunning)
            {
                return false;
            }
            return true;
        }
        #endregion Update Display

        #region Form Function-RichTextBox
        private void StartNewUnitResult()
        {
            string line;

            /* Clear unit result */
            rtxUnitResult.Clear();
            /* Add line separator to unit result */
            AddLineToUnitResult(LINE_SEPARATOR);
            /* Add Start Timestamp to unit result */
            GuiDataExchange.startTime = DateTime.Now;
            //line = "Start Timestamp\t: " + GuiDataExchange.startTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            line = "Start Timestamp\t: " + GuiDataExchange.startTime.ToString("dd-MMM-yyyy HH:mm:ss.fff");
            AddLineToUnitResult(line);
            /* Add Operator ID to unit result */
            line = "Operator ID\t: " + UserData.ID;
            AddLineToUnitResult(line);
        }

        private void SetUnitResultFooter()
        {
            string dutSts;
            if(printOverallSts == true)
            {
                dutSts = "PASS";
            }
            else
            {
                dutSts = "FAIL";
            }
            string line;

            /* Add line separator to unit result */
            AddLineToUnitResult(LINE_SEPARATOR);
            /* Add Stop Timestamp to unit result */
            //line = "Stop Timestamp\t: " + GuiDataExchange.stopTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            line = "Stop Timestamp\t: " + GuiDataExchange.stopTime.ToString("dd-MMM-yyyy HH:mm:ss.fff");
            line += "\n" + "Cycle Time\t: " + CalculateCycleTimeInString(GuiDataExchange.startTime, GuiDataExchange.stopTime);
            line += "\n\n" + "OVERALL INSPECTION RESULT\t: " + dutSts;
            AddLineToUnitResult(line);
            /* Add line separator to unit result */
            AddLineToUnitResult(LINE_SEPARATOR);
        }
        private void AutoSaveResult()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(AutoSaveResult), new object[] { });
            }
            else
            {
                rtxUnitResult.SaveFile(@"C:\RVIS\Zip\log.txt", RichTextBoxStreamType.PlainText);
            }
        }
        private void AddLineToUnitResult(string line, bool isTestData = false)
        {
            if (rtxUnitResult.InvokeRequired)
            {
                rtxUnitResult.Invoke(new Action<string, bool>(AddLineToUnitResult), new object[] { line, isTestData });
            }
            else
            {
                if (isTestData) rtxUnitResult.SelectionTabs = new int[] { 250, 500, 750, 1000 };
                else rtxUnitResult.SelectionTabs = new int[] { 50, 100, 150, 200 };

                AddLine(rtxUnitResult, line);
            }
        }

        private void AddLine(RichTextBox richTextBox, string line)
        {
            /* Remove empty char in string */
            string text = line.TrimEnd('\0');
            /* Set data with "PASS" keyword to Green color */
            if (line.Contains("PASS")) richTextBox.SelectionColor = Color.Lime;
            /* Set data with "FAIL" keyword to Red color */
            else if (line.Contains("FAIL")) richTextBox.SelectionColor = Color.Red;
            /* Set data without "PASS" & "FAIL" keyword to Lightblue color */
            else richTextBox.SelectionColor = Color.LightBlue;
            richTextBox.AppendText(line + "\r\n");
        }
        #endregion Form Function-RichTextBox


        #region Form Function-Image Load
        private void ShowImages(string masterImgDir, string currentImgDir)
        {
            Image masterImg = GetImage(masterImgDir);
            Image currentImg = GetImage(currentImgDir);

            /* Show master image */
            picMasterImg.Image = masterImg;
            picMasterImg.SizeMode = PictureBoxSizeMode.StretchImage;
            /* Show current test image */
            picTestImg.Image = currentImg;
            picTestImg.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>Get image from directory and release the resource.</summary>
        /// <param name="imgDir">Image directory including filename</param>
        /// <returns></returns>
        private Image GetImage(string imgDir)
        {
            using(Image img = Image.FromFile(imgDir))
            {
                Bitmap bmp = new Bitmap(img);
                return bmp;
            }
        }
        #endregion Form Function-Image Load

        private void UpdateTMConnectionStatus(CONNECTION_STS status)
        {
            string stsText = "";
            Color backColor = Color.Red;
            
            switch (status)
            {
                case CONNECTION_STS.Connected:
                    stsText += "Connected";
                    backColor = Color.Lime;
                    break;
                case CONNECTION_STS.Connecting:
                    stsText += "Connecting";
                    backColor = Color.Yellow;
                    break;
                case CONNECTION_STS.Disconnected:
                default:
                    stsText += "Disconnected";
                    backColor = Color.Red;
                    break;
            }

            ssTMRobot.Text = "TM-Robot: " + stsText;
            ssTMRobot.BackColor = backColor;
        }
        private void Update42QConnectionStatus()
        {
            string sts = "";
            Color backColor = Color.Red;
            if(RVISData.SettingData.mesController == "ON")
            {
                sts += "Connected";
                backColor = Color.Lime;
            }
            else
            {
                sts += "Disconnected";
                backColor = Color.Red;
            }
            ssMES.Text = "42Q: " + sts;
            ssMES.BackColor = backColor;
        }
        #endregion Form Function

        #region Functions
        /// <summary>Upgrade config file if necessary.</summary>
        private void UpgradeConfigFile()
        {
            if (Properties.Settings.Default.NeedUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.NeedUpgrade = false;
            }
        }

        private void SetCursorToEnd(object sender, EventArgs e)
        {
            RichTextBox rtx = sender as RichTextBox;
            /* set the current caret position to the end */
            rtx.SelectionStart = rtx.Text.Length;
            /* scroll to it */
            rtx.ScrollToCaret();
        }

        /// <summary>Get the time difference between start and stop time.</summary>
        private string CalculateCycleTimeInString(DateTime startTime, DateTime stopTime)
        {
            TimeSpan duration = stopTime - startTime;
            string cycleTime = "";
            /* If stop time is smaller than start time */
            if (stopTime < startTime) return "Invalid start/stop time";

            if (duration.TotalDays >= 1)
            {
                cycleTime += duration.Days + " day ";
            }
            if (duration.TotalHours >= 1)
            {
                cycleTime += duration.Hours + " hr ";
            }
            cycleTime += duration.Minutes + " min ";
            cycleTime += duration.Seconds + " sec";

            return cycleTime;
        }

        private void StopAllConnectionAndBackgroundTask()
        {
            /* End background task for status checking */
            rvisMMC.EndAllServices();

            /* Disconnect TCP Server */
            StopListener();

            /* Disconnect TM Modbus */
            /* Stop Modbus */
            if (ModbusControl.IsRunning)
            {
                ModbusControl.Disconnect();
            }
        }
        #endregion Functions

        #region Test Yield
        private void ResetTestYieldData()
        {
            TestYieldData.TotalTestedUnits = 0;
            TestYieldData.TotalPassedUnits = 0;
        }

        private void UpdateTestYieldData()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateTestYieldData), new object[] { });
            }
            else
            {
                lblTotalVal.Text = TestYieldData.TotalTestedUnits.ToString();
                lblPassVal.Text = TestYieldData.TotalPassedUnits.ToString();
                lblRateVal.Text = TestYieldData.PassingRate.ToString("P");
            }
        }
        #endregion Test Yield

        #region Start/Stop Listener
        private int StartListener(string serverIP, string serverPort)
        {
            TCPError error = TCPError.OK;
            /* Create instance */
            if (rvisMMC.tcpServer == null)
            {
                rvisMMC.tcpServer = new Server();
            }

            /* Set IP and Port number */
            error = rvisMMC.tcpServer.SetConfig(serverIP, serverPort);
            if (error != TCPError.OK)
            {
                return (int)error;
            }

            /* Start listener */
            error = rvisMMC.tcpServer.Start();
            if (error != TCPError.OK)
            {
                return (int)error;
            }

            /* return OK */
            return (int)error;
        }

        private void StopListener()
        {
            /* Create instance */
            if (rvisMMC.tcpServer != null && rvisMMC.tcpServer.IsRunning)
            {
                rvisMMC.tcpServer.Stop();
            }
        }
        #endregion Start/Stop Listener

        #region Event Handler
        private void RvisMMC_OnSerialResultPub(string serial, string masterImgDir)
        {
            string currentImgDir = SpecialSettingData.UIImageLoadPath + @"LIVE\current.png";

            /* Add SN to unit result */
            AddLineToUnitResult("Serial Number\t: " + serial);
            /* Add line separator to unit result */
            AddLineToUnitResult(LINE_SEPARATOR);
            /* Show Images */
            ShowImages(masterImgDir, currentImgDir);
        }

        private void RvisMMC_OnResultStringPub(string testgroup, string testname, string result, string masterImgDir, string testEnd)
        {
            resultNum++;
            string data = resultNum.ToString() +". " + testname + "\t" + result;
            string currentImgDir = SpecialSettingData.UIImageLoadPath + @"LIVE\current.png";
            /* Show data */
            AddLineToUnitResult(data, true);
            /* Show Images */
            ShowImages(masterImgDir, currentImgDir);
            if(result == "FAIL")
            {
                stsReporting = false;
            }
           // if(result == "PASS")
            //{
                //stsReporting = true;
           // }
        }

        private void RvisMMC_OnCoverResultPub(string coverResult)
        {
            if(coverResult ==  "PASS")
            {
                PrintToSystemLog("Cover: OK");
            }
            if(coverResult == "FAIL")
            {
                PrintToSystemLog("Please check cover position");
                ProjectHalt();
            }
        }

        private void RvisMMC_OnFinishInspecPub(bool sts, bool LastSts)
        {
            bool isPassed = stsReporting;
            printOverallSts = stsReporting;

            /* Set overall status */
            if (isPassed)
            {
                SetOverallStatus(OverallStatus.PASS);
                finalSts.ShowStatus("PASS");
                Task.Run(() => { finalSts.ShowDialog(); });
                RVISData.GuiDataExchange.finalSts = isPassed;
            }
            else
            {
                //201908161345 st status refresh
                stsReporting = true;
                //201908161345 ed status refresh
                SetOverallStatus(OverallStatus.FAIL);
                finalSts.ShowStatus("FAIL");
                Task.Run(() => { finalSts.ShowDialog(); });
                RVISData.GuiDataExchange.finalSts = isPassed;
            }
            /* Increment test yield data */
            IncrementTestYieldData(isPassed);
            UpdateTestYieldData();
            /* show unit test result footer */
            SetUnitResultFooter();
            /* set inspection status to false */
            Task.Run(() => { AutoSaveResult(); });
            Task.Run(() => { rvisMMC.FinishInspection(); });
            isInspecting = false;
            RVISData.GuiDataExchange.prjStillrunSts = false;
            PrintToSystemLog("Inspection Completed.");
            fileLog.Logging("Inspection Completed");
           // stsReporting = true;
            rtxLog.Text = null;
        }

        private void IncrementTestYieldData(bool isPassUnit)
        {
            TestYieldData.TotalTestedUnits++;

            if (isPassUnit) TestYieldData.TotalPassedUnits++;
        }

        private void RvisMMC_OnPhysicalStartButtonPub(bool sts)
        {
            if (sysWaitFlag == false)
            {
                if (sts == true)
                {
                    MAIN_START();
                }
            }
            else
            {
                MessageBox.Show("System is not yet ready. Please wait.");
            }
        }
        private void RvisMMC_OnRobotErrStatusPub(bool robotErrSts)
        {
            //do something
        }
        private void RvisMMC_OnRobotPauseStatusPub(bool robotPauseSts)
        {
           if(robotPauseSts == true)
            {
              //  PrintToSystemLog("Robot is paused");
            }
            else
            {
                //PrintToSystemLog("Initiate play and resume");
            }
        }
        private void RvisMMC_OnRobotRunStatusPub(bool robotRunSts)
        {
            if (robotRunSts == true)
            {
                PrintToSystemLog("Robot is runnning project");
            }
            else
            {
                PrintToSystemLog("Robot had stop");
             
            }

        }

        private void RvisMMC_OnRobotStopStatusPub(bool robotStopSts)
        {
            if(robotStopSts == true)
            {
                PrintToSystemLog("Robot is ready/had exited project");
            }
            else
            {
                PrintToSystemLog("Robot is running current project");
            }
        }
        private void RvisMMC_OnSystemErrStatusPub(bool isDoorLockedLeft, bool isDoorLockedRight, bool isTrolleySense1, bool isTrolleySense2)
        {
            if((isDoorLockedLeft == false)|| (isDoorLockedRight == false))
            {
                PrintToSystemLog("Door is not properly close");
            }
    
            if((isTrolleySense1 == false) || (isTrolleySense2 == false))
            {
                PrintToSystemLog("Trolley is not placed properly");
            }
            ProjectStop();
        }
        private void RvisMMC_OnSystemReadyStartStatusPub(bool systemReadyStartSts)
        {
            if(systemReadyStartSts == true)
            {
                sysWaitFlag = false;
                PrintToSystemLog("System is ready");
                SetOverallStatus(OverallStatus.READY);
            }
        }
        private void RvisMMC_OnMESConduitResponsePub(string response)
        {
            PrintToSystemLog("42Q response: "+ response);
        }

        private void RvisMMC_OnMESMeasurementResponsePub(bool response)
        {
            string res = null;
            if (response == true)
            {
                res = "Parametric data successfully sent to 42Q";
            }
            else
            {
                res = "Parametric data failed to sent to 42Q";
            }
            PrintToSystemLog(res);
        }

        private void RvisMMC_OnMESSerialSubmitPub(string str)
        {
            PrintToSystemLog(str);
        }
        #endregion Event Handler


        #region Print To System Log
        private void PrintToSystemLog(string log)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(PrintToSystemLog), new object[] {log});
            }
            else
            {
                if((log == "Door is not properly close")||
                   (log == "Trolley is not placed properly") )
                {
                    rtxLog.SelectionColor = Color.Red;
                    rtxLog.AppendText(log + "\n");
                }
                else
                {
                    rtxLog.SelectionColor = Color.Black;
                    rtxLog.AppendText(log + "\n");
                }
            }
            
        }
        #endregion

        #region Demo
        private void SampleUI()
            {
                SetOverallStatus(OverallStatus.ERROR);
                SampleOperatorID();
                SampleResult();
                SampleImage();
                SampleTestYield();
                SampleStatus(true, false);
            }

            private void SampleOperatorID()
            {
                lblOperatorIDVal.Text = "12345";
            }

            private void SampleResult()
            {
                DateTime startTime = DateTime.Now;
                DateTime stopTime;
                string cycleTime = "";
                rtxUnitResult.SelectionTabs = new int[] { 8, 16, 24, 32 };

                rtxUnitResult.Clear();
                AddLine(rtxUnitResult, "**************************************************");
                AddLine(rtxUnitResult, "Start Time\t: " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                AddLine(rtxUnitResult, "Serial Number\t: 18L531SPG");
                AddLine(rtxUnitResult, "**************************************************");
                AddLine(rtxUnitResult, "Test Item 1\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 2\t\t\tFAIL");
                AddLine(rtxUnitResult, "Test Item 3\t\t\tFAIL");
                AddLine(rtxUnitResult, "Test Item 4\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 5\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 6\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 7\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 8\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 9\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 10\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 11\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 12\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 13\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 14\t\t\tFAIL");
                AddLine(rtxUnitResult, "Test Item 15\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 16\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 17\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 18\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 19\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 20\t\t\tFAIL");
                AddLine(rtxUnitResult, "Test Item 21\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 22\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 23\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 24\t\t\tPASS");
                AddLine(rtxUnitResult, "Test Item 25\t\t\tPASS");
                //AddLine(rtxUnitResult, "Test Item 26\t\t\tPASS");
                //AddLine(rtxUnitResult, "Test Item 27\t\t\tPASS");
                //AddLine(rtxUnitResult, "Test Item 28\t\t\tPASS");
                //AddLine(rtxUnitResult, "Test Item 29\t\t\tPASS");
                //AddLine(rtxUnitResult, "Test Item 30\t\t\tPASS");
                AddLine(rtxUnitResult, "**************************************************");
                stopTime = DateTime.Now;
                cycleTime = CalculateCycleTimeInString(startTime, stopTime);
                AddLine(rtxUnitResult, "Stop Time\t: " + stopTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //AddLine(rtxUnitResult, "Cycle Time\t: " + cycleTime);
            }

            private void SampleImage()
            {
                string okSampleDir = @"..\..\img\OKSample.png";
                string ngSampleDir = @"..\..\img\NGSample.png";
                ShowImages(okSampleDir, ngSampleDir);
            }

            private void SampleTestYield()
            {
                double total = 100;
                double pass = 95;
                double rate = pass / total;

                /* Sample test yield */
                lblTotalVal.Text = total.ToString();
                lblPassVal.Text = pass.ToString();
                lblRateVal.Text = rate.ToString("P");
            }

            private void SampleStatus(bool tmOnline, bool mesOnline)
            {
                if (tmOnline)
                {
                    ssTMRobot.Text = "TM-Robot: Online";
                    ssTMRobot.BackColor = Color.Lime;
                }
                else
                {
                    ssTMRobot.Text = "TM-Robot: Offline";
                    ssTMRobot.BackColor = Color.Red;
                }

                if (mesOnline)
                {
                    ssMES.Text = "MES: Online";
                    ssMES.BackColor = Color.Lime;
                }
                else
                {
                    ssMES.Text = "MES: Offline";
                    ssMES.BackColor = Color.Red;
                }
            
            }

        #endregion Demo

    }
}
