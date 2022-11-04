using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Fridge
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Fridge name field is required")]
		[MaxLength(60, ErrorMessage = "Maximum lenght of fridge name is a 60 characters")]
		public string Name { get; set; }

		[MaxLength(60, ErrorMessage = "Maximum lenght of fridge owner name is a 60 characters")]
		public string OwnerName { get; set; }

		public List<Product> Products { get; set; } = new List<Product>();
		public List<FridgeProduct> FridgeProducts { get; set; } = new List<FridgeProduct>();

		public Guid FridgeModelId { get; set; }
		public FridgeModel FridgeModel { get; set; }
	}
}
