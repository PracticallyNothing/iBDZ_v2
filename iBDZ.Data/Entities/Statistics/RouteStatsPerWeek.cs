using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data.Statistics
{
    public class RouteStatsPerWeek
    {
		[Key]
		public int RouteId { get; set; }
		public Route Route { get; set; }

		[Range(0, 51)] public uint Week { get; set; }
		public uint Year { get; set; }

		public uint NumTravellers { get; set; }
		public decimal TotalEarnings { get; set; }
    }
}
