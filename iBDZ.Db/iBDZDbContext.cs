using iBDZ.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iBDZ.Db
{
	public class iBDZDbContext : IdentityDbContext<User>
	{
		public iBDZDbContext(DbContextOptions<iBDZDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ConnectingLine>()
				.HasOne(x => x.Node1)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ConnectingLine>()
				.HasOne(x => x.Node2)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Ticket>()
				.HasOne(x => x.StartStation)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Ticket>()
				.HasOne(x => x.EndStation)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<RouteTrainStation>().HasKey(
				x => new { x.RouteId, x.TrainStationId }
			);

			builder.Entity<RouteTrainStation>()
				.HasOne(x => x.Route)
				.WithMany(x => x.RouteTrainStations)
				.HasForeignKey(x => x.RouteId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<RouteTrainStation>()
				.HasOne(x => x.TrainStation)
				.WithMany(x => x.TrainStationRoutes)
				.HasForeignKey(x => x.TrainStationId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}

		public DbSet<Ticket> Tickets { get; set; }

		public DbSet<Train> Trains { get; set; }
		public DbSet<TrainCar> TrainCars { get; set; }
		public DbSet<TrainCarData> TrainCarData { get; set; }
		public DbSet<LocomotiveData> Locomotives { get; set; }
		public DbSet<CompositionChange> CompositionChanges { get; set; }

		public DbSet<Route> Routes { get; set; }
		public DbSet<TrainStation> TrainStations { get; set; }
		public DbSet<ConnectingLine> ConnectingLines { get; set; }
		public DbSet<RouteTrainStation> RouteTrainStations { get; set; }
	}
}
