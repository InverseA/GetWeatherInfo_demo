using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace GetWeatherInfo_demo
{
    class GetSunRiseSetInfo
    {
        private string Data = "";
        public GetSunRiseSetInfo(string AuthKey)
        {
            var _url = "https://opendata.cwb.gov.tw/fileapi/v1/opendataapi/A-B0062-001?Authorization=" + AuthKey + "&format=JSON";
            GetData SRSI = new GetData(_url);
            Data = SRSI.GetResult();
        }

        public string GetData()
        {
            return Data;
        }
    }
}
