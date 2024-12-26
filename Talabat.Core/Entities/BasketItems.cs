using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public  class BasketItems
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string PictureUrl { set; get; }  
        public string Brand { set; get; }
        public string Type {  set; get; }   
        public decimal Price { set; get; }
        public int Quantity {  set; get; }  
    }
}
