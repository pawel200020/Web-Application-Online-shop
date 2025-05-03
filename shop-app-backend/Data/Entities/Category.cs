using System.ComponentModel.DataAnnotations;
using AppAbstract.Store;
using Data.Validation;

namespace Data.Entities
{
	public class Category : ICategory
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "This field with name {0} required")]
		[StringLength(50)]
		[FirstLetterUppercase]
		public string Name { get; set; } = null!;
    }
}
