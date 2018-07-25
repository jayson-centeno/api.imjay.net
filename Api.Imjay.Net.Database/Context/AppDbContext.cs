using Api.Imjay.Net.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Api.Imjay.Net.Database.Context
{
    public class AppDbContext: DbContext 
    {
        private DbConnection _connection = null;

        public AppDbContext(DbConnection conection)
        {
            _connection = conection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Publication> Publications { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
