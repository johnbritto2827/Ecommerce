using System;
using System.Collections.Generic;

namespace ECommerce.Models
{

    public class ECommerceModel
    {
        public Login LoginDetails;

        public ProductAdd ProductDetails;

        public EmailInfo EmailDetails;

        public Order OrderDetails;

        public List<Login>ListLoginModels { get; set; }
        public Login ListLoginModel { get; set; }
        public List<ProductAdd> ListProductModels { get; set; }
        public List<Order> ListOrderModels { get; set; }
        public List<EmailInfo> ListEmailModels { get; set; }

    }
    public class Login
    { 
        public Int32 UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class ProductAdd
    {
        public Int32 ProductId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public Int32 Discount { get; set; }
        public string Specification { get; set; }
        public Int32 QtyinStock { get; set; }
        public decimal Rating { get; set; }
        public string Colour { get; set; }
        public string Storage { get; set; }
        public string FileName { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Order
    {
        public Int32 OrderId { get; set; }
        public Int32 UserId { get; set; }
        public string Image { get; set; }
        public Int32 ProductId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Int64 PinCode { get; set; }
        public string Email { get; set; }
        public string AddressType { get; set; }
        public Int16 Status { get; set; }
    }

    public class EmailInfo
    {
        public Int32 Id { get; set; }
        public string Email { get; set; }
    }
}
