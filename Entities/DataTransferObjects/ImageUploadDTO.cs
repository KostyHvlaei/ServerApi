using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
	public class ImageUploadDTO
	{
		public Guid ProductId { get; set; }
		public IFormFile file { get; set; }
	}
}
