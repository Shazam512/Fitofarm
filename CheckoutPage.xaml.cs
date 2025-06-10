using P3;
using System.Collections.ObjectModel;

namespace P3
{
    public partial class CheckoutPage : ContentPage
    {
        private ObservableCollection<Product> cart;
        private decimal totalPrice;
        private ProfilePage profilePage;

        public CheckoutPage(ObservableCollection<Product> cart, decimal totalPrice, ProfilePage profilePage)
        {
            InitializeComponent();
            this.cart = cart;
            this.totalPrice = totalPrice;
            this.profilePage = profilePage;

            TotalPriceLabel.Text = $"Итого: {totalPrice:C}";
        }

        public async void OnConfirmOrderClicked(object sender, EventArgs e)
        {
            var selectedPickupLocation = PickupLocationPicker.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedPickupLocation))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, выберите пункт выдачи.", "OK");
                return;
            }

            var selectedPaymentMethod = PaymentMethodPicker.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedPaymentMethod))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, выберите способ оплаты.", "OK");
                return;
            }

            var order = new Order
            {
                PickupLocation = selectedPickupLocation,
                ProductName = GetProductName(),
                Code = GenerateOrderCode(),
                TotalPrice = totalPrice,
                PaymentMethod = selectedPaymentMethod,
                UserLogin = Preferences.Get("User   Login", "Unknown User"),
                IsCancelled = false
            };

            // Добавьте заказ в список "Заказ в пути"
            profilePage.InTransitOrders.Add(order);
            profilePage.SaveOrders();

            await DisplayAlert("Успех", "Ваш заказ подтвержден!", "OK");
        }

        public string GetProductName()
        {
            return cart.Count > 0 ? cart[0].Name : "Неизвестный товар";
        }

        public string GenerateOrderCode()
        {
            var random = new Random();
            return $"{random.Next(100000, 999999)}";
        }
        public async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            var profilePage = new ProfilePage();

            // Переходим на страницу профиля
            await Navigation.PushAsync(profilePage);
        }
    }
}