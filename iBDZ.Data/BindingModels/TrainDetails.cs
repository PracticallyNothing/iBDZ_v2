using System;
using System.Collections.Generic;
using System.Text;

namespace iBDZ.Data.BindingModels
{
	public class ScheduleItem
	{
		public StationModel Station { get; set; }
		public DateTime TimeOfDeparture { get; set; }
		public DateTime TimeOfArrival { get; set; }
		public DateTime? Delay { get; set; } = null;
	}

	public class TrainCarInfo
	{
		public bool HasElectricity { get; set; }
		public bool HasAirConditioning { get; set; }
		public bool HasHeating { get; set; }

		public TrainCarType Type { get; set; }
		public TrainCarClass Class { get; set; }

		public string ModelName { get; set; }
		public int NumSeats { get; set; }
		public int NumSeatsPerDivision { get; set; }

		public Int64 Seats1 { get; set; }
		public Int64 Seats2 { get; set; }
	}

	public class LocomotiveInfo {
		public int Id { get; set; }
		public string Name { get; set; }
		public LocomotiveType Type { get; set; }
	}

	public class TrainDetails : ShortTrainInfo
	{
		public LocomotiveInfo Locomotive { get; set; }
		public List<ScheduleItem> Schedule { get; set; }
		public List<TrainCarInfo> Composition { get; set; }
	}
}
