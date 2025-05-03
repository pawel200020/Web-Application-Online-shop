using Data.Abstract;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DataServiceExtension
{
    public static void AddData(this IServiceCollection services)
    {
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IRatingsRepository,RatingsRepository>();
    }
}