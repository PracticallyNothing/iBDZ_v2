using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using iBDZ.Web.Models;
using Microsoft.AspNetCore.Identity;
using iBDZ.Data;

namespace iBDZ.Web.Controllers
{
	public class HomeController : Controller
    {
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;

		public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public IActionResult Index()
        {
			if (signInManager.IsSignedIn(User))
			{
				User u = userManager.FindByNameAsync(User.Identity.Name).Result;
				if (userManager.IsInRoleAsync(u, "Administrator").Result)
					return Redirect("/Admin/");
				else if (userManager.IsInRoleAsync(u, "TicketIssuer").Result)
					return Redirect("/TicketIssuing/");
				else
					return View(); // Return user home.
			}
			else // Return guest index.
			{
				return View();
			}
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
