using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MesMeasurement;
//A-0033 s
namespace RVISData
{
    public static class GuiDataExchange
    {
        public static DateTime startTime { get; set; }
        public static DateTime stopTime { get; set; }
        public static bool finalSts { get; set; }
        public static bool prjStillrunSts { get; set; }
        public static string serialNumber { get; set; }
        public static List<RoboticInspectionSystem> inspectionPayload = new List<RoboticInspectionSystem>();
        public static string dutResult { get; set; }
    }
}
