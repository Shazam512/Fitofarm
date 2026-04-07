using System.Collections.ObjectModel;

namespace P3
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Product> products;
        private ObservableCollection<Product> cart;

        public MainPage()
        {
            InitializeComponent();
            LoadProducts();
            ProductsCollectionView.ItemsSource = products;
            cart = new ObservableCollection<Product>();
        }

        // Метод для загрузки списка продуктов
        private void LoadProducts()
        {
            products = new ObservableCollection<Product>
            {
                new Product { Name = "Витамины для иммунитета", Price = 500, ImageUrl = "pos.png", Description = "Укрепляют иммунитет." },
                new Product { Name = "Омега-3 капсулы", Price = 800, ImageUrl = "omega.png", Description = "Полезны для сердца." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
                new Product { Name = "Пробиотики", Price = 600, ImageUrl = "file.png", Description = "Улучшает пищеварение." },
            };
        }

        // Обработчик нажатия на кнопку "Заказать" у продукта
        private async void OnOrderButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as Product;

            var existingProduct = cart.FirstOrDefault(p => p.Name == product.Name);
            if (existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                product.Quantity = 1;
                cart.Add(product);
            }

            await DisplayAlert("Успех", $"{product.Name} добавлен в корзину.", "OK");
        }

        private async void OnCartButtonClicked(object sender, EventArgs e)
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            await Navigation.PushAsync(new CartPage(cart, orders)); // Передаем корзину и коллекцию заказов
        }

        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            var profilePage = new ProfilePage();

            // Переходим на страницу профиля
            await Navigation.PushAsync(profilePage);
        }
    }
}