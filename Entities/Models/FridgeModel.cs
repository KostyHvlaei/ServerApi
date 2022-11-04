using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class FridgeModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Fridge model name is required")]
		[MaxLength(60, ErrorMessage = "Maximum lenght of fridge model name is a 60 characters")]
		public string Name { get; set; }

		[Range(1913, 2030, ErrorMessage = "Year must be between 1913 and 2030")]
		public int? Year { get; set; }
	}
}
