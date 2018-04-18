using System.Collections.Generic;
using CShop.Core.Models;

namespace CShop.Core.Services
{
  public interface IProductService
  {
    List<AnimalModel> GetAll();

  }
}
