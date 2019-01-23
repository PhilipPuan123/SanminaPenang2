using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;

namespace RVIS
{
    public partial class FrmSetting : Form
    {
        private bool settingErr = false;
        private string errorMessage;
        #region UI Control
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            LoadSettingFromConfigFile();
            CheckAllInputs();
        }

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingData.MesIP           = Properties.Settings.Default.mesIP;
            SettingData.MesPort         = Properties.Settings.Default.mesPort;
            SettingData.MesDevNum       = Properties.Settings.Default.mesDevNum;
            SettingData.LocalServerPath = Properties.Settings.Default.localServerPath;
            SettingData.TmIP            = Properties.Settings.Default.tmIP;
            SettingData.TmModbusPort    = Properties.Settings.Default.tmModbusPort;
            SettingData.PcServerIP      = Properties.Settings.Default.pcServerIP;
            SettingData.PcServerPort    = Properties.Settings.Default.pcServerPort;
            SettingData.DataResetFreq   = Properties.Settings.Default.dataResetFreq; ;
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /* Check the content of each textbox and selections */
            CheckAllInputs();

            if (settingErr)
            {
                MessageBox.Show("There is error in setting. \nPlease check if the settings are correct.\n"+ errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /* Save settings */
                SaveSettingToConfigFile();
                /* Close form */
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion UI Control

        #region Functions
        private void CheckAllInputs()
        {
            settingErr = false;
            errorMessage = "Details: \n";

            /* Check MES Config Settings */
            if (CheckDeviceNumInput(txtMESDevNum, lblMESDevNumErr))
            {
                errorMessage += "- Invalid MES Device Number\n";
            }
            if (CheckIPAddressInput(txtMESIP, lblMESIPErr))
            {
                errorMessage += "- Invalid MES IP\n";
            }
            if (CheckPortNumInput(txtMESPort, lblMESPortErr))
            {
                errorMessage += "- Invalid MES Port\n";
            }
            if (CheckPathInput(txtLocalServerPath, lblLocalServerPathErr))
            {
                errorMessage += "- Invalid Local Server Path\n";
            }

            /* Check PC Config Settings */
            if (CheckIPAddressInput(txtPCServerIP, lblPCServerIPErr))
            {
                errorMessage += "- Invalid PC Server IP\n";
            }
            if (CheckPortNumInput(txtPCServerPort, lblPCServerPortErr))
            {
                errorMessage += "- Invalid PC Server Port\n";
            }

            /* Check TM Config Settings */
            if (CheckIPAddressInput(txtTMIP, lblTMIPErr))
            {
                errorMessage += "- Invalid TM IP\n";
            }
            if (CheckPortNumInput(txtTMModbusPort, lblTMModbusPortErr))
            {
                errorMessage += "- Invalid TM Modbus Port\n";
            }
        }

        private bool CheckDeviceNumInput(TextBox txtDevNum, Label lblErr)
        {
            int deviceNum;
            bool error = false;

            /* If device number is not integer */
            if (Int32.TryParse(txtDevNum.Text, out deviceNum) != true)
            {
                error = true;
            }
            /* If device number not within allowable range */
            else if (deviceNum < 0 || 999 < deviceNum)
            {
                error = true;
            }

            /* if error */
            if (error)
            {
                lblErr.Visible = true;
                settingErr = true;
            }
            else
            {
                lblErr.Visible = false;
            }
            return error;
        }

        private bool CheckIPAddressInput(TextBox txtIP,Label lblErr)
        {
            bool error = false;
            if (IsValidIPAddress(txtIP.Text) != true)
            {
                error = true;
            }

            /* if error */
            if (error)
            {
                lblErr.Visible = true;
                settingErr = true;
            }
            else
            {
                lblErr.Visible = false;   
            }
            return error;
        }

        private bool IsValidIPAddress(string ip)
        {
            IPAddress ipAddress;
            return IPAddress.TryParse(ip, out ipAddress);
        }

        private bool CheckPortNumInput(TextBox txtPort, Label lblErr)
        {
            bool error = false;
            int port;

            if (Int32.TryParse(txtPort.Text, out port) != true)
            {
                error = true;
            }

            /* if error */
            if (error)
            {
                lblErr.Visible = true;
                settingErr = true;
            }
            else
            {
                lblErr.Visible = false;
            }
            return error;
        }

        private bool CheckPathInput(TextBox txtPath, Label lblErr)
        {
            bool error = false;
            /* Check if directory exist */
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                error = true;
            }
            /* Check if directory exist */
            if (System.IO.Directory.Exists(txtPath.Text) != true)
            {
                error = true;
            }

            /* if error */
            if (error)
            {
                lblErr.Visible = true;
                settingErr = true;
            }
            else
            {
                lblErr.Visible = false;
            }
            return error;
        }

        private void LoadSettingFromConfigFile()
        {
            /* Update settngs on UI */
            txtMESIP.Text           = Properties.Settings.Default.mesIP;
            txtMESPort.Text         = Properties.Settings.Default.mesPort;
            txtMESDevNum.Text       = Properties.Settings.Default.mesDevNum;
            txtLocalServerPath.Text = Properties.Settings.Default.localServerPath;
            txtTMIP.Text            = Properties.Settings.Default.tmIP;
            txtTMModbusPort.Text    = Properties.Settings.Default.tmModbusPort;
            txtPCServerIP.Text      = Properties.Settings.Default.pcServerIP;
            txtPCServerPort.Text    = Properties.Settings.Default.pcServerPort;
            switch (Properties.Settings.Default.dataResetFreq)
            {
                case "Hourly":
                    rdoHourly.Checked = true;
                    break;
                case "Daily":
                default:
                    rdoDaily.Checked = true;
                    break;
            }
        }

        private void SaveSettingToConfigFile()
        {
            /* Save settings in config file */
            Properties.Settings.Default.mesIP           = txtMESIP.Text;
            Properties.Settings.Default.mesPort         = txtMESPort.Text;
            Properties.Settings.Default.mesDevNum       = txtMESDevNum.Text;
            Properties.Settings.Default.localServerPath = txtLocalServerPath.Text;
            Properties.Settings.Default.tmIP            = txtTMIP.Text;
            Properties.Settings.Default.tmModbusPort    = txtTMModbusPort.Text;
            Properties.Settings.Default.pcServerIP      = txtPCServerIP.Text;
            Properties.Settings.Default.pcServerPort    = txtPCServerPort.Text;
            if (rdoDaily.Checked)
            {
                Properties.Settings.Default.dataResetFreq = rdoDaily.Text;
            }
            else if (rdoHourly.Checked)
            {
                Properties.Settings.Default.dataResetFreq = rdoHourly.Text;
            }

            Properties.Settings.Default.Save();
        }
        #endregion Functions
    }
}
