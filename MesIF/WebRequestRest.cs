using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesIF
{
    public class WebRequestRest
    {

        public int SendRequest(string uri,string jsonString)
        {
            try
            {
                //var client = new RestClient(uri);
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("content-type", "application/json");
                //request.AddParameter(data,ParameterType.RequestBody);

                //IRestResponse response = client.Execute(request);

            }
            catch(Exception ex)
            {

            }

            return 0;
        }
    }
}
