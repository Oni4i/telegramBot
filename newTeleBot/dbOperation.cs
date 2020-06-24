
using System.Data;
using System.Data.SQLite;

namespace newTeleBot
{
    class dbOperation
    {
        static public bool TableExist(string aName, SQLiteConnection aConnect, object aSync)
        {
            lock (aSync)
            {
                if (aConnect.State == ConnectionState.Closed)
                    aConnect.Open();
               

                string query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{aName}'";
                using (SQLiteCommand command = new SQLiteCommand(query, aConnect))
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


        static public void createTable(SQLiteConnection aConnect, object aSync)
        {
            lock (aSync)
            {
                if (aConnect.State == ConnectionState.Closed)
                    aConnect.Open();

                new SQLiteCommand($"CREATE TABLE logsInfo(" +
                            "'transact'	INTEGER, "  +
                            "'description' TEXT)"
                        , aConnect).ExecuteReader();
            }

            
        }
    }
}
