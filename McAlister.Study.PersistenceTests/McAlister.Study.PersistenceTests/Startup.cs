using AutoMapper;
using McAlister.Study.PersistenceTests.Business;
using McAlister.Study.PersistenceTests.Definitions.Interfaces;
using McAlister.Study.PersistenceTests.Entities;
using McAlister.Study.PersistenceTests.MapperProfiles;
using McAlister.Study.PersistenceTests.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace McAlister.Study.PersistenceTests
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
            var conStr = Configuration.GetConnectionString("DevConStr");
            services.AddDbContext<WideWorldImportersContext>(options =>
            {
                options.UseSqlServer(conStr);
            });

            services.AddScoped<WideWorldImportersContext, WideWorldImportersContext>();
            services.AddTransient<IDbConnection>((cs) => new SqlConnection(conStr));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrders, Orders>();
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersistenceTests", Version = "v1" });
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                //Next two lines will map the following properties to each other: property_name -> PropertyName
                mc.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                mc.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                //By default, AutoMapper only recognizes public members
                mc.AddProfile<OrderProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddLogging(c =>
            {
                c.AddDebug();
                c.AddConsole();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("In Development environment");
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersistenceTests v1");
            });

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
