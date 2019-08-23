using iBDZ.Data.DataValidation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iBDZ.Data
{
	public class User : IdentityUser
	{
		public User()
		{
		}

		public User(string userName) : base(userName)
		{
		}

		[EGN]
		public string EGN { get; set; }

		[StringLength(15)]
		public string FirstName { get; set; }

		[StringLength(15)]
		public string LastName { get; set; }
	}
}
