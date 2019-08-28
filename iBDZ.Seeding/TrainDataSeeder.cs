using iBDZ.Data;
using iBDZ.Db;
using iBDZ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBDZ.Seeding
{
	public class TrainDataSeeder : ISeeder
	{
		public async Task SeedAsync(IServiceProvider serviceProvider)
		{
			iBDZDbContext db = (iBDZDbContext)serviceProvider.GetService(typeof(iBDZDbContext));
			SeedTrainCarData(db);
			SeedLocomotivesData(db);
			await db.SaveChangesAsync();
		}

		private void SeedLocomotivesData(iBDZDbContext db)
		{
			CSV data = CSV.ReadFile(
				@"C:\Users\User\source\repos\iBDZ_v2\iBDZ.Seeding\Data\Locomotives.csv",
				"|",
				true
			);

			List<string> locomotiveNames = db.Locomotives.Select(x => x.Name).ToList();
			foreach (var line in data.Data)
			{
				LocomotiveData locomotiveData = new LocomotiveData()
				{
					Name = line["Name"],
					Type = Enum.Parse<LocomotiveType>(line["Type"])
				};

				if (!locomotiveNames.Contains(locomotiveData.Name))
					db.Locomotives.Add(locomotiveData);
			}
		}

		private void SeedTrainCarData(iBDZDbContext db)
		{
			CSV data = CSV.ReadFile(
				@"C:\Users\User\source\repos\iBDZ_v2\iBDZ.Seeding\Data\TrainCars.csv",
				"|",
				true
			);

			List<string> trainCarNames = db.TrainCarData.Select(x => x.Name).ToList();
			foreach (var line in data.Data)
			{
				TrainCarData trainCarData = ReadTrainCarData(line);
				if (!trainCarNames.Contains(trainCarData.Name))
					db.TrainCarData.Add(trainCarData);
			}
		}

		private static TrainCarData ReadTrainCarData(CSVLine line)
		{
			TrainCarData trainCarData = new TrainCarData()
			{
				Class = Enum.Parse<TrainCarClass>(line["Class"]),
				Type = Enum.Parse<TrainCarType>(line["Type"]),
				Name = line["Name"],
				HasElectricity = bool.Parse(line["HasElectricity"]),
				HasAirConditioning = bool.Parse(line["HasAirConditioning"]),
				HasHeating = bool.Parse(line["HasHeating"]),
				NumSeatsPerDivison = int.Parse(line["NumSeatsPerDivision"]),
			};
			trainCarData.NumSeats =
				trainCarData.NumSeatsPerDivison
				* (trainCarData.Type == TrainCarType.Open ? 1 : 9);
			return trainCarData;
		}
	}
}
