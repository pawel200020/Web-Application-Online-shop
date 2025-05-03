using AppCore.Store;
using Data;
using Microsoft.Extensions.DependencyInjection;

namespace AppCore;

public static class AppCoreServiceExtension
{
    public static void AddAppCore(this IServiceCollection services)
    {
        services.AddScoped<ICategoriesManager, CategoriesManager>();
        services.AddScoped<IRatingsManager, RatingsManager>();
        services.AddScoped<IOrdersManager, OrdersManager>();
        services.AddScoped<IProductsManager,ProductsManager>();
        services.AddData();
    }
}