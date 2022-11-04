using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class UserForRegistrationDTO
	{
		[StringLength(30, MinimumLength = 2, ErrorMessage = "First Name must have min 2 and max 30 characters")]
		public string FirstName { get; set; }

		[StringLength(30, MinimumLength = 2, ErrorMessage = "Last Name must have min 2 and max 30 characters")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Username is required")]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "UserName must have min 3 and max 30 characters")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }

		public ICollection<string> Roles { get; set; }
	}
}
