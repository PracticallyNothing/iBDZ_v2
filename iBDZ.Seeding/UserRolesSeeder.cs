using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBDZ.Seeding
{
	public class UserRolesSeeder : ISeeder
	{
		private static readonly string[] Roles = new string[]
		{
			"User",
			"TicketIssuer",
			"Administrator"
		};

		public async Task SeedAsync(IServiceProvider serviceProvider)
		{
			RoleManager<IdentityRole> roleManager =
				(RoleManager<IdentityRole>)serviceProvider.GetService(typeof(RoleManager<IdentityRole>));

			foreach (var r in Roles)
				await roleManager.CreateAsync(new IdentityRole(r));
		}
	}
}
