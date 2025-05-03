using AppAbstract.Store;
using Microsoft.AspNetCore.Http;

namespace AppCore.BusinessEntities;

public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAvalible { get; }
    public double Price { get; }
    public int Quantity { get; }
    public DateTime ManufactureDate { get; }
    public string? Picture { get; }
    public string? Caption { get; }
    public IEnumerable<IProductsCategories> ProductsCategories { get; }
    public double AverageVote { get; }
    public int UserVote { get; }
    public IFormFile? PictureFile { get; }
    public IEnumerable<ICategory> Categories { get; }
}