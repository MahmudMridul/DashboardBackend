using System.ComponentModel.DataAnnotations;
using DashboardBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DashboardBackend.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public CustomerType Type { get; set; }

        // One customer can put multiple orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public enum CustomerType
    {
        Active,
        Inactive,
    }
}
