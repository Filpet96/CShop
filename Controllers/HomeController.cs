using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CShop.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace CShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;

        public HomeController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConnectionString");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Animals()
        {
            ViewData["Message"] = "Animals Page.";
            List<AnimalModel> animal;
            using(var connection = new MySqlConnection(_connectionString))
            {
                animal = connection.Query<AnimalModel>(
                    "SELECT * FROM animals").ToList();
            }

            return View(animal);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Cart()
        {
            ViewData["Message"] = "My cart page.";

            List<AnimalModel> products;

            var guid = GetGuidCookie();

            using (var connection = new MySqlConnection(_connectionString))
            {
                products = connection.Query<AnimalModel>(
                    "SELECT cart.*, animals.* FROM cart, animals WHERE cart.guid=@guid AND cart.productId=animals.id", new { guid }).ToList();
            }



            return View(products);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult AddToCart(int id)
        {
            var guid = GetGuidCookie();

            using (var connection = new MySqlConnection(_connectionString))
            {
                var checkProduct = connection.QuerySingleOrDefault(

                    "SELECT productId FROM cart WHERE productId=@id AND guid=@guid", new { id, guid }
                );

                if (checkProduct == null)
                {
                    connection.Execute(

                        "INSERT INTO cart (productId, amount, guid) VALUES (@id, 1, @guid)", new { id, guid }
                    );
                } else {
                    connection.Execute(

                        "UPDATE cart SET amount=amount+1 WHERE guid=@guid AND productId=@id", new { guid, id}
                    );
                }
                

            }
            return RedirectToAction("Animals");
        }
        public IActionResult EmptyCart()
        {
            var guid = GetGuidCookie();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(
                    "DELETE FROM cart WHERE guid=@guid ", new { guid }
                );
            }
            return RedirectToAction("Cart");
        }

        public string GetGuidCookie()
        {
            string guidCookie = Request.Cookies["guid"];
            if (guidCookie != null)
                return guidCookie;

            guidCookie = Guid.NewGuid().ToString();
            Response.Cookies.Append("guid", guidCookie);

            return guidCookie;
        }



        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Checkout(CheckoutModel checkout)
        {
            var guid = GetGuidCookie();

            if (string.IsNullOrWhiteSpace(checkout.Email) || string.IsNullOrWhiteSpace(checkout.Address))
            {
                return View();
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                try 
                {
                    var total = connection.QuerySingleOrDefault<decimal>(
                        "SELECT SUM(animals.price * cart.amount) AS total FROM cart, animals WHERE cart.guid=@guid AND cart.productId=animals.id", new { guid });

                    var productInfo = connection.Query<AnimalModel>(
                    "SELECT cart.*, animals.* FROM cart, animals WHERE cart.guid=@guid AND cart.productId=animals.id", new { guid }).ToList();

                    connection.Execute(
                        "INSERT INTO orders (guid, email, address, total) " +
                        "VALUES (@guid, @email, @address, @total)", 
                        new { guid, email = checkout.Email, address = checkout.Address, total }
                );
                    foreach (var product in productInfo)
                    {
                        
                   
                    connection.Execute(
                        "INSERT INTO orderRows (guid, productName, productPrice) " +
                        "VALUES (@guid, @productName, @productPrice)",
                            new { guid, productName = product.name, productPrice = product.price }
                                      );
                    }
                    connection.Execute(
                        "DELETE FROM cart WHERE guid=@guid ", new { guid }
                    );
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }

            return View(checkout);
        }

    }
}
