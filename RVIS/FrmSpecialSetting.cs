/* To-do:
 * - Automatically create folder if it is not exist.
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

using System.IO;

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

            /* If there is error in any setting input */
            if (settingErr)
            {
                MessageBox.Show("There is error in setting. \nPlease check if the settings are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /* Save settings */
                SaveSettingToConfigFile();
                /* Update common special setting data */
                DataUtility.UpdateSpecialSettingDataFromConfig();
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
            ErrorCheckSQLDataSourceInput(txtDataSource, lblDataSourceErr);
            ErrorCheckSQLAttachDBFilenameInput(txtAttachDBFilename, lblAttachDBFilenameErr);

            /* Check Software Directory */
            ErrorCheckUIImageLoadPath(txtUIImageLoadPath, lblUIImageLoadPathErr);
            ErrorCheckTMImageSavePath(txtTMImageSavePath, lblTMImageSavePathErr);
        }

        private void ErrorCheckSQLDataSourceInput(TextBox dataSource, Label lblErr)
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

        private void ErrorCheckSQLAttachDBFilenameInput(TextBox filename, Label lblErr)
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

        private void ErrorCheckUIImageLoadPath(TextBox dir, Label lblErr)
        {
            bool error = false;
            /* If directory is empty */
            if (string.IsNullOrEmpty(dir.Text))
            {
                error = true;
            }
            /* Create directory if it not exist */
            else if (CreateDirectoryIfNotExist(dir.Text) != true)
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

        private void ErrorCheckTMImageSavePath(TextBox dir, Label lblErr)
        {
            bool error = false;
            /* If directory is empty */
            if (string.IsNullOrEmpty(dir.Text))
            {
                error = true;
            }
            /* Create directory if it not exist */
            else if (CreateDirectoryIfNotExist(dir.Text) != true)
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

        /// <summary>
        /// Create directory if it does not exist. Return true if able to create directory or directory already exists.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private bool CreateDirectoryIfNotExist(string dir)
        {
            if (Directory.Exists(dir) != true)
            {
                DirectoryInfo dirInfo = Directory.CreateDirectory(dir);
                if (dirInfo.Exists != true)
                {
                    return false;
                }
            }
            return true;
        }


        private void LoadSettingFromConfigFile()
        {
            txtDataSource.Text          = Properties.Settings.Default.SqlDataSource;
            txtAttachDBFilename.Text    = Properties.Settings.Default.SqlAttachDbFilename;
            txtUIImageLoadPath.Text     = Properties.Settings.Default.UIImageLoadPath;
            txtTMImageSavePath.Text     = Properties.Settings.Default.TMImageSavePath;
        }

        private void SaveSettingToConfigFile()
        {
            Properties.Settings.Default.SqlDataSource       = txtDataSource.Text;
            Properties.Settings.Default.SqlAttachDbFilename = txtAttachDBFilename.Text;
            Properties.Settings.Default.UIImageLoadPath     = txtUIImageLoadPath.Text;
            Properties.Settings.Default.TMImageSavePath     = txtTMImageSavePath.Text;

            Properties.Settings.Default.Save();
        }
    }
}
