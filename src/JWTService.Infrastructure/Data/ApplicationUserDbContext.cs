using JWTService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JWTService.Infrastructure.Data
{
    public class ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options, IConfiguration config) : DbContext(options)
    {
        public DbSet<ApplicationUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(config.GetSection("Schema").Value);
        }
    }
}
