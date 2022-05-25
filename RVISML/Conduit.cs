///A-0003 s

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MesConduit;
using Newtonsoft.Json;
using RVISData;

namespace RVISMMC
{
    public class Conduit
    {
        #region Conduit Payload
        public static void Parser(RVISConduitData root)
        {
            root.source.client_id = RVISData.SettingData.MesConduitClientId;
            root.source.employee = RVISData.UserData.ID;
            root.source.password = "";
            root.source.workstation = new Workstation();
            root.source.workstation.type = RVISData.SettingData.MesType;
            root.source.workstation.station = RVISData.SettingData.MesConduitStation;
            root.version = "1.0";
            root.token = "";
            root.keep_alive = false;
            root.single_transaction = false;
            root.transactions = new List<Transaction>();
            TransactionData(root.transactions);
        }
        public static void TransactionData(List <Transaction> trans)
        {
            RVISML RVISMLData = new RVISML(); //use for getting serial #

            Transaction transdata = new Transaction();
            transdata.unit = new Unit();
            transdata.unit.unit_id = RVISData.GuiDataExchange.serialNumber;
            transdata.unit.part_number = "";
            transdata.unit.quantity = "-1"; 
            transdata.unit.revision = "";
            transdata.commands = new List<object>();
            transdata.refresh_unit = false;
            trans.Add(transdata);
        }
        #endregion

    }

}
