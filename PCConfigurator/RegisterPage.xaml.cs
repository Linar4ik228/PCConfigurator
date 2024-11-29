using PCConfigurator;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCConfigurator
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем значения из полей
            string username = "'" + tbLogin.Text + "'";
            string password = "'" + tbPassword.Password + "'";
            string confirmPassword = "'" + tbPassword2.Password + "'";

            // Проверка, что пароли совпадают
            if (password != confirmPassword)
            {
                // Если пароли не совпадают, показываем предупреждение через MessageBox
                MessageBox.Show("Пароли должны совпадать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Если одно из полей пустое
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                
                // Если пароли совпадают и все поля заполнены:
                SQLiteConnection connect = new SQLiteConnection("Data Source=Database\\configurator.db;Version=3;");
                try
                {
                    connect.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO Users (username, password_hash) VALUES ({username}, {password})", connect))
                    {
                        cmd.Parameters.AddWithValue("@username", tbLogin.Text);
                        cmd.Parameters.AddWithValue("@password", tbPassword.Password);
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connect.Close();
                }

                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Переход на страницу авторизации
                this.NavigationService.Navigate(new LoginPage());
            }
        }
    }
}
