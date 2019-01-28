using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using TMModbusIF;
using TcpIF;

namespace TestToolKit
{
    
    public partial class FrmTestToolKit : Form
    {
        private const string TITLE = "Test ToolKit V1.00";
        private bool isModbusConnected = false;
        private bool isPauseAvailable = false;

        private Server tcpServer = null;
        private bool isServerStarted = false;
        
        private Client tcpClient = null;
        private bool isClientStarted = false;

        public FrmTestToolKit()
        {
            InitializeComponent();
            this.Text = TITLE;
        }

        #region Form Controls
        private void Form1_Load(object sender, EventArgs e)
        {
            grpControl.Enabled = false;
            grpStatus.Enabled = false;

            btnSendToServer.Enabled = false;

            bgwUIThread.DoWork += BgwUIThread_DoWork;
            bgwUIThread.ProgressChanged += BgwUIThread_ProgressChanged;
            bgwUIThread.RunWorkerCompleted += BgwUIThread_RunWorkerCompleted;

            bgwUIThread.WorkerReportsProgress = true;
            bgwUIThread.WorkerSupportsCancellation = true;

            if (bgwUIThread.IsBusy != true) bgwUIThread.RunWorkerAsync();
        }

        private void rtxMESLog_TextChanged(object sender, EventArgs e)
        {
            /* set the current caret position to the end */
            rtxMESLog.SelectionStart = rtxMESLog.Text.Length;
            /* scroll it automatically */
            rtxMESLog.ScrollToCaret();
        }

        private void rtxTcpServerLog_TextChanged(object sender, EventArgs e)
        {
            /* set the current caret position to the end */
            rtxTcpServerLog.SelectionStart = rtxTcpServerLog.Text.Length;
            /* scroll it automatically */
            rtxTcpServerLog.ScrollToCaret();
        }
        private void rtxTcpClientLog_TextChanged(object sender, EventArgs e)
        {
            /* set the current caret position to the end */
            rtxTcpClientLog.SelectionStart = rtxTcpClientLog.Text.Length;
            /* scroll it automatically */
            rtxTcpClientLog.ScrollToCaret();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int error;
            if (!isModbusConnected)
            {
                error = ModbusControl.Connect(txtIP.Text, "502");
                if (error == (int)MError.OK)
                {
                    ModbusConnectedUI();
                }
                else
                {
                    MessageBox.Show("Error Code:" + error.ToString());
                }
            }
            else
            {
                ModbusControl.Disconnect();
                ModbusDisconnectedUI();
            }
        }
        
            #region TCP Listerner
            private void btnStartListener_Click(object sender, EventArgs e)
            {
                TCPError error;

                if (!isServerStarted)
                {
                    /* Create instance */
                    if (tcpServer == null)
                    {
                        tcpServer = new Server();
                    }
                    /* Convert port number to int */
                    tcpServer.SetConfig(txtListenerIP.Text, Int32.Parse(txtListenerPort.Text));

                    error = tcpServer.Start();
                    if(error == TCPError.OK)
                    {
                        tcpServer.OnDataReceived += TcpServer_OnDataReceived;
                        tcpServer.OnDataSend += TcpServer_OnDataSend;
                        btnStartListener.Text = "Disconnect";
                        isServerStarted = true;
                    }
                }
                else
                {
                    tcpServer.OnDataReceived -= TcpServer_OnDataReceived;
                    tcpServer.OnDataSend -= TcpServer_OnDataSend;
                    tcpServer.Stop();
                    btnStartListener.Text = "Connect";
                    isServerStarted = false;
                }
            }

            private void TcpServer_OnDataSend(object sender, string parameter)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                if (InvokeRequired)
                {
                    BeginInvoke(new Server.DataSend(TcpServer_OnDataSend), sender, parameter);
                }
                else
                {
                    ShowOnTcpServerLog("<< " + parameter);
                    ShowOnMESLog("<< " + parameter);
                }
            }

            private void TcpServer_OnDataReceived(object sender, string parameter)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                if (InvokeRequired)
                {
                    BeginInvoke(new Server.DataReceived(TcpServer_OnDataReceived), sender, parameter);
                }
                else
                {
                    ShowOnTcpServerLog(">> " + parameter);
                }
            }
            #endregion TCP Listerner
        
            #region TCP Client
            private void btnConnectServer_Click(object sender, EventArgs e)
            {
                TCPError error;

                if (!isClientStarted)
                {
                    /* Create instance */
                    if (tcpClient == null)
                    {
                        tcpClient = new Client();
                    }
                    /* Convert port number to int */
                    error = tcpClient.Connect(txtServerIP.Text, Int32.Parse(txtServerPort.Text),3000);

                    if (error != TCPError.OK)
                    {
                        ShowOnTcpClientLog(error.ToString());
                    }
                    else
                    {
                        tcpClient.OnDataReceived += TcpClient_OnDataReceived;
                        tcpClient.OnDataSend += TcpClient_OnDataSend;
                        btnConnectServer.Text = "Disconnect";
                        isClientStarted = true;
                    }
                }
                else
                {
                    tcpClient.OnDataReceived -= TcpClient_OnDataReceived;
                    tcpClient.OnDataSend -= TcpClient_OnDataSend;
                    tcpClient.Disconnect();
                    btnConnectServer.Text = "Connect";
                    isClientStarted = false;
                }
            }

            private void btnSendToServer_Click(object sender, EventArgs e)
            {
                TCPError error = TCPError.OK;
                if (txtClientMsg.TextLength == 0) return;
                byte[] data = Encoding.UTF8.GetBytes(txtClientMsg.Text);

                // Send data to server
                if (tcpClient != null)
                {
                    error = tcpClient.SendAndReceiveData(data);
                }
                if(error != TCPError.OK)
                {
                    ShowOnTcpClientLog(error.ToString());
                    return;
                }

                // Clear textbox
                txtClientMsg.Text = string.Empty;
            }

            private void TcpClient_OnDataSend(object sender, string parameter)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Client.DataSend(TcpClient_OnDataSend), sender, parameter);
                }
                else
                {
                    ShowOnTcpClientLog("<< " + parameter);
                }
            }

            private void TcpClient_OnDataReceived(object sender, string parameter)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Client.DataReceived(TcpClient_OnDataReceived), sender, parameter);
                }
                else
                {
                    ShowOnTcpClientLog(">> " + parameter);
                }
            }
            #endregion TCP Client

            #region TM Modbus Buttons
            private void btnStart_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.Start);
            }

            private void btnStop_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.Stop);
            }

            private void btnPause_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.Pause);
            }

            private void btnRobotX_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRobotCoorX);
            }

            private void btnRobotY_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRobotCoorY);
            }

            private void btnRobotZ_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRobotCoorZ);
            }

            private void btnRobotRX_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRobotCoorRX);
            }

            private void btnRobotRY_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRobotCoorRY);
            }

            private void btnRobotRZ_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRobotCoorRZ);
            }

            private void btnToolX_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetToolCoorX);
            }

            private void btnToolY_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetToolCoorY);
            }

            private void btnToolZ_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetToolCoorZ);
            }

            private void btnToolRX_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetToolCoorRX);
            }

            private void btnToolRY_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetToolCoorRY);
            }

            private void btnToolRZ_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetToolCoorRZ);
            }

            private void btnErrorSts_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetErrorStatus);
            }

            private void btnRunSts_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetRunStatus);
            }

            private void btnEditSts_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetEditStatus);
            }

            private void btnPauseSts_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetPauseStatus);
            }

            private void btnPermissionSts_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetPermissionStatus);
            }

            private void btnGetLastError_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetLastError);
            }

            private void btnCtrlBoxDI_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetControlBoxDIn);
            }

            private void btnEndModDI_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetEndModuleDIn);
            }

            private void btnCtrlBoxDO_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetControlBoxDOut);
            }

            private void btnEndModDO_Click(object sender, EventArgs e)
            {
                execModbusCommand(TMModbusCmd.GetEndModuleDOut);
            }
        #endregion TM Modbus Buttons

        #endregion Form Controls


        #region BackgroundWorker
        private void BgwUIThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void BgwUIThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (isPauseAvailable) btnStart.Text = "Pause";
            else btnStart.Text = "Start";

            if (isServerStarted)
            {
                btnStartListener.Text = "Disconnect";
            }
            else
            {
                btnStartListener.Text = "Connect";
            }


            if (isClientStarted)
            {
                btnConnectServer.Text = "Disconnect";
                btnSendToServer.Enabled = true;
            }
            else
            {
                btnConnectServer.Text = "Connect";
                btnSendToServer.Enabled = false;
                if (tcpClient != null)
                {
                    tcpClient.OnDataReceived -= TcpClient_OnDataReceived;
                    tcpClient.OnDataSend -= TcpClient_OnDataSend;
                }
            }
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
                    /* Check whether modbus is still running */
                    if (isModbusConnected && (GetStatus(TMModbusCmd.GetRunStatus) == true) && (GetStatus(TMModbusCmd.GetPauseStatus) == false))
                    {
                        isPauseAvailable = true;
                    }
                    else
                    {
                        isPauseAvailable = false;
                    }

                    /* Check whether tcp server is still running */
                    if (isServerStarted && tcpServer.IsRunning)
                    {
                        isServerStarted = true;
                    }
                    else
                    {
                        isServerStarted = false;
                    }

                    /* Check whether tcp client is still running */
                    if (isClientStarted && tcpClient.connected)
                    {
                        isClientStarted = true;
                    }
                    else
                    {
                        isClientStarted = false;
                    }

                    bgwUIThread.ReportProgress(0);
                    Thread.Sleep(500);
                }
            }
        }
        #endregion BackgroundWorker
        

        #region Functions
        private bool GetStatus(TMModbusCmd command)
        {
            string result;
            MError error = (MError)ModbusControl.ProcCommand(command, out result);
            if (error == MError.ConnectionLost || error == MError.NotConnected)
            {
                lblOutput.Text = error.ToString();
                ModbusDisconnectedUI();
            }
            if (error != MError.OK)
            {
                lblOutput.Text = error.ToString();
                return false;
            }
            else
            {
                return Convert.ToBoolean(result);
            }
        }

        private void ModbusConnectedUI()
        {
            btnConnect.Text = "Disconnect";
            isModbusConnected = true;

            grpControl.Enabled = true;
            grpStatus.Enabled = true;
        }

        private void ModbusDisconnectedUI()
        {
            btnConnect.Text = "Connect";
            isModbusConnected = false;

            grpControl.Enabled = false;
            grpStatus.Enabled = false;
        }

        /// <summary>
        /// Execute Modbus command.
        /// </summary>
        /// <param name="command"></param>
        private void execModbusCommand(TMModbusCmd command)
        {
            string result;
            int error = ModbusControl.ProcCommand(command, out result);
            if (error == (int)MError.ConnectionLost || error == (int)MError.NotConnected)
            {
                lblOutput.Text = error.ToString();
                ModbusDisconnectedUI();
            }
            if (error != (int)MError.OK) lblOutput.Text = error.ToString();
            else lblOutput.Text = result;
        }

        int MESLOG_MAX_LINES = 5;
        int TCPLOG_MAX_LINES = 100;

        #region Tcp Client Log
        private void ShowOnTcpClientLog(string str)
        {
            AddLinesToTcpClientLog(str);
            RemoveOldTCPClientLogLines();
        }

        private void AddLinesToTcpClientLog(string str)
        {
            string text = str.TrimEnd('\0');
            rtxTcpClientLog.AppendText(text + "\r\n");
        }

        private void RemoveOldTCPClientLogLines()
        {
            if (rtxTcpClientLog.Lines.Count() > TCPLOG_MAX_LINES)
            {
                int exceededLines = rtxTcpClientLog.Lines.Count() - TCPLOG_MAX_LINES;

                string[] newLines = new string[TCPLOG_MAX_LINES];
                Array.Copy(rtxTcpClientLog.Lines, exceededLines, newLines, 0, TCPLOG_MAX_LINES);
                rtxTcpClientLog.Lines = newLines;
            }
        }
        #endregion Tcp Client Log

        #region Tcp Server Log
        private void ShowOnTcpServerLog(string str)
        {
            AddLinesToTcpServerLog(str);
            RemoveOldTCPServerLogLines();
        }

        private void AddLinesToTcpServerLog(string str)
        {
            string text = str.TrimEnd('\0');
            rtxTcpServerLog.AppendText(text + "\r\n");
        }

        private void RemoveOldTCPServerLogLines()
        {
            if (rtxTcpServerLog.Lines.Count() > TCPLOG_MAX_LINES)
            {
                int exceededLines = rtxTcpServerLog.Lines.Count() - TCPLOG_MAX_LINES;

                string[] newLines = new string[TCPLOG_MAX_LINES];
                Array.Copy(rtxTcpServerLog.Lines, exceededLines, newLines, 0, TCPLOG_MAX_LINES);
                rtxTcpServerLog.Lines = newLines;
            }
        }
        #endregion Tcp Server Log

        #region MES Log
        private void ShowOnMESLog(string[] strings)
        {
            AddLinesToMESLog(strings);
            RemoveOldMESLogLines();
        }
        
        private void ShowOnMESLog(string strings)
        {
            AddLinesToMESLog(strings);
            RemoveOldMESLogLines();
        }

        private void AddLinesToMESLog(string[] strings)
        {
            foreach (string text in strings)
            {
                rtxMESLog.AppendText(text + "\r\n");
            }
        }

        private void AddLinesToMESLog(string strings)
        {
            string text = strings.TrimEnd('\0');
            if (strings.Contains("PASSED"))
            {
                rtxMESLog.SelectionColor = Color.Lime;
            }
            else if (strings.Contains("FAILED"))
            {
                rtxMESLog.SelectionColor = Color.Red;
            }
            rtxMESLog.AppendText(text + "\r\n");
            rtxMESLog.SelectionColor = Color.Yellow;
        }

        private void RemoveOldMESLogLines()
        {
            if (rtxMESLog.Lines.Count() > MESLOG_MAX_LINES)
            {
                int exceededLines = rtxMESLog.Lines.Count() - MESLOG_MAX_LINES;

                string[] newLines = new string[MESLOG_MAX_LINES];
                Array.Copy(rtxMESLog.Lines, exceededLines, newLines, 0, MESLOG_MAX_LINES);
                rtxMESLog.Lines = newLines;
            }
        }

        #endregion MES Log

        //private void GetProjectStatus()
        //{
        //    //Task.Delay(3000);
        //    if((GetStatus(TMCommand.GetRunStatus) == true) && (GetStatus(TMCommand.GetPauseStatus) == false) )
        //    {
        //        btnStart.Text = "Pause";
        //    }
        //    else
        //    {
        //        btnStart.Text = "Start";
        //    }
        //}
        #endregion Functions

        private void FrmTestToolKit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ModbusControl.IsRunning)
            {
                ModbusControl.Disconnect();
            }
        }
    }
}
