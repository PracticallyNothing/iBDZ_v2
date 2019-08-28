using System;
using System.ComponentModel.DataAnnotations;

namespace iBDZ.Data
{
	public class ConnectingLine
	{
		public int Id { get; set; }

		[Required]
		public TrainStation Node1 { get; set; }

		[Required]
		public TrainStation Node2 { get; set; }

		public bool Electrified { get; set; }

		[Range(0.5, 1)]
		public double AverageSpeed { get; set; }
	}
}