namespace DashboardBackend.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }

        // An order must be assoicated with a customer
        public int CustomerID { get; set; }
        public Customer Customer { get; set; } = null!;
    }

    public enum OrderStatus
    {
        Pending,
        Paused,
        Processing,
        Delivered,
    }
}
