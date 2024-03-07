using System.ComponentModel.DataAnnotations;

namespace DashboardBackend.Models
{
    public class OrderProduct
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
    }
}
