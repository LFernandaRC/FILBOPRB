using EventosCorferias.Models;
using Shiny.Push;

namespace EventosCorferias.Firebase
{
    public class PushDelegate : IPushDelegate
    {
        public Task OnNewToken(string token)
        {
            // 🔑 Guarda o envía el token a tu backend / Firebase
            ClaseBase claseBase;
            claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", token, "OnNewToken", "OnNewToken", "OnNewToken");

            return Task.CompletedTask;
        }

        public Task OnUnRegistered(string token)
        {
            ClaseBase claseBase;
            claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", token, "OnUnRegistered", "OnUnRegistered", "OnUnRegistered");

            return Task.CompletedTask;
        }

        public Task OnEntry(PushNotification notification)
        {
            ClaseBase claseBase;
            claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", notification.Data.ToString(), "OnEntry", "OnEntry", "OnEntry");
            return Task.CompletedTask;
        }

        public Task OnReceived(PushNotification notification)
        {
            ClaseBase claseBase;
            claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", notification.Data.ToString(), "OnReceived", "OnReceived", "OnReceived");
            return Task.CompletedTask;
        }
    }
}
