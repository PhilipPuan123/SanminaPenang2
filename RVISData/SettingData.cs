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
        public static string MesClientID { get; set; }
        public static string MesType { get; set; }
        public static string MesMeasureURL { get; set; }
        public static string MesMeasureResourceName { get; set; }
        public static string MesMeasureService { get; set; }
        public static string MesMeasureSecret { get; set; }
        public static string MesMeasureClientId { get; set; }
        public static string MesMeasureStation { get; set; }
        public static string MesMeasureProcessName { get; set; }
        public static string MesMeasureTestId { get; set; }
       public static string MesMeasureToolingId { get; set; }
        public static string MesMeasureRevision { get; set; }
        public static string MesConduitURL { get; set; }
        public static string MesConduitStation { get; set; }
        public static string MesConduitClientId { get; set; }
        public static string MesConduitParserToken { get; set; }

        /* TM Configuration */
        public static string TmIP { get; set; }
        public static string TmModbusPort { get; set; }
        /* PC Configuration */
        public static string PcServerIP { get; set; }
        public static string PcServerPort { get; set; }
        public static string DataResetFreq { get; set; }
        public static string mesController { get; set; }
    }
}
