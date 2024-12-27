using Karluna.Entities.Entities;
using Karluna.Entities.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Data.DbContext
{
    public class KtsDbContext : IdentityDbContext
    {
        public KtsDbContext(DbContextOptions<KtsDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Product>().HasKey(c => c.Id);
            //builder.Entity<Product>().Property(c => c.Id).UseIdentityColumn();
            
            builder.Entity<StockSubCategory>().HasMany(c => c.StockProducts).WithOne(d => d.StockSubCategory).HasForeignKey(d => d.SubCategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<StockSubCategory>().HasMany(c => c.StockVersions).WithOne(d => d.StockSubCategory).HasForeignKey(d => d.SubCategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<StockCategory>().HasMany(c => c.StockSubCategories).WithOne(d => d.StockCategory).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockCategory>().HasMany(c => c.StockProducts).WithOne(c => c.StockCategory).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Entities.Entities.StockProductVersion>().HasMany(c => c.StockProducts).WithOne(c => c.StockProductVersion).HasForeignKey(d => d.VersionId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StockBrand>().HasMany(c => c.StockProducts).WithOne(c => c.StockBrand).HasForeignKey(d => d.BrandId).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }

        DbSet<User> Users { get; set; }
        DbSet<UserRole> Roles { get; set; }
        DbSet<StockProduct> StockProducts { get; set; }
        DbSet<StockCategory> StockCategories { get; set; }
        DbSet<StockSubCategory> StockSubCategories { get; set; }
        DbSet<Entities.Entities.StockProductVersion> StockProductVersions { get; set; }
        DbSet<StockBrand> StockBrands { get; set; }
    }
}
