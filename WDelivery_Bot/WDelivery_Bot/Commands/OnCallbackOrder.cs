using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Telegram.Bot.Args;
using WDelivery_Bot.Model;
using WDelivery_Bot.Keyboards;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace WDelivery_Bot.Commands
{
    class OnCallbackOrder
    {
        public static async void OnClientOnCallbackQuery(object sender, CallbackQueryEventArgs ev)
        {
            
            string apiAddress = "https://wdeliveryapi.azurewebsites.net";
            var client = new HttpClient();
            client.BaseAddress = new Uri(apiAddress);

            var message = ev.CallbackQuery.Message;

            if (ev.CallbackQuery.Data == "Помпa")
            {
                var person = new OrderBrandResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Water_Brand = "Помпa";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/water_brand", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.SendPhotoAsync(
             chatId: ev.CallbackQuery.Message.Chat,
             photo: "https://content.rozetka.com.ua/goods/images/big/283802252.jpg",
             replyMarkup: MainButtons.GetButtons());

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null);

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "*Помпa* \n\nЦіна: 80 грн - 20 л., 48 грн - 12 л. або ж 40 грн - 10 л. відповідно. \n\n" +
               "Використовується для бутлів 20 л. (5 галонів), для бутля 12 л. (3 галона) та для бутля 10 літрів, працює при натисканні рукою" +
               "на верхній стакан-кнопку. Помпа виробляється в біло/блакитному кольорі 🚰", ParseMode.Markdown,
               replyMarkup: Inline.VolumeKeyboard);

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


            else if (ev.CallbackQuery.Data == "Bissleri")
            {
                var person = new OrderBrandResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Water_Brand = "Bissleri";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/water_brand", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.SendPhotoAsync(
             chatId: ev.CallbackQuery.Message.Chat,
             photo: "https://i2.wp.com/bookacan.com/wp-content/uploads/2015/11/kingfisher-20liter-250x3001.png?fit=250%2C375&ssl=1",
             replyMarkup: MainButtons.GetButtons());

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null); 

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Вода *Bissleri*\n\nЦіна: 80 грн - 20 л., 48 грн - 12 л. або ж 40 грн - 10 л. відповідно. \n\nУ тарі місткістю 20, 12 або 10 л з" +
               "оптимальним вмістом мінеральних речовин, природна, негазована, без консервантів, без введення будь-яких мікро- та макроелементів 💠", ParseMode.Markdown,
               replyMarkup: Inline.VolumeKeyboard);

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
      
           
            else if (ev.CallbackQuery.Data == "Natture")
            {
                var person = new OrderBrandResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Water_Brand = "Natture";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/water_brand", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.SendPhotoAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               photo: "https://i0.wp.com/bookacan.com/wp-content/uploads/2019/04/download-1.png?w=300&ssl=1",
               replyMarkup: MainButtons.GetButtons());

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null);

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Вода *Natture*\n\nЦіна: 80 грн - 20 л., 48 грн - 12 л. або ж 40 грн - 10 л. відповідно. \n\nУ тарі місткістю 20, 12 або 10 л з" +
               "оптимальним вмістом мінеральних речовин, природна, негазована, без консервантів, без введення будь-яких мікро- та макроелементів 💠", ParseMode.Markdown,
               replyMarkup: Inline.VolumeKeyboard);

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


            else if (ev.CallbackQuery.Data == "Jerrasic Water")
            {
                var person = new OrderBrandResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Water_Brand = "Jerrasic Water";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/water_brand", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.SendPhotoAsync(
              chatId: ev.CallbackQuery.Message.Chat,
              photo: "https://i1.wp.com/bookacan.com/wp-content/uploads/2017/05/Jeevika-20-Litre-Water-Can-Home-Delivery.jpg?w=300&ssl=1",
              replyMarkup: MainButtons.GetButtons());


                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null);

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Вода *Jerrasic Water*\n\nЦіна: 80 грн - 20 л., 48 грн - 12 л. або ж 40 грн - 10 л. відповідно. \n\nУ тарі місткістю 20, 12 або 10 л з" +
               "оптимальним вмістом мінеральних речовин, природна, негазована, без консервантів, без введення будь-яких мікро- та макроелементів 💠", ParseMode.Markdown,
               replyMarkup: Inline.VolumeKeyboard);

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


            else if (ev.CallbackQuery.Data == "20 л.")
            {
             
                var person = new OrderVolumeResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Volume = "20";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/volume", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null);

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Введіть бажану кількість.\n" +
                "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑",
               replyMarkup: new ForceReplyMarkup() { Selective = true });

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


            else if (ev.CallbackQuery.Data == "12 л.")
            {

                var person = new OrderVolumeResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Volume = "12";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/volume", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null);

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Введіть бажану кількість.\n" +
                "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑",
               replyMarkup: new ForceReplyMarkup() { Selective = true });

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


            else if (ev.CallbackQuery.Data == "10 л.")
            {

                var person = new OrderVolumeResponse();

                person.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person.Volume = "10";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/volume", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: null);

                await Program.client.SendTextMessageAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               text: "Введіть бажану кількість.\n" +
                "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑",
               replyMarkup: new ForceReplyMarkup() { Selective = true });

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


            else if (ev.CallbackQuery.Data == "Назад до постачальників")
            {

                await Program.client.EditMessageReplyMarkupAsync(
               chatId: ev.CallbackQuery.Message.Chat,
               messageId: ev.CallbackQuery.Message.MessageId,
               replyMarkup: Inline.MainOrderKeyboard);
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


   
            else if (ev.CallbackQuery.Data == "📅 Завтра")
            {
                
                string User_Id = Convert.ToString(ev.CallbackQuery.From.Id);

                var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id}");
                result.EnsureSuccessStatusCode();
                var content = result.Content.ReadAsStringAsync().Result;

                var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                Console.WriteLine(ev.CallbackQuery.Message.Date.AddDays(1).ToShortDateString());


            foreach (var item in info)
            {
                var person = new OrderDateResponse();

                person.Order_Id = item.Order_Id;
                person.Order_Date = $"{ev.CallbackQuery.Message.Date.AddDays(1).ToShortDateString()}";

                var json = JsonConvert.SerializeObject(person);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = await client.PatchAsync("Order/update/date", data);

                var patchcontent = patch.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent);
                }

                await Program.client.EditMessageReplyMarkupAsync(
                chatId: ev.CallbackQuery.Message.Chat,
                messageId: ev.CallbackQuery.Message.MessageId,
                replyMarkup: Inline.DeliveryTime);
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


            else if (ev.CallbackQuery.Data == "🗓 Післязавтра")
            {
                string User_Id = Convert.ToString(ev.CallbackQuery.From.Id);

                var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id}");
                result.EnsureSuccessStatusCode();
                var content = result.Content.ReadAsStringAsync().Result;

                var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                Console.WriteLine(ev.CallbackQuery.Message.Date.AddDays(2).ToShortDateString());

                foreach (var item in info)
                {
                    var person = new OrderDateResponse();

                    person.Order_Id = item.Order_Id;
                    person.Order_Date = $"{ev.CallbackQuery.Message.Date.AddDays(2).ToShortDateString()}";

                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var patch = await client.PatchAsync("Order/update/date", data);

                    var patchcontent = patch.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(patchcontent);
                }

                await Program.client.EditMessageReplyMarkupAsync(
                chatId: ev.CallbackQuery.Message.Chat,
                messageId: ev.CallbackQuery.Message.MessageId,
                replyMarkup: Inline.DeliveryTime);
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


            else if (ev.CallbackQuery.Data == "9:00 - 12:00")
            {
                string User_Id = Convert.ToString(ev.CallbackQuery.From.Id);

                var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id}");
                result.EnsureSuccessStatusCode();
                var content = result.Content.ReadAsStringAsync().Result;

                var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                foreach (var item in info)
                {
                    var person = new OrderTimeResponse();

                    person.Order_Id = item.Order_Id;
                    person.Order_Time = $"9:00 - 12:00";

                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var patch = await client.PatchAsync("Order/update/time", data);

                    var patchcontent = patch.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(patchcontent);
                }


                var person_checker = new UserStatusResponse();

                person_checker.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person_checker.Checker = "OK";

                var json_checker = JsonConvert.SerializeObject(person_checker);
                var data_checker = new StringContent(json_checker, Encoding.UTF8, "application/json");

                var patch_cheker = await client.PatchAsync("Main/update/checker", data_checker);

                var patchcontent_checker = patch_cheker.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent_checker);

                await Program.client.SendTextMessageAsync(
                             chatId: ev.CallbackQuery.Message.Chat,
                             text: "Замовлення створено ✅",
                             replyMarkup: MainButtons.GetButtons());

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


            else if (ev.CallbackQuery.Data == "12:00 - 15:00")
            {
                string User_Id = Convert.ToString(ev.CallbackQuery.From.Id);

                var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id}");
                result.EnsureSuccessStatusCode();
                var content = result.Content.ReadAsStringAsync().Result;

                var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                foreach (var item in info)
                {
                    var person = new OrderTimeResponse();

                    person.Order_Id = item.Order_Id;
                    person.Order_Time = $"12:00 - 15:00";

                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var patch = await client.PatchAsync("Order/update/time", data);

                    var patchcontent = patch.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(patchcontent);
                }


                var person_checker = new UserStatusResponse();

                person_checker.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person_checker.Checker = "OK";

                var json_checker = JsonConvert.SerializeObject(person_checker);
                var data_checker = new StringContent(json_checker, Encoding.UTF8, "application/json");

                var patch_cheker = await client.PatchAsync("Main/update/checker", data_checker);

                var patchcontent_checker = patch_cheker.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent_checker);


                await Program.client.SendTextMessageAsync(
                               chatId: ev.CallbackQuery.Message.Chat,
                               text: "Замовлення створено ✅",
                               replyMarkup: MainButtons.GetButtons());

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


            else if (ev.CallbackQuery.Data == "15:00 - 18:00")
            {
                string User_Id = Convert.ToString(ev.CallbackQuery.From.Id);

                var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id}");
                result.EnsureSuccessStatusCode();
                var content = result.Content.ReadAsStringAsync().Result;

                var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                foreach (var item in info)
                {
                    var person = new OrderTimeResponse();

                    person.Order_Id = item.Order_Id;
                    person.Order_Time = $"15:00 - 18:00";

                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var patch = await client.PatchAsync("Order/update/time", data);

                    var patchcontent = patch.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(patchcontent);
                }


                var person_checker = new UserStatusResponse();

                person_checker.User_Id = Convert.ToString(ev.CallbackQuery.From.Id);
                person_checker.Checker = "OK";

                var json_checker = JsonConvert.SerializeObject(person_checker);
                var data_checker = new StringContent(json_checker, Encoding.UTF8, "application/json");

                var patch_cheker = await client.PatchAsync("Main/update/checker", data_checker);

                var patchcontent_checker = patch_cheker.Content.ReadAsStringAsync().Result;

                Console.WriteLine(patchcontent_checker);


                await Program.client.SendTextMessageAsync(
                               chatId: ev.CallbackQuery.Message.Chat,
                               text: "Замовлення створено ✅",
                               replyMarkup: MainButtons.GetButtons());

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
