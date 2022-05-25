using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesMeasurementResponse
{
    //Json to Csharp convertion after received json response from 42Q
    public class RVISMeasurementDataResponse
    {
        public RVISMeasurementDataResponse()
        {
            success = false;
            message = "";
            data = new List<object>();
        }
        public bool success { get; set; }
        public string message { get; set; }
        public List<object> data { get; set; }
    }
    public class RootObject
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<object> data { get; set; }
    }
}

