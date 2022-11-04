using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class UserForAuthenticationDTO
	{
		[Required(ErrorMessage = "User name is required")]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "UserName must have min 3 and max 30 characters")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
	}
}
