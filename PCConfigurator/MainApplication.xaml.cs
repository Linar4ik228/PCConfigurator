﻿using System;
using System.Windows;

namespace PCConfigurator
{
    public partial class MainApplication : Window
    {
        private decimal totalPrice = 0; // Variable for total price
        private DatabaseHelper dbHelper;

        public MainApplication()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(); // Initialize DatabaseHelper
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
                // Clear existing items in ComboBoxes
                ComboBoxProcessors.Items.Clear();
                ComboBoxMotherboards.Items.Clear();
                ComboBoxStorage.Items.Clear();
                ComboBoxCooling.Items.Clear();
                ComboBoxGraphicsCards.Items.Clear();
                ComboBoxRAM.Items.Clear();
                ComboBoxCases.Items.Clear();

                // Load components from the database
                var processors = dbHelper.GetProcessors();
                var motherboards = dbHelper.GetMotherboards();
                var storage = dbHelper.GetStorageDevices();
                var cooling = dbHelper.GetCooling();
                var graphicsCards = dbHelper.GetGraphicsCards();
                var ram = dbHelper.GetRAM();
                var cases = dbHelper.GetCases();

                // Add items to ComboBoxes
                foreach (var processor in processors)
                    ComboBoxProcessors.Items.Add(processor);
                foreach (var motherboard in motherboards)
                    ComboBoxMotherboards.Items.Add(motherboard);
                foreach (var storageDevice in storage)
                    ComboBoxStorage.Items.Add(storageDevice);
                foreach (var coolingSystem in cooling)
                    ComboBoxCooling.Items.Add(coolingSystem);
                foreach (var graphicsCard in graphicsCards)
                    ComboBoxGraphicsCards.Items.Add(graphicsCard);
                foreach (var ramModule in ram)
                    ComboBoxRAM.Items.Add(ramModule);
                foreach (var caseComponent in cases)
                    ComboBoxCases.Items.Add(caseComponent);

                // Subscribe to selection change events
                ComboBoxProcessors.SelectionChanged += ComboBox_SelectionChanged;
                ComboBoxMotherboards.SelectionChanged += ComboBox_SelectionChanged;
                ComboBoxStorage.SelectionChanged += ComboBox_SelectionChanged;
                ComboBoxCooling.SelectionChanged += ComboBox_SelectionChanged;
                ComboBoxGraphicsCards.SelectionChanged += ComboBox_SelectionChanged;
                ComboBoxRAM.SelectionChanged += ComboBox_SelectionChanged;
                ComboBoxCases.SelectionChanged += ComboBox_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox comboBox && comboBox.SelectedItem != null)
            {
                var selectedComponent = comboBox.SelectedItem as Component;
                Console.WriteLine($"Выбран компонент: {selectedComponent?.Name}, Цена: {selectedComponent?.Price}");
                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            totalPrice = 0;  // Reset total price

            // Add price for selected components
            if (ComboBoxProcessors.SelectedItem is DatabaseHelper.Component processor)
                totalPrice += processor.Price;
            if (ComboBoxMotherboards.SelectedItem is DatabaseHelper.Component motherboard)
                totalPrice += motherboard.Price;
            if (ComboBoxStorage.SelectedItem is DatabaseHelper.Component storage)
                totalPrice += storage.Price;
            if (ComboBoxCooling.SelectedItem is DatabaseHelper.Component cooling)
                totalPrice += cooling.Price;
            if (ComboBoxGraphicsCards.SelectedItem is DatabaseHelper.Component graphicsCard)
                totalPrice += graphicsCard.Price;
            if (ComboBoxRAM.SelectedItem is DatabaseHelper.Component ram)
                totalPrice += ram.Price;
            if (ComboBoxCases.SelectedItem is DatabaseHelper.Component caseComponent)
                totalPrice += caseComponent.Price;

            TotalPriceText.Dispatcher.Invoke(() =>
            {
                TotalPriceText.Text = $"Общая стоимость: {totalPrice:N2} ₽";
            });
        }

        private void btnBackFromComponents_Click(object sender, RoutedEventArgs e)
        {
            ComponentsPanel.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear selected items from all ComboBoxes
            ComboBoxProcessors.SelectedItem = null;
            ComboBoxMotherboards.SelectedItem = null;
            ComboBoxStorage.SelectedItem = null;
            ComboBoxCooling.SelectedItem = null;
            ComboBoxGraphicsCards.SelectedItem = null;
            ComboBoxRAM.SelectedItem = null;
            ComboBoxCases.SelectedItem = null;

            // Update total price after clearing
            UpdateTotalPrice();
            TotalPriceText.Text = "Общая стоимость: 0 руб."; // This can be done in UpdateTotalPrice
        }

        public class Component
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            // Override ToString for better display in ComboBox
            public override string ToString()
            {
                return $"{Name} - {Price:N2} руб"; // Format price in rubles with two decimal places
            }
        }
    }
}