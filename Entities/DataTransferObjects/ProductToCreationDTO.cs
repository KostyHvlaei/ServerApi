using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class ProductToCreationDTO
	{
		[Required(ErrorMessage = "Product name is a required field")]
		[MaxLength(60, ErrorMessage = "Maximum lenght of the Name is 60 characters")]
		public string Name { get; set; }

		[Range(0, 9999999, ErrorMessage = "Default quantity can't be less the 0")]
		public int? DefaultQuantity { get; set; }
	}
}
