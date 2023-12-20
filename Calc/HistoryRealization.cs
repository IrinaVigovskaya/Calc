using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public interface IHistory
    {
        string GetHistoryData();
        string UpdateHistoryData(string History, string historyData);
        string ClearHistory(string History);
    }

    public class RAM_History: IHistory
    {
        public string GetHistoryData()
        {
            return null;
        }

        public string UpdateHistoryData(string History, string data)
        {
            return History += data;
        }

        public string ClearHistory(string History)
        {
            return null;
        }
    }


    public class DataBase : IHistory
    {

        public string GetHistoryData()
        {
            string TextHistory = null;
            string connectionString = @"Data Source = C:\Users\Admin\OneDrive\Рабочий стол\Учёба\Разработка\Calc\CalculDB.sqlite;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM History";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TextHistory += reader["example"];
                        }
                    }
                }
                connection.Close();
            }
            return TextHistory;
        }

        public string UpdateHistoryData(string History, string historyData)
        {
            string connectionString = @"Data Source = C:\Users\Admin\OneDrive\Рабочий стол\Учёба\Разработка\Calc\CalculDB.sqlite;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO History (example) VALUES (@dataToAdd)";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@dataToAdd", historyData);
                    command.ExecuteNonQuery();
                }

                connection.Close();
                return GetHistoryData();
            }
        }

        public string ClearHistory(string History)
        {
            string connectionString = @"Data Source = C:\Users\Admin\OneDrive\Рабочий стол\Учёба\Разработка\Calc\CalculDB.sqlite;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM History";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
                return GetHistoryData();
            }

        }
    }

    public class FileHistory: IHistory
    {
        public string GetHistoryData()
        {
            string path = @"C:\Users\Admin\OneDrive\Рабочий стол\Учёба\Разработка\Calc\ExampleFile.txt";
            string data = System.IO.File.ReadAllText(path);
            return data;
        }

        public string UpdateHistoryData(string History, string historyData)
        {
            string path = @"C:\Users\Admin\OneDrive\Рабочий стол\Учёба\Разработка\Calc\ExampleFile.txt";
            System.IO.File.AppendAllText(path, historyData);
            return GetHistoryData();
        }

        public string ClearHistory(string History)
        {
            string path = @"C:\Users\Admin\OneDrive\Рабочий стол\Учёба\Разработка\Calc\ExampleFile.txt";
            System.IO.File.WriteAllText(path, string.Empty);
            return null;
        }

    }

}
