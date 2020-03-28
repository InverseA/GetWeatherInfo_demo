using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWeatherInfo_demo
{
    class DataSet
    {
        public string Location { get; set; }               //地區
        public string Temperature { get; set; }            //氣溫
        public string Apparent_temperature { get; set; }   //體感溫度
        public string Forecast_3Hour { get; set; }         //3小時溫度預測
        public string RainProb_6Hour { get; set; }         //6小時內降雨機率
        public string Humidity { get; set; }               //濕度
        public string Rain_Hour { get; set; }              //時累計雨量
        public string SunRiseTime { get; set; }            //日出時間
        public string SunSetTime { get; set; }             //日落時間
        public string WeatherCode { get; set; }            //天氣描述代碼
        public string WeatherDescription { get; set; }     //天氣描述
        
    }
}
