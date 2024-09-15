
using Microsoft.EntityFrameworkCore;
using haproco_backend_core.Data;
using DotNetEnv;


namespace haproco_backend_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var dbconnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<DataContext>(
            //    options => options.UseSqlServer(

            //    )
            //);
            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(
                options => options.UseNpgsql()
            );


            DotNetEnv.Env.Load();
            var dev = System.Environment.GetEnvironmentVariable("DEVCLIENT");

            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("localhost", policyBuilder =>
                    {
                        policyBuilder.WithOrigins(dev != null ? $"http{dev}" : "");
                        policyBuilder.WithOrigins(dev != null ? $"https{dev}" : "");
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

                    options.AddPolicy("react-haproco", policyBuilder =>
                    {
                        policyBuilder.WithOrigins("http://dev.haproco.com");
                        policyBuilder.WithOrigins("https://dev.haproco.com");
                        policyBuilder.AllowAnyHeader();
                        policyBuilder.AllowAnyMethod();
                        policyBuilder.AllowCredentials();
                    });

                    options.AddPolicy("react-haproco-vercel", policyBuilder =>
                    {
                        policyBuilder.WithOrigins("http://peterhagane.com");
                        policyBuilder.WithOrigins("https://peterhagane.com");
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


            WeatherForecast.Map(app);
            TestTable.Map(app);

            app.Run();
        }
    }
}
