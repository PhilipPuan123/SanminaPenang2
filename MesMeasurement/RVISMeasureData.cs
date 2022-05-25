using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesMeasurement
{
    //Csharp to json data convertion based on customer payload format
    public class RVISMeasureData
    {
        public RVISMeasureData()
        {
            test_steps = new TestSteps();
            serial = "";
            product = "";
            test_id = "PAN01";
            software_id = "";
            software_rev = "";
            station = "FLTS";
            process_name = "FLATNESS MEASUREMENT";
            status = "";
            error_code = "PAN01";
            type = "PRODUCTION";
            @operator = "";
            password = "";
            start_time = "";
            end_time = "";
            comment = "";
            fixture = new Fixture();
            fixture_position = "";
            flex_header_field = "";
            flex_header_field1 = "";

        }
        public TestSteps test_steps { get; set; }
        public string serial { get; set; }
        public string product { get; set; }
        public string test_id { get; set; }
        public string software_id { get; set; }
        public string software_rev { get; set; }
        public string station { get; set; }
        public string process_name { get; set; }
        public string status { get; set; }
        public string error_code { get; set; }
        public string type { get; set; }
        public string @operator { get; set; }
        public string password { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string comment { get; set; }
        public Fixture fixture { get; set; }
        public string fixture_position { get; set; }
        public string flex_header_field { get; set; }
        public string flex_header_field1 { get; set; }

    }
    public class RoboticInspectionSystem
    {
        public string name { get; set; }
        public string description { get; set; }
        public string comparator { get; set; }
        public string lowLimit { get; set; }
        public string highLimit { get; set; }
        public string expected { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
        public string status { get; set; }
        public string comment { get; set; }
        public string sequence { get; set; }
    }

    public class TestSteps
    {
        public List<RoboticInspectionSystem> Robotic_Inspection_System { get; set; }
    }

    public class Fixture
    {
        public string tooling_id { get; set; }
        public string revision { get; set; }

        public Fixture()
        {
            tooling_id = "1";
            revision = "0";
        }

    }

    public class RootObject
    {
        public TestSteps test_steps { get; set; }
        public string serial { get; set; }
        public string product { get; set; }
        public string test_id { get; set; }
        public string software_id { get; set; }
        public string software_rev { get; set; }
        public string station { get; set; }
        public string process_name { get; set; }
        public string status { get; set; }
        public string error_code { get; set; }
        public string type { get; set; }
        public string @operator { get; set; }
        public string password { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string comment { get; set; }
        public Fixture fixture { get; set; }
        public string fixture_position { get; set; }
        public string flex_header_field { get; set; }
        public string flex_header_field1 { get; set; }
    }
    
 

}
