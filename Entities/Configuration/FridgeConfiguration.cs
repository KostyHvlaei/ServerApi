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
	public class FridgeConfiguration : IEntityTypeConfiguration<Fridge>
	{
		public void Configure(EntityTypeBuilder<Fridge> builder)
		{
			builder.HasData
			(
				new Fridge
				{
					Id = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79"),
					Name = "Genady Gorin's fridge",
					OwnerName = "Genady Gorin",
					FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c")
				},
				new Fridge
				{
					Id = new Guid("ae16b99d-85f3-4121-bdad-e704c29e3981"),
					Name = "Kitchen",
					OwnerName = "Some guy",
					FridgeModelId = new Guid("f4793a96-678a-4cae-a6a3-f7cc51a6b98c")
				},
				new Fridge
				{
					Id = new Guid("d447d5c7-3d61-495c-8d36-89c337e3a9ef"),
					Name = "Stolovaya n2",
					FridgeModelId = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252")
				},
				new Fridge
				{
					Id = new Guid("8be43fc6-4398-4714-8794-edacee214946"),
					Name = "Stolovaya n3",
					FridgeModelId = new Guid("24964f45-61f7-4f9c-9ab8-c4981ad6a252")
				},
				new Fridge
				{
					Id = new Guid("92bafc65-cc77-485c-9756-81ad0aaa8008"),
					Name = "Mine fridge",
					OwnerName = "Man who wants to become developer",
					FridgeModelId = new Guid("e3130a06-9410-44c0-bc38-6145b5e60426")
				}
			);
		}
	}
}
