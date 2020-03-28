using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace GetWeatherInfo_demo
{
    class SunTime
    {
        public string dataTime { get; set; }
        public string SunRise { get; set; }
        public string SunSet { get; set; }
    }
    
    class Rain
    {
        public string locationName { get; set; }
        public string value { get; set; }
    }

    class GeneralInfo
    {
        public string Location { get; set; }
        public string Temperature { get; set; }
        public string Temperature_3Hr_Later { get; set; }
        public string Apparent_temperature { get; set; }
        public string RainProb_6Hour { get; set; }
        public string Humidity { get; set; }

    }

    class LocationDataSet
    {
        private string XmlFilePath = @"GeneralInfo\F-D0047-093\65_72hr_CH.xml";
        private static List<DataSet> DataSet_List = new List<DataSet>();
        private JObject RainAccumulatedInfo;
        private JObject SunRiseSetInfo;
        private JObject GeneralInfo;
        
        public void CreateDataSetList(string _SunRiseSetInfo, string _RainAccumulatedInfo)
        {
            SunRiseSetInfo = JObject.Parse(_SunRiseSetInfo);
            RainAccumulatedInfo = JObject.Parse(_RainAccumulatedInfo);

            //Set SunRise and Set info ========================
            IList<JToken> SunRiseSetResults = SunRiseSetInfo["cwbopendata"]["dataset"]["locations"]["location"][1]["time"].Children().ToList();
            //[1] means New Taipei City, please see the structure of json file A-B0062-001.

            IList<SunTime> suntime_list = new List<SunTime>();
            foreach (JToken result in SunRiseSetResults)
            {
                SunTime _st = new SunTime();
                _st.dataTime = result["dataTime"].ToString();
                _st.SunRise = result["parameter"][1]["parameterValue"].ToString();
                _st.SunSet = result["parameter"][5]["parameterValue"].ToString();
                suntime_list.Add(_st);
            }

            //Set rain info ========================
            IList<JToken> RainAccumulateResults = RainAccumulatedInfo["cwbopendata"]["location"].Children().ToList();
            IList<Rain> RainAccumulate_list = new List<Rain>();
            foreach (JToken result in RainAccumulateResults)
            {
                Rain _rain = new Rain();
                _rain.locationName = result["locationName"].ToString();
                _rain.value = result["weatherElement"][1]["elementValue"]["value"].ToString();
                RainAccumulate_list.Add(_rain);
            }

            //Set General info ========================
            XmlDocument XmlSource = new XmlDocument();
            XmlSource.Load(XmlFilePath);
            string _json = JsonConvert.SerializeXmlNode(XmlSource);
            GeneralInfo = JObject.Parse(_json);
            IList<JToken> GeneralResults = GeneralInfo["cwbopendata"]["dataset"]["locations"]["location"].Children().ToList();
            IList<GeneralInfo> General_list = new List<GeneralInfo>();
            foreach(JToken result in GeneralResults)
            {
                GeneralInfo _generalResult = new GeneralInfo();
                _generalResult.Location = result["locationName"].ToString();
                _generalResult.Temperature = result["weatherElement"][0]["time"][0]["elementValue"]["value"].ToString();
                _generalResult.Temperature_3Hr_Later = result["weatherElement"][0]["time"][1]["elementValue"]["value"].ToString();
                _generalResult.Apparent_temperature = result["weatherElement"][8]["time"][0]["elementValue"]["value"].ToString();
                _generalResult.RainProb_6Hour = result["weatherElement"][3]["time"][0]["elementValue"]["value"].ToString();
                _generalResult.Humidity = result["weatherElement"][2]["time"][0]["elementValue"]["value"].ToString();
                General_list.Add(_generalResult);
            }

            //combine all list to one dataset ========================
            foreach (GeneralInfo _cell in General_list)
            {
                try
                {
                    DataSet _ds = new DataSet();
                    _ds.Location = _cell.Location;
                    _ds.Temperature = _cell.Temperature;
                    _ds.Apparent_temperature = _cell.Apparent_temperature;
                    _ds.Forecast_3Hour = _cell.Temperature_3Hr_Later;
                    _ds.RainProb_6Hour = _cell.RainProb_6Hour;
                    _ds.Humidity = _cell.Humidity;
                    for (int i = 0; i < RainAccumulate_list.Count; i++)
                    {
                        string _temp = RainAccumulate_list[i].locationName + "區";
                        if (_temp == _ds.Location)
                            _ds.Rain_Hour = RainAccumulate_list[i].value;
                    }
                    for (int i = 0; i < RainAccumulate_list.Count; i++)
                    {
                        if (suntime_list[i].dataTime == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            _ds.SunRiseTime = suntime_list[i].SunRise;
                            _ds.SunSetTime = suntime_list[i].SunSet;
                        }
                    }
                    DataSet_List.Add(_ds);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not found date : " + _cell.Location + "\n");
                }
            }

        }
        public List<DataSet> GetWeatherInfoList()
        {
            return DataSet_List;
        }

    }
}
