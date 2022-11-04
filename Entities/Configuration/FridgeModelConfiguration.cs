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
	public class FridgeModelConfiguration : IEntityTypeConfiguration<FridgeModel>
	{
		public void Configure(EntityTypeBuilder<FridgeModel> builder)
		{
			builder.HasData
			(
				new FridgeModel
				{
					Id = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c"),
					Name = "Atalant b100",
					Year = 2005
				},
				new FridgeModel
				{
					Id = new Guid("b6138124-9e39-4aaf-b039-5e149dd4c928"),
					Name = "Samsung v32",
					Year = 2021
				},
				new FridgeModel
				{
					Id = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252"),
					Name = "Samsung k20",
					Year = 2019
				},
				new FridgeModel
				{
					Id = new Guid("e3130a06-9410-44c0-bc38-6145b5e60426"),
					Name = "Soyuz 1337",
					Year = 1964
				},
				new FridgeModel
				{
					Id = new Guid("b4e73b10-115e-4371-b851-9cd08cd69740"),
					Name = "Bosh c999",
					Year = 2020
				}
			);
		}
	}
}
