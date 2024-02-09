namespace DashboardBackend.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Paused,
        Processing,
        Delivered,
    }
}
