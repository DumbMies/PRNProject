using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace PRNProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProductionRequest> ProductionRequest { get; set; }
        public DbSet<Jewelry> Jewelry { get; set; }
        public DbSet<QuotationRequest> QuotationRequest { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Gemstone> Gemstone { get; set; }
        public DbSet<MaterialSet> MaterialSet { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<JewelryDesign> JewelryDesign { get; set; }

        private string GetAppSettingsPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);

            while (directoryInfo != null && !File.Exists(Path.Combine(directoryInfo.FullName, "appsettings.json")))
            {
                directoryInfo = directoryInfo.Parent;
            }

            if (directoryInfo == null)
            {
                throw new FileNotFoundException("Could not locate 'appsettings.json' in any parent directory.");
            }

            return directoryInfo.FullName;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = GetAppSettingsPath();


            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.UserEmail).HasMaxLength(200);
                entity.Property(e => e.UserAddress).HasMaxLength(200);
                entity.Property(e => e.UserRole).HasMaxLength(200);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<ProductionRequest>(entity =>
            {
                entity.HasKey(e => e.ProductionRequestID);
                entity.Property(e => e.ProductionRequestName).HasMaxLength(200);
                entity.Property(e => e.ProductionRequestStatus).HasMaxLength(200);
                entity.Property(e => e.ProductionRequestAddress).HasMaxLength(200);
                entity.Property(e => e.ProductionRequestQuantity);
                entity.Property(e => e.CreatedDate).IsRequired();

            });

            modelBuilder.Entity<Jewelry>(entity =>
            {
                entity.HasKey(e => e.JewelryID);
                entity.Property(e => e.JewelryName).HasMaxLength(200);
                entity.Property(e => e.JewelryDescription).HasMaxLength(200);
                entity.Property(e => e.JewelryStatus).HasMaxLength(200);
                entity.Property(e => e.JewelryImage).HasMaxLength(200);
                entity.Property(e => e.CreatedDate).IsRequired();
            });

            modelBuilder.Entity<QuotationRequest>(entity =>
            {
                entity.HasKey(e => e.QuotationRequestID);
                entity.Property(e => e.QuotationRequestName).HasMaxLength(200);
                entity.Property(e => e.QuotationRequestStatus).HasMaxLength(200);
                entity.Property(e => e.LaborPrice).HasColumnType("decimal(18,0)");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,0)");
                entity.Property(e => e.CreatedDate).IsRequired();

            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialID);
                entity.Property(e => e.MaterialName).HasMaxLength(200);
                entity.Property(e => e.MaterialPrice).HasColumnType("decimal(18,0)");
            });

            modelBuilder.Entity<Gemstone>(entity =>
            {
                entity.HasKey(e => e.GemstoneID);
                entity.Property(e => e.GemstoneName).HasMaxLength(200);
                entity.Property(e => e.GemstonePrice).HasColumnType("decimal(18,0)");
                entity.Property(e => e.GemstoneWeight).HasColumnType("decimal(18,0)");
            });

            modelBuilder.Entity<MaterialSet>(entity =>
            {
                entity.HasKey(e => e.MaterialSetID);
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,0)");
            });

            modelBuilder.Entity<JewelryDesign>(entity =>
            {
                entity.HasKey(e => e.JewelryDesignID);
                entity.Property(e => e.JewelryDesignName).HasMaxLength(200);
                entity.Property(e => e.JewelryDesignImage).HasMaxLength(200);
                entity.Property(e => e.JewelryDesignFile).HasMaxLength(200);
                entity.Property(e => e.JewelryDesignStatus).HasMaxLength(200);
                entity.Property(e => e.CreatedDate).IsRequired();
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasKey(e => new { e.JewelryID, e.UserID });
                entity.Property(e => e.DeliveryDate).IsRequired();
            });
        }

    }
}
