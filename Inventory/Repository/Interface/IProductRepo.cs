using Inventory.Model;
using System.Collections.Generic;

namespace Inventory.Repository.Interface
{
    public interface IProductRepo
    {
        List<ProductModel> GetProductsRepo(ProductFilter filter);
        bool AddProductsRepo(ProductModel product);
        bool UpdateProduct(ProductModel product);
        bool DeleteProduct(int productId);
    }
}
