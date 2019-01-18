using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Cryptography;
using System.Data.SqlClient;

namespace RVIS
{
    public partial class FrmAddRemoveUser : Form
    {
        #region Declaration
        SqlConnection sqlCon = new SqlConnection(UserDataSetting.CONNECTION_STRING);
        SqlCommand sqlCmd;
        SqlDataAdapter sqlAdapter;
        #endregion Declarationk

        public FrmAddRemoveUser()
        {
            InitializeComponent();
            dgvUserData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUserData.AllowUserToResizeColumns = false;
            dgvUserData.AllowUserToResizeRows = false;
            DisplayData();
        }

        #region Form Control
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string userId = txtUserID.Text;
            string password = txtPassword.Text;
            bool admin = chkAdmin.Checked;
            /* Check if User ID length is correct */
            if (IsValidUserID(userId) != true)
            {
                txtUserID.Focus();
                return;
            }
            /* Check if password length is correct */
            if (IsValidPassword(password) != true)
            {
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }
            /* Generate salt based on prefix and userId */
            byte[] salt = Security.GenerateSalt(UserDataSetting.SALT_STRING, userId);
            string passwordHash = Security.GenerateHashString(Encoding.ASCII.GetBytes(password), salt, UserDataSetting.HASH_ITERATION, UserDataSetting.HASH_LENGTH);

            if (IsUserIDExists(userId))
            {
                MessageBox.Show("User ID already exists. Please insert different User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
                return;
            }
            else if (AddUserToSQL(userId, passwordHash, admin) != 0)
            {
                txtUserID.Focus();
                return;
            }
            DisplayData();
            ClearData();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string userId = txtUserID.Text;
            /* Check if User ID length is correct */
            if (IsValidUserID(userId) != true)
            {
                txtUserID.Focus();
                return;
            }

            if (IsUserIDExists(userId) != true)
            {
                MessageBox.Show("User ID does not exists. Please select User ID from the table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserID.Focus();
            }
            else if (MessageBox.Show("Are you sure you want to remove this user?\n" + userId, "Remove user", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {

            }
            else if (RemoveUserFromSQL(userId) != 0)
            {
                txtUserID.Focus();
            }
            else
            {
                ClearData();
            }
            DisplayData();
        }

        private void dgvUserData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int colIndexUserId = (int)UserDataSetting.DB_COL_INDEX.USER_ID;
            int colIndexIsAdmin = (int)UserDataSetting.DB_COL_INDEX.IS_ADMIN;

            txtUserID.Text = dgvUserData.Rows[e.RowIndex].Cells[colIndexUserId].Value.ToString();
            chkAdmin.Checked = Convert.ToBoolean(dgvUserData.Rows[e.RowIndex].Cells[colIndexIsAdmin].Value);
        }
        #endregion Form Control

        #region Functions
        #region SQL Control
        private void DisplayData()
        {
            try
            {
                sqlCon.Open();
                DataTable dt = new DataTable();
                sqlAdapter = new SqlDataAdapter("SELECT * FROM Users", sqlCon);
                sqlAdapter.Fill(dt);
                dgvUserData.DataSource = dt;
                /* Hide password column */
                dgvUserData.Columns["password"].Visible = false;
                /* Disable cell select */
                dgvUserData.Columns["user_id"].ReadOnly = true;
                dgvUserData.Columns["isAdmin"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                /* If SQL connection is open, close it. */
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
        private bool IsUserIDExists(string userId)
        {
            try
            {
                /* Check if user id existed in database */
                sqlCmd = new SqlCommand("SELECT * FROM Users WHERE user_id = @user_id", sqlCon);
                SqlParameter uID = new SqlParameter("@user_id", SqlDbType.VarChar);
                uID.Value = userId;
                sqlCmd.Parameters.Add(uID);
                sqlCmd.Connection.Open();

                using (SqlDataReader sqlDataReader = sqlCmd.ExecuteReader())
                {
                    /* If matched result is found */
                    if (sqlDataReader.HasRows)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return false;
        }

        private int AddUserToSQL(string userId, string password, bool isAdmin)
        {
            try
            {
                /* Insert new user into database */
                sqlCmd = new SqlCommand("INSERT INTO Users(user_id,password,isAdmin) values(@user_id,@password,@isAdmin)", sqlCon);
                sqlCmd.Parameters.AddWithValue("@user_id", userId);
                sqlCmd.Parameters.AddWithValue("@password", password);
                sqlCmd.Parameters.AddWithValue("@isAdmin", isAdmin);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            MessageBox.Show("User ID successfully registered!");
            return 0;
        }

        private int RemoveUserFromSQL(string userId)
        {
            try
            {
                /* Delete user from database */
                sqlCmd = new SqlCommand("DELETE Users where user_id = @user_id", sqlCon);
                sqlCmd.Parameters.AddWithValue("@user_id", userId);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            MessageBox.Show("User ID successfully removed!");
            return 0;
        }

        private int UpdateUserData(string userId, string password, bool isAdmin)
        {
            try
            {
                /* Update data based on user id */
                sqlCmd = new SqlCommand("UPDATE Users SET password = @password, isAdmin = @isAdmin WHERE user_id = @user_id", sqlCon);
                sqlCmd.Parameters.AddWithValue("@user_id", userId);
                sqlCmd.Parameters.AddWithValue("@password", password);
                sqlCmd.Parameters.AddWithValue("@isAdmin", isAdmin);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return 0;
        }
        #endregion SQL Control
                
        private void ClearData()
        {
            txtUserID.Clear();
            txtPassword.Clear();
            chkAdmin.Checked = false;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private bool IsValidUserID(string userId)
        {
            /* If user id is empty or incorrect length */
            if ((userId == "") ||
                (userId.Length < UserDataSetting.USER_ID_MIN_LENGTH) ||
                (userId.Length > UserDataSetting.USER_ID_MAX_LENGTH))
            {
                MessageBox.Show("Please enter a valid User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            /* If user id contains characters other than numbers */
            else if (IsDigitsOnly(userId) != true)
            {
                MessageBox.Show("Please enter a valid User ID.\nUser ID should contains numbers only.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool IsValidPassword(string password)
        {
            /* If password is empty or incorrect length */
            if ((password == "") ||
                (password.Length < UserDataSetting.PASSWORD_MIN_LENGTH) ||
                (password.Length > UserDataSetting.PASSWORD_MAX_LENGTH))
            {
                MessageBox.Show("Please enter a valid password. (Password must be 8~16 characters)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion Functions
    }
}
