namespace VMMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductContext : DbContext
    {
       
        public ProductContext()
            : base("name=ProductDb")
        {
        }

        

         public virtual DbSet<Product> Products { get; set; }
    }

   
}