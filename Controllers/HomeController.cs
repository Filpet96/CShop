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
            AnimalModel animal;
            using(var connection = new MySqlConnection(_connectionString))
            {
                 animal = connection.QuerySingleOrDefault<AnimalModel>(
                    "SELECT * FROM animals WHERE id = 1");
            }

            return View(animal);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
