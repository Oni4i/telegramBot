using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace newTeleBot.Commands
{
    class PIN : Command
    {
        public PIN(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            bot.UnpinChatMessageAsync(chat.Id);
            bot.PinChatMessageAsync(chat.Id, Convert.ToInt32(receiveParams[0]));
        }
    }
}
