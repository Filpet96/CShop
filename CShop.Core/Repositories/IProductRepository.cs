using System.Collections.Generic;
using CShop.Core.Models;

namespace CShop.Core.Repositories
{
    public interface IProductRepository
  {
    List<AnimalModel> GetAll();

  }
}
