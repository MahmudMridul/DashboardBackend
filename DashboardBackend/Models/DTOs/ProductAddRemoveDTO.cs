namespace DashboardBackend.Models.DTOs
{
    public class ProductAddRemoveDTO
    {
        public Category Category { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public int Stock { get; set; }
    }
}
