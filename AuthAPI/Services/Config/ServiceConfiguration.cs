using AuthAPI.Repository.Implementation;
using AuthAPI.Repository.Interface;
using AuthAPI.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Services.Config
{
    public static class ServiceConfiguration
    {
        public static void AddScopedServices(this IServiceCollection service)
        {
            #region Service Injection
            service.AddScoped<IAuthService, AuthServices>();
            #endregion

            #region Repository Injection
            service.AddScoped<IAuthRepo, AuthRepo>();
            #endregion
        }
    }
}
