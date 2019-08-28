using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data
{
    public class RouteTrainStation
    {
		[Required]
		public int RouteId { get; set; }

		[Required]
		public int TrainStationId { get; set; }

		public Route Route { get; set; }
		public TrainStation TrainStation { get; set; }

		public uint Order { get; set; }
    }
}
