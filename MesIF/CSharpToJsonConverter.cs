using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MesIF
{
    /*
    public class RVISData
    {
        public RVISData()
        {
            // Initialize all value
            serial = "";
            station = "";
            @operator = "";
            status = "";
            test_steps = new TestSteps();


        }

        public string serial { get; set; }
        public string station { get; set; }
        public string @operator{ get; set; }
        public string status { get; set; }
        public TestSteps test_steps { get; set; }
    }

    public class RootObject
    {
        /// <summary>Used to identify the unit being processed.</summary>
        public string serial { get; set; }

        /// <summary>Generally used as the customer’s part number, or the product identification.</summary>
        public string product { get; set; }

        /// <summary>Type or nature of the test. E.g. Functional, Inspection, ICT, AOI.</summary>
        public string test_id { get; set; }

        /// <summary>Software version identification used to produce the data result. 0-</summary>
        public string software_id { get; set; }

        /// <summary>Software revision that identifies the application used to produce the data result.</summary>
        public string software_rev { get; set; }

        /// <summary>
        /// Test station identification. Measurement uses to identify from where the test result was 
        /// generated, if a station is not set in Measurement, it uses this name and the process_name
        /// (if provided) to set a new one.
        /// </summary>
        public string station { get; set; }

        /// <summary>Operator’s badge number/identification used to log in SFDC or Conduit.</summary>
        public string @operator { get; set; }

        /// <summary>Operator’s password used to log in on Conduit when Employee Validation is enabled.</summary>
        public string password { get; set; }

        /// <summary>Date and time that the test started.</summary>
        public string start_time { get; set; }

        /// <summary>Date and time that the test finished.</summary>
        public string end_time { get; set; }

        /// <summary>The MDS process name used to process the unit on Conduit. The process_name must be
        /// the same process where the unit is at the moment.</summary>
        public string process_name { get; set; }

        /// <summary>Operation type. Possible values are: PRODUCTION, ENGINEERING, TESTING and SKIPPING.</summary>
        public string type { get; set; }

        public string comment { get; set; }
        public string status { get; set; }
        public string error_code { get; set; }
        public double measkey { get; set; }
        public Tooling fixture { get; set; }
        public string fixture_position { get; set; }
        public json pm_data { get; set; }
        public TestSteps test_steps { get; set; }
        public jsonArray commands { get; set; }
    }

    public class Tooling
    {
        public string tooling_id { get; set; }
        public string revision { get; set; }
    }

    public class TestSteps
    {
        public List<TestStepItem> TestGroup1;
        public List<TestStepItem> TestGroup2;
        public List<TestStepItem> TestGroup3;
    }

    public class TestStepItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public string comparator { get; set; }
        public double lowLimit { get; set; }
        public double lowControlLimit { get; set; }
        public double highLimit { get; set; }
        public double highControlLimit { get; set; }
        public string expected { get; set; }
        public string units { get; set; }
        public string status { get; set; }
        public string comment { get; set; }
        public string value { get; set; }
        public int sequence { get; set; }
        public Tooling tooling { get; set; }
    }

    public class json
    {
        // TO-DO: Impelement json class
    }

    public class jsonArray
    {
        // TO-DO: Impelement json class
    }
    */
}
