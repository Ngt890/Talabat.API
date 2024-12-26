using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public  class ProductWithBrandSpecification:BaseSpecifications<Product>
    {
        //is used to get all
        public ProductWithBrandSpecification(ProductSpecParams Params)
            :base(P=>
            (string.IsNullOrEmpty(Params.Search)||P.Name.ToLower().Contains(Params.Search))
            &&
            (!Params.BrandId.HasValue ||P.BrandId == Params.BrandId)
            &&
            (!Params.CategoryId.HasValue|| P.CategoryId== Params.CategoryId)
            
            //ProductBrand==BrandId && P=>P.ProductCategory==CategoryId
            )
        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P=>P.Category);
            if(!String.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                { case "PriceAsc":
                        AddOrderbyAsc(P=>P.Price);
                         break;
                        case "PriceDesc":
                        AddOrderbyDesc(P=>P.Price);
                        break ;
                    default:
                        AddOrderbyAsc(P=>P.Name);   
                            break;

                }
            }
            ApplyPagination(Params.PageSize * (Params.PageIndex-1),Params.PageSize);

        }
        public ProductWithBrandSpecification( int id):base(P=>P.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
          
        }

       

    }
}
