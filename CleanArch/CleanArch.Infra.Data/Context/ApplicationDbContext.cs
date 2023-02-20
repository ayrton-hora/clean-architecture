using Microsoft.EntityFrameworkCore;

using CleanArch.Domain.Entities;

namespace CleanArch.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
         
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
