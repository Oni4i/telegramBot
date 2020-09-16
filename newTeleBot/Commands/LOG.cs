using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace newTeleBot.Commands
{
    class LOG : Command
    {
        public LOG(TelegramBotClient bot, Chat chat, string receiveMessage) : base(bot, chat, receiveMessage)
        {
            if (receiveParams.Length == 3)
            {
                string dateOfLog = receiveParams[0];
                string transact = receiveParams[1];
                string operation = receiveParams[2];

                string pathDateOfLogDirectory = Const.PathLog + "\\" + String.Join('_', dateOfLog.Split('.'));
                string pathFile = pathDateOfLogDirectory + "\\" + transact + "_" + operation + Const.LogExtension;

                if (!IsDirectoryExists(Const.PathLog))
                    MessageToSend = $"Directory with path {Const.PathLog} isn't exist";
                else if (!IsDirectoryExists(pathDateOfLogDirectory))
                    MessageToSend = $"Directory with path {pathDateOfLogDirectory} isn't exist";
                else if (!isFileExists(pathFile))
                    MessageToSend = $"File with path {pathFile} isn't exist";
                else
                    MessageToSend = System.IO.File.ReadAllText(pathFile);

                sendMessage(MessageToSend);
            } else if (receiveParams.Length == 2)
            {
                string dateOfLog = receiveParams[0];
                string transact = receiveParams[1];

                string pathDateOfLogDirectory = Const.PathLog + "\\" + String.Join('_', dateOfLog.Split('.'));

                if (!IsDirectoryExists(Const.PathLog))
                    MessageToSend = $"Directory with path {Const.PathLog} isn't exist";
                else if (!IsDirectoryExists(pathDateOfLogDirectory))
                    MessageToSend = $"Directory with path {pathDateOfLogDirectory} isn't exist";
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(pathDateOfLogDirectory);

                    SendFiles(dir, transact);
                }
            } else if (receiveParams.Length < 2)
            {
                MessageToSend = $"Not enough parameters";
                sendMessage(MessageToSend);
            } else
            {
                MessageToSend = $"Too much parameters";
                sendMessage(MessageToSend);
            }
        }

        public bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool isFileExists(string path)
        {
            return System.IO.File.Exists(path);
        }
        public bool IsRequiredTransactFromPath(string path, string transact)
        {
            string[] splitedPath = path.Split('\\');
            string receivedTransact = splitedPath[splitedPath.Length - 1].Split('_')[0];

            if (receivedTransact == transact)
                return true;

            return false;
        }

        public string GetRequestFromPath(string path)
        {
            string[] splitedPath = path.Split('\\');
            string date = String.Join('.', splitedPath[splitedPath.Length - 2].Split('_'));
            string transactOperation = String.Join(' ', splitedPath[splitedPath.Length - 1].Split('_'));
            transactOperation = "/LOG " + date + " " + transactOperation.Substring(0, transactOperation.Length - Const.LogExtension.Length);

            return transactOperation;
        }

        async void SendFiles(DirectoryInfo dir, string transact)
        {
            foreach (var item in dir.GetFiles())
            {
                string path = item.FullName;
               
                if (IsRequiredTransactFromPath(path, transact))
                {
                    await senderBot.SendTextMessageAsync(Chat, GetRequestFromPath(path));
                    Thread.Sleep(100);
                }
            }
        }


    }
}
