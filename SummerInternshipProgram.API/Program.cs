
using NLog.Extensions.Logging;
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

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();



            //Dependency Injection of Services and Helpers
            builder.Services.AddScoped<ILogHelper, LogHelper>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
