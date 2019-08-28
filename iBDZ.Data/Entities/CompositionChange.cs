using System.ComponentModel.DataAnnotations;

namespace iBDZ.Data
{
	public enum Action {
		Add = 1, 
		Remove = 0
	}

	public class CompositionChange
	{
		// Id (Int32)
		public int Id { get; set; }

		// Train (foreign key, train to have its composition changed)
		[Required]
		public Train Train { get; set; }

		// TrainStation (foreign key, must be part of train's current route)
		[Required]
		public TrainStation TrainStation { get; set; }

		// Action (One of Add, Remove)
		[Required]
		public Action Action { get; set; }

		// TrainCarId (foreign key, if removing, must be part of train composition,
		//             if adding, must be new car)
		[Required]
		public TrainCar TrainCar {get;set;}
	}
}