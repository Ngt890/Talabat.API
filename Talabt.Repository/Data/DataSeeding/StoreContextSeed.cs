using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.DataSeeding
{
    public class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            if (_dbcontext.ProductBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabt.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        _dbcontext.Set<ProductBrand>().Add(brand);
                }

               await _dbcontext.SaveChangesAsync();

            }

            if (_dbcontext.ProductCategories.Count() == 0)
            {
                var categoriesdata = File.ReadAllText("../Talabt.Repository/Data/DataSeeding/categories.json");
                var categories=JsonSerializer.Deserialize<List<ProductCategory>>(categoriesdata);
                if (categories?.Count > 0)
                {
                    foreach(var category in categories) 
                        _dbcontext.Set<ProductCategory>().Add(category);

                }
                 await _dbcontext.SaveChangesAsync();
            }



            if(_dbcontext.Products.Count() == 0)
            {
                var productdata = File.ReadAllText("../Talabt.Repository/Data/DataSeeding/products.json");
                var products=JsonSerializer.Deserialize<List<Product>>(productdata);
                if (products?.Count > 0)
                { foreach (var product in products)
                        _dbcontext.Set<Product>().Add(product);     
                 }

              await  _dbcontext.SaveChangesAsync();


            }

            if (!_dbcontext.DeliveryMethod.Any())
            {
                var DeliveryData = File.ReadAllText("../Talabt.Repository/Data/DataSeeding/delivery.json");
                var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
                if (Delivery?.Count > 0)
                {
                    foreach (var DeliverySet in Delivery)
                        _dbcontext.Set<DeliveryMethod>().Add(DeliverySet);
                }

                await _dbcontext.SaveChangesAsync();


            }


        }
       






    }
}
