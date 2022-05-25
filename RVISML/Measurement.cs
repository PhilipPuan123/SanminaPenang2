using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RVISData;
using MesMeasurement;

namespace RVISMMC
{
    public class Measurement
    {
       
        public static void MeasurementParser(RVISMeasureData root) 
        {
            RVISML rVISML = new RVISML();
            root.test_steps = new TestSteps();
            root.test_steps.Robotic_Inspection_System = RVISData.GuiDataExchange.inspectionPayload;
            root.serial = RVISData.GuiDataExchange.serialNumber;
            root.product = "";
            root.test_id = RVISData.SettingData.MesMeasureTestId;
            root.software_id = "";
            root.software_rev = "";
            root.station = RVISData.SettingData.MesMeasureStation;
            root.process_name = RVISData.SettingData.MesMeasureProcessName;
            root.status = RVISData.GuiDataExchange.dutResult;
            root.error_code = "PAN01";
            root.type = "PRODUCTION";
            root.@operator = RVISData.UserData.ID; ;
            root.password = RVISData.UserData.Password; ;
            root.start_time = GuiDataExchange.startTime.ToString("yyyy-MM-dd HH:mm:ss");
            root.end_time = GuiDataExchange.stopTime.ToString("yyyy-MM-dd HH:mm:ss");
            root.comment = "";
            root.fixture = new Fixture();
            root.fixture.revision = "0";
            root.fixture.tooling_id = RVISData.SettingData.MesMeasureToolingId;
            root.fixture_position = "";
            root.flex_header_field = "";
            root.flex_header_field1 = "";

        }
    }
}
