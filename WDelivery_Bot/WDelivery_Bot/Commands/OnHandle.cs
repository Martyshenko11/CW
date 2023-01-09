using Newtonsoft.Json;
using System;
using System.Net.Http;
using Telegram.Bot.Args;
using WDelivery_Bot.Keyboards;
using System.Text;
using WDelivery_Bot.Model;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace WDelivery_Bot.Commands
{
    class OnHandle
    {


        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            string apiAddress = "https://wdeliveryapi.azurewebsites.net";
            var client = new HttpClient();
            client.BaseAddress = new Uri(apiAddress);

            var msg = e.Message;
            if (e.Message.From.Id != 5668340169)
            {

                if (msg.Contact != null)
                {

                    Console.WriteLine($"Id:{e.Message.From.Id} Phone number: {msg.Contact.PhoneNumber}");

                    var person = new UserPhoneResponse();

                    person.User_Id = Convert.ToString(e.Message.From.Id);
                    person.Phone = Convert.ToString(msg.Contact.PhoneNumber);

                    var json = JsonConvert.SerializeObject(person);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var patch = await client.PatchAsync("Main/update/phone", data);

                    var patchcontent = patch.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(patchcontent);


                    await Program.client.SendTextMessageAsync(
                           chatId: msg.Chat.Id,
                           text: "Ваш номер телефону успішно збережено 📱",
                           replyMarkup: MainButtons.GetSettingsButtons());
                }



                if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть Вашу адресу")
                    || e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть коректні дані ❌ \n\nВведіть Вашу адресу у форматі:" +
                    "вулиця, будинок, номер квартири.\nНаприклад: Героїв Дніпра, 12, кв.22 🏡"))
                {
                    var person = new UserAddressResponse();

                    person.User_Id = Convert.ToString(e.Message.From.Id);
                    person.Address = $"{e.Message.Text}";

                    if (e.Message.Text != null)
                    {
                        var json = JsonConvert.SerializeObject(person);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");

                        var patch = await client.PatchAsync("Main/update/address", data);

                        var patchcontent = patch.Content.ReadAsStringAsync().Result;

                        Console.WriteLine(patchcontent);

                        await Program.client.SendTextMessageAsync(
                                   chatId: msg.Chat.Id,
                                   text: "Вашу адресу успішно збережено ✅",
                                   replyMarkup: MainButtons.GetSettingsButtons());
                    }
                    else
                    {
                        await Program.client.SendTextMessageAsync(
                              chatId: msg.Chat.Id,
                              text: "Введіть коректні дані ❌ \n\nВведіть Вашу адресу у форматі:" +
                    "вулиця, будинок, номер квартири.\nНаприклад: Героїв Дніпра, 12, кв.22 🏡",
                              replyMarkup: new ForceReplyMarkup() { Selective = true });
                    }
                }

                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть бажану кількість.\n" +
                    "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑")
                    || e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть коректні дані ❌ \n\nВведіть бажану кількість.\n" +
                    "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑"))
                {
                    var person = new OrderNumberResponse();

                    person.User_Id = Convert.ToString(e.Message.From.Id);
                    person.Num_Water = $"{e.Message.Text}";

                    try
                    {
                        if (Convert.ToInt32(e.Message.Text) < 16 && Convert.ToInt32(e.Message.Text) > 0)
                        {



                            var json = JsonConvert.SerializeObject(person);
                            var data = new StringContent(json, Encoding.UTF8, "application/json");

                            var patch = await client.PatchAsync("Order/update/number", data);

                            var patchcontent = patch.Content.ReadAsStringAsync().Result;

                            Console.WriteLine(patchcontent);



                            string User_Id = Convert.ToString(e.Message.From.Id);

                            var result = await client.GetAsync($"Order/get/tempinfo?User_Id={User_Id}");
                            result.EnsureSuccessStatusCode();
                            var content = result.Content.ReadAsStringAsync().Result;

                            var info = JsonConvert.DeserializeObject<OrderTempResponse>(content);




                            var check_result = await client.GetAsync($"Order/get/checkisorderinfo?User_Id={User_Id}&Water_Brand={info.Water_Brand}&Volume={info.Volume}");
                            check_result.EnsureSuccessStatusCode();
                            var check_content = check_result.Content.ReadAsStringAsync().Result;

                            var check_info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(check_content);



                            if (check_info.Count == 0)
                            {

                                string orderNumber = Guid.NewGuid().ToString();
                                Console.WriteLine(orderNumber);
                                var person_add = new OrderMainResponse();

                                person_add.Order_Id = orderNumber;
                                person_add.User_Id = Convert.ToString(e.Message.From.Id);
                                person_add.Water_Brand = info.Water_Brand;
                                person_add.Volume = info.Volume;
                                person_add.Num_Water = info.Num_Water;
                                person_add.Price = Convert.ToString(Convert.ToInt32(person_add.Volume) * Convert.ToInt32(person_add.Num_Water) * 4);


                                var json_add = JsonConvert.SerializeObject(person_add);
                                var data_add = new StringContent(json_add, Encoding.UTF8, "application/json");

                                var patch_add = await client.PostAsync("Order/add/order", data_add);

                                var patchcontent_add = patch_add.Content.ReadAsStringAsync().Result;

                                Console.WriteLine(patchcontent_add);

                            }

                            else
                            {
                                var checked_order = "";
                                foreach (var item in check_info)
                                {
                                    checked_order = item.Order_Id;
                                }

                                var person_add = new OrderMainResponse();

                                person_add.Order_Id = checked_order;
                                person_add.User_Id = Convert.ToString(e.Message.From.Id);
                                person_add.Water_Brand = info.Water_Brand;
                                person_add.Volume = info.Volume;
                                person_add.Num_Water = info.Num_Water;
                                person_add.Price = Convert.ToString(Convert.ToInt32(person_add.Volume) * Convert.ToInt32(person_add.Num_Water) * 4);


                                var json_add = JsonConvert.SerializeObject(person_add);
                                var data_add = new StringContent(json_add, Encoding.UTF8, "application/json");

                                var patch_add = await client.PostAsync("Order/add/order", data_add);

                                var patchcontent_add = patch_add.Content.ReadAsStringAsync().Result;

                                Console.WriteLine(patchcontent_add);
                            }
                            await Program.client.SendTextMessageAsync(msg.Chat.Id, "Товар успішно додано до кошика ✔️", replyMarkup: MainButtons.SecondMenuItems());
                        }
                        else
                        {
                            await Program.client.SendTextMessageAsync(
                              chatId: msg.Chat.Id,
                              text: "Введіть коректні дані ❌ \n\nВведіть бажану кількість.\n" +
                    "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑",
                             replyMarkup: new ForceReplyMarkup() { Selective = true });
                        }
                    }
                    catch (System.FormatException)
                    {
                        await Program.client.SendTextMessageAsync(
                               chatId: msg.Chat.Id,
                               text: "Введіть коректні дані ❌ \n\nВведіть бажану кількість.\n" +
                    "🛑 Зверніть увагу, максимальна кількість данного товару не може бути більше 15 🛑",
                              replyMarkup: new ForceReplyMarkup() { Selective = true });
                    }
                }


                if (msg.Text != null)
                {
                    Console.WriteLine($"Username:{e.Message.From.Username} Id:{e.Message.From.Id} Message: {msg.Text}");

                    switch (msg.Text)
                    {
                        case "/start":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Вітаю! Я бот для замовлення доставки питної води 🚰",
                                replyMarkup: MainButtons.GetButtons());
                            break;

                        case "/menu":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Головне меню 🔻",
                                replyMarkup: MainButtons.GetButtons());
                            break;

                        case "💧 Замовити":
                            try
                            {
                                string User_Id_Check = Convert.ToString(e.Message.From.Id);

                                var result_check = await client.GetAsync($"Main/get/statuschecker?User_Id={User_Id_Check}");

                                result_check.EnsureSuccessStatusCode();
                                var content_check = result_check.Content.ReadAsStringAsync().Result;

                                var info_check = JsonConvert.DeserializeObject<UserStatusResponse>(content_check);

                                if (info_check.Checker != "OK")
                                {


                                    string User_Id_Verify = Convert.ToString(e.Message.From.Id);

                                    var result_verify = await client.GetAsync($"Main/get/userverify?User_Id={User_Id_Verify}");
                                    try
                                    {
                                        result_verify.EnsureSuccessStatusCode();
                                        var content_verify = result_verify.Content.ReadAsStringAsync().Result;

                                        var info_verify = JsonConvert.DeserializeObject<UserVerifyResponse>(content_verify);


                                        if (info_verify.Address != null && info_verify.City != null && info_verify.Phone != null)
                                        {
                                            await Program.client.SendTextMessageAsync(
                                                chatId: msg.Chat.Id,
                                                text: "Виберіть пункт меню ⤵️",
                                                replyMarkup: Inline.MainOrderKeyboard);
                                        }
                                        else
                                        {
                                            await Program.client.SendTextMessageAsync(
                                               chatId: msg.Chat.Id,
                                               text: "Будь-ласка, введіть усі дані ❗️",
                                               replyMarkup: MainButtons.GetSettingsButtons());
                                        }
                                    }
                                    catch (System.Net.Http.HttpRequestException)
                                    {
                                        await Program.client.SendTextMessageAsync(
                                            chatId: msg.Chat.Id,
                                            text: "Для створення замовлення зареєструйтеся в системі ⚠️ \nДля реєстрації потрібно надати " +
                                            "інформацію про: \nНомер телефону 📱 \nМісто 🌆 \nАдресу, за якою буде доставлено воду 🏠",
                                            replyMarkup: MainButtons.GetSettingsButtons());
                                    }

                                }
                                else
                                {
                                    string User_Id_TotalInfo = Convert.ToString(e.Message.From.Id);

                                    var result_totalinfo = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id_TotalInfo}");
                                    result_totalinfo.EnsureSuccessStatusCode();
                                    var content_totalinfo = result_totalinfo.Content.ReadAsStringAsync().Result;

                                    var info_totalinfo = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content_totalinfo);

                                    var result_items_info = "";
                                    var total_sum_totalinfo = 0;
                                    var total_date = "";
                                    var total_time = "";

                                    foreach (var item in info_totalinfo)
                                    {
                                        result_items_info += item.Water_Brand + "\nКількість: " + item.Num_Water + " шт." + "\nЛітраж: " + item.Volume + " л." +
                                            "\nЦіна: " + item.Price + "грн\n\n";
                                        total_sum_totalinfo += Convert.ToInt32(item.Price);
                                        total_date = item.Order_Date;
                                        total_time = item.Order_Time;
                                    }

                                    await Program.client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: $"У вас вже є активна доставка 🟢 \n\n{result_items_info}*Сума:* {total_sum_totalinfo} грн\n" +
                                        $"*Час доставки:* {total_time} \n*Дата доставки:* {total_date}", ParseMode.Markdown,
                                        replyMarkup: MainButtons.GetButtons());
                                }
                            }
                            catch (System.Net.Http.HttpRequestException)
                            {
                                await Program.client.SendTextMessageAsync(
                                           chatId: msg.Chat.Id,
                                           text: "Для створення замовлення зареєструйтеся в системі ⚠️ \nДля реєстрації потрібно надати " +
                                            "інформацію про: \nНомер телефону 📱 \nМісто 🌆 \nАдресу, за якою буде доставлено воду 🏠",
                                           replyMarkup: MainButtons.GetSettingsButtons());
                            }

                            break;

                        case "⚙️ Налаштування":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Виберіть пункт меню ⤵️",
                                replyMarkup: MainButtons.GetSettingsButtons());
                            break;

                        case "🌆 Мiсто":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Виберіть своє місто 🏬",
                                replyMarkup: Inline.CityKeyboard);
                            break;

                        case "🏠 Адреса":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Введіть Вашу адресу у форматі: вулиця, будинок, номер квартири.\nНаприклад: Героїв Дніпра, 12, кв.22 🏡",
                                 replyMarkup: new ForceReplyMarkup() { Selective = true });
                            break;

                        case "🔙 Назад":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Виберіть пункт меню ⤵️",
                                replyMarkup: MainButtons.GetButtons());
                            break;

                        case "🛒 Кошик":

                            try
                            {
                                string User_Id_Check_Trash = Convert.ToString(e.Message.From.Id);

                                var result_check_trash = await client.GetAsync($"Main/get/statuschecker?User_Id={User_Id_Check_Trash}");

                                result_check_trash.EnsureSuccessStatusCode();
                                var content_check_trash = result_check_trash.Content.ReadAsStringAsync().Result;

                                var info_check_trash = JsonConvert.DeserializeObject<UserStatusResponse>(content_check_trash);

                                if (info_check_trash.Checker != "OK")
                                {
                                    string User_Id = Convert.ToString(e.Message.From.Id);

                                    var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id}");
                                    result.EnsureSuccessStatusCode();
                                    var content = result.Content.ReadAsStringAsync().Result;

                                    var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);

                                    var result_items = "";
                                    var total_sum = 0;

                                    foreach (var item in info)
                                    {
                                        result_items += item.Water_Brand + "\nКількість: " + item.Num_Water + " шт." + "\nЛітраж: " + item.Volume + " л." +
                                            "\nЦіна: " + item.Price + "грн\n\n";
                                        total_sum += Convert.ToInt32(item.Price);
                                    }

                                    if (total_sum > 0)
                                    {
                                        await Program.client.SendTextMessageAsync(
                                            chatId: msg.Chat.Id,
                                            text: $"{result_items}Сума: {total_sum} грн\n",
                                            replyMarkup: MainButtons.DeleteTrashItems());
                                    }
                                    else
                                    {
                                        await Program.client.SendTextMessageAsync(
                                            chatId: msg.Chat.Id,
                                            text: $"Кошик порожній 🤷‍♂️",
                                            replyMarkup: MainButtons.GetButtons());
                                    }

                                }
                                else
                                {
                                    string User_Id_TotalInfo = Convert.ToString(e.Message.From.Id);

                                    var result_totalinfo = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id_TotalInfo}");
                                    result_totalinfo.EnsureSuccessStatusCode();
                                    var content_totalinfo = result_totalinfo.Content.ReadAsStringAsync().Result;

                                    var info_totalinfo = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content_totalinfo);

                                    var result_items_info = "";
                                    var total_sum_totalinfo = 0;
                                    var total_date = "";
                                    var total_time = "";

                                    foreach (var item in info_totalinfo)
                                    {
                                        result_items_info += item.Water_Brand + "\nКількість: " + item.Num_Water + " шт." + "\nЛітраж:" + item.Volume + " л." +
                                            "\nЦіна: " + item.Price + "грн\n\n";
                                        total_sum_totalinfo += Convert.ToInt32(item.Price);
                                        total_date = item.Order_Date;
                                        total_time = item.Order_Time;
                                    }

                                    await Program.client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: $"У вас вже є активна доставка 🟢 \n\n{result_items_info}*Сума:* {total_sum_totalinfo} грн\n" +
                                        $"*Час доставки:* {total_time} \n*Дата доставки:* {total_date}", ParseMode.Markdown,
                                        replyMarkup: MainButtons.GetButtons());
                                }
                            }
                            catch (System.Net.Http.HttpRequestException)
                            {
                                await Program.client.SendTextMessageAsync(
                                           chatId: msg.Chat.Id,
                                           text: "Для створення замовлення зареєструйтеся в системі ⚠️ \nДля реєстрації потрібно надати " +
                                            "інформацію про: \nНомер телефону 📱 \nМісто 🌆 \nАдресу, за якою буде доставлено воду 🏠",
                                           replyMarkup: MainButtons.GetSettingsButtons());
                            }

                            break;

                        case "💼 Про нас":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: $"Компанія WDelivery - це інноваційний сервіс з доставки води 💧 у багатьох містах України🚛 \n\n" +
                                "Доставка натуральної артезіанської води із скважини глибиною 335 метрів. Не додаємо жодних консервантів та домішок! \n\n" +
                                "- понад 11 років роботи;\n- доставили понад 5 мільйонів бутлів;\n- маємо понад 60 000 вдячних клієнтів;\n" +
                                "- щоранку 50 автомобілів виїжджають у всі мікрорайони міст України.\n\nНаше виробництво сертифіковано" +
                                "за міжнародними стандартами якості ISO 9001 та ISO 22000, які працюють на базі принципів HACCP 🇺🇦 \n\n" +
                                "У разі виникнення будь-яких питань або проблем, звертайтеся до нашого адміністратора @admin_wdelivery або за номером телефону +380999999900 ☎️",
                                replyMarkup: MainButtons.GetButtons());
                            break;

                        case "💵 Оформити замовлення":

                            await Program.client.SendTextMessageAsync(
                               chatId: msg.Chat.Id,
                               text: "Виберіть пункт меню ⤵️",
                           replyMarkup: Inline.DeliveryDay);

                            break;


                        case "❌ Видалити товари з корзини":
                            
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Підтвердіть видалення 🔻",
                                replyMarkup: MainButtons.DeleteItemsAgree());
                                                  
                            break;

                        case "🔙 Скасувати видалення":

                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "🛒 Кошик",
                                replyMarkup: MainButtons.DeleteTrashItems());

                            break;

                        case "☑️ Підтвердити видалення":

                            string User_Id_delete = Convert.ToString(e.Message.From.Id);

                            var result_delete = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id_delete}");
                            result_delete.EnsureSuccessStatusCode();
                            var content_delete = result_delete.Content.ReadAsStringAsync().Result;

                            var info_delete = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content_delete);


                            foreach (var item in info_delete)
                            {
                                var delete = await client.DeleteAsync($"Order/delete/orderinfo?User_Id={item.Order_Id}");
                            }


                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Товари успішно видалено ✅",
                                replyMarkup: MainButtons.GetButtons());

                            break;

                        case "☑️ Додати інший товар":

                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "🛑 Зверніть увагу, максимальна кількість товару не може бути більше 15 🛑",
                                replyMarkup: MainButtons.GetButtons());

                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Виберіть пункт меню ⤵️",
                                replyMarkup: Inline.MainOrderKeyboard);

                            break;


                            //default:
                            //    await Program.client.SendTextMessageAsync(msg.Chat.Id, "Виберіть пункт меню", replyMarkup: MainButtons.GetButtons());
                            //    break;
                    }
                }
            }

            else
            {
                if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть дату доставки"))
                {
                    try
                    {
                        var result_totalinfo = await client.GetAsync($"Admin/get/allordersbydate?Order_Date={e.Message.Text}");
                        result_totalinfo.EnsureSuccessStatusCode();
                        var content_totalinfo = result_totalinfo.Content.ReadAsStringAsync().Result;

                        var info_totalinfo = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content_totalinfo);

                        var result_items_info = "";

                        foreach (var item in info_totalinfo)
                        {
                            result_items_info += "Id: " + item.User_Id + "\n" + item.Water_Brand + "\nКількість: " + item.Num_Water + " шт." +
                                "\nЛітраж: " + item.Volume + " л." +
                                "\nЦіна: " + item.Price + " грн\n\n";
                        }

                        await Program.client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: $"{result_items_info}",
                                        replyMarkup: MainButtons.AdminKeyboard());
                    }
                    catch (Telegram.Bot.Exceptions.ApiRequestException)
                    {
                        await Program.client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       text: $"На цю дату немає замовлень",
                                       replyMarkup: MainButtons.AdminKeyboard());
                    }

                }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть Id користувача"))
                {
                    try
                    {
                        var result = await client.GetAsync($"Admin/get/userinfobyid?User_Id={e.Message.Text}");
                        result.EnsureSuccessStatusCode();
                        var content = result.Content.ReadAsStringAsync().Result;

                        var info = JsonConvert.DeserializeObject<UserVerifyResponse>(content);

                        await Program.client.SendTextMessageAsync(
                                           chatId: msg.Chat.Id,
                                           text: $"Місто: {info.City} \nАдреса: {info.Address} \nНомер телефону: {info.Phone}",
                                           replyMarkup: MainButtons.AdminKeyboard());
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await Program.client.SendTextMessageAsync(
                                       chatId: msg.Chat.Id,
                                       text: $"Користувача з даним Id в системі немає",
                                       replyMarkup: MainButtons.AdminKeyboard());
                    }
                }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Введіть номер користувача"))
                {
                    try
                    {
                        var result_totalinfo = await client.GetAsync($"Admin/get/userinfobyphone?Phone=%2B{e.Message.Text}");
                        result_totalinfo.EnsureSuccessStatusCode();
                        var content_totalinfo = result_totalinfo.Content.ReadAsStringAsync().Result;

                        var info_totalinfo = JsonConvert.DeserializeObject<List<UserDbRepository>>(content_totalinfo);

                        var result_items_info = "";

                        foreach (var item in info_totalinfo)
                        {
                            result_items_info += "Id: " + item.User_Id + "\nМісто: " + item.City + "\nАдреса: " + item.Address;
                        }

                        await Program.client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: $"{result_items_info}",
                                        replyMarkup: MainButtons.AdminKeyboard());
                    } 
                    catch(Telegram.Bot.Exceptions.ApiRequestException)
                    {
                        await Program.client.SendTextMessageAsync(
                                      chatId: msg.Chat.Id,
                                      text: $"Користувача з даним номером в системі немає",
                                      replyMarkup: MainButtons.AdminKeyboard());
                    }
                }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Для видалення доставки введіть Id користувача"))
                {
                    try
                    {
                        var result_temp = await client.GetAsync($"Order/get/tempinfo?User_Id={e.Message.Text}");
                        result_temp.EnsureSuccessStatusCode();
                        var content_temp = result_temp.Content.ReadAsStringAsync().Result;

                        var info_temp = JsonConvert.DeserializeObject<OrderTempResponse>(content_temp);

                        var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={e.Message.Text}");
                        result.EnsureSuccessStatusCode();
                        var content = result.Content.ReadAsStringAsync().Result;

                        var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                        foreach (var item in info)
                        {
                            var delete = await client.DeleteAsync($"Order/delete/orderinfo?User_Id={item.Order_Id}");
                        }

                        var person_checker = new UserStatusResponse();

                        person_checker.User_Id = e.Message.Text;
                        person_checker.Checker = "COMPLETED";

                        var json_checker = JsonConvert.SerializeObject(person_checker);
                        var data_checker = new StringContent(json_checker, Encoding.UTF8, "application/json");

                        var patch_cheker = await client.PatchAsync("Main/update/checker", data_checker);

                        var patchcontent_checker = patch_cheker.Content.ReadAsStringAsync().Result;

                        Console.WriteLine(patchcontent_checker);


                        await Program.client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Доставку успішно видалено!",
                            replyMarkup: MainButtons.AdminKeyboard());
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await Program.client.SendTextMessageAsync(
                                      chatId: msg.Chat.Id,
                                      text: $"Користувача з даним Id в системі немає",
                                      replyMarkup: MainButtons.AdminKeyboard());
                    }

                }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Для пошуку доставки введіть Id користувача"))
                {
                    try
                    {
                        var result_temp = await client.GetAsync($"Order/get/tempinfo?User_Id={e.Message.Text}");
                        result_temp.EnsureSuccessStatusCode();
                        var content_temp = result_temp.Content.ReadAsStringAsync().Result;

                        var info_temp = JsonConvert.DeserializeObject<OrderTempResponse>(content_temp);


                        string User_Id_TotalInfo = Convert.ToString(e.Message.Text);

                        var result_totalinfo = await client.GetAsync($"Order/get/totalorderinfo?User_Id={User_Id_TotalInfo}");
                        result_totalinfo.EnsureSuccessStatusCode();
                        var content_totalinfo = result_totalinfo.Content.ReadAsStringAsync().Result;

                        var info_totalinfo = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content_totalinfo);

                        var result_items_info = "";
                        var total_sum_totalinfo = 0;
                        var total_date = "";
                        var total_time = "";

                        foreach (var item in info_totalinfo)
                        {
                            result_items_info += item.Water_Brand + "\nКількість: " + item.Num_Water + " шт." + "\nЛітраж: " + item.Volume + " л." +
                                "\nЦіна: " + item.Price + " грн\n\n";
                            total_sum_totalinfo += Convert.ToInt32(item.Price);
                            total_date = item.Order_Date;
                            total_time = item.Order_Time;
                        }

                        await Program.client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: $"{result_items_info}*Сума:* {total_sum_totalinfo} грн\n" +
                            $"*Час доставки:* {total_time} \n*Дата доставки:* {total_date}", ParseMode.Markdown,
                            replyMarkup: MainButtons.AdminKeyboard());
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await Program.client.SendTextMessageAsync(
                                      chatId: msg.Chat.Id,
                                      text: $"Користувача з даним Id в системі немає",
                                      replyMarkup: MainButtons.AdminKeyboard());
                    }

                }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Для підтвердження доставки введіть Id користувача"))
                {
                    try
                    {
                        var result_temp = await client.GetAsync($"Order/get/tempinfo?User_Id={e.Message.Text}");
                        result_temp.EnsureSuccessStatusCode();
                        var content_temp = result_temp.Content.ReadAsStringAsync().Result;

                        var info_temp = JsonConvert.DeserializeObject<OrderTempResponse>(content_temp);


                        var result = await client.GetAsync($"Order/get/totalorderinfo?User_Id={e.Message.Text}");
                        result.EnsureSuccessStatusCode();
                        var content = result.Content.ReadAsStringAsync().Result;

                        var info = JsonConvert.DeserializeObject<List<OrderTrashResponse>>(content);


                        foreach (var item in info)
                        {
                            var delete = await client.DeleteAsync($"Order/delete/orderinfo?User_Id={item.Order_Id}");
                        }


                        var person_checker = new UserStatusResponse();

                        person_checker.User_Id = e.Message.Text;
                        person_checker.Checker = "COMPLETED";

                        var json_checker = JsonConvert.SerializeObject(person_checker);
                        var data_checker = new StringContent(json_checker, Encoding.UTF8, "application/json");

                        var patch_cheker = await client.PatchAsync("Main/update/checker", data_checker);

                        var patchcontent_checker = patch_cheker.Content.ReadAsStringAsync().Result;

                        Console.WriteLine(patchcontent_checker);


                        await Program.client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Доставку підтвердженно!",
                            replyMarkup: MainButtons.AdminKeyboard());
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        await Program.client.SendTextMessageAsync(
                                      chatId: msg.Chat.Id,
                                      text: $"Користувача з даним Id в системі немає",
                                      replyMarkup: MainButtons.AdminKeyboard());
                    }
                }

                if (msg.Text != null)
                {

                    switch (msg.Text)
                    {
                        case "/start":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Вітаю! Режим адміністратора успішно активовано",
                                replyMarkup: MainButtons.AdminKeyboard());
                            break;

                        case "/menu":
                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Головне меню",
                                replyMarkup: MainButtons.AdminKeyboard());
                            break;

                        case "🟢 Активні доставки":

                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Введіть дату доставки у форматі MM/DD/YYYY",
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
                            break;

                        case "🆔 Пошук користувача за Id":

                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Введіть Id користувача",
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
                            break;

                        case "📲 Пошук користувача за номером":

                            await Program.client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Введіть номер користувача у форматі 380999999999",
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
                            break;


                        case "❌ Видалити доставку":

                            await Program.client.SendTextMessageAsync(
                               chatId: msg.Chat.Id,
                               text: "Для видалення доставки введіть Id користувача",
                               replyMarkup: new ForceReplyMarkup() { Selective = true });
                            break;

                        case "🔍 Пошук доставки за Id":

                            await Program.client.SendTextMessageAsync(
                               chatId: msg.Chat.Id,
                               text: "Для пошуку доставки введіть Id користувача",
                               replyMarkup: new ForceReplyMarkup() { Selective = true });

                            break;

                        case "✅ Підтвердити доставку":

                            await Program.client.SendTextMessageAsync(
                               chatId: msg.Chat.Id,
                               text: "Для підтвердження доставки введіть Id користувача",
                               replyMarkup: new ForceReplyMarkup() { Selective = true });

                            break;

                        //default:
                        //    await Program.client.SendTextMessageAsync(msg.Chat.Id, "Виберіть пункт меню", replyMarkup: MainButtons.AdminKeyboard());
                        //    break;
                    }

                }


            }
                    
        }
    }
}
