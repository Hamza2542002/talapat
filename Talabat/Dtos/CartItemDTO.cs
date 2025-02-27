using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class CartItemDTO
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? PicUrl { get; set; }
        [Required]
        public string? BrandName { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        [Required, Range(0.1,double.MaxValue, ErrorMessage = "Price Must be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required, Range(1,double.MaxValue, ErrorMessage = "Quantity Must be Greater Than Zero")]
        public int Quantity { get; set; }
    }
}