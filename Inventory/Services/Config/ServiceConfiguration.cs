using Inventory.Repository.Implementation;
using Inventory.Repository.Interface;
using Inventory.Services.Implementation;
using Inventory.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Services.Config
{
    public static class ServiceConfiguration
    {
        public static void AddScopedServices(this IServiceCollection service)
        {
            #region Service Injection
            service.AddScoped<IProductService, ProductService>();
            #endregion

            #region Repository Injection
            service.AddScoped<IProductRepo, ProductRepo>();
            #endregion
        }
    }
}
