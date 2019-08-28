using System;
using System.Collections.Generic;
using System.Text;

namespace iBDZ.Data
{
	public class TrainDelay
	{
		public int Id { get; set; }

		public Train Train { get; set; }

		public TrainStation TrainStation { get; set; }

		public TimeSpan Delay { get; set; }
	}
}
