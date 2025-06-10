using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace P3
{
    public partial class ProfilePage : ContentPage
    {
        public ObservableCollection<Order> InTransitOrders { get; private set; }
        private ObservableCollection<Order> receivedOrders;
        private ObservableCollection<Order> deletedOrders;

        public ProfilePage()
        {
            InitializeComponent();
            InTransitOrders = new ObservableCollection<Order>();
            receivedOrders = new ObservableCollection<Order>();
            deletedOrders = new ObservableCollection<Order>();

            InTransitOrdersListView.ItemsSource = InTransitOrders;
            ReceivedOrdersListView.ItemsSource = receivedOrders;
            DeletedOrdersListView.ItemsSource = deletedOrders;

            LoadOrders();
            CheckUserRegistration();
        }

        private void LoadOrders()
        {
            // Загрузка заказов из хранилища (например, Preferences или файл)
            var savedOrdersJson = Preferences.Get("Orders", string.Empty);
            if (!string.IsNullOrEmpty(savedOrdersJson))
            {
                var savedOrders = JsonConvert.DeserializeObject<ObservableCollection<Order>>(savedOrdersJson);
                foreach (var order in savedOrders)
                {
                    if (order.IsCancelled)
                    {
                        deletedOrders.Add(order);
                    }
                    else if (order.TotalPrice > 0) // Предполагаем, что заказы с ценой больше 0 в пути
                    {
                        InTransitOrders.Add(order);
                    }
                    else
                    {
                        receivedOrders.Add(order);
                    }
                }
            }
        }

        private async void OnDeleteOrderClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var order = button?.CommandParameter as Order;
            if (order != null)
            {
                bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить этот заказ?", "Да", "Нет");
                if (confirm)
                {
                    InTransitOrders.Remove(order);
                    order.IsCancelled = true;
                    deletedOrders.Add(order);
                    SaveOrders();
                }
            }
        }

        public void SaveOrders()
        {
            var allOrders = new ObservableCollection<Order>();

            // Добавляем заказы из каждой коллекции по одному
            foreach (var order in InTransitOrders)
            {
                allOrders.Add(order);
            }

            foreach (var order in receivedOrders)
            {
                allOrders.Add(order);
            }

            foreach (var order in deletedOrders)
            {
                allOrders.Add(order);
            }

            var ordersJson = JsonConvert.SerializeObject(allOrders);
            Preferences.Set("Orders", ordersJson);
        }
        private async void CheckUserRegistration()
        {
            var userLogin = Preferences.Get("User Login", string.Empty);
            if (string.IsNullOrEmpty(userLogin))
            {
                await Navigation.PushAsync(new RegistrationPage());
            }
            else
            {
                DisplayUserLogin(userLogin);
            }
        }

        private void DisplayUserLogin(string login)
        {
            UserLoginLabel.Text = $"Добро пожаловать, {login}!";
        }
        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            // Переход на главную страницу
            await Navigation.PushAsync(new MainPage());
        }
        private async void OnDeleteAllOrdersClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить все заказы навсегда?", "Да", "Нет");
            if (confirm)
            {
                // Удаляем все заказы из коллекции DeletedOrders
                deletedOrders.Clear();

                SaveOrders();
            }
        }
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
{
    bool confirm = await DisplayAlert("Подтверждение", "Вы уверены, что хотите выйти?", "Да", "Нет");
    if (confirm)
    {
        Preferences.Remove("User  Login");
        Preferences.Remove("Orders"); // Удаление всех заказов

        await Navigation.PushAsync(new RegistrationPage());
    }
}
    }
}