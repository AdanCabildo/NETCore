using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;

namespace SampleWebApiAspNetCore.Services
{
    public class SeedDataService : ISeedDataService
    {
        public void Initialize(FoodDbContext foodContext, FilipinoDbContext filipinoContext)
        {
            foodContext.FoodItems.Add(new FoodEntity() { Calories = 1000, Type = "Starter", Name = "Lasagne", Created = DateTime.Now });
            foodContext.FoodItems.Add(new FoodEntity() { Calories = 1100, Type = "Main", Name = "Hamburger", Created = DateTime.Now });
            foodContext.FoodItems.Add(new FoodEntity() { Calories = 1200, Type = "Dessert", Name = "Spaghetti", Created = DateTime.Now });
            foodContext.FoodItems.Add(new FoodEntity() { Calories = 1300, Type = "Starter", Name = "Pizza", Created = DateTime.Now });

            foodContext.SaveChanges();

            filipinoContext.FilipinoItems.Add(new FilipinoEntity() { Calories = 250, Type = "Special", Name = "Pinakbet", Created = DateTime.Now });
            filipinoContext.FilipinoItems.Add(new FilipinoEntity() { Calories = 500, Type = "General", Name = "Menudo", Created = DateTime.Now });
            filipinoContext.FilipinoItems.Add(new FilipinoEntity() { Calories = 450, Type = "Favorites", Name = "Sinigang", Created = DateTime.Now });
            filipinoContext.FilipinoItems.Add(new FilipinoEntity() { Calories = 600, Type = "Special", Name = "Igado", Created = DateTime.Now });

            filipinoContext.SaveChanges();
        }
    }
}