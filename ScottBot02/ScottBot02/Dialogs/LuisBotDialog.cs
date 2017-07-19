//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Builder.Luis;
//using Microsoft.Bot.Builder.Luis.Models;
//using Microsoft.Bot.Connector;
//using ScottBot02.Bot;
//using ScottBot02.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;


//namespace ScottBot02.Dialogs
//{
//    [Serializable]
//    public class LuisBotDialog : LuisDialog<object>
//    {

//        String weAreTalking = "";

//        public LuisBotDialog()
//        {
//        }

//        public LuisBotDialog(ILuisService service) : base(service)
//        {
//        }

//        [LuisIntent("")]
        
//        public async Task StartAsync(IDialogContext context, LuisResult result)
//        {
//            string message = $"I will only check the weather now. ~T_T!" + string.Join(", ", result.Intents.Select(i => i.Intent));
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }

//        [LuisIntent("")]
//        public async Task None(IDialogContext context, LuisResult result)
//        {
//            String entityInNone = "";
//            string replyString = "";
//            if (weAreTalking.Equals("Weather Check") && TryToFindLocation(result, out entityInNone))
//            {
//                replyString = await GetWeatherAsync(entityInNone);
//                await context.PostAsync(replyString);
//                context.Wait(MessageReceived);
//            }
//            else
//            {
//                replyString = $"I will only check the weather now." + string.Join(", ", result.Intents.Select(i => i.Intent));
//                await context.PostAsync(replyString);
//                context.Wait(MessageReceived);
//            }
//        }

//        [LuisIntent("Hello")]
//        public async Task SayHello(IDialogContext context, LuisResult result)
//        {
//            weAreTalking = "hello";
//            string message = $"Hi, hehe.";
//            await context.PostAsync(message);
//            context.Wait(MessageReceived);
//        }


//        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
//        {
//            var activity = await result as Activity;

//            // calculate something for us to return
//            int length = (activity.Text ?? string.Empty).Length;

//            // return our reply to the user
//            await context.PostAsync($"Scott, You sent {activity.Text} which was {length} characters");

//            context.Wait(MessageReceivedAsync);
//        }



//        public bool TryToFindLocation(LuisResult result, out String location)
//        {
//            location = "";
//            EntityRecommendation title;
//            if (result.TryFindEntity("Location", out title))
//            {
//                location = title.Entity;
//            }
//            else
//            {
//                location = "";
//            }
//            return !location.Equals("");
//        }

//        private async Task<string> GetWeatherAsync(string cityname)
//        {
//            WeatherData weatherdata = await ScottBotTask.GetWeatherAsync(cityname);
//            if (weatherdata == null || weatherdata.HeWeatherdataservice30 == null)
//            {
//                return string.Format("I dont know the weather in \"{0}\" yet.", cityname);
//            }
//            else
//            {
//                HeweatherDataService30[] weatherServices = weatherdata.HeWeatherdataservice30;
//                if (weatherServices.Length <= 0) return string.Format("I dont know the weather in \"{0}\" yet.", cityname);
//                Basic cityinfo = weatherServices[0].basic;
//                if (cityinfo == null) return string.Format("I guess \"{0}\" this is not a city name.", cityname);
//                String cityinfoString = "city information：" + cityinfo.city + "\r\n"
//                    + "update time :" + cityinfo.update.loc + "\r\n"
//                    + "location:" + cityinfo.lat + "," + cityinfo.lon + "\r\n";
//                Aqi cityAirInfo = weatherServices[0].aqi;
//                String airInfoString = "air info:" + cityAirInfo.city.aqi + "\r\n"
//                    + "PM2.5 avarge/hr:" + cityAirInfo.city.pm25 + "(ug/m³)\r\n"
//                    + "PM10 avarge/hr:" + cityAirInfo.city.pm10 + "(ug/m³)\r\n"
//                    + "SO2 avarge/hr:" + cityAirInfo.city.so2 + "(ug/m³)\r\n"
//                    + "NO2 avarge/hr:" + cityAirInfo.city.no2 + "(ug/m³)\r\n"
//                    + "CO avarge/hr:" + cityAirInfo.city.co + "(ug/m³)\r\n";

//                Suggestion citySuggestion = weatherServices[0].suggestion;
//                String suggestionString = "People Suggestion：" + "\r\n"
//                    + "dress suggestion:" + citySuggestion.drsg.txt + "\r\n"
//                    + "UV suggestion:" + citySuggestion.uv.txt + "\r\n"
//                    + "Comfortable suggestion:" + citySuggestion.comf.txt + "\r\n"
//                    + "Traval suggestion:" + citySuggestion.trav.txt + "\r\n"
//                    + "Flu suggestion:" + citySuggestion.flu.txt + "\r\n";

//                Daily_Forecast[] cityDailyForecast = weatherServices[0].daily_forecast;
//                Now cityNowStatus = weatherServices[0].now;
//                String nowStatusString = "Weather:" + "\r\n"
//                    + "Temperature:" + cityNowStatus.tmp + "\r\n"
//                    + "Feel Temperature:" + cityNowStatus.fl + "\r\n"
//                    + "Wind Speed:" + cityNowStatus.wind.spd + "(Kmph)\r\n"
//                    + "Humidity:" + cityNowStatus.hum + "(%)\r\n"
//                    + "Visibility:" + cityNowStatus.vis + "(km)\r\n";

//                return string.Format("Now{0}Weather：\r\n{1}", cityname, cityinfoString + nowStatusString + airInfoString + suggestionString);
//            }
//        }


//    }
//}