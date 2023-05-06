using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using RetailManagement.Utils;
using Timelogger.Repos;
using Timelogger.Services;
using System;
using Timelogger.Api.Handlers;
using Timelogger.Model;

namespace Timelogger.Api
{
    public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITimeslotRepository, TimeslotRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITimeslotService, TimeslotService>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IProjectHandler, ProjectHandler>();
            services.AddScoped<ITimeslotHandler, TimeslotHandler>();
            services.AddScoped<ICustomerHandler, CustomerHandler>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen();

            services.AddControllers(options =>
            {
                options.InputFormatters.Insert(0, JsonPatchInputFormatterExt.GetJsonPatchInputFormatter());
            }).AddNewtonsoftJson();

            // Add framework services.
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			services.AddMvc(options => options.EnableEndpointRouting = false);

			if (_environment.IsDevelopment())
			{
				services.AddCors();
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "openapi/{documentName}/openapi.json";
            })
               .UseSwaggerUI(c =>
               {
                   // set route prefix to openapi, e.g. http://localhost:8080/openapi/index.html
                   c.RoutePrefix = "openapi";

                   //TODO: Or alternatively use the original OpenAPI contract that's included in the static files
                   c.SwaggerEndpoint("/api/openapi", "timelogger api");
               });


            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
			using (var scope = serviceScopeFactory.CreateScope())
			{
				SeedDatabase(scope);
			}
		}

		private static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ApiContext>();
			var testCustomer = new Customer
			{
				ID = Guid.NewGuid(),
				Name = "Test customer",
			};

			var testProject1 = new Project
			{
				ID = Guid.NewGuid(),
				Name = "test project",
				StartDate = DateTime.UtcNow,
				EndDate = DateTime.UtcNow.AddDays(7),
				Deadline = DateTime.UtcNow.AddDays(5),
				CustomerId = testCustomer.ID
            };

			var testSlot1 = new Timeslot
			{
				ID = Guid.NewGuid(),
				StartTime = DateTimeOffset.UtcNow,
				Duration = TimeSpan.FromMinutes(30),
				ProjectId = testProject1.ID
			};

            var testSlot2 = new Timeslot
            {
                ID = Guid.NewGuid(),
                StartTime = DateTimeOffset.UtcNow.AddHours(3),
                Duration = TimeSpan.FromMinutes(60),
                ProjectId = testProject1.ID
            };

            context.Customers.Add(testCustomer);
            context.Projects.Add(testProject1);
            context.Timeslots.Add(testSlot1);
            context.Timeslots.Add(testSlot2);

            context.SaveChanges();
		}
	}
}