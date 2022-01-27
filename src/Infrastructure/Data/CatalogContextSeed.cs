using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Data;

public class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext,
        ILoggerFactory loggerFactory, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.CatalogBrands.AnyAsync())
            {
                await catalogContext.CatalogBrands.AddRangeAsync(
                    GetPreconfiguredCatalogBrands());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogTypes.AnyAsync())
            {
                await catalogContext.CatalogTypes.AddRangeAsync(
                    GetPreconfiguredCatalogTypes());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogItems.AnyAsync())
            {
                await catalogContext.CatalogItems.AddRangeAsync(
                    GetPreconfiguredItems());

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            var log = loggerFactory.CreateLogger<CatalogContextSeed>();
            log.LogError(ex.Message);
            await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>
            {
                new("RPM"),
                new("Vitoil"),
                new("SnakeOil"),
                new("RimRam"),
                new("Other")
            };
    }

    static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>
            {
                new("Brakes"),
                new("Lighting"),
                new("Wheels & Tires"),
                new("Batteries"),
                new("Oil")
            };
    }

    static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
            {
                new(2,5, "Our Halogen Headlights are made to fit majority of vehicles with our  universal fitting mold. Product requires some assembly.", "Halogen Headlights (2 Pack)", 38.99M,  "http://catalogbaseurltobereplaced/images/products/product_lighting_headlight.jpg"),
                new(2,5, "Our Bugeye Headlights use Halogen light bulbs are made to fit into a standard bugeye slot. Product requires some assembly and includes light bulbs.", "Bugeye Headlights (2 Pack)", 48.99M, "http://catalogbaseurltobereplaced/images/products/product_lighting_bugeye-headlight.jpg"),
                new(2,5, "Clear bulb that with a universal fitting for all headlights/taillights.  Simple Installation, low wattage and a clear light for optimal visibility and efficiency.","Turn Signal Light Bulb", 6.49M, "http://catalogbaseurltobereplaced/images/products/product_lighting_lightbulb.jpg"),
                new(3,4, "A Parts Unlimited favorite, the Matte Finish Rim is affordable low profile style. Fits all low profile tires.","Matte Finish Rim", 75.99M, "http://catalogbaseurltobereplaced/images/products/product_wheel_rim.jpg"),
                new(3,1, "Stand out from the crowd with a set of aftermarket blue rims to make you vehicle turn heads and at a price that will do the same.","Blue Performance Alloy Rim", 88.99M, "http://catalogbaseurltobereplaced/images/products/product_wheel_rim-blue.jpg"),
                new(3,1, "Light Weight Rims with a twin cross spoke design for stability and reliable performance.","High Performance Rim", 99.49M, "http://catalogbaseurltobereplaced/images/products/product_wheel_rim-red.jpg"),
                new(3,4, "For the endurance driver, take advantage of our best wearing tire yet. Composite rubber and a heavy duty steel rim.","Wheel Tire Combo", 72.49M, "http://catalogbaseurltobereplaced/images/products/product_wheel_tyre-wheel-combo.jpg"),
                new(3,4, "Save time and money with our ever popular wheel and tire combo. Pre-assembled and ready to go.","Chrome Rim Tire Combo", 129.99M, "http://catalogbaseurltobereplaced/images/products/product_wheel_tyre-rim-chrome-combo.jpg"),
                new(3,1, "Having trouble in the wet? Then try our special patent tire on a heavy duty steel rim. These wheels perform excellent in all conditions but were designed specifically for wet weather.","Wheel Tire Combo (4 Pack)", 219.99M, "http://catalogbaseurltobereplaced/images/products/product_wheel_tyre-wheel-combo-pack.jpg"),
                new(1,5, "Our brake disks and pads perform the best togeather. Better stopping distances without locking up, reduced rust and dusk.","Disk and Pad Combo", 25.99M, "http://catalogbaseurltobereplaced/images/products/product_brakes_disk-pad-combo.jpg"),
                new(1,5, "Our Brake Rotor Performs well in wet coditions with a smooth responsive feel. Machined to a high tolerance to ensure all of our Brake Rotors are safe and reliable.","Brake Rotor", 18.99M, "http://catalogbaseurltobereplaced/images/products/product_brakes_disc.jpg"),
                new(1,1, "Upgrading your brakes can increase stopping power, reduce dust and noise. Our Disk Calipers exceed factory specification for the best performance.","Brake Disk and Calipers", 43.99M, "http://catalogbaseurltobereplaced/images/products/product_brakes_disc-calipers-red.jpg"),
                new(4,5, "Calcium is the most common battery type. It is durable and has a long shelf and service life. They also provide high cold cranking amps.","12-Volt Calcium Battery", 129.99M, "http://catalogbaseurltobereplaced/images/products/product_batteries_basic-battery.jpg"),
                new(4,1, "Spiral Coil batteries are the preferred option for high performance Vehicles where extra toque is need for starting. They are more resistant to heat and higher charge rates than conventional batteries.","Spiral Coil Battery", 154.99M, "http://catalogbaseurltobereplaced/images/products/product_batteries_premium-battery.jpg"),
                new(4,5, "Battery Jumper Leads have a built in surge protector and a includes a plastic carry case to keep them safe from corrosion.","Jumper Leads", 16.99M, "http://catalogbaseurltobereplaced/images/products/product_batteries_jumper-leads.jpg"),
                new(5,2, "Ensure that your vehicle's engine has a longer life with our new filter set. Trapping more dirt to ensure old freely circulates through your engine.","Filter Set", 28.99M, "http://catalogbaseurltobereplaced/images/products/product_oil_filters.jpg"),
                new(5,2, "This Oil and Oil Filter combo is suitable for all types of passenger and light commercial vehicles. Providing affordable performance through excellent lubrication and breakdown resistance.","Oil and Filter Combo", 34.49M, "http://catalogbaseurltobereplaced/images/products/product_oil_oil-filter-combo.jpg"),
                new(5,3, "This Oil is designed to reduce sludge deposits and metal friction throughout your cars engine. Provides performance no matter the condition or temperature.","Synthetic Engine Oil", 36.49M, "http://catalogbaseurltobereplaced/images/products/product_oil_premium-oil.jpg")
        };
    }
}
