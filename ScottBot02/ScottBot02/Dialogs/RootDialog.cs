using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ScottBot02.Models;
using ScottBot02.Bot;

namespace ScottBot02.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private LUISJson LUISJson;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task WaitForLuisMessage(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var TextToLuis = activity.Text;

            LUISJson = await LuisBot.GetLuisResult(TextToLuis);

            await context.PostAsync(LUISJson.topScoringIntent.intent); 


            context.Wait(WaitForLuisMessage);
        }

        private async Task HandleLuisMessage(IDialogContext context)
        {

        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var greeting = "Hi, I'm Eve, your event bot. You can ask me questions about your event. Please state a question.";
            await context.PostAsync(greeting);
            
            context.Wait(WaitForLuisMessage);
        }
    }
}