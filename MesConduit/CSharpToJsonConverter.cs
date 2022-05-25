using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MesConduit
{
    //object required for json format conversion based on  customer payload format
    public class RVISConduitData
    {
        public RVISConduitData()
        {
            // Initialize all value
            source = new Source();
            version = "";
            token = "";
            keep_alive = false;
            single_transaction = false;
            transactions = new List<Transaction>();
        }
        public Source source { get; set; }
        public string version { get; set; }
        public string token { get; set; }
        public bool keep_alive { get; set; }
        public bool single_transaction { get; set; }
        public List<Transaction> transactions { get; set; }
    }
    public class Workstation
    {
        public string type { get; set; }
        public string station { get; set; }

    }
    public class Source
    {
        public string client_id { get; set; }
        public string employee { get; set; }
        public string password { get; set; }
        public Workstation workstation { get; set; }

    }
     public class Unit
    {
        public string unit_id { get; set; }
        public string part_number { get; set; }
        public string quantity { get; set; }
        public string revision { get; set; }
    }
    public class Transaction
    {
        public Unit unit { get; set; }
        public List<object> commands { get; set; }
        public bool refresh_unit { get; set; }

    }
    public class RootObject
    {
        public Source source { get; set; }
        public string version { get; set; }
        public string token { get; set; }
        public bool keep_alive { get; set; }
        public bool single_transaction { get; set; }
        public List<Transaction> transactions { get; set; }
    }
}
