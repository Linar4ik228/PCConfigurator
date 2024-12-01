using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace PCConfigurator
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            // Set the path to the database relative to the executable file.
            string databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "configurator.db");
            connectionString = ($"Data Source={databaseFilePath};Version=3;");
        }

        // Method to get a connection to the database
        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        // Get processors from the database
        public List<Component> GetProcessors()
        {
            var processors = new List<Component>();
            using (var connection = GetConnection())
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

        // Get graphics cards from the database
        public List<Component> GetGraphicsCards()
        {
            var gpus = new List<Component>();
            using (var connection = GetConnection())
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

        // Get motherboards from the database
        public List<Component> GetMotherboards()
        {
            var motherboards = new List<Component>();
            using (var connection = GetConnection())
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

        // Get RAM from the database
        public List<Component> GetRAM()
        {
            var ram = new List<Component>();
            using (var connection = GetConnection())
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

        // Get storage devices from the database
        public List<Component> GetStorageDevices()
        {
            var storage = new List<Component>();
            using (var connection = GetConnection())
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

        // Get cooling systems from the database
        public List<Component> GetCooling()
        {
            var cooling = new List<Component>();
            using (var connection = GetConnection())
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

        // Get cases from the database
        public List<Component> GetCases()
        {
            var cases = new List<Component>();
            using (var connection = GetConnection())
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

        // Component class to store component details (name and price)
        public class Component
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            // Override ToString() to display in ComboBox (with formatted price)
            public override string ToString()
            {
                return $"{Name} - {Price:N2} руб."; // Display price with two decimal places
            }
        }
    }
}
