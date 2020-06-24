using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace newTeleBot.Commands
{
    class TRANSACT : Command
    {
        public TRANSACT(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            if (receiveParams.Length > 1)
            {
                MessageToSend = "Too much arguments";
            } else if (receiveParams.Length == 1 && receiveParams[0] != "")
            {
                bool find = false;

                //search transact
                foreach (var file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "\\Log\\"))
                {
                    var fileSplit = file.Split("_");
                    var fileTransact = fileSplit[fileSplit.Length - 1][..^4];

                    if (!find && fileTransact == receiveParams[0])
                    {
                        find = true;


                        //open log
                        using (var a = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {

                            byte[] array = new byte[a.Length];
                            a.Read(array, 0, array.Length);
                            string textFromFile = System.Text.Encoding.UTF8.GetString(array);

                            MessageToSend = textFromFile;

                        }
                    }
                }

                if (!find)
                {
                    MessageToSend = $"Transaction {receiveParams[0]} not found";
                }
            }

            sendMessage(MessageToSend);
        }
    }
}
