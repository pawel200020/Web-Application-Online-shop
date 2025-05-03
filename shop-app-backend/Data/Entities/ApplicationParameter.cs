using System.ComponentModel.DataAnnotations;
using AppAbstract.Configuration;

namespace Data.Entities
{
    public class ApplicationParameter : IApplicationParameter
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
