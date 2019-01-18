/* Todo:
 * - SQL database path
 * - Factorize GenerateSalt and GenerateHashString
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

using System.Data.SqlClient;

namespace RVIS
{
    public partial class FrmLogin : Form
    {
        #region Property
        public string UserID { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; private set; }
        #endregion Property
        
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            IsAdmin = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(UserDataSetting.CONNECTION_STRING);
            bool isAuthenticationOK = false;
            bool isSQLError = false;
            string userId = txtUserID.Text;
            string password = txtPassword.Text;

            IsAdmin = false;

            /* If service key is inserted, skip checking authentication from database */
            if (IsServiceKeyInserted(userId, password))
            {   
                IsAdmin = true;
                isAuthenticationOK = true;
            }
            else
            {
                /* If user id is empty or incorrect length */
                if ((userId == "") ||
                    (userId.Length < UserDataSetting.USER_ID_MIN_LENGTH) ||
                    (userId.Length > UserDataSetting.USER_ID_MAX_LENGTH))
                {
                    MessageBox.Show("Please enter a valid User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserID.Focus();
                    return;
                }
                /* If user id contains characters other than numbers */
                else if (IsDigitsOnly(userId) != true)
                {
                    MessageBox.Show("Please enter a valid User ID.\nUser ID should contains numbers only.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserID.Focus();
                    return;
                }

                /* If password is empty or incorrect length */
                if ((password == "") ||
                    (password.Length < UserDataSetting.PASSWORD_MIN_LENGTH) ||
                    (password.Length > UserDataSetting.PASSWORD_MAX_LENGTH))
                {
                    MessageBox.Show("Please enter valid password. (Password must be 8~16 characters)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                /* Generate salt based on prefix and userId */
                byte[] salt = Security.GenerateSalt(UserDataSetting.SALT_STRING, userId);
                string passwordHash = Security.GenerateHashString(Encoding.ASCII.GetBytes(password), salt, UserDataSetting.HASH_ITERATION, UserDataSetting.HASH_LENGTH);

                /* Compare user login info with database */
                try
                {
                    /* Get user id and password from SQL */
                    //SqlCommand sqlCmd = new SqlCommand("SELECT user_id, password FROM Users WHERE user_id = @user_id AND password = @password", sqlCon);
                    SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Users WHERE user_id = @user_id AND password = @password", sqlCon);

                    /* Define new variables in SQL command text */
                    SqlParameter uID = new SqlParameter("@user_id", SqlDbType.VarChar);
                    SqlParameter uPassword = new SqlParameter("@password", SqlDbType.VarChar);
                    /* Assign value into variables */
                    uID.Value = userId;
                    uPassword.Value = passwordHash;
                    sqlCmd.Parameters.Add(uID);
                    sqlCmd.Parameters.Add(uPassword);
                    /* Open SQL connection */
                    sqlCmd.Connection.Open();
                    /* Send command text to connection to build SqlDataReader object */
                    using(SqlDataReader sqlDataReader = sqlCmd.ExecuteReader())
                    {
                        /* If matched result is found */
                        if (sqlDataReader.HasRows)
                        {
                            if (sqlDataReader.Read())
                            {
                                /* Check from Sql data whether the user is admin access */
                                IsAdmin = (bool)sqlDataReader["isAdmin"];
                            }
                            isAuthenticationOK = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    isSQLError = true;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    /* Close SQL connection */
                    if (sqlCon.State == ConnectionState.Open)
                    {
                        sqlCon.Close();
                    }
                }
            }

            /* If user id and password is correct */
            if (isAuthenticationOK)
            {
                /* Return OK */
                UserID = userId;
                Password = password;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else if(!isSQLError)
            {
                MessageBox.Show("User ID and password mismatch.\nPlease enter correct user ID and password.", "Authentication failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
            }
            else
            {
                MessageBox.Show("Unable to access database, please contact administrator.", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Function
        private bool IsServiceKeyInserted(string userId, string password)
        {
            if(userId ==UserDataSetting.SERVICEMAN_USER_ID && password == UserDataSetting.SERVICEMAN_PASSWORD)
            {
                return true;
            }
            return false;
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }


        #endregion Function

    }
}
