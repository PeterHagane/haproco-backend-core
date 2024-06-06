
namespace haproco_backend_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("localhost", policyBuilder =>
                    {
                        policyBuilder.WithOrigins("http://localhost:5173");
                        policyBuilder.WithOrigins("https://localhost:5173");
                        policyBuilder.AllowAnyHeader();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowCredentials();
                    });

                    options.AddPolicy("react-haproco", policyBuilder =>
                    {
                        policyBuilder.WithOrigins("http://haproco.com");
                        policyBuilder.WithOrigins("https://haproco.com");
                        policyBuilder.AllowAnyHeader();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowCredentials();
                    });

                    options.AddPolicy("react-haproco-vercel", policyBuilder =>
                    {
                        policyBuilder.WithOrigins("http://haproco-frontend-react.vercel.app");
                        policyBuilder.WithOrigins("https://haproco-frontend-react.vercel.app");
                        policyBuilder.AllowAnyHeader();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowCredentials();
                    });
                }
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("localhost");
            app.UseCors("react-haproco");
            app.UseCors("react-haproco-vercel");

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();


            app.Run();
        }
    }
}
