using System.ComponentModel.DataAnnotations;

namespace Talabt.API.DTOS
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string PictureUrl { set; get; }
        [Required]
        public string Brand { set; get; }
        [Required]
        public string Type { set; get; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Can Not Be Zero")]
        public decimal Price { set; get; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity Must Be One Item At Least")]
        public int Quantity { set; get; }
    }
}