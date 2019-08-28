using iBDZ.Data.BindingModels;
using iBDZ.Db;
using iBDZ.Services.Contracts;
using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace iBDZ.Services
{
	public class MapService : IMapService
	{
		private readonly iBDZDbContext db;
		private readonly IMapper mapper;

		public MapService(iBDZDbContext db, IMapper mapper)
		{
			this.db = db;
			this.mapper = mapper;
		}

		public MapRenderData GetRenderingData()
		{
			return new MapRenderData()
			{
				Stations = db.TrainStations.Select(x => mapper.Map<StationModel>(x)).ToList(),
				ConnectingLines = 
					db.ConnectingLines
						.Include(x => x.Node1)
						.Include(x => x.Node2)
						.Select(x => mapper.Map<ConnectingLineModel>(x))
						.ToList()
			};
		}
	}
}
