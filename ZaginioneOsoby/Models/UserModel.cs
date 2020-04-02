using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZaginioneOsoby.Models
{
	public class UserModel : IdentityUser
	{
		[Required(ErrorMessage ="Dej email")]
		public override string Email { get => base.Email; set => base.Email = value; }
		[Required(ErrorMessage ="Dej username no co Ty?")]
		public override string UserName { get => base.UserName; set => base.UserName = value; }
		public string DisplayName { get; set; }
		public string Notes { get; set;}
		[Required(ErrorMessage = "Byczku hasło dej")]
		public virtual List<OsobaZaginionaModel> OsobaZaginionaModels { get; set; }

		public  Role role { get; set; }

		public enum Role {Admin, User};
		
	}
}
