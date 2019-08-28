using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data.Statistics
{
	/*
	 * UserStatsPerWeek
	 *    - UserId(string, must match the UserStats' Id it belongs to)
	 *    - Week (Int, must be between 1 and 52)
	 *    - Year(Int)
	 *    - TimeOnWebsiteHours(double)
	 *    - NumWebsiteVisits(Int)
	 *    - NumTravels(Int)
	 */
	public class UserStatsPerWeek
	{
		public int Id { get; set; }	

		[Required]
		public User User { get; set; }

		[Range(0, 51)]
		public uint Week { get; set; }

		public uint Year { get; set; }

		public double TimeOnWebsiteHours { get; set; }

		public int NumWebsiteVisits { get; set; }

		public int NumTravels { get; set; }
	}

	/*
	 * UserTravelHistory
	 *    - UserId(string, must match the UserStats' Id it belongs to)
	 *    - TrainId (Int32)
	 *    - TimeOfPurchase (DateTime, when the user purchased their ticket)
	 *    - StartStation(TrainStation, where the user boarded the train)
	 *    - FinalStation(TrainStation, where the user got off the train)
	 */
	public class UserTravelHistory
	{
		public int Id { get; set; }

		[Required]
		public User User { get; set; }

		[Required]
		public Train Train { get; set; }

		[Required]
		public DateTime TimeOfPurchase { get; set; }

		[Required]
		public TrainStation StartStation { get; set; }

		[Required]
		public TrainStation EndStation { get; set; }
	}

	/*
	 * UserStats
	 *    - UserId(string, must match Id of User (only upon creation))
	 *    - TravelTimeHours(Double)
	 *    - TravelDistanceKm(Double)
	 *    - MostTravelledRoute(string, Name)
	 *    - WeeklyStats(List<UserStatsPerWeek>)
	 *    - TravelHistory(List<UserTravelHistory>)
	 *    - DateRegistered(DateTime, when the user made his/her account)
	 */
	public class UserStats
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public User User { get; set; }

		public double TravelTimeHours { get; set; }

		public double TravelDistanceKm { get; set; }

		[StringLength(50)]
		public string MostTravelledRoute { get; set; }

		public List<UserStatsPerWeek> WeeklyStats { get; set; }

		public List<UserTravelHistory> TravelHistory { get; set; }

		public DateTime DateRegistered { get; set; }
	}
}
