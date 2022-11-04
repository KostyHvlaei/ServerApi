using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class FridgeProduct
	{
		public Guid Id { get; set; }

		[Required]
		[Range(0, 999999, ErrorMessage = "Quentity can't be less then 0")]
		public int Quantity { get; set; }

		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public Guid FridgeId { get; set; }
		public Fridge Fridge { get; set; }
	}
}
