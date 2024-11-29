using System;
using System.Windows;
using System.IO;

namespace PCConfigurator
{
    public partial class MainApplication : Window
    {
        private decimal totalPrice = 0; // Переменная для общей стоимости
        private DatabaseHelper dbHelper;

        public MainApplication()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(); // Инициализация DatabaseHelper
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().Show();
        }

        private void btnBackFromMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;
        }

        private void btnComponents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuPanel.Visibility = Visibility.Collapsed;
                ComponentsPanel.Visibility = Visibility.Visible;
                FillComboBoxes();
                ComputerImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/image/fim.png"));
                btnClear.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void FillComboBoxes()
        {
            try
            {
                ComboBoxProcessors.Items.Clear();
                foreach (var processor in dbHelper.GetProcessors())
                {
                    ComboBoxProcessors.Items.Add(processor);
                }

                // Аналогично заполняются другие ComboBox'ы...
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox comboBox && comboBox.SelectedItem is Component selectedItem)
            {
                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            totalPrice = 0;

            if (ComboBoxProcessors.SelectedItem is Component processor)
                totalPrice += processor.Price;

            // Аналогично для других ComboBox'ов...

            TotalPriceText.Text = $"Общая стоимость: {totalPrice} руб.";
        }

        private void btnBackFromComponents_Click(object sender, RoutedEventArgs e)
        {
            ComponentsPanel.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxProcessors.SelectedItem = null;
            // Аналогично для других ComboBox'ов...
            TotalPriceText.Text = "Общая стоимость: 0 руб.";
        }

        public class Component
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }
}