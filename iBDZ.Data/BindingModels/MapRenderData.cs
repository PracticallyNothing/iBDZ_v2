using System;
using System.Collections.Generic;
using System.Text;

namespace iBDZ.Data.BindingModels
{
	public class StationModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}

	public class ConnectingLineModel {
		public int Node1Id { get; set; }
		public int Node2Id { get; set; }
	}
	
    public class MapRenderData
    {
		public MapRenderData()
		{
			Stations = new List<StationModel>();
			ConnectingLines = new List<ConnectingLineModel>();
		}

		public List<StationModel> Stations { get; set; }
		public List<ConnectingLineModel> ConnectingLines { get; set; }
    }
}
