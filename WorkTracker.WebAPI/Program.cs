
using Microsoft.EntityFrameworkCore;
using WorkTracker.BusinessLogic;
using WorkTracker.Data.DAL;
using WorkTracker.Data.Model;

namespace WorkTracker.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IWorkBusinessLogic, WorkBusinessLogic>();
            builder.Services.AddScoped<IWorkRepository, WorkRepository>();
            builder.Services.AddAutoMapper(typeof(BusinessLogic.Mapper).Assembly);

            // get connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<WorkContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });


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

            app.UseCors("AllowAnyOrigin");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
