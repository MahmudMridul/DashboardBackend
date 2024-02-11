﻿namespace DashboardBackend.Models
{
    public class Product
    {
        public int ID { get; set; }
        public Category Category { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public ProductStatus Status { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }

    public enum Category
    {
        Men,
        Women, 
        Children,
        Sports,
        Graphic
    }

    public enum Size
    {
        Small,
        Medium,
        Large,
        XLarge,
        XXLarge
    }

    public enum Color
    {
        Red,
        Blue,
        Green,
        Yellow,
        Black, 
        White, 
    }

    public enum ProductStatus
    {
        Low,
        Medium,
        Ok
    }

}
