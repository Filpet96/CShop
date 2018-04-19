using System;
using CShop.Core.Models;
using System.Collections.Generic;
namespace CShop.Models
{
    public class CheckoutModelInfo
    {
            public CheckoutModel displayCheckout { get; set; }
            public List<AnimalModel> AnimalList { get; set; }
    }
}
