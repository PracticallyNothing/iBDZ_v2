using System;
using System.Collections.Generic;
using System.Text;

namespace iBDZ.Data.BindingModels
{
    public class ShortTrainInfo
    {
		/// <summary>
		/// The train's Id which this short info corresponds to. 
		/// Used to show further details for the train.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Name of the route the train is taking.
		/// </summary>
		public string Route { get; set; }

		public DateTime TimeOfArrival { get; set; }
		public DateTime TimeOfDeparture { get; set; }

		/// <summary>
		/// Either the departed station if the train is in transit or
		/// the place where the train is currently stopped.
		/// </summary>
		public string CurrOrPrevStation { get; set; }
		
		/// <summary> The next station in the train's path. </summary>
		public string NextStation { get; set; }

		/// <summary> The train's current state. </summary>
		/// <seealso cref="TrainState"/>
		public TrainState State { get; set; }

		/// <summary>
		/// A 0 to 1 double that represents how much of the train is already booked.
		/// </summary>
		public double BookedPercentage { get; set; }
    }
}
