using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace newTeleBot.Commands
{
    class HELP : Command
    {
        public HELP(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            MessageToSend = "Commands:\n";

            foreach (CommandsList cmd in Enum.GetValues(typeof(CommandsList)))
            {
                MessageToSend += "/" + cmd.ToString() + "\n";
            }

            sendMessage(MessageToSend);
        }
    }
}