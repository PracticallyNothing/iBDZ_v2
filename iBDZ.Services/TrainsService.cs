using AutoMapper;
using iBDZ.Data.BindingModels;
using iBDZ.Db;
using iBDZ.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iBDZ.Services
{
	public class TrainsService : ITrainsService
	{
		private readonly iBDZDbContext db;
		private readonly IMapper mapper;

		public TrainsService(iBDZDbContext db, IMapper mapper)
		{
			this.db = db;
			this.mapper = mapper;
		}

		public List<ScheduleItem> CalculateSchedule(int trainId)
		{
			throw new NotImplementedException();
		}

		public List<ShortTrainInfo> GetTimetableInfo()
		{
			List<ShortTrainInfo> info = db.Trains
				.Include(x => x.Route)
				.Select(x => mapper.Map<ShortTrainInfo>(x))
				.ToList();

			info.Add(new ShortTrainInfo()
			{
				Id = 0,
				BookedPercentage = 0.7,
				CurrOrPrevStation = "Варна",
				NextStation = "Тополи",
				Route = "Варна - Горна Оряховица - София",
				State = Data.TrainState.InTransit
			});

			return info;
		}

		public TrainDetails GetTrainDetails(int trainId)
		{
			TrainDetails details = mapper.Map<TrainDetails>(db.Trains.Find(trainId));
			return details;
		}
	}
}
