using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iBDZ.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
    public class StatsController : Controller
    {
        public IActionResult Statistics()
        {
            return View();
        }
    }
}