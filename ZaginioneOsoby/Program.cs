﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZaginioneOsoby.Data;
using ZaginioneOsoby.Models;

namespace ZaginioneOsoby
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost(args);

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var dbContext =
					services.GetService<ApplicationDbContext>();
					var roleManager = services.GetService<RoleManager<IdentityRole>>();
					var userManager = services.GetService<UserManager<UserModel>>();
					dbContext.Database.Migrate();
					// Wype³nij bazê danymi pocz¹tkowymi
					DbSeeder.Seed(dbContext, roleManager, userManager);


				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}

			host.Run();
		}

		private static object CreateWebHostBuilder(string[] args)
		{
			throw new NotImplementedException();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
