using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVIS
{
    internal enum CONNECTION_STS
    {
        Disconnected = 0,
        Connecting,
        Connected
    };
    internal class UserDataSetting
    {
        /* User id and password length */
        public const int USER_ID_MIN_LENGTH = 5;
        public const int USER_ID_MAX_LENGTH = 5;
        public const int PASSWORD_MIN_LENGTH = 8;
        public const int PASSWORD_MAX_LENGTH = 16;
        /* Service Key */
        public const string SERVICEMAN_USER_ID = "PIDSM";//"PFSAP"
        public const string SERVICEMAN_PASSWORD = "service";// kr1st1n4f4y3

    }
    /*  DO NOT CHANGE THIS
     *  Changing this will affect existing password stored in database */
    internal class AuthenticationConstant
    {
        public const int HASH_ITERATION = 100;                  // Number of iteration of encryption
        public const int HASH_LENGTH    = 16;                   // Length of Hash
        public const string SALT_STRING_PREFIX = "S4nm1n4";     // Prefix for salt string
        
        /* SQL column index */
        public enum DB_COL_INDEX : UInt16
        {
            USER_ID = 0,
            PASSWORD = 1,
            IS_ADMIN = 2
        };
    }
}
