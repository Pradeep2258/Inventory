using Inventory.Model;
using Inventory.Repository.Interface;
using Inventory.Services.Interface;
using Microsoft.Extensions.Logging;
using SharedModule;
using System;
using System.Collections.Generic;

namespace Inventory.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepo _productRepo;
        public ProductService(ILogger<ProductService> logger,IProductRepo productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        public bool AddProducts(ProductModel product)
        {
            const string methodName = "AddProducts";
            _logger.LogDebug($"Method Entry : {methodName}");

            bool result;
            try
            {
                 result = _productRepo.AddProductsRepo(product);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }

        public bool DeleteProduct(int productId)
        {
            const string methodName = "DeleteProduct";
            _logger.LogDebug($"Method Entry : {methodName}");

            bool result;
            try
            {
                result = _productRepo.DeleteProduct(productId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }

        public List<ProductModel> GetProducts(ProductFilter filter, JwtUserDetails claimDetails)
        {
            const string methodName = "GetProductsService";
            _logger.LogDebug($"Method Entry : {methodName}");

            List<ProductModel> result;
            try
            {
                if (claimDetails.UserRole == 2)
                {
                    //testing chnages

                    result = _productRepo.GetProductsRepo(filter);
                }
                else
                {
                    throw new UnauthorizedAccessException("Unauthorise Access");
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }

        public bool UpdateProduct(ProductModel product)
        {
            const string methodName = "UpdateProduct";
            _logger.LogDebug($"Method Entry : {methodName}");

            bool result;
            try
            {
                result = _productRepo.UpdateProduct(product);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }
    }
}
