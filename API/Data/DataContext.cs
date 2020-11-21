using API.Entities;
using API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        private readonly IApiConfiguration config;
        private static string connectionString;

        public DataContext(IApiConfiguration config)
        {
            this.config = config;
        }
        
        public DbSet<Employee> Employees { get; set; }
        public void Migrate() => Database.Migrate();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString is null)
                connectionString = config.AvgConnectionString;

            optionsBuilder.UseSqlServer(connectionString, option => option.EnableRetryOnFailure());
        }
    }
}
