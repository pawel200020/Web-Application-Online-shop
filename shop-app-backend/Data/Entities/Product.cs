using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppAbstract.Store;
using Data.Entities.Dependencies;
using Data.Validation;
using Microsoft.AspNetCore.Http;

namespace Data.Entities
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string Name { get; set; } = null!;
        public bool IsAvalible { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public double Price { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This field with name {0} required")]
        public DateTime ManufactureDate { get; set; }
        public string? Picture { get; set; }
        public string? Caption { get; set; }
        public IEnumerable<ProductsCategories> ProductsCategories { get; set; } = null!;
        [NotMapped]
        IEnumerable<IProductsCategories> IProduct.ProductsCategories
        {
            get => ProductsCategories;
        }
        [NotMapped]
        public double AverageVote { get; set; }
        [NotMapped]
        public int UserVote { get; set; }
        [NotMapped]
        public IFormFile? PictureFile { get; set; }

        [NotMapped]
        public IEnumerable<ICategory> Categories => ProductsCategories.Select(productsCategories => productsCategories.Category);
    }
}
