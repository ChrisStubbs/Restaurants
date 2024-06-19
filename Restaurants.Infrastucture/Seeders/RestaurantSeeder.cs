using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastucture.Persistence;

namespace Restaurants.Infrastucture.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                List<Restaurant> restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private List<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Description = "KFC Corporation, doing business as KFC (also commonly referred to by its historical name Kentucky Fried Chicken), is an American fast food restaurant chain that specializes in fried chicken. Headquartered in Louisville, Kentucky, it is the world's second-largest restaurant chain",
                Category = "Fast food",
                HasDelivery = true,
                ContactEmail = "contact@kfc.com",
                Dishes =
                [
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs)",
                        Price = 5.99m
                    },
                    new()
                    {
                        Name = "Chicken legs",
                        Description = "Chicken legs",
                        Price = 4.99m
                    },
                    new()
                    {
                        Name = "Chicken breast",
                        Description = "Chicken"
                    }
                    ],
                Address = new()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "Wc2N 5DU"
                }
            },
            new()
            {
                Name = "McDonalds",
                Description = "McDonald's Corporation is an American multinational fast food chain, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States. ",
                Category = "Fast food",
                HasDelivery = true,
                ContactEmail = "Contact@McDonalds.com",
                Dishes =
                [
                    new()
                    {
                        Name = "Big Mac",
                        Description = "Big Mac",
                        Price = 3.99m
                    },
                    new()
                    {
                        Name = "Cheeseburger",
                        Description = "Cheeseburger",
                        Price = 2.99m
                    },
                    new()
                    {
                        Name = "McChicken",
                        Description = "McChicken",
                        Price = 4.99m
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Oxford St",
                    PostalCode = "W1D 1BS"
                }

            }
        ];

        return restaurants;
    }
}

