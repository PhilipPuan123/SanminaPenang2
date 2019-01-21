/* To-do:
 * - Force log-off and reset data based on settings(daily/hourly)
 * - Add local server path in Setting UI
 * - Add database path in Setting UI
 * 
 * 
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

namespace RVIS
{
    public partial class FrmMain : Form
    {
        #region Declaration
        #region Constant
        private const int MESLOG_MAX_LINES = 100;
        private const int UI_REFRESH_RATE_MS = 500;
        private const string LINE_SEPARATOR = "**************************************************";
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

        private bool isInspecting = false;      // flag for inspection in process
        private bool needLogOff = false;        // flag for log-off required

        /* Classes */
        private RVISML rvisMMC = new RVISML();
        //private Server server;


        #endregion Declaration

        #region Form Controls
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = MAIN_TITLE;
            InitializeTmrClock();
            InitializeBackgroundWorker();
            ShowDateTime();
            EnableAdminAccess(false);
            //SampleUI();

            /* Start backgroundWorker */
            //bgwUIThread.RunWorkerAsync();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Stop UI Thread */
            if (bgwUIThread.WorkerSupportsCancellation == true)
            {
                bgwUIThread.CancelAsync();
            }
        }

        #region Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            /* Start new unit test result */
            StartNewUnitResult();

            /* Subscribe to MMC event */
            rvisMMC.OnSerialResultPub += RvisMMC_OnSerialResultPub;
            rvisMMC.OnResultStringPub += RvisMMC_OnResultStringPub;
            rvisMMC.OnRobotStatusPub += RvisMMC_OnRobotStatusPub;
            
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
                    case 1:     //*.txt
                        rtxUnitResult.SaveFile(sfdUnitData.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 2:     //*.rtf
                        rtxUnitResult.SaveFile(sfdUnitData.FileName);
                        break;
                }
            }
        }
        #endregion Buttons

        private void InitializeTmrClock()
        {
            tmrClock.Enabled = true;
            tmrClock.Interval = 1000;
        }


        #region MenuStrips
        private void tsmiLogin_Click(object sender, EventArgs e)
        {
            LoadLoginForm(sender, e);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            string serverIP = SettingData.PcServerIP;
            string serverPort = SettingData.PcServerPort;
            string tmIP = SettingData.TmIP;
            string tmModbusPort = SettingData.TmModbusPort;
            int error;

            /* Start Listerner */
            error = StartListener(serverIP, serverPort);
            if(error != 0)
            {
                MessageBox.Show("ErrorCode: "+ error);
            }

            /* Connect TM Modbus */
            error = ModbusControl.Connect(tmIP,tmModbusPort);
            if (error != 0)
            {
                MessageBox.Show("ErrorCode: " + error);
            }

            tsmiConnect.Enabled = false;
            tsmiDisconnect.Enabled = true;
        }

        private void tsmiDisconnect_Click(object sender, EventArgs e)
        {
            /* Disconnect TCP Server */
            StopListener();

            /* Disconnect TM Modbus */
            ModbusControl.Disconnect();
            
            /* Disable button */
            tsmiConnect.Enabled = true;
            tsmiDisconnect.Enabled = false;
        }

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
        #endregion MenuStrips

        #region StatusStrip
        private void tmrClock_Tick(object sender, EventArgs e)
        {
            ShowDateTime();
        }

        /// <summary>Update date and time on UI.</summary>
        private void ShowDateTime()
        {
            ssDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy  hh:mm:ss tt");
        }
        #endregion StatusStrip

        private void rtxUnitResult_TextChanged(object sender, EventArgs e)
        {
            SetCursorToEnd(sender, EventArgs.Empty);
        }
        #endregion Form Controls

        #region BackgroundWorker
        private void InitializeBackgroundWorker()
        {
            /* UI Thread */
            bgwUIThread.DoWork += BgwUIThread_DoWork;
            bgwUIThread.ProgressChanged += BgwUIThread_ProgressChanged;
            bgwUIThread.RunWorkerCompleted += BgwUIThread_RunWorkerCompleted;
            bgwUIThread.WorkerReportsProgress = true;
            bgwUIThread.WorkerSupportsCancellation = true;
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
                    /* Check operator login */

                    /* Check modbus conection */

                    /* Check TM Tcp connection */

                    /* Check MES connection */

                    /* Check project status */

                    /* Check IO status */

                    Task.Delay(UI_REFRESH_RATE_MS);
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
        #endregion BackgroundWorker

        #region Form Function
        private void LoadLoginForm(object sender, EventArgs e)
        {
            using (FrmLogin frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    UserData.ID = frmLogin.UserID;
                    UserData.Password = frmLogin.Password;
                    UserData.IsAdmin = frmLogin.IsAdmin;
                }

                lblOperatorIDVal.Text = UserData.ID;
                EnableAdminAccess(UserData.IsAdmin);
            }
        }

        private void EnableAdminAccess(bool isAdmin)
        {
            /* If admin access, set background color to Cyan */
            if (isAdmin)
            {
                tsmiTools.Enabled = true;
                lblOperatorIDVal.BackColor = Color.Cyan;
            }
            else
            {
                tsmiTools.Enabled = false;
                lblOperatorIDVal.BackColor = SystemColors.Control;
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnSave.Enabled = false;
            tsmiLogin.Enabled = false;
            tsmiConnection.Enabled = false;

            /* If user not logged in */
            if (String.IsNullOrEmpty(lblOperatorIDVal.Text))
            {
                /* Enable login button only */
                tsmiLogin.Enabled = true;
            }
            /* If inspection is running */
            else if (isInspecting)
            {
                /* Enable stop button only */
                btnStop.Enabled = true;
            }
            else
            {
                btnStart.Enabled = true;
                tsmiLogin.Enabled = true;
                tsmiConnection.Enabled = true;

                if (rtxUnitResult.TextLength > 0)
                {
                    btnSave.Enabled = true;
                }
            }
        }

        private void AddLineToUnitResult(string line)
        {
            AddLine(rtxUnitResult, line);
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

        /// <summary>Show master and current test unit image on UI.</summary>
        private void ShowImages(Image masterImg, Image currentImg)
        {
            /* Show master image */
            picMasterImg.Image = masterImg;
            picMasterImg.SizeMode = PictureBoxSizeMode.StretchImage;
            /* Show current test image */
            picTestImg.Image = currentImg;
            picTestImg.SizeMode = PictureBoxSizeMode.StretchImage;
        }

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
        #endregion Form Function

        #region Functions
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

        private int StartListener(string serverIP, string serverPort)
        {
            TCPError error = TCPError.OK;
            ///* Create instance */
            //if (server == null)
            //{
            //    server = new Server();
            //}

            ///* Set IP and Port number */
            //error = server.SetConfig(serverIP, serverPort);
            //if (error != TCPError.OK)
            //{
            //    return (int)error;
            //}

            ///* Start listener */
            //error = server.Start();
            //if (error != TCPError.OK)
            //{
            //    return (int)error;
            //}

            ///* return OK */
            //return (int)error;


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
            ///* Create instance */
            //if (server != null && server.IsRunning)
            //{
            //    server.Stop();
            //}

            /* Create instance */
            if (rvisMMC.tcpServer != null && rvisMMC.tcpServer.IsRunning)
            {
                rvisMMC.tcpServer.Stop();
            }
        }
        #endregion Functions

        #region Event Handler
        private void RvisMMC_OnSerialResultPub(string serial)
        {
            /* Add SN to unit result */
            AddLineToUnitResult("Serial Number\t: " + serial);
            /* Add line separator to unit result */
            AddLineToUnitResult(LINE_SEPARATOR);
        }

        private void RvisMMC_OnRobotStatusPub(string robotRunSts, string robotErrSts, string robotPauseSts)
        {
            throw new NotImplementedException();
        }

        private void RvisMMC_OnResultStringPub(string checkpt, string result, string dir, string testEnd)
        {
            throw new NotImplementedException();
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
                SampleStatus(true, true, false);
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
                Image okSample = Image.FromFile("..\\..\\img\\OKSample.png");
                Image ngSample = Image.FromFile("..\\..\\img\\NGSample.png");
                ShowImages(okSample, ngSample);
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

            private void SampleStatus(bool modbusOnline, bool tmTcpOnline, bool mesOnline)
            {
                if (modbusOnline)
                {
                    ssModbus.Text = "Modbus: Online";
                    ssModbus.BackColor = Color.Lime;
                }
                else
                {
                    ssModbus.Text = "Modbus: Offline";
                    ssModbus.BackColor = Color.Red;
                }

                if (tmTcpOnline)
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
