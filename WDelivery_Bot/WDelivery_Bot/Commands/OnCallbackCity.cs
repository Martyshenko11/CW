using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using WDelivery_Bot.Keyboards;
using WDelivery_Bot.Model;

namespace WDelivery_Bot.Commands
{
     class OnCallbackCity
    {
         public static async void OnClientOnCallbackQuery(object sender, CallbackQueryEventArgs ev)
        {
            string apiAddress = "https://wdeliveryapi.azurewebsites.net";
            var client = new HttpClient();
            client.BaseAddress = new Uri(apiAddress);

            if (ev.CallbackQuery.Data == "Київ")
            {
                Console.WriteLine($"Id:{ev.CallbackQuery.From.Id}");

                

                var person = new UserCityResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.City = "Київ";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Main/update/city", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);


                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Ваше місто *Київ* успішно збережено 🏙", ParseMode.Markdown,
               replyMarkup: MainButtons.GetSettingsButtons());

                await Program.client.EditMessageReplyMarkupAsync(
              chatId: ev.CallbackQuery.Message.Chat,
              messageId: ev.CallbackQuery.Message.MessageId,
              replyMarkup: null);

                try
                {
                    await Program.client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                }
                catch (Telegram.Bot.Exceptions.InvalidParameterException)
                {
                    Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                    await Program.client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: MainButtons.GetButtons());
                }
            }

            else if (ev.CallbackQuery.Data == "Харків")
            {
                Console.WriteLine($"Id:{ev.CallbackQuery.From.Id}");

               


                var person = new UserCityResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.City = "Харків";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Main/update/city", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);


               await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Ваше місто *Харків* успішно збережено 🏙", ParseMode.Markdown,
               replyMarkup: MainButtons.GetSettingsButtons());

                await Program.client.EditMessageReplyMarkupAsync(
            chatId: ev.CallbackQuery.Message.Chat,
            messageId: ev.CallbackQuery.Message.MessageId,
            replyMarkup: null);

                try
                {
                    await Program.client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                }
                catch (Telegram.Bot.Exceptions.InvalidParameterException)
                {
                    Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                    await Program.client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: MainButtons.GetButtons());
                }
            }

            else if (ev.CallbackQuery.Data == "Львів")
            {
                Console.WriteLine($"Id:{ev.CallbackQuery.From.Id}");


                client.BaseAddress = new Uri(apiAddress);

                var person = new UserCityResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.City = "Львів";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Main/update/city", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);




              await Program.client.SendTextMessageAsync(
              chatId: ev.CallbackQuery.Message.Chat,
              text: "Ваше місто *Львів* успішно збережено 🏙", ParseMode.Markdown,
              replyMarkup: MainButtons.GetSettingsButtons());

                await Program.client.EditMessageReplyMarkupAsync(
            chatId: ev.CallbackQuery.Message.Chat,
            messageId: ev.CallbackQuery.Message.MessageId,
            replyMarkup: null);

                try
                {
                    await Program.client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                }
                catch (Telegram.Bot.Exceptions.InvalidParameterException)
                {
                    Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                    await Program.client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: MainButtons.GetButtons());
                }
            }

            else if (ev.CallbackQuery.Data == "Дніпро")
            {
                Console.WriteLine($"Id:{ev.CallbackQuery.From.Id}");



                var person = new UserCityResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.City = "Дніпро";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Main/update/city", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);



              await Program.client.SendTextMessageAsync(
              chatId: ev.CallbackQuery.Message.Chat,
              text: "Ваше місто *Дніпро* успішно збережено 🏙", ParseMode.Markdown,
              replyMarkup: MainButtons.GetSettingsButtons());

                await Program.client.EditMessageReplyMarkupAsync(
            chatId: ev.CallbackQuery.Message.Chat,
            messageId: ev.CallbackQuery.Message.MessageId,
            replyMarkup: null);

                try
                {
                    await Program.client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                }
                catch (Telegram.Bot.Exceptions.InvalidParameterException)
                {
                    Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                    await Program.client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: MainButtons.GetButtons());
                }
            }

            else if (ev.CallbackQuery.Data == "Одеса")
            {
                Console.WriteLine($"Id:{ev.CallbackQuery.From.Id}");


                var person = new UserCityResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.City = "Одеса";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Main/update/city", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);



              await Program.client.SendTextMessageAsync(
              chatId: ev.CallbackQuery.Message.Chat,
              text: "Ваше місто *Одеса* успішно збережено 🏙", ParseMode.Markdown,
              replyMarkup: MainButtons.GetSettingsButtons());

                await Program.client.EditMessageReplyMarkupAsync(
            chatId: ev.CallbackQuery.Message.Chat,
            messageId: ev.CallbackQuery.Message.MessageId,
            replyMarkup: null);

                try
                {
                    await Program.client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                }
                catch (Telegram.Bot.Exceptions.InvalidParameterException)
                {
                    Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                    await Program.client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: MainButtons.GetButtons());
                }
            }
        }
    }
}
