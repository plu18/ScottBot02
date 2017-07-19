
using Newtonsoft.Json;
using ScottBot02.Models;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScottBot02.Bot
{
    public class ScottBotTask
    {


        public static async Task<WeatherData> GetWeatherAsync(string city)
        {
            try
            {
                string ServiceURL = $"https://free-api.heweather.com/v5/weather?city=" + city + "&lang=en&key=c6226ddb74a74dd2a1cb15f00ab70034";
                string ResultString;
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    ResultString = await client.DownloadStringTaskAsync(ServiceURL).ConfigureAwait(false);
                }
                WeatherData weatherData = (WeatherData)JsonConvert.DeserializeObject(ResultString, typeof(WeatherData));
                return weatherData;
            }
            catch (WebException ex)
            {
                //handle your exception here  
                //throw ex;
                return null;
            }
        }
    }
}