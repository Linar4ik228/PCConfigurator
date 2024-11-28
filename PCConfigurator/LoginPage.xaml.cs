using System;
using System.Collections.Generic;
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
            // Получаем значения из полей логина и пароля
            string username = tbLogin.Text;
            string password = tbPassword.Password;


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Если одно из полей пустое, показываем сообщение
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Успешная авторизация (любые данные считаются правильными)
                MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Переход на главное приложение (MainApplication)
                MainApplication mainApplication = new MainApplication();
                mainApplication.Show();

                var window = Window.GetWindow(this);
                if (window != null)
                {
                    window.Close();
                }
            }
        }
    }
}
