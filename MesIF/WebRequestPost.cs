using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace MesIF
{
    public class WebRequestPost
    {
        private string uri;
        private byte[] writeData;

        public string URI
        {
            get { return uri; }
            set { uri = value; }
        }

        public byte[] WriteData
        {
            get { return writeData; }
            set
            { writeData = value; }
        }

        public WebRequestPost(string uri)
        {
            URI = uri;
        }

        public void Main()
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            }
            catch(Exception e)
            {

            }

        }

    }
}
