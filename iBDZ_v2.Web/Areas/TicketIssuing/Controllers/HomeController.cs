using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBDZ.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace iBDZ.Web.Areas.TicketIssuing
{
	[Area("TicketIssuing")]
    public class HomeController : Controller
    {
		private readonly IMapService mapService;

		public HomeController(IMapService mapService)
		{
			this.mapService = mapService;
		}

		public IActionResult Index()
        {
            return View(mapService.GetRenderingData());
        }
    }
}