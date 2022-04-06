using Inventory.Model;
using Inventory.Services.Implementation;
using Inventory.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedModule;
using SharedModule.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IProductService _productService;
        public InventoryController(ILogger<InventoryController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        [Route("/GetProducts")]
        public IActionResult GetProducts([FromQuery] ProductFilter filter)
        {
            const string methodName = "GetProducts";
            _logger.LogDebug($"Method Entry : {methodName}");
            List<ProductModel> model = new List<ProductModel>();
            try
            {
                //Validate the role
                JwtUserDetails claimDetails = Extension.GetJwtClaimUserDetails(HttpContext.User);

                model = _productService.GetProducts(filter, claimDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return Ok(model);
        }

        [HttpPost]
        [Route("/AddProducts")]
        public IActionResult AddProducts([FromBody] ProductModel product)
        {
            const string methodName = "AddProducts";
            _logger.LogDebug($"Method Entry : {methodName}");
            try
            {
                //Validate User with role

                var role = HttpContext.User.Claims;
                var data = _productService.AddProducts(product);
                if (data)
                {
                    return Ok("Data Saved");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return Ok("No Data Saved");
        }
        [HttpPut]
        [Route("/UpdateProducts")]
        public IActionResult UpdateProduct([FromBody] ProductModel product)
        {
            const string methodName = "UpdateProduct";
            _logger.LogDebug($"Method Entry : {methodName}");
            try
            {
                //Validate User with role

                var role = HttpContext.User.Claims;
                var data = _productService.UpdateProduct(product);
                if (data)
                {
                    return Ok("Product Updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return Ok("Product Not Updated");
        }
        [HttpPut]
        [Route("/DeleteProduct")]
        public IActionResult DeleteProduct([FromQuery]int productId)
        {
            const string methodName = "UpdateProduct";
            _logger.LogDebug($"Method Entry : {methodName}");
            try
            {
                //Validate User with role

                var role = HttpContext.User.Claims;
                var data = _productService.DeleteProduct(productId);
                if (data)
                {
                    return Ok("Product Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ":" + ex.Message);
            }
            _logger.LogDebug($"Method Exit : {methodName}");
            return Ok("Product Not Deleted");
        }
    }
}
