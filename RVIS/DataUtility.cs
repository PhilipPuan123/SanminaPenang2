using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RVISData;

namespace RVIS
{
    internal class DataUtility
    {
        public static void UpdateSettingDataFromConfig()
        {
            /* 42Q Configuration */
            SettingData.MesIP           = Properties.Settings.Default.mesIP;
            SettingData.MesPort         = Properties.Settings.Default.mesPort;
            SettingData.MesDevNum       = Properties.Settings.Default.mesDevNum;
            SettingData.LocalServerPath = Properties.Settings.Default.localServerPath;
            /* TM Configuration */
            SettingData.TmIP            = Properties.Settings.Default.tmIP;
            SettingData.TmModbusPort    = Properties.Settings.Default.tmModbusPort;
            /* PC Configuration */
            SettingData.PcServerIP      = Properties.Settings.Default.pcServerIP;
            SettingData.PcServerPort    = Properties.Settings.Default.pcServerPort;
            SettingData.DataResetFreq   = Properties.Settings.Default.dataResetFreq;
        }

        public static void UpdateSpecialSettingDataFromConfig()
        {
            /* SQL setting */
            SpecialSettingData.SqlDataSource        = Properties.Settings.Default.SqlDataSource;
            SpecialSettingData.SqlAttachDbFilename  = Properties.Settings.Default.SqlAttachDbFilename;
            /* Software Directory */
            SpecialSettingData.UIImageLoadPath  = Properties.Settings.Default.UIImageLoadPath;
            SpecialSettingData.TMImageSavePath  = Properties.Settings.Default.TMImageSavePath;
        }
    }
}
