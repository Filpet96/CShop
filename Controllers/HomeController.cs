﻿using System;
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

            return View();
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
                connection.Execute(
                    "INSERT INTO cart (productId, amount, guid) VALUES (@id, 1, @guid)", new { id, guid }
                );
            }
            return RedirectToAction("Animals");
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

    }
}
