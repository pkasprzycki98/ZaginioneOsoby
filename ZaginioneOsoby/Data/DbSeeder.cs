using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaginioneOsoby.Models;

namespace ZaginioneOsoby.Data
{
	public static class DbSeeder
	{
		public static void Seed(ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager)
		{
			if (!applicationDbContext.Users.Any())
			{
				CreateUser(applicationDbContext, roleManager, userManager).GetAwaiter().GetResult();
			}
			
		}
		private static async Task CreateUser(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager)
		{
			string user_role = "User";
			string admin_role = "Admin";
			if (!await roleManager.RoleExistsAsync(user_role))
			{
				await roleManager.CreateAsync(new IdentityRole(user_role));
			}
			if (!await roleManager.RoleExistsAsync(admin_role))
			{
				await roleManager.CreateAsync(new IdentityRole(admin_role));
			}
		

			var role_user = new List<UserModel.Role>
			{
				UserModel.Role.User
			};
			#region createAdminUser
			var user_admin = new UserModel()
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = "Admin",
				Email = "Admin@Admin.com",
				role = UserModel.Role.Admin
				
			};
			if (await userManager.FindByNameAsync(user_admin.UserName) == null)
			{
				await userManager.CreateAsync(user_admin, "Pass4Admin");
				await userManager.AddToRoleAsync(user_admin, admin_role);
				await userManager.AddToRoleAsync(user_admin, user_role);
				user_admin.EmailConfirmed = true;
				user_admin.LockoutEnabled = false;
			}
			#endregion
			#region createUser
			var user_Kasprzyk = new UserModel()
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = "Kasprzyk",
				Email = "Kasprzyk1998@gmail.com",
				role = UserModel.Role.User
			};
			if (await userManager.FindByNameAsync(user_Kasprzyk.UserName) == null)
			{
				await userManager.CreateAsync(user_Kasprzyk, "Pass4User");
				await userManager.AddToRoleAsync(user_Kasprzyk, user_role);

				user_Kasprzyk.EmailConfirmed = true;
				user_Kasprzyk.LockoutEnabled = false;
			}
			#endregion

			await dbContext.SaveChangesAsync();
		}
	}
}
