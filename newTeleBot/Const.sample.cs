using System;
using System.Collections.Generic;
using System.Text;

namespace newTeleBot
{
    static class Const
    {
        public static string BotToken {
            get { return ""; }
        }
        public static string PathLog
        {
            get { return ""; }
        }
        public static string LogExtension
        {
            get { return ""; }
        }

        public static Dictionary<string, List<string>> Commands
        {
            get { 
                return new Dictionary <string, List<string>> { 
                    ["HELP"]     = new List <string> { },
                    ["PIN"]      = new List <string> { "ID" },
                    ["LOG"] = new List <string> { "DATE (Format Y.m.d)", "LOG", "OPERATION"}
                };
            }
        }
    }
}
