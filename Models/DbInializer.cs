using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VMMVC.Models
{
    public class DbInializer : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext cont)
        {
            cont.Products.Add(new Product()
            {

                Id = 0,
                Name = "Шоколад",
                Price = 13,
                Rest = 10
            });

            cont.Products.Add(new Product()

            {
                Id = 1,
                Name = "печенье",
                Price = 18,
                Rest = 20
            });

            cont.Products.Add(new Product()


            {
                Id = 2,
                Name = "чай",
                Price = 21,
                Rest = 20
            });

            cont.Products.Add(new Product()

            {
                Id = 3,
                Name = "Кофе",
                Price = 35,
                Rest = 15
            });
            
        }
    }
}