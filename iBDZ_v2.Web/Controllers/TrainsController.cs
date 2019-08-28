using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBDZ.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iBDZ.Web.Controllers
{
    public class TrainsController : Controller
    {
		private readonly ITrainsService trainsService;

		public TrainsController(ITrainsService trainsService, IMapService mapService)
		{
			this.trainsService = trainsService;
		}

		[HttpGet]
        public IActionResult Timetable()
        {
            return View(trainsService.GetTimetableInfo());
        }

		[HttpGet]
		public IActionResult Details(int id)
		{
			return View(trainsService.GetTrainDetails(id));
		}

		[HttpGet]
		[Authorize]
		public IActionResult AToB()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Live()
		{
			return View();
		}
    }
}