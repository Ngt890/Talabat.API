using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data
{
    public class StoreContext:DbContext
    {
        //Allowing for DependancyInjection
        public StoreContext(DbContextOptions<StoreContext> options)
            :base(options) 
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }

        public DbSet<Product>Products { get; set; } 
        public DbSet<ProductBrand> ProductBrands {  get; set; } 
        public DbSet<ProductCategory>ProductCategories { get; set; }   
        public DbSet<Order> Order { get; set; }    
        public DbSet<DeliveryMethod> DeliveryMethod{ get; set; }

        public DbSet<OrderItem > OrderItem { get; set; }   




    }
}
