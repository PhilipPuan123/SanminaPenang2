using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesConduitResponse
{
    //Json to CSharp format convertion for conduit data
    public class RVISConduitDataResponse
    {
        public RVISConduitDataResponse()
        {
            source = new Source();
            status = new Status();
            transaction_responses = new List<TransactionResponse>();
            version = "";
        }
        public Source source { get; set; }
        public Status status { get; set; }
        public List<TransactionResponse> transaction_responses { get; set; }
        public string version { get; set; }
    }
    public class Workstation
    {
        public string station { get; set; }
        public string type { get; set; }
    }
    public class Source
    {
        public string client_id { get; set; }
        public string employee { get; set; }
        public Workstation workstation { get; set; }
    }
    public class Status
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Status2
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Unit
    {
        public string part_number { get; set; }
        public string quantity { get; set; }
        public string revision { get; set; }
        public string unit_id { get; set; }
    }
    public class UnitInfo
    {
        public object auto_commands { get; set; }
        public List<object> command_template { get; set; }
        public string finisher_executed { get; set; }
        public string message { get; set; }
        public List<object> scanning_template { get; set; }
        public string status { get; set; }
    }
    public class ScannedUnit
    {
        public Status2 status { get; set; }
        public Unit unit { get; set; }
        public UnitInfo unit_info { get; set; }
    }
    public class Status3
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class TransactionResponse
    {
        public object command_responses { get; set; }
        public ScannedUnit scanned_unit { get; set; }
        public Status3 status { get; set; }
    }
    public class RootObject
    {
        public Source source { get; set; }
        public Status status { get; set; }
        public List<TransactionResponse> transaction_responses { get; set; }
        public string version { get; set; }
    }
}
