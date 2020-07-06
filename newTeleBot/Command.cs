using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace newTeleBot
{
    class Command
    {
        protected TelegramBotClient senderBot { get; set; }
        protected Chat Chat {get; set;}
        protected string[] receiveParams { get; set; }
        protected string MessageToSend { get; set; }

        public Command (TelegramBotClient bot, Chat chat, string receiveMessage)
        {
            MessageToSend = "Wrong request";
            senderBot = bot;
            Chat = chat;
            receiveParams = receiveMessage.Split(" ")[1..];
        }

        public async void sendMessage(string messageText)
        {
            await senderBot.SendTextMessageAsync(Chat, messageText);
        }

        public async void sendMessageWithBtns (string messageText, List<InlineKeyboardButton> Btns)
        {
            await senderBot.SendTextMessageAsync(Chat.Id, messageText, replyMarkup: new InlineKeyboardMarkup(Btns));
        }
    }
}
