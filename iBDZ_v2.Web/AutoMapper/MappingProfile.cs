using AutoMapper;
using iBDZ.Data;
using iBDZ.Data.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBDZ.Web.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile() : base()
		{
			CreateMap<TrainStation, StationModel>();

			CreateMap<ConnectingLine, ConnectingLineModel>()
				.ForMember(x => x.Node1Id, opts => { opts.MapFrom(x => x.Node1.Id); })
				.ForMember(x => x.Node2Id, opts => { opts.MapFrom(x => x.Node2.Id); });

			CreateMap<TrainCarData, TrainCarInfo>();

			CreateMap<TrainCar, TrainCarInfo>()
				.IncludeMembers(x => x.Data)
				.ForMember(x => x.ModelName, opts => opts.MapFrom(x => x.Data.Name))
			;

			CreateMap<LocomotiveData, LocomotiveInfo>();

			CreateMap<Train, ShortTrainInfo>();

			CreateMap<Train, TrainDetails>()
				.ForMember(x => x.Locomotive, opts => { opts.MapFrom(x => x.Locomotive); })
				.ForMember(x => x.Composition, opts => { opts.MapFrom(x => x.Composition); })
				;
		}
	}
}
