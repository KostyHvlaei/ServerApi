using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class ProductDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int? DefaultQuantity { get; set; }
	}
}
