using Inventory.Model;
using SharedModule;
using System.Collections.Generic;

namespace Inventory.Services.Interface
{
    public interface IProductService
    {
        List<ProductModel> GetProducts(ProductFilter filter, JwtUserDetails claimDetails);
        bool AddProducts(ProductModel product);
        bool UpdateProduct(ProductModel product);
        bool DeleteProduct(int productId);
    }
}
