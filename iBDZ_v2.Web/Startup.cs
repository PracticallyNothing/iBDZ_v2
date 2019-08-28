using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;
using iBDZ.Db;
using iBDZ.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using iBDZ.Services;
using iBDZ.Seeding;
using iBDZ.Data.BindingModels;
using iBDZ.Web.AutoMapper;
using iBDZ.Services.Contracts;

namespace iBDZ.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			var mapperConfig = new MapperConfiguration(conf =>
			{
				conf.AddProfile<MappingProfile>();
			});

			services.AddSingleton(mapperConfig.CreateMapper());
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<IMapService, MapService>();
			services.AddTransient<ITrainsService, TrainsService>();

			services.AddDbContext<iBDZDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly("iBDZ.Web"))
			);
			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<iBDZDbContext>()
				.AddDefaultTokenProviders();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapAreaRoute(
				  name: "Admin",
				  areaName: "Administration",
				  template: "Admin/{controller=Home}/{action=Index}"
				);

				routes.MapAreaRoute(
					name: "TicketIssuing",
					areaName: "TicketIssuing",
					template: "TicketIssuing/{controller=Home}/{action=Index}"
				);

				routes.MapAreaRoute(
					name: "Identity",
					areaName: "Identity",
					template: "Identity/{controller}/{action}"
				);

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}"
				);
			});

			SeedDb(serviceProvider);
		}

		private static void SeedDb(IServiceProvider serviceProvider)
		{
			var rolesTask = new UserRolesSeeder().SeedAsync(serviceProvider);
			rolesTask.Wait();

			var routeTask = new RouteSeeder().SeedAsync(serviceProvider);
			routeTask.Wait();

			var trainDataTask = new TrainDataSeeder().SeedAsync(serviceProvider);
			trainDataTask.Wait();
		}
	}
}
