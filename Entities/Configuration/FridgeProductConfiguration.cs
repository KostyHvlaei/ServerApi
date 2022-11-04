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
	public class FridgeProductConfiguration : IEntityTypeConfiguration<FridgeProduct>
	{
		public void Configure(EntityTypeBuilder<FridgeProduct> builder)
		{
			builder.HasData
			(
				new FridgeProduct
				{
					Id = new Guid("4e1a3a80-11b3-4add-af8d-7a8e6b33517a"),
					Quantity = 3,
					ProductId = new Guid("94fea888-0773-4d71-988a-32185bf61eee"),
					FridgeId = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79")
				},
				new FridgeProduct
				{
					Id = new Guid("886a0a29-3ca8-4f35-af38-e8ca3ec6d2e1"),
					Quantity = 0,
					ProductId = new Guid("70497196-fb2d-4e29-8f17-1c2068afd916"),
					FridgeId = new Guid("1fd5ea01-c9e9-4215-b844-fd66e80d3e79")
				},
				new FridgeProduct
				{
					Id = new Guid("3ebf24a1-1c0f-45e2-b7ff-c80a119a53cb"),
					Quantity = 0,
					ProductId = new Guid("e5d96170-0301-459b-96f4-795a65783654"),
					FridgeId = new Guid("8be43fc6-4398-4714-8794-edacee214946")
				},
				new FridgeProduct
				{
					Id = new Guid("bfc256b5-a2f1-4021-abcb-03ba7a1686bc"),
					Quantity = 10,
					ProductId = new Guid("29f71e4f-161a-4dab-86af-e2f7aec1e2ba"),
					FridgeId = new Guid("92bafc65-cc77-485c-9756-81ad0aaa8008")
				},
				new FridgeProduct
				{
					Id = new Guid("41174222-9f3d-4a56-b422-4e9d01bb13b1"),
					Quantity = 5,
					ProductId = new Guid("297a1295-eb00-4447-bb88-5d9b69a1e1f1"),
					FridgeId = new Guid("ae16b99d-85f3-4121-bdad-e704c29e3981")
				}
			);
		}
	}
}
