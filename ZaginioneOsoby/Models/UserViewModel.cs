using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZaginioneOsoby.Models
{
	public class UserViewModel
	{
		[Required(ErrorMessage = "Byczku wstaw nick swój")]
		public string username { get; set; }
		[Required(ErrorMessage = "Byczku hasło dej")]
		public string password { get; set; }
		[Required]
		public string email { get; set; }

	}
}
