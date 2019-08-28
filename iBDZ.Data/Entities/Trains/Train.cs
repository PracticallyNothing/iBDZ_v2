using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using iBDZ.Data.Statistics;

namespace iBDZ.Data
{
	public enum TrainState
	{
		/// <summary> The train is in the depot and is awaiting its next route. </summary>
		Inactive = 0,
		/// <summary> The train is on its way to the next station in its route. </summary>
		InTransit = 1,
		/// <summary> The train is currently being assembled at the starting station. </summary>
		Composing = 2,
		/// <summary> The train is waiting for passengers to get on before continuing. </summary>
		Boarding = 3,
		/// <summary> The train is experiencing a delay due to [unfortunate circumstance]. </summary>
		Delayed = 4,
		/// <summary> The train is going through a <see cref="CompositionChange"/>. </summary>
		ChangingComposition = 5,
	}

	public class Train
	{
		public Train()
		{
			Delays = new List<TrainDelay>();
			Composition = new List<TrainCar>();
			CompositionChanges = new List<CompositionChange>();
		}

		public int Id { get; set; }

		[Required]
		public LocomotiveData Locomotive { get; set; }

		public List<TrainCar> Composition { get; set; }
			
		public List<CompositionChange> CompositionChanges { get; set; }
			
		public List<TrainDelay> Delays { get; set; }

		[Required]
		public DateTime TimeOfDeparture { get; set; }

		[Required]
		public Route Route { get; set; }
	}
}
