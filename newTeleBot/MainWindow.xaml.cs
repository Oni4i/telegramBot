using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using newTeleBot.Commands;
using System.Data;
using System.Data.SQLite;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using Telegram.Bot.Requests;
using System;

namespace newTeleBot
{


    public partial class MainWindow : Window
    {
        TelegramBotClient botClient;
        public SQLiteConnection Connect { get; set; }
        public object _sqlite_sync = new object();


        public MainWindow()
        {
            InitializeComponent();

            botInstalation();

        }

        void botInstalation()
        {

            botClient = new TelegramBotClient(Const.BotToken);

            botClient.OnMessage += Bot_OnMessage;
      

            botClient.StartReceiving();
        }



        async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message != null && e.Message.Text != null)
            {
                try
                {
                    if (e.Message != null && e.Message.Chat.Type == ChatType.Private)
                    {
                        privateMessage(e);



                    }
                    else if (e.Message != null && e.Message.Chat.Type == ChatType.Supergroup && e.Message.Text[0] == '/')
                    {
                        groupMessage(e);
                    }
                }
                catch (Exception ex)
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat, ex.Message);
                }
            }
            else
            {

            }
        }

        void groupMessage(MessageEventArgs e)
        {
            Chat chat = e.Message.Chat;
            string receiveMessage = e.Message.Text;

            //get command from message
            string command = receiveMessage.Split(' ')[0][1..];

            selectCommand(chat, command, receiveMessage);
        }


        void selectCommand(Chat chat, string command, string receiveMessage)
        {
            Command cmd;

            switch (command.ToUpper())
            {
                case "HELP":
                    cmd = new HELP(botClient, chat, receiveMessage);
                    break;
                case "LOG":
                    cmd = new LOG(botClient, chat, receiveMessage);
                    break;
                case "PIN":
                    cmd = new PIN(botClient, chat, receiveMessage);
                    break;
                default:
                    cmd = new Command(botClient, chat, receiveMessage);
                    cmd.sendMessage("Command not found");
                    break;
            }
        }

        async void privateMessage(MessageEventArgs e)
        {
            bool find = false;

            //update status
            grid_root.Dispatcher.Invoke(() => StatusLabel.Content = "I received message");
            //update status
            Task.Run(() => {
                System.Threading.Thread.Sleep(500);
                grid_root.Dispatcher.Invoke(() => StatusLabel.Content = "Checking match...");
            });

            //search transact
            foreach (var file in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + "\\Log\\"))
            {
                var fileSplit = file.Split("_");
                var fileTransact = fileSplit[fileSplit.Length - 1][..^4];

                if (!find && fileTransact == e.Message.Text)
                {
                    find = true;

                    //update status
                    Task.Run(() => {
                        System.Threading.Thread.Sleep(500);
                        grid_root.Dispatcher.Invoke(() => StatusLabel.Content = "Matched!");
                    });

                    //open log
                    using (var a = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        //update status
                        Task.Run(() => {
                            System.Threading.Thread.Sleep(500);
                            grid_root.Dispatcher.Invoke(() => StatusLabel.Content = "Sending logs...");
                        });


                        byte[] array = new byte[a.Length];
                        a.Read(array, 0, array.Length);
                        string textFromFile = System.Text.Encoding.UTF8.GetString(array);

                        //Send message
                        await botClient.SendTextMessageAsync(
                            e.Message.Chat,
                            textFromFile
                            );

                        //update status
                        Task.Run(() => {
                            System.Threading.Thread.Sleep(500);
                            grid_root.Dispatcher.Invoke(() => StatusLabel.Content = "Sent!");
                        });
                    }
                }
            }

            if (!find)
            {

                await botClient.SendTextMessageAsync(
                        e.Message.Chat,
                        "Transaction not found"
                        );

                //update status
                Task.Run(() => {
                    System.Threading.Thread.Sleep(500);
                    grid_root.Dispatcher.Invoke(() => StatusLabel.Content = "Transaction not found");
                });
            }
        }
    }
}
