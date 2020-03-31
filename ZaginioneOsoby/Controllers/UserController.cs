using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZaginioneOsoby.Data;
using ZaginioneOsoby.Models;
using ZaginioneOsoby.Services.IServices;

namespace ZaginioneOsoby.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IUserService _userService;
        public UserController(ApplicationDbContext dbContext, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, SignInManager<UserModel> signInManager, IUserService userService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("username,password")] UserViewModel model)
        {
            var succes = await _userService.Login(model.username, model.password);

            if (succes == false)
            {
                return new UnauthorizedResult();
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("username,email,password")] UserViewModel model)
        {
            var succes = await _userService.Register(model.username, model.email, model.password);

            if (succes == false)
            {
                return new UnauthorizedResult();
            }
            else
            {
                await _userService.Login(model.username, model.password);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult UserList()
        {
            IEnumerable<UserModel> users = _dbContext.Users.ToList();

            return View(users);
        }
        public async Task<IActionResult> UpdateUserRole(string username, UserModel.Role rola)
        {
      
            var user = _dbContext.Users.FirstOrDefault(model => model.UserName == username);


            if (User.IsInRole("Moderator") || User.IsInRole("User") && user.role == UserModel.Role.Admin)
            {
                return BadRequest("Admin pls dont touch");
            }

                if (user.role == UserModel.Role.Admin)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.RemoveFromRoleAsync(user, "User");
            }
            else if (user.role == UserModel.Role.User)
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
            }
            if (rola == UserModel.Role.User)
            {
                user.role = UserModel.Role.User;
                await _userManager.AddToRoleAsync(user, rola.ToString());

            }
            if (rola == UserModel.Role.Admin)
            {             
                user.role = UserModel.Role.Admin;
                await _userManager.AddToRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "User");
            }

            return RedirectToAction("UserList");
        }

        
    }
}