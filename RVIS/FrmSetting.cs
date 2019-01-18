/* To-do:
 * - check setting is in correct format and value
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

using System.Net;

namespace RVIS
{
    public partial class FrmSetting : Form
    {
        private bool settingErr = false;

        #region UI Control
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            LoadSettingFromConfigFile();

            /* Update settngs on UI */
            txtMESIP.Text           = SettingData.MesIP;
            txtMESPort.Text         = SettingData.MesPort;
            txtMESDevNum.Text       = SettingData.MesDevNum;
            txtTMIP.Text            = SettingData.TmIP;
            txtTMModbusPort.Text    = SettingData.TmModbusPort;
            txtPCServerIP.Text      = SettingData.PcServerIP;
            txtPCServerPort.Text    = SettingData.PcServerPort;
            switch (SettingData.DataResetFreq)
            {
                case "Hourly":
                    rdoHourly.Checked = true;
                    break;
                case "Daily":
                default:
                    rdoDaily.Checked = true;
                    break;
            }

            /* Remove */
            CheckAllInputs();
        }

        private void rdoHourly_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHourly.Checked)
            {
                SettingData.DataResetFreq = rdoHourly.Text;
            }
        }

        private void rdoDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDaily.Checked)
            {
                SettingData.DataResetFreq = rdoDaily.Text;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /* Check the content of each textbox */
            CheckAllInputs();

            if (settingErr)
            {
                MessageBox.Show("There is error in setting. \nPlease check if the settings are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /* Save settings */
                SaveSettingToConfigFile();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion UI Control

        #region Functions
        private void CheckAllInputs()
        {
            settingErr = false;

            /* Check MES Config Settings */
            CheckDeviceNumInput(txtMESDevNum, lblMESDevNumErr);
            CheckIPAddressInput(txtMESIP, lblMESIPErr);
            CheckPortNumInput(txtMESPort, lblMESPortErr);

            /* Check PC Config Settings */
            CheckIPAddressInput(txtPCServerIP, lblPCServerIPErr);
            CheckPortNumInput(txtPCServerPort, lblPCServerPortErr);

            /* Check TM Config Settings */
            CheckIPAddressInput(txtTMIP, lblTMIPErr);
            CheckPortNumInput(txtTMModbusPort, lblTMModbusPortErr);
        }

        private void CheckDeviceNumInput(TextBox txtDevNum, Label lblErr)
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

        private void LoadSettingFromConfigFile()
        {
            /* Load user settings from config file */
            SettingData.MesIP           = Properties.Settings.Default.mesIP;
            SettingData.MesPort         = Properties.Settings.Default.mesPort;
            SettingData.MesDevNum       = Properties.Settings.Default.mesDevNum;
            SettingData.TmIP            = Properties.Settings.Default.tmIP;
            SettingData.TmModbusPort    = Properties.Settings.Default.tmModbusPort;
            SettingData.PcServerIP      = Properties.Settings.Default.pcServerIP;
            SettingData.PcServerPort    = Properties.Settings.Default.pcServerPort;
            SettingData.DataResetFreq   = Properties.Settings.Default.dataResetFreq;
        }

        private void SaveSettingToConfigFile()
        {
            Properties.Settings.Default.mesIP           = SettingData.MesIP;
            Properties.Settings.Default.mesPort         = SettingData.MesPort;
            Properties.Settings.Default.mesDevNum       = SettingData.MesDevNum;
            Properties.Settings.Default.pcServerIP      = SettingData.PcServerIP;
            Properties.Settings.Default.pcServerPort    = SettingData.PcServerPort;
            Properties.Settings.Default.tmIP            = SettingData.TmIP;
            Properties.Settings.Default.tmModbusPort    = SettingData.TmModbusPort;

            /* Save settings in config file */
            Properties.Settings.Default.Save();
            
        }
        #endregion Functions
    }

    internal class SettingData
    {
        public static string MesIP { get; set; }
        public static string MesPort { get; set; }
        public static string MesDevNum { get; set; }
        public static string TmIP { get; set; }
        public static string TmModbusPort { get; set; }
        public static string PcServerIP { get; set; }
        public static string PcServerPort { get; set; }
        public static string DataResetFreq { get; set; }
    }
}
