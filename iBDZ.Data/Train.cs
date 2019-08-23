using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iBDZ.Data
{
	public enum TrainState
	{
		Inactive = 0,
		InTransit = 1,
		Composing = 2,
		Boarding = 3,
		Delayed = 4,
		ChangingComposition = 5,
	}

	public class Train
	{
		public int Id { get; set; }

		[Required]
		public LocomotiveData Locomotive { get; set; }

		public IList<TrainCar> Composition { get; set; }

		public IList<CompositionChange> CompositionChanges { get; set; }

		[Required]
		public Route Route { get; set; }

		[Required]
		public TimeSpan Delay { get; set; }

		[Required]
		public TrainState State { get; set; }
	}
}
