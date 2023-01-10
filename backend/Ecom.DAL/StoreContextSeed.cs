using Ecom.BLL.Entities;
using Ecom.BLL.Entities.Order;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.DAL
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(context.ProductBrands!= null && !context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Ecom.Dal/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    for(int i =0 ; i <brands?.Count(); i++)
                    {
                        context.ProductBrands.Add(brands[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (context.ProductBrands != null && !context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Ecom.Dal/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    for (int i = 0; i < types?.Count(); i++)
                    {
                        context.ProductTypes.Add(types[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var productData = File.ReadAllText("../Ecom.Dal/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    for (int i = 0; i < products?.Count(); i++)
                    {
                        context.Products.Add(products[i]);
                    }
                    await context.SaveChangesAsync();
                }
                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethods = File.ReadAllText("../Ecom.Dal/Data/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethods);
                    for (int i = 0; i < methods?.Count(); i++)
                    {
                        context.DeliveryMethods.Add(methods[i]);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}
