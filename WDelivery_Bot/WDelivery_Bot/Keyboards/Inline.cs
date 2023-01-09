using Telegram.Bot.Types.ReplyMarkups;

namespace WDelivery_Bot.Keyboards
{
    class Inline
    {
        public static InlineKeyboardMarkup CityKeyboard = new InlineKeyboardMarkup(new[]
{
    new []
    {
        InlineKeyboardButton.WithCallbackData("Київ"),

    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("Львів"),
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("Харків"),
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("Дніпро"),
    },
    new []
    {
    InlineKeyboardButton.WithCallbackData("Одеса")
    }
});


        public static InlineKeyboardMarkup MainOrderKeyboard = new InlineKeyboardMarkup(new[]
{
    new []
    {
        InlineKeyboardButton.WithCallbackData("Помпa"),
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("Bissleri"),
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("Natture"),
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("Jerrasic Water"),
    }
});


        public static InlineKeyboardMarkup VolumeKeyboard = new InlineKeyboardMarkup(new[]
{
    new []
    {
        InlineKeyboardButton.WithCallbackData("20 л."),
        InlineKeyboardButton.WithCallbackData("12 л."),
        InlineKeyboardButton.WithCallbackData("10 л."),
    },
     new []
    {
        InlineKeyboardButton.WithCallbackData("Назад до постачальників"),
    }
    });


        public static InlineKeyboardMarkup MainDelivery = new InlineKeyboardMarkup(new[]
      {
        new []
    {
        InlineKeyboardButton.WithCallbackData("💵 Оформити замовлення"),
    }
});


        public static InlineKeyboardMarkup DeliveryTime = new InlineKeyboardMarkup(new[]
       {
        new []
    {
        InlineKeyboardButton.WithCallbackData("9:00 - 12:00"),
    },
        new []
    {
        InlineKeyboardButton.WithCallbackData("12:00 - 15:00"),
    },
        new []
    {
        InlineKeyboardButton.WithCallbackData("15:00 - 18:00"),
    }
});


        public static InlineKeyboardMarkup DeliveryDay = new InlineKeyboardMarkup(new[]
       {
        new []
    {
        InlineKeyboardButton.WithCallbackData("📅 Завтра"),
    },
        new []
    {
        InlineKeyboardButton.WithCallbackData("🗓 Післязавтра"),
    }
});

       
    }
}
