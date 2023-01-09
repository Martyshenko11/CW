using System;
using Telegram.Bot;
using WDelivery_Bot.Commands;


namespace WDelivery_Bot
{
    class Program 
    {
        private static string Token { get; set; } = "";
        
        public static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnHandle.OnMessageHandler;
            client.OnCallbackQuery += OnCallbackCity.OnClientOnCallbackQuery;
            client.OnCallbackQuery += OnCallbackOrder.OnClientOnCallbackQuery;
            Console.ReadLine();
            client.StopReceiving();
        }    
    }
}
