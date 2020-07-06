using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace newTeleBot.Commands
{
    class HELP : Command
    {
        public HELP(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            MessageToSend = "Commands:\n";

            /*
            foreach (CommandsList cmd in Enum.GetValues(typeof(CommandsList)))
            {
                MessageToSend += "/" + cmd.ToString() + "\n";
            }*/

            foreach (var cmd in Const.Commands)
            {
                MessageToSend += $"/{cmd.Key} {string.Join(" ", cmd.Value)}\n";
            }


            sendMessage(MessageToSend);
            //sendMessageWithBtns(MessageToSend, listOfBtns);
        }
    }
}