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
            SettingData.MesClientID     = Properties.Settings.Default.mesClientID;
            SettingData.MesType         = Properties.Settings.Default.mesType;
            SettingData.MesMeasureURL = Properties.Settings.Default.mesMeasureURL;
            SettingData.MesMeasureResourceName = Properties.Settings.Default.mesMeasureResourceName;
            SettingData.MesMeasureService = Properties.Settings.Default.mesMeasureService;
            SettingData.MesMeasureSecret = Properties.Settings.Default.mesMeasureSecret;
            SettingData.MesMeasureClientId = Properties.Settings.Default.mesMeasureClientId;
            SettingData.MesMeasureStation = Properties.Settings.Default.mesMeasureStation;
            SettingData.MesMeasureProcessName = Properties.Settings.Default.mesMeasureProcessName;
            SettingData.MesMeasureTestId = Properties.Settings.Default.mesMeasureTestId;
            SettingData.MesMeasureToolingId = Properties.Settings.Default.mesMeasureToolingId;
            SettingData.MesMeasureRevision = Properties.Settings.Default.mesMeasureRevision;
            SettingData.MesConduitURL = Properties.Settings.Default.mesConduitURL;
            SettingData.MesConduitClientId = Properties.Settings.Default.mesConduitClientId;
            SettingData.MesConduitStation = Properties.Settings.Default.mesConduitStation;
            SettingData.MesConduitParserToken = Properties.Settings.Default.mesConduitParserToken;
            /* TM Configuration */
            SettingData.TmIP            = Properties.Settings.Default.tmIP;
            SettingData.TmModbusPort    = Properties.Settings.Default.tmModbusPort;
            /* PC Configuration */
            SettingData.PcServerIP      = Properties.Settings.Default.pcServerIP;
            SettingData.PcServerPort    = Properties.Settings.Default.pcServerPort;
            SettingData.DataResetFreq   = Properties.Settings.Default.dataResetFreq;
            SettingData.mesController = Properties.Settings.Default.MesController;
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
