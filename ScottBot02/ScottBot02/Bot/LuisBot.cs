using Newtonsoft.Json;
using ScottBot02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ScottBot02.Bot
{
    public class LuisBot
    {

        public static async Task<LUISJson> GetLuisResult(string query)
        {
            LUISJson luisResponse;

            string luisUrl = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/a57ad0bb-6568-4d5f-983f-33c6c3346454?subscription-key=3bae4fbe7b7d4f4292f8316ccb95399e&timezoneOffset=0&verbose=true&q={query}";


            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(luisUrl);

            luisResponse = JsonConvert.DeserializeObject<LUISJson>(response);

            return luisResponse;
        }


    }
}