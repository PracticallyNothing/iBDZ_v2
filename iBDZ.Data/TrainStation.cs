using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBDZ.Data
{
	public class TrainStation
    {
		public int Id { get; set; }

		[Required]
		[StringLength(25)]
		public string Name { get; set; }

		public double Longitute { get; set; }

		public double Latitude { get; set; }

		public IList<RouteTrainStation> TrainStationRoutes { get; set; }
	}
}
