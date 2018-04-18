using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using CShop.Core.Models;
using CShop.Core.Repositories;
using CShop.Core.Services.Implementations;

namespace CShop.Core.UnitTests.Services
{
   public class ProductServiceTests
   {
       private ProductService _productService;
       private IProductRepository _productRepository;

       [SetUp]
       public void SetUp()
       {
           _productRepository = A.Fake<IProductRepository>();
           _productService = new ProductService(_productRepository);
       }

       [Test]
       public void GetAll_ReturnsProductList()
       {
           //Arrange
           var expectedProducts = new List<AnimalModel>
           {
               new AnimalModel { id = 1337 }
           };

           A.CallTo(() => _productRepository.GetAll()).Returns(expectedProducts);

           //Act
           var result = _productService.GetAll();

           //Assert
           Assert.That(result, Is.EqualTo(expectedProducts));
       }
    }
}
