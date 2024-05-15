
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using SummerInternshipProgram.API.Data;
using SummerInternshipProgram.API.Helpers.Implementation;
using SummerInternshipProgram.API.Helpers.Interface;

namespace SummerInternshipProgram.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            NLogProviderOptions nlpopts = new NLogProviderOptions
            {
                IgnoreEmptyEventId = true,
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true,
                ParseMessageTemplates = true,
                IncludeScopes = true,
                ShutdownOnDispose = true
            };
            //Get AppSettings, this should be a hiden file through Manage user secrets
            IConfiguration config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();



            //Add Logging to project
            builder.Services.AddLogging(
                    builder =>
                    {
                        builder.AddConsole().SetMinimumLevel(LogLevel.Trace);
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddNLog(nlpopts);
                    });
            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddNLog();
            });

            //Dependency Injection of Services and Helpers
            builder.Services.AddScoped<ILogHelper, LogHelper>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add Database
           
            builder.Services.AddDbContext<EmploymentDbContext>(options =>
               options.UseCosmos(config["CosmosDb,PrimaryKey"], config["CosmosDb,PrimaryKey"], config["CosmosDb,DatabaseName"]));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); 


            app.MapControllers();

            app.Run();
        }
    }
}
