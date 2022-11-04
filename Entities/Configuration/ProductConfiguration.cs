using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasData
			(
				new Product
				{
					Id = new Guid("94fea888-0773-4d71-988a-32185bf61eee"),
					Name = "Pizza",
					DefaultQuantity = 6
				},
				new Product
				{
					Id = new Guid("297a1295-eb00-4447-bb88-5d9b69a1e1f1"),
					Name = "Juice",
					DefaultQuantity = 1
				},
				new Product
				{
					Id = new Guid("70497196-fb2d-4e29-8f17-1c2068afd916"),
					Name = "Apple",
					DefaultQuantity = 10
				},
				new Product
				{
					Id = new Guid("e5d96170-0301-459b-96f4-795a65783654"),
					Name = "Carrot",
					DefaultQuantity = 10
				},
				new Product
				{
					Id = new Guid("29f71e4f-161a-4dab-86af-e2f7aec1e2ba"),
					Name = "Chicken",
					DefaultQuantity = 2
				}

			);
		}
	}
}
