using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MesIF;
using Newtonsoft.Json;

namespace JsonTester
{
    class Program
    {
        

        static void Main(string[] args)
        {
            RVISData testData = new RVISData();

            PackData(testData);

            var json = JsonConvert.SerializeObject(testData,Formatting.Indented);

            Console.WriteLine(json);
            Console.ReadKey();
        }


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
    }
}
