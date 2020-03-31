using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaginioneOsoby.Models;

namespace ZaginioneOsoby.Data
{
	public class ApplicationDbContext : IdentityDbContext<UserModel>
	{
		public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			
		}

		public DbSet<OsobaZaginionaModel> OsobyZaginione { get; set; }

		public DbSet<ZaginioneOsoby.Models.Place> Place { get; set; }
	}
}
