using System;
using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace PCConfigurator
{
    public partial class MainApplication : Window
    {
        private decimal totalPrice = 0; // Переменная для общей стоимости

        public MainApplication()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки "Открыть меню"
        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            // Скрыть кнопку "Открыть меню"
            btnOpenMenu.Visibility = Visibility.Collapsed;

            // Показать меню с кнопками "Периферия" и "Комплектующие"
            MenuPanel.Visibility = Visibility.Visible;
        }

        // Обработчик для кнопки "Выйти"
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно
            this.Hide();

            // Открываем окно авторизации (MainWindow)
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        // Обработчик для кнопки "Назад" из меню
        private void btnBackFromMenu_Click(object sender, RoutedEventArgs e)
        {
            // Скрыть меню с кнопками "Периферия" и "Комплектующие"
            MenuPanel.Visibility = Visibility.Collapsed;

            // Показать кнопку "Открыть меню"
            btnOpenMenu.Visibility = Visibility.Visible;
        }

        // Обработчик для кнопки "Комплектующие"
        private void btnComponents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Скрыть меню
                MenuPanel.Visibility = Visibility.Collapsed;

                // Показать панель с комплектующими
                ComponentsPanel.Visibility = Visibility.Visible;

                // Заполнение ComboBox'ов данными (заглушка, если вы еще не заполнили их)
                FillComboBoxes();

                // Загрузка и отображение картинки
                ComputerImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/image/fim.png"));

                // Показать кнопки "Очистить" и "Сохранить"
                btnClear.Visibility = Visibility.Visible;
                
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, выводим ее в консоль или окно сообщения
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }

        }

        // Заполнение ComboBox'ов (заглушка для данных)
        private void FillComboBoxes()
        {
            // Заполнение ComboBox для процессоров
            ComboBoxProcessors.Items.Clear();
            ComboBoxProcessors.Items.Add(new Component { Name = "i3-3300", Price = 1200 });
            ComboBoxProcessors.Items.Add(new Component { Name = "i5-12500f", Price = 2500 });
            ComboBoxProcessors.Items.Add(new Component { Name = "Xeon 2640 v3", Price = 3200 });

            // Заполнение ComboBox для видеокарт
            ComboBoxGraphicsCards.Items.Clear();
            ComboBoxGraphicsCards.Items.Add(new Component { Name = "GTX 1050 Ti", Price = 15000 });
            ComboBoxGraphicsCards.Items.Add(new Component { Name = "RTX 3060", Price = 25000 });
            ComboBoxGraphicsCards.Items.Add(new Component { Name = "RX 6800", Price = 35000 });

            // Заполнение других ComboBox аналогично...
        }

        // Обработчик для изменения выбора в ComboBox
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox comboBox)
            {
                var selectedItem = comboBox.SelectedItem as Component;
                if (selectedItem != null)
                {
                    // Обновляем общую цену
                    UpdateTotalPrice();
                }
            }
        }

        // Метод для обновления общей цены
        private void UpdateTotalPrice()
        {
            totalPrice = 0;

            // Добавить цену для каждого выбранного компонента
            if (ComboBoxProcessors.SelectedItem is Component processor)
                totalPrice += processor.Price;

            if (ComboBoxGraphicsCards.SelectedItem is Component gpu)
                totalPrice += gpu.Price;

            // Добавить другие компоненты аналогично...

            // Обновить текст общей стоимости
            TotalPriceText.Text = $"Общая стоимость: {totalPrice} руб.";

            
            
        }

        // Обработчик для кнопки "Назад" из комплектующих
        private void btnBackFromComponents_Click(object sender, RoutedEventArgs e)
        {
            // Скрыть панель с комплектующими
            ComponentsPanel.Visibility = Visibility.Collapsed;

            // Показать панель с меню
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Очистка выбранных компонентов
            ComboBoxProcessors.SelectedItem = null;
            ComboBoxGraphicsCards.SelectedItem = null;
            ComboBoxMotherboards.SelectedItem = null;
            ComboBoxRAM.SelectedItem = null;
            ComboBoxStorage.SelectedItem = null;

            // Обновить общую цену (например, установить 0)
            TotalPriceText.Text = "Общая стоимость: 0 руб.";
        }


    }

    // Класс для представления компонента
    public class Component
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}