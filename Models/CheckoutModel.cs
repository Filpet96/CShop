using System;
namespace CShop.Models
{
    public class CheckoutModel
    {
            public string Guid { get; set; }
            public string Email { get; set; }
            public string Address   { get; set; }
            public decimal Total { get; set; }
    }
}
