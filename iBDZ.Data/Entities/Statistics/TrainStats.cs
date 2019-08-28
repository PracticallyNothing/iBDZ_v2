using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data.Statistics
{

	// TrainServicePerStationStats
	//     - ServiceId(Int32, must match Id of TrainServiceHistory entry)
	//     - TrainStationId(Int32, must match existing TrainStation Id)
	//     - TimeOfArrival(DateTime, when the train got to this station)
	//     - TimeOfDeparture(DateTime, when the train left this station)
	//     - DelayMinutes(Double)
	//     - NumPeopleBoarding(int, how many people got on the train here)
	//     - NumPeopleUnboarding(int, how many people left the train here)
	public class TrainServicePerStation {
		public int Id { get; set; }
		
		[Required] public TrainServiceHistory ServiceHistory { get; set; }

		[Required] public TrainStation TrainStation { get; set; }
		
		public DateTime TimeOfArrival { get; set; }
		public DateTime TimeOfDeparture { get; set; }
		public double DelayMinutes { get; set; }

		public int NumPeopleBoarding { get; set; }
		public int NumPeopleUnboarding { get; set; }
	}

	// TrainServiceHistory
	//     - Id(Int32)
	//     - TrainId(Int32, must match Id of train)
	//     - RouteId(Int32, the route that was serviced)
	//     - TimeOfDeparture(DateTime, when the train started the trip)
	//     - TimeOfArrival(DateTime, when the train ended the trip)
	//     - TotalDelayMinutes(Double)
	//     - FinalPayout(Currency, total levs earned from trip)
	//     - HighestCapacityPercent(Double, min 0, max 100)
	//     - AverageCapacityPercent(Double, min 0, max 100)
	//     - LowestCapacityPercent(Double, min 0, max 100)
	//     - MostBoardingTrainStation(TrainStation Id, where the highest number of people boarded)
	//     - MostUnboardingTrainStation(TrainStation Id, where the highest number of people left)
	//     - PerStationStats(List<TrainServicePerStationStats>)
	public class TrainServiceHistory
	{
		public int Id { get; set; }
		public Train Train { get; set; }
		public Route Route { get; set; }
		public DateTime TimeOfDeparture { get; set; }
		public DateTime TimeOfArrival { get; set; }
		public double TotalDelayMinutes { get; set; }
		public decimal FinalPayout { get; set; }

		[Range(0, 1)] public double HighestCapacityPercent { get; set; }
		[Range(0, 1)] public double AverageCapacityPercent { get; set; }
		[Range(0, 1)] public double LowestCapacityPercent { get; set; }

		public TrainStation MostBoardingStation { get; set; }
		public TrainStation MostUnboardingStation { get; set; }

		public List<TrainServicePerStation> PerStationStats { get; set; } = new List<TrainServicePerStation>();
	}

	// TrainStats
	//     - TrainId(Int32, must match Id of train)
	//     - TotalTravelledTimeHours(Double)
	//     - TotalTravelledDistanceKm(Double)
	//     - TotalDelayMinutes(Double)
	//     - AverageCapacityPercent(Double)
	//     - ServiceHistory(List<TrainServiceHistory>)
	public class TrainStats
	{
		[Key]
		[Required]
		public int Id { get; set; }

		public Train Train { get; set; }

		public double TotalTravelledTimeHours { get; set; }
		public double TotalTravelledDistanceKm { get; set; }
		public double TotalDelayMinutes { get; set; }
		public double AverageCapacityPercent { get; set; }
		public List<TrainServiceHistory> ServiceHistory { get; set; }
	}
}
