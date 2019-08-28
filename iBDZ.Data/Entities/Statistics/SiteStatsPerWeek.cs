using System.ComponentModel.DataAnnotations;

namespace iBDZ.Data.Statistics
{
	public class SiteStatsPerWeek
	{
		public int Id { get; set; }

		[Range(0, 51)]
		public uint Week { get; set; }
		public uint Year { get; set; }

		public uint NumVisits { get; set; }

		public uint HighestSimultaneousUserCount { get; set; }
	}
}
