using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
namespace BotTest
{
    [LuisModel("711a5176-f87a-473e-9e25-856b475f21eb", "30947615e9b04edea0c30a91b7ee1135")]
    [Serializable]
    public class TravelAgentDialog : LuisDialog<object>
    {
        public const string Entity_Location_ToLocation = "Location::ToLocation";

        public TravelAgentDialog(ILuisService service = null)
            : base(service)
        {
        }

        [LuisIntent("BookFlight")]
        public async Task BookFlight(IDialogContext context, LuisResult result)
        {
            string where = string.Empty;

            EntityRecommendation toLocation;
            if (result.TryFindEntity(Entity_Location_ToLocation, out toLocation))
            {
                where = toLocation.Entity;
            }

            if (!string.IsNullOrEmpty(where))
            {
                await context.PostAsync($"Ok, flight to {where} has been booked successfully.");
            }
            else
            {
                await context.PostAsync("Please specify your destination.");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand what you said.";

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

    }
}