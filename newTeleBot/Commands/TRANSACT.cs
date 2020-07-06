using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace newTeleBot.Commands
{
    class TRANSACT : Command
    {
        public TRANSACT(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            if (receiveParams.Length == 1 || receiveParams.Length == 2)
            {
                var responseFromDB = receiveParams.Length == 1 ? dbOperation.GetField(receiveParams[0]) : dbOperation.GetField(receiveParams[0], receiveParams[1]);
                if (responseFromDB.Count() > 0)
                {
                    MessageToSend = "";
                    foreach (var field in responseFromDB)
                    {
                        foreach (var column in field)
                        {
                            MessageToSend += $"{column.Key}: {column.Value}\n";
                        }
                        MessageToSend += "\n_______________\n";
                    }
                }
                else if (receiveParams.Length == 1)
                {
                    MessageToSend = $"Transaction {receiveParams[0]} not found";
                }
                else
                {
                    MessageToSend = $"Transaction {receiveParams[0]} of operation {receiveParams[1]} not found";
                }
            }
            sendMessage(MessageToSend);
        }
    }
}
