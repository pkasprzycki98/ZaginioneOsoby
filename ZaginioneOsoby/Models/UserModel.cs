using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaginioneOsoby.Models
{
	public class UserModel : IdentityUser
	{
		public string DisplayName { get; set; }
		public string Notes { get; set;}
		public virtual List<OsobaZaginionaModel> OsobaZaginionaModels { get; set; }

		public  Role role { get; set; }

		public enum Role {Admin, User};

	}
}
