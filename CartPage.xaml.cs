using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace P3
{
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<Product> cart;
        public ObservableCollection<Order> orders;

        public CartPage(ObservableCollection<Product> cart, ObservableCollection<Order> orders)
        {
            InitializeComponent();
            this.cart = cart;
            this.orders = orders;
            CartCollectionView.ItemsSource = this.cart;
            UpdateTotalPrice();
        }

        // Метод сохранения текущего состояния корзины в локальное хранилище
        public void SaveCart()
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            Preferences.Set("Cart", cartJson);
        }

        // Обработчик события нажатия на кнопку уменьшения количества товара
        public void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as Product;

            if (product.Quantity > 1)
            {
                product.Quantity--;
            }
            else
            {
                cart.Remove(product);
            }

            SaveCart(); // Сохраняем корзину после изменения
            UpdateTotalPrice();
        }

        // Обработчик события нажатия на кнопку увеличения количества товара
        public void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as Product;
            product.Quantity++;
            SaveCart(); // Сохраняем корзину после изменения
            UpdateTotalPrice();
        }

        // Обработчик события нажатия на кнопку удаления товара из корзины
        public void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as Product;
            cart.Remove(product);
            SaveCart();
            UpdateTotalPrice();
        }

        public void UpdateTotalPrice()
        {
            // Логика обновления цены
            decimal totalPrice = 0;
            foreach (var product in cart)
            {
                totalPrice += product.Price * product.Quantity;
            }
            TotalPriceLabel.Text = $"Итого: {totalPrice},00 ₽";
        }

        // Обработчик нажатия на кнопку оформления заказа
        public async void OnCheckoutButtonClicked(object sender, EventArgs e)
        {
            var totalPrice = cart.Sum(p => p.Price * p.Quantity);

            var profilePage = Application.Current.MainPage.Navigation.NavigationStack
                .OfType<ProfilePage>()
                .FirstOrDefault();

            var checkoutPage = new CheckoutPage(cart, totalPrice, profilePage);

            await Application.Current.MainPage.Navigation.PushAsync(checkoutPage);
        }

        public async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        public async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            var profilePage = new ProfilePage();

            await Navigation.PushAsync(profilePage);
        }
    }
}