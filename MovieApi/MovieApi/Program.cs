using Microsoft.EntityFrameworkCore;
using MovieApi.DBContext;
using MovieApi.Services;
using MovieApi.Services.Interface;
//using MovieApi.Storage;

namespace MovieApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build() ;
            builder.Services.AddControllers();
            builder
                .Services
                .AddCors(
                        (options) =>
                            {
                            options.AddPolicy(
                                "AllowReactOrigin",
                                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                            }
                        );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MovieServiceContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ILibraryService, LibraryService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IVideoService, VideoService>();
            var app = builder.Build();

            app.UseCors("AllowReactOrigin");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
