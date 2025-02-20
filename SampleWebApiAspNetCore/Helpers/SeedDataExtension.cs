﻿using SampleWebApiAspNetCore.Repositories;
using SampleWebApiAspNetCore.Services;

namespace SampleWebApiAspNetCore.Helpers
{
    public static class SeedDataExtension
    {
        public static void SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var foodDbContext = scope.ServiceProvider.GetRequiredService<FoodDbContext>();
                var drinkDbContext = scope.ServiceProvider.GetRequiredService<FilipinoDbContext>();
                var seedDataService = scope.ServiceProvider.GetRequiredService<ISeedDataService>();

                seedDataService.Initialize(foodDbContext, drinkDbContext);
            }
        }
    }
}