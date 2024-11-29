using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows;

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

        // Получение процессоров из базы данных
        public List<Component> GetProcessors()
        {
            var processors = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM Processors";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            processors.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return processors;
        }

        // Получение видеокарт из базы данных
        public List<Component> GetGraphicsCards()
        {
            var gpus = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM Graphics_Cards";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gpus.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return gpus;
        }

        // Получение материнских плат из базы данных
        public List<Component> GetMotherboards()
        {
            var motherboards = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM Motherboards";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            motherboards.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return motherboards;
        }

        // Получение ОЗУ из базы данных
        public List<Component> GetRAM()
        {
            var ram = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM RAM";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ram.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return ram;
        }

        // Получение накопителей из базы данных
        public List<Component> GetStorage()
        {
            var storage = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM Storage";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storage.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return storage;
        }

        // Получение охлаждения из базы данных
        public List<Component> GetCooling()
        {
            var cooling = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM Cooling";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cooling.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return cooling;
        }

        // Получение корпусов из базы данных
        public List<Component> GetCases()
        {
            var cases = new List<Component>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT model, price FROM Cases";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cases.Add(new Component
                            {
                                Name = reader.GetString(0),
                                Price = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }
            return cases;
        }

        

       
    }
}
