using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace PCConfigurator
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем значения из полей
            string username = tbLogin.Text;
            string password = tbPassword.Password;

            // Проверка, что поля не пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Строка подключения к базе данных SQLite
            using (SQLiteConnection connect = new SQLiteConnection("Data Source=C:\\Users\\Линар Загидуллин\\Source\\Repos\\PCConfigurator\\PCConfigurator\\Database\\configurator.db;Version=3;"))
            {
                try
                {
                    connect.Open();

                    // Создаём запрос для проверки существования пользователя и пароля
                    string query = "SELECT COUNT(*) FROM Users WHERE username = @username AND password_hash = @password";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connect))
                    {
                        // Добавляем параметры для предотвращения SQL инъекций
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // В реальной ситуации нужно бы использовать хеширование пароля

                        // Выполняем запрос
                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (result > 0)
                        {
                            MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Переход на главную страницу
                            MainApplication mainApplication = new MainApplication();
                            mainApplication.Show();

                            // Закрыть текущее окно или скрыть его
                            Window.GetWindow(this)?.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Логирование или обработка ошибки
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // Код для очистки, если необходимо
                    connect.Close();
                }
            }
        }
    }
}
