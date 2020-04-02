using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaginioneOsoby.Data;
using ZaginioneOsoby.Models;
using ZaginioneOsoby.Services.IServices;

namespace ZaginioneOsoby.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<UserModel> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<UserModel> _signInManager;
		public UserService(ApplicationDbContext dbContext, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, SignInManager<UserModel> signInManager)
		{
			_dbContext = dbContext;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;	
		}
		public async Task<bool> Login(string username, string password)
		{
			if (username == null)
			{
				return false;
			}
			
			var user =  await _userManager.FindByNameAsync(username); // szukanie user'a
			if (user == null && username.Contains("@"))
			{
				user = await _userManager.FindByEmailAsync(username); // przyzwolenie na emial
				
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, password, true, true); // logowanie
			

			if (result.Succeeded)
			{
				return true;
			}

			return false;

		}

		public async Task<bool> Register(string UserName, string Email, string password) 
		{
			string user_role = "User";

			if (! await _roleManager.RoleExistsAsync(user_role))
			{
				await _roleManager.CreateAsync(new IdentityRole(user_role));
			}
			List<UserModel.Role> roles = new List<UserModel.Role> {
				UserModel.Role.User
			};
			UserModel user = new UserModel
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = UserName,
				Email = Email,
				role = UserModel.Role.User
			};
			await _userManager.CreateAsync(user, password);
			await _userManager.AddToRoleAsync(user, user_role);

			user.EmailConfirmed = true;
			user.LockoutEnabled = false;

			if (await _userManager.FindByNameAsync(user.UserName) != null)
				return true;
			else
				return false;
		}

		public bool ResetPassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public bool SetRole(string username, string role)
		{
			throw new NotImplementedException();
		}
	}
}
