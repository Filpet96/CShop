using System.Collections.Generic;
//using Microsoft.Extensions.Configuration;
using CShop.Core.Models;
using System.Linq;
using CShop.Core.Repositories;
using CShop.Core.Repositories.Implementations;
using Dapper;
using MySql.Data.MySqlClient;


namespace CShop.Core.Repositories.Implementations
{
  public class ProductRepository : IProductRepository
  {

    private readonly string _connectionString;

        public ProductRepository(string connectionString)
    {
            _connectionString = connectionString;
    }

    public List<AnimalModel> GetAll()
    {
      using(var connection = new MySqlConnection(_connectionString))
      {
          return connection.Query<AnimalModel>(
              "SELECT * FROM animals").ToList();
      }

    }
  }
}
