using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RVIS
{
    public partial class FrmSpecialSetting : Form
    {
        private bool settingErr = false;

        public FrmSpecialSetting()
        {
            InitializeComponent();
        }

        private void FrmSpecialSetting_Load(object sender, EventArgs e)
        {
            LoadSettingFromConfigFile();
            CheckAllInputs();
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
                /* Close form */
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckAllInputs()
        {
            settingErr = false;

            /* Check SQL Config Settings */
            CheckSQLDataSource(txtDataSource, lblDataSourceErr);
            CheckSQLAttachDBFilename(txtAttachDBFilename, lblAttachDBFilenameErr);
        }

        private void CheckSQLDataSource(TextBox dataSource, Label lblErr)
        {
            bool error = false;
            if (string.IsNullOrEmpty(dataSource.Text))
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

        private void CheckSQLAttachDBFilename(TextBox filename, Label lblErr)
        {
            bool error = false;
            if (string.IsNullOrEmpty(filename.Text))
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

        private void LoadSettingFromConfigFile()
        {
            txtDataSource.Text          = Properties.Settings.Default.SqlDataSource;
            txtAttachDBFilename.Text    = Properties.Settings.Default.SqlAttachDbFilename;
        }

        private void SaveSettingToConfigFile()
        {
            Properties.Settings.Default.SqlDataSource       = txtDataSource.Text;
            Properties.Settings.Default.SqlAttachDbFilename = txtAttachDBFilename.Text;
        }

    }
}
