using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GetWeatherInfo_demo
{

    class Program
    {
        static void Main(string[] args)
        {
            var AuthKey = "CWB-5C319909-BAA2-418D-99EE-79A59D452440";
            Console.WriteLine("***** Demo Weather info from CWB opendata.");
            Console.WriteLine("***** I use my authkey for this, may changed someday.");
            Console.WriteLine("***** Please note env is framework 4.5, ");
            Console.WriteLine("***** be sure installed if run on WinServer 2008.");
            Console.WriteLine("***** Also notice I use newton json.");
            Console.WriteLine("\n");

            GetSunRiseSetInfo SunRiseSetInfo_Data = new GetSunRiseSetInfo(AuthKey);                 //A-B0062-001
            GetGeneralInfo GeneralInfo_Data = new GetGeneralInfo(AuthKey);                          //F-D0047-093 /65_72hr_CH.xml
            GetRainAccumulatedInfo RainAccumulatedInfo_data = new GetRainAccumulatedInfo(AuthKey);  //O-A0002-001
            LocationDataSet DataSetList = new LocationDataSet();
            DataSetList.CreateDataSetList(SunRiseSetInfo_Data.GetData(), RainAccumulatedInfo_data.GetData());
            List<DataSet> WeatherDataList = DataSetList.GetWeatherInfoList();
            ListAllWeatherInfo_NTPC(WeatherDataList);
            
        }

        private static void ListAllWeatherInfo_NTPC(List<DataSet> WeatherDataList)
        {
            foreach (DataSet _cell in WeatherDataList)
            {
                Console.WriteLine("\n[" + _cell.Location + "]===");
                Console.WriteLine("氣溫：" + _cell.Temperature);
                Console.WriteLine("體感溫度：" + _cell.Apparent_temperature);
                Console.WriteLine("濕度：" + _cell.Humidity);
                Console.WriteLine("時累計雨量：" + _cell.Rain_Hour);
                Console.WriteLine("3小時溫度預測：" + _cell.Forecast_3Hour);
                Console.WriteLine("6小時內降雨機率：" + _cell.RainProb_6Hour);
                Console.WriteLine("日出時間：" + _cell.SunRiseTime);
                Console.WriteLine("日落時間：" + _cell.SunSetTime);
                Console.WriteLine("天氣描述代碼：" + _cell.WeatherCode);
                Console.WriteLine("天氣描述：" + _cell.WeatherDescription);
            }
            
        }

    }
}
