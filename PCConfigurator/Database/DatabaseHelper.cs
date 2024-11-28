using System;
using System.Data.SQLite;
using System.IO;

namespace PCConfigurator
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            // Указываем путь к базе данных. Путь относительно исполнимого файла.
            string databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "configurator.db");
            connectionString = $"Data Source={databaseFilePath};Version=3;";
        }

        // Метод для получения соединения с базой данных
        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        // Пример метода для выполнения запроса (например, получение всех пользователей)
        public void GetUsers()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Users"; // Пример SQL-запроса
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = reader["Username"].ToString();
                            Console.WriteLine(username); // Печать результата запроса
                        }
                    }
                }
            }
        }

        // Пример метода для добавления нового пользователя
        public void AddUser(string username, string password)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cmd.ExecuteNonQuery(); // Выполняем запрос
                }
            }
        }
    }
}
