using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Product
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Product name is a required field")]
		[MaxLength(60, ErrorMessage = "Maximum lenght of the Name is 60 characters")]
		public string Name { get; set; }

		[Range(0, 9999999, ErrorMessage = "Default quantity can't be less the 0")]
		public int? DefaultQuantity { get; set; }

		public List<Fridge> Fridges { get; set; } = new List<Fridge>();
		public List<FridgeProduct> FridgeProducts { get; set; } = new List<FridgeProduct>();
	}
}
