using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace newTeleBot.Commands
{
    class OWNREQUEST : Command
    {
        public OWNREQUEST(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            
            if (receiveParams.Length > 2)
            {
                if (receiveParams[0].ToUpper() == "SELECT")
                {

                    var responseFromDB = dbOperation.GetFieldByOwnRequest(receiveParams);
                    if (responseFromDB.Count > 0)
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
                else
                {
                    MessageToSend = "You can use only SELECT-operation";
                }
            }
            else
            {
                MessageToSend = "Too low params";
            }


            sendMessage(MessageToSend);

        }

    }
}
