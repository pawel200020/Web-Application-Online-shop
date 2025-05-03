using AppCore.Store;
using Microsoft.Extensions.DependencyInjection;
using ShopCore;

namespace AppCore;

public static class ServicesConfiguration
{
    public static void AddAppCore(this IServiceCollection services)
    {
        services.AddScoped<ICategoriesManager, CategoriesManager>();
        services.AddScoped<Ratings>();
        services.AddScoped<Orders>();
        services.AddScoped<IProductsManager,ProductsManager>();
    }
}