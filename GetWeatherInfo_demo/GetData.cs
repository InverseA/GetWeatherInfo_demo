using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GetWeatherInfo_demo
{
    class GetData
    {
        private string Result = "";
        public GetData(string _url)
        {
            HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_url);
            HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
            using (StreamReader sr = new StreamReader(_response.GetResponseStream()))
            {
                Result = sr.ReadToEnd();
                sr.Close();
            }
        }
        
        public string GetResult()
        {
            return Result;
        }
    }
}
