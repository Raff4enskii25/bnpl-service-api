using Bnpl.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Serilog;


namespace Bnpl.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddSingleton<TimeProvider, MoscowTimeProvider>();

                builder.Services.AddSerilog((services, configuration) => configuration
                    .ReadFrom.Configuration(builder.Configuration)
                    .ReadFrom.Services(services));

                builder.Services.AddDbContext<Data.AppDbContext>(options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

                builder.Services.AddControllers();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                app.UseSerilogRequestLogging();

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

            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }

            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
