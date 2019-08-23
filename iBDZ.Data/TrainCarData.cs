using System.ComponentModel.DataAnnotations;

namespace iBDZ.Data
{
	public enum TrainCarType
	{
		/// <summary> A coach car without separation between seats. </summary>
		/// https://en.wikipedia.org/wiki/Open_coach
		Open = 1,
		/// <summary> A coach car with a single corridor to one side of the car and compartments that separate the seats. </summary>
		/// https://en.wikipedia.org/wiki/Corridor_coach
		Corridor = 2,
		/// <summary> A coach car with beds. </summary>
		/// https://en.wikipedia.org/wiki/Couchette_car
		Couchette = 3
	}

	public enum TrainCarClass
	{
		First = 1,
		Second = 2
	}

	public class TrainCarData
	{
		// Id (Int32)
		public int Id { get; set; }

		// Type (One of Locomotive, Compartments, Economy, Beds)
		public TrainCarType Type { get; set; }

		// Class (One of First, Second)
		public TrainCarClass Class { get; set; }

		// Name (string, max length 30)
		[StringLength(30)]
		public string Name { get; set; }

		// NumSeats (int)
		public int NumSeats { get; set; }

		// NumSeatsPerDivision (int)
		/// <summary>
		/// Number of seats per row for Open coaches or per compartment for Couchettes and corridor coaches.
		/// </summary>
		public int NumSeatsPerDivison { get; set; }

		// HasElectricity (boolean)
		public bool HasElectricity { get; set; }

		// HasAirConditioning (boolean, whether the train car can cool itself)
		public bool HasAirConditioning { get; set; }

		// HasHeating (boolean, whether the train car can heat itself)
		public bool HasHeating { get; set; }

		public bool HasCoupes => Type != TrainCarType.Open;
	}
}