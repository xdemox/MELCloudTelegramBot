namespace MELCloudTelegramBot.Models;
public class Menu
{
    public Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup Keyboard { get; set; }
    public string Text { get; set; }
    public Telegram.Bot.Types.Enums.ParseMode ParseMode { get; set; }
}