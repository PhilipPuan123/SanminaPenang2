using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVIS
{
    internal static class UserData
    {
        public static string ID { get; set; }
        public static string Password { get; set; }
        public static AccessLevel UserAccess { get; set; } = AccessLevel.User;
    }

    internal static class SettingData
    {
        public static string MesIP { get; set; }
        public static string MesPort { get; set; }
        public static string MesDevNum { get; set; }
        public static string LocalServerPath { get; set; }
        public static string TmIP { get; set; }
        public static string TmModbusPort { get; set; }
        public static string PcServerIP { get; set; }
        public static string PcServerPort { get; set; }
        public static string DataResetFreq { get; set; }
    }
}
