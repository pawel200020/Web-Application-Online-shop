using Microsoft.AspNetCore.Http;

namespace AppAbstract.Store;

public interface IProduct
{
    int Id { get; }
    string Name { get; }
    bool IsAvalible { get; }
    double Price { get; }
    int Quantity { get; }
    DateTime ManufactureDate { get; }
    string? Picture { get; }
    string? Caption { get; }
    IEnumerable<IProductsCategories> ProductsCategories { get;}
    double AverageVote { get; }
    int UserVote { get; }
    IFormFile? PictureFile { get; }
    IEnumerable<ICategory> Categories { get; }
}
