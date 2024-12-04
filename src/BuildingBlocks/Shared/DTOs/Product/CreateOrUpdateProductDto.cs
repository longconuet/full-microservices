using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public abstract class CreateOrUpdateProductDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Max length for Name is 250 characters")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Max length for Name is 250 characters")]
        public string Summary { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
