using Data.Abstract;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DataServiceExtension
{
    public static void AddData(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException()), ServiceLifetime.Transient);
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IRatingsRepository,RatingsRepository>();
    }
}