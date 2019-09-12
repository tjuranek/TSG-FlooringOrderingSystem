using System.Collections.Generic;

namespace FOS.Models.Interfaces
{
    public interface IProductRepository
    {
        List<Product> RetrieveProducts();
    }
}
