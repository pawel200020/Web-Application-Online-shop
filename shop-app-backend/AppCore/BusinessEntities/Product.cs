using AppAbstract.Store;
using Microsoft.AspNetCore.Http;

namespace AppCore.BusinessEntities;

public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAvalible {  get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public DateTime ManufactureDate { get; set; }
    public string? Picture {  get; set;  }
    public string? Caption {  get; set;  }
    public IEnumerable<IProductsCategories> ProductsCategories { get; set;  }
    public double AverageVote {  get; set; }
    public int UserVote { get; set; }
    public IFormFile? PictureFile { get; set;  }
    public IEnumerable<ICategory> Categories { get; set; }
}