using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetWeatherInfo_demo
{
    class GetRainAccumulatedInfo
    {
        private string Data = "";
        public GetRainAccumulatedInfo(string AuthKey)
        {
            var _url = "https://opendata.cwb.gov.tw/fileapi/v1/opendataapi/O-A0002-001?Authorization=" + AuthKey + "&format=JSON";
            GetData RAI = new GetData(_url);
            Data = RAI.GetResult();
        }

        public string GetData()
        {
            return Data;
        }
    }
}
