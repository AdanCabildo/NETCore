using Microsoft.EntityFrameworkCore;
using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public class FilipinoDbContext : DbContext
    {
        public FilipinoDbContext(DbContextOptions<FilipinoDbContext> options)
            : base(options)
        {
        }

        public DbSet<FilipinoEntity> FilipinoItems { get; set; } = null!;
    }
}