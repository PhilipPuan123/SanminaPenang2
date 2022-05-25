using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RVISData;
using MES42Q.Palindrome.SDK;

namespace MesConduit
{
    //webrequest responsible to send and receive data 42Q
    public class WebRequestREST
    {
        #region send request and get response from 42Q without palindrome func
        public string SendRequest(string uri, string jsonString)
        {
            string output = null;
            try
            {
                var client = new RestClient(uri);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                output = response.Content;
            }
            catch (Exception ex)
            {

            }
            return output;
        }
        #endregion

        #region send request and get response from 42Q with palindrome func
        public string SendRequestWithPalindrome(string uri, string jsonString)
        {
            string output = null;
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            var target = TargetResources.At(RVISData.SettingData.MesMeasureURL, RVISData.SettingData.MesMeasureResourceName);
            ClientCredentials credentials = new ClientCredentials(RVISData.SettingData.MesMeasureClientId, DateUtils.FormatIso8601Date(DateTime.UtcNow), RVISData.SettingData.MesMeasureService);
            AuthorizationHeader header = AuthorizationHeader.From(RVISData.SettingData.MesMeasureSecret, credentials, target, jsonString);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", header.ToString());
            IRestResponse response = client.Execute(request);
            output = response.Content;
            return output;
        }
        #endregion
    }
}
