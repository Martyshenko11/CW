using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace WDelivery_Bot.Keyboards
{
     class MainButtons
    {

        public static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "💧 Замовити" }, new KeyboardButton { Text = "🛒 Кошик" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "💼 Про нас" }, new KeyboardButton { Text = "⚙️ Налаштування" } }
                },
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup GetSettingsButtons()
        {
            return new ReplyKeyboardMarkup

            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "🌆 Мiсто" }, KeyboardButton.WithRequestContact("📱 Телефон") },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "🔙 Назад" }, new KeyboardButton { Text = "🏠 Адреса" } }
                },
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup DeleteTrashItems()
        {
            return new ReplyKeyboardMarkup

            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "💵 Оформити замовлення" }, new KeyboardButton { Text = "❌ Видалити товари з корзини" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "🔙 Назад" } }
                },
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup DeleteItemsAgree()
        {
            return new ReplyKeyboardMarkup

            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "☑️ Підтвердити видалення" }, new KeyboardButton { Text = "🔙 Скасувати видалення" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "🔙 Назад" } }
                },
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup SecondMenuItems()
        {
            return new ReplyKeyboardMarkup

            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "☑️ Додати інший товар" }, new KeyboardButton { Text = "🛒 Кошик" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "🔙 Назад" } }
                },
                ResizeKeyboard = true
            };
        }


        public static IReplyMarkup AdminKeyboard()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "🟢 Активні доставки" }, new KeyboardButton { Text = "🔍 Пошук доставки за Id" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "📲 Пошук користувача за номером" }, new KeyboardButton { Text = "🆔 Пошук користувача за Id" } },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "❌ Видалити доставку" }, new KeyboardButton { Text = "✅ Підтвердити доставку" } }
                },
                ResizeKeyboard = true
            };
        }
    }
}
