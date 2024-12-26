using Talabat.Core.Entities;

namespace Talabt.API.DTOS
{
    public class ProductToReturnDto
    {
        private int id; 
        public String Name { get; set; }
        public String Description { get; set; }
        public String PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

       

       
    }
}
