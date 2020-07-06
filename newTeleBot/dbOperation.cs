
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Telegram.Bot.Types;


namespace newTeleBot
{
    static class dbOperation
    {
        static SQLiteConnection Connect = new SQLiteConnection($"DataSource={Const.DBName};Version=3;");
        static object _sqlite_sync = new object();

        static public bool TableExist(string aName)
        {
            lock (_sqlite_sync)
            {
                if (Connect.State == ConnectionState.Closed)
                    Connect.Open();


                string query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{aName}'";
                using (SQLiteCommand command = new SQLiteCommand(query, Connect))
                {
                    using (SQLiteDataReader data_reader = command.ExecuteReader())
                    {
                        if (data_reader.HasRows)
                            return true;
                        else
                            return false;
                    }
                }
            }
        }

        static public void CreateTable()
        {
            lock (_sqlite_sync)
            {
                if (Connect.State == ConnectionState.Closed)
                    Connect.Open();

                new SQLiteCommand($"CREATE TABLE logsInfo(" +
                            "'date' TEXT, " +
                            "'transaction' INTEGER, " +
                            "'operation' TEXT, " +
                            "'request' TEXT, " +
                            "'response' TEXT, " +
                            "'error' TEXT, " +
                            "'hash' TEXT, " +
                            "'nextHash' TEXT," +
                            "'isRightAnswer' INTEGER) "
                        , Connect).ExecuteReader();
            }
        }

        static public List<Dictionary<string, object>> GetField(string aTransact, string aOperation = null)
        {
            List<Dictionary<string, object>> resultField = new List<Dictionary<string, object>>();
            lock (_sqlite_sync)
            {
                if (Connect.State == ConnectionState.Closed)
                    Connect.Open();

                string query;

                if (aOperation == null)
                {
                    query = $"SELECT * FROM \"{Const.TableName}\" WHERE \"transaction\" = '{aTransact}';";
                }
                else
                {
                    query = $"SELECT * FROM \"{Const.TableName}\" WHERE \"transaction\" = '{aTransact}' and \"operation\" = '{aOperation}';";
                }

                SQLiteCommand command = new SQLiteCommand(query, Connect);
                SQLiteDataReader reader = command.ExecuteReader();

                int count = 0;
                foreach (DbDataRecord record in reader)
                {
                    resultField.Add(new Dictionary<string, object>(9));

                    for (int i = 0; i < record.FieldCount; i++)
                    {
                        resultField[count].Add(record.GetName(i), record.GetValue(i));
                    }
                    count++;
                }
            }

            return resultField;
        }

        static public List<Dictionary<string, object>> GetFieldByDate(string year, string month = null, string day = null)
        {
            List<Dictionary<string, object>> resultField = new List<Dictionary<string, object>>();

            lock (_sqlite_sync)
            {
                if (Connect.State == ConnectionState.Closed)
                {
                    Connect.Open();
                }

                string query = $"SELECT * FROM \"{Const.TableName}\" WHERE substr(\"date\", 7, 4) = '{year}'";

                if (month != null)
                {
                    query += $" and substr(\"date\", 4, 2) = '{month}'";

                    if (day != null)
                    {
                        query += $" and substr(\"date\", 1, 2) = '{day}'";
                    }
                }

                SQLiteCommand command = new SQLiteCommand(query, Connect);
                SQLiteDataReader reader = command.ExecuteReader();

                int count = 0;
                foreach (DbDataRecord record in reader)
                {
                    resultField.Add(new Dictionary<string, object>(9));

                    for (int i = 0; i < record.FieldCount; i++)
                    {
                        resultField[count].Add(record.GetName(i), record.GetValue(i));
                    }
                    count++;
                }

            }


            return resultField;
        }
        static public List<Dictionary<string, object>> GetFieldByOwnRequest(string[] request)
        {
            List<Dictionary<string, object>> resultField = new List<Dictionary<string, object>>();

            lock (_sqlite_sync)
            {
                if (Connect.State == ConnectionState.Closed)
                {
                    Connect.Open();
                }

                string query = string.Join(" ", request);

                SQLiteCommand command = new SQLiteCommand(query, Connect);
                SQLiteDataReader reader = command.ExecuteReader();

                int count = 0;
                foreach (DbDataRecord record in reader)
                {
                    resultField.Add(new Dictionary<string, object>(9));

                    for (int i = 0; i < record.FieldCount; i++)
                    {
                        resultField[count].Add(record.GetName(i), record.GetValue(i));
                    }
                    count++;
                }
            }
            return resultField;

        }


    }
}
