using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class ProductToUpdateInFridgeDTO
	{
		[Range(1, 999999, ErrorMessage = "Quantity must be more than 0")]
		public int Quantity { get; set; }
	}
}
