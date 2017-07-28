using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ScottBot02.Models;
using ScottBot02.Bot;
using System.Linq;
using ScottBot02.Services;

namespace ScottBot02.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private LUISJson LUISJson;
        //private ComputerVisionService _computerVisionService = new ComputerVisionService();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task WaitForLuisMessage(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            


            context.Wait(WaitForLuisMessage);
        }

        private async Task HandleLuisMessage(IDialogContext context)
        {

        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            

            if (activity.Attachments.Count > 0)
            {
                var imageResource = activity.Attachments.FirstOrDefault().ContentUrl;

                ComputerVisionService _computerVisionService = new ComputerVisionService();

                ImageResult _imageResult = await _computerVisionService.AnalyseImageUrlAsync(imageResource);

                await context.PostAsync("I guess this image shows that " + _imageResult.Description.Captions.FirstOrDefault().Text + ".");
            }
            else
            {
                var TextToLuis = activity.Text;

                LUISJson = await LuisBot.GetLuisResult(TextToLuis);

                if (LUISJson.topScoringIntent.intent == "Greeting")
                {
                    var greeting = "Hi, I'm your image chat bot. You can send me some picture. I can talk about it.";
                    await context.PostAsync(greeting);
                }
                else
                {
                    await context.PostAsync("not greet");
                }
            }

            
            
            context.Wait(MessageReceivedAsync);
        }
    }
}