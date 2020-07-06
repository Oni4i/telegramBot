using System;
using System.Collections.Generic;
using System.Text;

namespace newTeleBot
{
    static class Const
    {
        public static string DBName
        {
            get { return "logsDB.db"; }
        }
        public static string TableName
        {
            get { return "logsInfo"; }
        }
        public static string BotToken
        {
            get { return "1194145986:AAEgcwG2W14W3w7YVkn8bQW4HnBmET52Pec"; }
        }

        public static Dictionary<string, List<string>> Commands = new Dictionary<string, List<string>>
        {
            ["HELP"] = new List<string>{""},
            ["TRANSACT"] = new List<string> { "NUMBER", "OPERATION"},
            ["TRANSACTDATE"] = new List<string> { "YEAR", "MONTH", "DAY"},
            ["OWNREQUEST"] = new List<string> { "request"},
            ["PIN"] = new List<string> { "request" }
        };

    }
}
