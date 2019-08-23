using iBDZ.Data;
using iBDZ.Db;
using iBDZ.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBDZ.Seeding
{
	public class RouteSeeder : ISeeder
	{
		public async Task SeedAsync(IServiceProvider serviceProvider)
		{
			var db = (iBDZDbContext)serviceProvider.GetService(typeof(iBDZDbContext));

			SeedTrainStations(db);
			SeedRoutes(db);

			await db.SaveChangesAsync();

			return;
		}

		private void SeedTrainStations(iBDZDbContext db)
		{
			CSV csv = CSV.ReadFile(@"C:\Users\User\source\repos\iBDZ_v2\iBDZ.Seeding\Data\TrainStationData.csv", " | ", true);

			List<string> trainStationNames = db.TrainStations.Select(x => x.Name).ToList();
			foreach (var line in csv.Data)
			{
				if (!trainStationNames.Contains(line["Name"]))
				{
					db.TrainStations.Add(new TrainStation()
					{
						Name = line["Name"],
						Latitude = double.Parse(line["Latitude"]),
						Longitute = double.Parse(line["Longitude"]),
					});
				}
			}
		}

		private void SeedRoutes(iBDZDbContext db)
		{
			CSV csv = CSV.ReadFile(@"C:\Users\User\source\repos\iBDZ_v2\iBDZ.Seeding\Data\Routes.txt", "|", true);

			List<string> routeNames = db.Routes.Select(x => x.Name).ToList();
			foreach (var line in csv.Data)
			{
				if (!routeNames.Contains(line["Name"]))
				{
					Route route = new Route() { Name = line["Name"] };
					db.Routes.Add(route);

					string[] stations = line["Stations"].Split(',');
					for (int i = 0; i < stations.Length - 1; i++)
					{
						AddConnectingLine(db, stations[i], stations[i + 1]);
						AddRouteTrainStation(db, route, stations, i);
					}
					AddRouteTrainStation(db, route, stations, stations.Length - 1);
				}
			}
		}

		private void AddRouteTrainStation(iBDZDbContext db, Route route, string[] stations, int i)
		{
			RouteTrainStation routeTrainStation = new RouteTrainStation()
			{
				TrainStationId = db.TrainStations.First(x => x.Name == stations[i]).Id,
				RouteId = route.Id,
				Order = (uint)i,
			};
			db.RouteTrainStations.Add(routeTrainStation);
		}

		private void AddConnectingLine(iBDZDbContext db, string station1, string station2)
		{
			ConnectingLine res = new ConnectingLine()
			{
				AverageSpeed = 1,
				Electrified = true,
				Node1 = db.TrainStations.First(x => x.Name == station1),
				Node2 = db.TrainStations.First(x => x.Name == station2)
			};
			db.ConnectingLines.Add(res);
		}
	}
}
