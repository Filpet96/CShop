using System.Collections.Generic;
using CShop.Core.Models;
using CShop.Core.Repositories;
using CShop.Core.Repositories.Implementations;


namespace CShop.Core.Services.Implementations
{
  public class ProductService : IProductService
  {

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productsRepository)
    {
        _productRepository = productsRepository;
    }

    public List<AnimalModel> GetAll()
    {
            // Write manual fail here.
      return _productRepository.GetAll();
    }
  }
}
