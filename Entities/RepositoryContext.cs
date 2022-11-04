using System;
using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	public class RepositoryContext : IdentityDbContext<User>
	{
		public RepositoryContext(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.Entity<Product>()
				.HasMany(p => p.Fridges)
				.WithMany(f => f.Products)
				.UsingEntity<FridgeProduct>(
					fp => fp
						.HasOne(fp => fp.Fridge)
						.WithMany(f => f.FridgeProducts)
						.HasForeignKey(fp => fp.FridgeId),
					fp => fp
						.HasOne(fp => fp.Product)
						.WithMany(p => p.FridgeProducts)
						.HasForeignKey(fp => fp.ProductId),
					fp =>
					{
						fp.HasKey(fp => fp.Id);
						fp.ToTable("FridgeProducts");
						fp.HasIndex(fp => new { fp.ProductId, fp.FridgeId })
						.IsUnique(true);
					});

			modelBuilder.ApplyConfiguration(new FridgeModelConfiguration());
			modelBuilder.ApplyConfiguration(new FridgeConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new FridgeProductConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<FridgeModel> FridgeModels { get; set; }
		public DbSet<Fridge> Fridges { get; set; }
	}
}
