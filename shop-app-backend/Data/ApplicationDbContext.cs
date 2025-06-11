using Data.Entities;
using Data.Entities.Dependencies;
using Data.Entities.GlobalConfiguration;
using Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ApplicationDbContext :IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext() : base()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        //fabryka jest konieczna ze względu na to że nie odpala się Program.cs, konfig jest pusty i migracja się wywala
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Portal"))
                    //.AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.Development.json", optional: false)
                    .Build();
                
                
                var settingsSection = configuration.GetSection("Settings");
                // var appSettings = new AppSettings();
                // settingsSection.Bind(appSettings);

                // Create DB context with connection from your AppSettings 
                
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersProducts>().HasKey(p => new {p.OrderId, p.ProductId});
            modelBuilder.Entity<ProductsCategories>().HasKey(p => new {p.CategoryId, p.ProductId});
            modelBuilder.Entity<Avatar>()
                .Property(p=> p.TsInsert)
                .HasComputedColumnSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Avatar>()
                .Property(p=> p.TsInsert)
                .HasComputedColumnSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Avatar>()
                .Property(p=> p.TsInsert)
                .HasComputedColumnSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Avatar>()
                .Property(p=> p.TsInsert)
                .HasComputedColumnSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();
            
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrdersProducts> OrdersProducts { get; set; }
        public virtual DbSet<ProductsCategories> ProductsCategories { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<ApplicationParameter> ApplicationParameters { get; set; }
        public virtual DbSet<Avatar> UserAvatars { get; set; }
    }
}
