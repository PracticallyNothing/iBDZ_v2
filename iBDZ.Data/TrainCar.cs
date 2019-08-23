using System;

namespace iBDZ.Data
{
	public class TrainCar
	{
		// Id(Int32)
		public int Id { get; set; }

		// TrainCarData(foreign key)
		public TrainCarData Data { get; set; }

		// Train(foreign key, train which the car is a part of)
		public Train Train { get; set; }

		// Order(uint, describes where in the composition this train car sits)
		public uint Order { get; set; }

		// Seats(some kind of bitset, where every bit tells
		//       whether the seat is taken or not (0 for free, 1 for taken))
		// Note: Nobody is going to like this, but in comparison to having
		//       40'500 entities for just 50 trains, this is better.
		public Int64 Seats1 { get; set; }
		public Int64 Seats2 { get; set; }

		/// <summary>
		///		Is the seat with this number already taken?
		/// </summary>
		/// <param name="seatNumber">
		///		The seat's number as it would appear in real life 
		///		(e.g. the first seat in the first compartment is 11, not 00)
		/// </param>
		/// <returns>Whether the seat is taken.</returns>
		public bool IsSeatTaken(int seatNumber)
		{
			if (seatNumber <= 10)
				throw new ArgumentException("Seat number is invalid.");

			// Row number on open coaches or compartment number in couchettes and corridor coaches.
			int divNumber = seatNumber / 10 - 1;
			int seat = seatNumber % 10 - 1;

			if (divNumber < 1 || seat < 1 || seat > Data.NumSeatsPerDivison)
			{
				throw new ArgumentException("Seat number is invalid.", nameof(seatNumber));
			}

			int seatBit = divNumber * Data.NumSeatsPerDivison + seat;
			if (seatBit < 64)
			{
				return (Seats1 & (1 << seatBit)) == 1;
			}
			else
			{
				return (Seats2 & (1 << (seatBit - 64))) == 1;
			}
		}
	}
}