using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class FridgeToCreationDTO
	{
		[Required(ErrorMessage = "Fridge name field is required")]
		[MaxLength(60, ErrorMessage = "Maximum lenght of fridge name is a 60 characters")]
		public string Name { get; set; }

		[MaxLength(60, ErrorMessage = "Maximum lenght of fridge owner name is a 60 characters")]
		public string OwnerName { get; set; }

		[Required(ErrorMessage = "FridgeModelId is required")]
		public Guid FridgeModelId { get; set; }
	}
}
