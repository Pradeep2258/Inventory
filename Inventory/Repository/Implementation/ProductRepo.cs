using Inventory.Model;
using Inventory.Repository.Interface;
using Inventory.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly ILogger<ProductRepo> _logger;
        private readonly IConfiguration _config;
        private readonly string connectionString;
        public ProductRepo(ILogger<ProductRepo> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
            connectionString = _config.GetConnectionString("DBConnection");
        }

        public bool AddProductsRepo(ProductModel product)
        {
            const string methodName = "AddProductsRepo";
            _logger.LogDebug($"Method Entry : {methodName}");
            bool result = false;
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = cnn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = Constants.SP_INSERT_PRODUCT
                };
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@product_name", product.ProductName),
                    new SqlParameter("@category_id", product.CategoryID),
                    new SqlParameter("@price", product.Price),
                    new SqlParameter("@quantity", product.Quantity),
                    new SqlParameter("@merchant_id", product.MerchantId),
                    new SqlParameter("@inserted_by", product.InsertedBy)
                };
                cmd.Parameters.AddRange(sqlParameters);
                cnn.Open();
                cmd.ExecuteScalar();
                result = true;
                cnn.Close();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx.StackTrace + ":" + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }

        public bool UpdateProduct(ProductModel product)
        {
            const string methodName = "UpdateProduct";
            _logger.LogDebug($"Method Entry : {methodName}");
            bool result = false;
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = cnn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = Constants.SP_UPDATE_PRODUCT
                };
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@product_id", product.ProductID),
                    new SqlParameter("@product_name", product.ProductName),
                    new SqlParameter("@category_id", product.CategoryID),
                    new SqlParameter("@price", product.Price),
                    new SqlParameter("@quantity", product.Quantity),
                };
                cmd.Parameters.AddRange(sqlParameters);
                cnn.Open();
                cmd.ExecuteScalar();
                result = true;
                cnn.Close();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx.StackTrace + ":" + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }

        public List<ProductModel> GetProductsRepo(ProductFilter filter)
        {
            const string methodName = "GetProductsRepo";
            _logger.LogDebug($"Method Entry : {methodName}");
            //string conn
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                cnn.Open();
                string query = string.Format("select product_name,price,quantity from product where is_active = 1 and  product_name  like '%{0}%' ", filter.ProductName);
                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductModel model = new ProductModel
                    {
                        ProductName = sqlDataReader.GetString(0),
                        Price = sqlDataReader.GetDecimal(1),
                        Quantity = sqlDataReader.GetInt32(2)
                    };
                    products.Add(model);
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx.StackTrace + ":" + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return products;
        }

        public bool DeleteProduct(int productId)
        {
            const string methodName = "UpdateProduct";
            _logger.LogDebug($"Method Entry : {methodName}");
            bool result = false;
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = cnn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = Constants.SP_DELETE_PRODUCT
                };
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@product_id", productId),
                  
                };
                cmd.Parameters.AddRange(sqlParameters);
                cnn.Open();
                cmd.ExecuteScalar();
                result = true;
                cnn.Close();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx.StackTrace + ":" + sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return result;
        }
    }
}
