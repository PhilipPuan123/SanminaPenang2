using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVISDev
{
    internal class UserDataSetting
    {
        /* User id and password length */
        public const int USER_ID_MIN_LENGTH = 5;
        public const int USER_ID_MAX_LENGTH = 5;
        public const int PASSWORD_MIN_LENGTH = 8;
        public const int PASSWORD_MAX_LENGTH = 16;
        /* Service Key */
        public const string SERVICEMAN_USER_ID = "PFSAP";
        public const string SERVICEMAN_PASSWORD = "kr1st1n4f4y3";
        /* Hash setting - DO NOT CHANGE THIS
         * Chaning this will affect existing password stored in database */
        public const int HASH_ITERATION = 100;
        public const string SALT_STRING = "S4nm1n4";
        public const int HASH_LENGTH = 16;
        /* SQL database setting */
        //public const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\RVIS_DB.mdf;Integrated Security=True;Connect Timeout=30";
        public const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\12127\Documents\Visual Studio 2017\Projects\Work\RVISDev\RVISDev\db\RVIS_DB.mdf"";Integrated Security=True";
        //public const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\Visual Studio 2017\Projects\Work\RVISDev\RVISDev\db\RVIS_DB.mdf"";Integrated Security=True";

        /* SQL column index */
        public enum DB_COL_INDEX : UInt16
        {
            USER_ID = 0,
            PASSWORD = 1,
            IS_ADMIN = 2
        };
    }

    internal static class UserData
    {
        public static string ID { get; set; }
        public static string Password { get; set; }
        public static bool IsAdmin { get; set; }
    }
}
