using iBDZ.Data.DataValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data
{
	public class Ticket
	{
		// Id (Int32)
		public int Id { get; set; }
		
		// IssuedTo (string, First and last names of person ticket was issued to)
		[Required]
		[StringLength(30)]
		public string IssuedTo { get; set; }
		
		// IssuedToEGN (string, EGN of person ticket was issued to, must be 10 chars long, must be valid EGN)
		[EGN]
		public string IssuedToEGN { get; set; }
		
		// TrainId (foreign key)
		[Required]
		public Train Train { get; set; }
		
		// TimeOfPurchase (DateTime, when the ticket was purchased)
		[Required]
		public DateTime TimeOfPurchase { get; set; }

		// StartStation (foreign key, the station on which the users gets onto the train)
		[Required]
		public TrainStation StartStation { get; set; }
		
		// EndStation (foreign key, the station on which the user must get off the train)
		[Required]
		public TrainStation EndStation { get; set; }
		
		// ValidUntil (DateTime, date and time after which this ticket cannot be assigned a seat or used)
		[Required]
		public DateTime ValidUnitl { get; set; }
		
		// TrainCarNumber (uint, must match a TrainCar's Order, may be null to show it will be assigned later)
		public uint? TrainCarNumber { get; set; }

		// SeatNumber (uint, may be null to indicate that the seat will be assigned later)
		public uint? SeatNumber { get; set; }

		// PriceLevs (Double)
		[Required]
		[Range(1, 350)]
		public double PriceLevs { get; set; }
	}
}
