using iBDZ.Data.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace iBDZ.Services.Contracts
{
	public interface ITrainsService
	{
		/// <summary>
		/// Load all timetable data.
		/// </summary>
		/// <returns></returns>
		List<ShortTrainInfo> GetTimetableInfo();

		/// <summary> Get extra details for a given train. </summary>
		/// <param name="trainId">The id of the train in question.</param>
		/// <returns>Details about the train.</returns>
		TrainDetails GetTrainDetails(int trainId);

		/// <summary> Calculates the schedule of a train. </summary>
		/// <param name="trainId"></param>
		/// <returns></returns>
		List<ScheduleItem> CalculateSchedule(int trainId);
	}
}
