using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace GetWeatherInfo_demo
{
    class GetGeneralInfo
    {
        public GetGeneralInfo(string AuthKey)
        {
            string _path = @"GeneralInfo";
            if (Directory.Exists(_path))
            {
                Directory.Delete(_path, true);
            }
            var _url = "https://opendata.cwb.gov.tw/fileapi/v1/opendataapi/F-D0047-093?Authorization=" + AuthKey + "&format=ZIP";
            using(WebClient wc = new WebClient())
            {
                Directory.CreateDirectory(_path);                
                wc.DownloadFile(_url, _path + "\\F-D0047-093.zip");
                ZipFile.ExtractToDirectory(_path + "\\F-D0047-093.zip", _path + "\\F-D0047-093");
            }
        }
    }
}
