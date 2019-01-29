/* To-do:
 * - Force log-off and reset data based on settings(daily/hourly)
 * - Update Test yield on test end
 */

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
using MesIF;
using TcpIF;
using TMModbusIF;
using RVISMMC;
using RVISData;

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
            FAIL
        }

        /* Form Title */
        private static AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute));
        private static Version appversion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
        private string MAIN_TITLE = titleAttribute.Title + " V" + appversion;
        /* Test Data */
        private DateTime startTime;
        private DateTime stopTime;

        private static DateTime resetTime;
        private bool isInspecting           = false;    // flag for inspection in progress
        private bool isLogOffNeeded         = false;    // flag for required to log-off 
        private bool isResetTestYieldNeeded = false;    // flag for required to reset test yield
        private CONNECTION_STS tmConnectionSts = CONNECTION_STS.Disconnected;
        /* Classes */
        private RVISML rvisMMC = new RVISML();
        #endregion Declaration

        #region Form Controls
        public FrmMain()
        {
            InitializeComponent();
            UpgradeConfigFile();

            DataUtility.UpdateSettingDataFromConfig();
            DataUtility.UpdateSpecialSettingDataFromConfig();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = MAIN_TITLE;
            InitializeTmrClock();
            ShowDateTime();
            EnableAccess(UserData.UserAccess);
            //SampleUI();

            /* Start backgroundWorker */
            InitializeBackgroundWorker();
            bgwUIThread.RunWorkerAsync();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAllConnectionAndBackgroundTask();
        }

        #region Form Controls-Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            /* Start new unit test result */
            StartNewUnitResult();

            /* Subscribe to MMC event */
            rvisMMC.OnSerialResultPub += RvisMMC_OnSerialResultPub;
            rvisMMC.OnResultStringPub += RvisMMC_OnResultStringPub;
            
            /* Start MMC */
            rvisMMC.Start();

            isInspecting = true;
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
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
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
            resetTime = DateTime.Now;

            switch (SettingData.DataResetFreq)
            {
                case "Hourly":
                    resetTime.AddHours(1);
                    break;
                case "Daily":
                default:
                    resetTime.AddDays(1);
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
            ////var statusStrip     = ssTMRobot;
            //string serverIP     = SettingData.PcServerIP;
            //string serverPort   = SettingData.PcServerPort;
            //string tmIP         = SettingData.TmIP;
            //string tmModbusPort = SettingData.TmModbusPort;
            //int error;

            ///* Set status strip to Connecting */
            //tmConnectionSts = CONNECTION_STS.Connecting;

            ///* Start Listerner */
            //error = StartListener(serverIP, serverPort);
            //if(error != 0)
            //{
            //    MessageBox.Show("ErrorCode: "+ error);
            //}

            ///* Connect TM Modbus */
            //error = ModbusControl.Connect(tmIP,tmModbusPort);
            //if (error != 0)
            //{
            //    MessageBox.Show("ErrorCode: " + error);
            //}

            ///* If error */
            //if (error != 0)
            //{
            //    StopAllConnectionAndBackgroundTask();
            //    /* Set status strip to Disconnected */
            //    tmConnectionSts = CONNECTION_STS.Disconnected;
            //}
            //else
            //{
            //    /* Start background task for status checking */
            //    Task.Factory.StartNew(async () => { await rvisMMC.BackgroundSysCheck(); }, TaskCreationOptions.LongRunning);
            //    /* Set status strip to Connected */
            //    tmConnectionSts = CONNECTION_STS.Connected;

            //    tsmiConnect.Enabled = false;
            //    tsmiDisconnect.Enabled = true;
            //}


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
                MessageBox.Show("ErrorCode: " + listenerError);
            }


            /* Connect TM Modbus */
            modbusError = ModbusControl.Connect(tmIP, tmModbusPort);
            if (modbusError != 0)
            {
                MessageBox.Show("ErrorCode: " + modbusError);
            }

            /* If error */
            if (listenerError != 0 || modbusError != 0)
            {
                StopAllConnectionAndBackgroundTask();
                /* Set status strip to Disconnected */
                tmConnectionSts = CONNECTION_STS.Disconnected;
            }
            else
            {
                /* Start background task for status checking */
                Task.Factory.StartNew(async () => { await rvisMMC.BackgroundSysCheck(); }, TaskCreationOptions.LongRunning);
                /* Set status strip to Connected */
                tmConnectionSts = CONNECTION_STS.Connected;
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
            using (FrmLogin frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    UserData.ID = frmLogin.UserID;
                    UserData.Password = frmLogin.Password;
                    UserData.UserAccess = frmLogin.UserAccess;
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
                    break;
                case AccessLevel.Admin:
                    tsmiTools.Visible = true;
                    tsmiService.Visible = false;
                    lblOperatorIDVal.BackColor = Color.Cyan;
                    break;
                case AccessLevel.User:
                default:
                    tsmiTools.Visible = false;
                    tsmiService.Visible = false;
                    lblOperatorIDVal.BackColor = SystemColors.Control;
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
            /* Test Yield */
            UpdateTestYieldData();
            /* Status strip */
            UpdateStatusStrip();
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
                btnStart.Enabled = false;
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
                btnStop.Enabled = false;
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
        #endregion Update Display-Buttons

        #region Update Display-Status Strips
        private void UpdateStatusStrip()
        {
            UpdateTMConnectionStatus(tmConnectionSts);
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
            startTime = DateTime.Now;
            line = "Start Timestamp\t: " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            AddLineToUnitResult(line);
            /* Add Operator ID to unit result */
            line = "Operator ID\t: " + UserData.ID;
            AddLineToUnitResult(line);
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
            lblTotalVal.Text = TestYieldData.TotalTestedUnits.ToString();
            lblPassVal.Text = TestYieldData.TotalPassedUnits.ToString();
            lblRateVal.Text = TestYieldData.PassingRate.ToString("P");
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
            string data = testname + "\t" + result;
            string currentImgDir = SpecialSettingData.UIImageLoadPath + @"LIVE\current.png";

            /* Show data */
            AddLineToUnitResult(data, true);
            /* Show Images */
            ShowImages(masterImgDir, currentImgDir);
        }
        #endregion Event Handler

        #region Demo
        private void SampleUI()
            {
                SampleOverallResult(OverallStatus.ERROR);
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

            private void SampleOverallResult(OverallStatus status)
            {
                string dispTxt = "";
                Color backColor = Color.Orange;
                Color fontColor = Color.Black;
                switch (status)
                {
                    case OverallStatus.READY:
                        dispTxt = "READY";
                        backColor = Color.Orange;
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
