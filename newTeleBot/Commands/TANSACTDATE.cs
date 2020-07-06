using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace newTeleBot.Commands
{
    class TRANSACTDATE : Command
    {
        public TRANSACTDATE(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            if (receiveParams.Length > 0 && receiveParams.Length < 4)
            {
                List<Dictionary<string, object>> responseFromDB = new List<Dictionary<string, object>>();
                switch (receiveParams.Length)
                {
                    case 1:
                        responseFromDB = dbOperation.GetFieldByDate(receiveParams[0]);
                        break;
                    case 2:
                        responseFromDB = dbOperation.GetFieldByDate(receiveParams[0], receiveParams[1]);
                        break;
                    case 3:
                        responseFromDB = dbOperation.GetFieldByDate(receiveParams[0], receiveParams[1], receiveParams[2]);
                        break;
                }
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
            }
            sendMessage(MessageToSend);

        }
    }
}