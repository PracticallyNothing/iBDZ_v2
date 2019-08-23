using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data
{
    public class Route
    {
		public int Id { get; set; }

		[StringLength(50)]
		public string Name { get; set; }

		public double TotalLength { get; set; }

		public IList<RouteTrainStation> RouteTrainStations { get; set; }
	}
}
