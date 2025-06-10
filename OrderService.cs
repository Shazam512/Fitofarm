using System.Collections.ObjectModel;

namespace P3
{
    public static class OrderService
    {
        private static ObservableCollection<Order> orders = new ObservableCollection<Order>();

        public static void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public static ObservableCollection<Order> GetAllOrders()
        {
            return orders;
        }
    }
}