﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaginioneOsoby.Services.IServices
{
	public interface IUserService
	{
		public Task<bool> Login(string username, string password);
		public Task<bool> Register(string username, string email, string password);
		public bool SetRole(string username, string role);
		public bool ResetPassword(string username, string oldPassword, string newPassword);
	}
}
