using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVISData
{
    public enum AccessLevel
    {
        User = 0,
        Admin = 1,
        Service = 2,
    }

    public static class UserData
    {
        public static string ID { get; set; }
        public static string Password { get; set; }
        public static AccessLevel UserAccess { get; set; } = AccessLevel.User;
    }
}
