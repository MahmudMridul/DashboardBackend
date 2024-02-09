﻿namespace DashboardBackend.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public CustomerType Type {  get; set; } 
    }

    public enum CustomerType
    {
        Active,
        Inactive, 
    }
}
