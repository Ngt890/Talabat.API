using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
  public  class Product:BaseEntity

    {
        public String Name { get; set; }   
        public String Description { get; set; }
        public String PictureUrl {  get; set; } 
        public decimal Price { get; set; }
       
        public int BrandId {  get; set; }     //FK for ProductBrand
        public int CategoryId {  get; set; }  //FK for ProductCategory

        //RelationShip
        public ProductBrand Brand { get; set; } //NavigationalProperty[1]
        public ProductCategory Category { get; set; }   //NavigationalProperty[1]




    }
}
