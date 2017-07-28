using Newtonsoft.Json;
using ScottBot02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ScottBot02.Services
{
    public class ComputerVisionService
    {

        private string _key = "2a55f74b65344ac7b9f00cde32bf40a7";

        private string _analyseImageUri = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/analyze?" +
            "visualFeatures=Description,Categories,Tags,Faces,ImageType,Color,Adult&details=Celebrities&language=en";

        public ComputerVisionService()
        {
        }

        public async Task<ImageResult> AnalyseImageStreamAsync(Stream stream)
        {

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var response = await httpClient.PostAsync(_analyseImageUri, streamContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResult = JsonConvert.DeserializeObject<ImageResult>(json);

                    return imageResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }


        public async Task<ImageResult> AnalyseImageUrlAsync(string imageUrl)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""url"":""" + imageUrl + @"""}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_analyseImageUri, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResult = JsonConvert.DeserializeObject<ImageResult>(json);

                    return imageResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }
    }
}