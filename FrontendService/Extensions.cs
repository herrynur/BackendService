using FrontendService.Application.Service;
using FrontendService.Settings;

namespace FrontendService
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPlaceServices, PlaceServices>();

            return services;
        }
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiUrlSetting>(configuration.GetSection(nameof(ApiUrlSetting)));

            return services;
        }
    }
}
