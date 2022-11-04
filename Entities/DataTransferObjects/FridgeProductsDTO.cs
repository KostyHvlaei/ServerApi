using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class FridgeProductsDTO
	{
		public Guid FridgeId { get; set; }
		public string FridgeName { get; set; }
		public string OwnerName { get; set; }
		public string ModelName { get; set; }
		public IEnumerable<FridgeProductDTO> Products { get; set; }
	}
}
