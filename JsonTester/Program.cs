using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using MesIF;
//A-0003 Test s
using Newtonsoft.Json;
using MesConduit;
using RVISMMC;
//A-0003 Test e
//A-0032 s measurement data
using MesMeasurement;
using SystemLog;
//A-0032 e measurement data

namespace JsonTester
{
    class Program
    {
 

        static void Main(string[] args)
        {
            // Request2MES(); //A-0003 test for MES conduit data *****
           // ReadResponseFromMES(); //A-0004 test for MES response *****
            //A-0032 s measurement data
            RVISML rVISML = new RVISML();
            for(int i= 0; i<38; i++)
            {
                rVISML.ListTestStepData("NEXSYS_LOGO", "PASSED");
            }
            //RequestMeasurement(); //A-0032 test for MES measurement data *****
           // rVISML.MeasurementCSharp2Json();

            //A-0032 e measurement data
        }

        //A-0003 Parsing data in Json format send to MES
        public static void Request2MES()
        {
            string response = null;
            RVISML rVISML = new RVISML();
            WebRequestREST webRequestREST = new WebRequestREST();

            MesConduit.RVISConduitData conduitData = new RVISConduitData();
            Conduit.Parser(conduitData);
            //A-x1 s
            //var json = JsonConvert.SerializeObject(conduitData, Formatting.Indented);
            var json = "{\"source\": {\"client_id\": \"mp5547dc1_uat\",\"employee\": \"24098\",\"password\": \"\",\"workstation\": {\"type\": \"Device\",\"station\": \"993\"}},\"version\": \"1.0\",\"token\": \"\",\"keep_alive\": false,\"single_transaction\": false,\"transactions\": [{\"unit\": {\"unit_id\": \"PN00004\",\"part_number\": \"MQPMUE4718B\",\"quantity\": \"-1\",\"revision\": \"\"},\"refresh_unit\": true}] }";
            //A-x1 e
            //webRequestREST.SendRequest("http://localhost/", json);
            //HI
            response = webRequestREST.SendRequest("http://medconduat1.sanmina.com:18003/conduit", json);
            rVISML.MESResponseInterpreter(response);
            Console.WriteLine(json);
            Console.ReadKey();
        }

        //A-0004 Interpret response from MES JSON
        public static void ReadResponseFromMES()
        {
            string exampleResponse = "{'source': {'client_id': 'WRONG-CLIENT-ID','employee': '24098','password': '','workstation': {'station': '38','type': 'Device'}},'status': {'code': 'OK','message': 'No enabled client located for client_id: WRONG-CLIENT-ID'},'transaction_responses': [],'version': '1'}";
            //MesConduit.JsonResponseData responseData = JsonConvert.DeserializeObject<JsonResponseData>(exampleResponse);
            //Console.WriteLine(responseData.status.message);
            Console.ReadKey();
        }
        //A-0032 s measurement data test
        public static void RequestMeasurement()
        {

            MesMeasurement.RVISMeasureData measureData = new RVISMeasureData();
            Measurement.MeasurementParser(measureData);
            var json = JsonConvert.SerializeObject(measureData, Formatting.Indented);
            Console.SetBufferSize(600, 600);
            Console.Write(json);
            Console.ReadKey();
           


        }
        //A-0032 e measurement data test

        /* //A-0003 s
                private static void PackData(RVISData root)
                {
                    // Header
                    root.serial = "abcd1234";
                    root.station = "RVIS01";
                    root.status = "PASSED";
                    //root.test_steps = new TestSteps();

                    List<TestStepItem> testStep1List = new List<TestStepItem>();
                    TestStepItem testStep1_1 = new TestStepItem();
                    testStep1_1.name = "Check screw1";
                    testStep1_1.status = "PASSED";
                    testStep1List.Add(testStep1_1);

                    //root.test_steps.TestGroup1 = testStep1List;
                }
                */
    }
}
