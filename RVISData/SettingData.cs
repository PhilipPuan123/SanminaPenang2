using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVISData
{
    public static class SettingData
    {
        /* 42Q Configuration */
        public static string MesIP { get; set; }
        public static string MesPort { get; set; }
        public static string MesDevNum { get; set; }
        public static string LocalServerPath { get; set; }
        /* TM Configuration */
        public static string TmIP { get; set; }
        public static string TmModbusPort { get; set; }
        /* PC Configuration */
        public static string PcServerIP { get; set; }
        public static string PcServerPort { get; set; }
        public static string DataResetFreq { get; set; }
    }
}
