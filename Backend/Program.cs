using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackerDeFavorisApi.Models;
using TrackerDeFavorisApi.Services; // Assurez-vous que le namespace de votre service Omdb est bien importé

namespace TrackerDeFavorisApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<UserContext>();
        builder.Services.AddDbContext<FilmContext>();
        builder.Services.AddDbContext<FavorisContext>();

        builder.Services.AddHttpClient<OmdbService>();

        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
