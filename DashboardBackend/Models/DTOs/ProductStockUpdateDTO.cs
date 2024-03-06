
namespace DashboardBackend.Models.DTOs
{
    public class ProductStockUpdateDTO
    {
        public Category[] Categories { get; set; } = [];
        public Size[] Sizes { get; set; } = [];
        public Color[] Colors { get; set; } = [];
        public int Stock { get; set; }
        public bool IsAdd { get; set; }
    }
}
