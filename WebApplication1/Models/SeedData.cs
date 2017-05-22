using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;
            }

            Category pop = new Category { Name = "Pop" };
            Category rnb = new Category { Name = "R&B" };
            Category rock = new Category { Name = "Rock" };
            context.Categories.AddRange(pop, rnb, rock);
            context.SaveChanges();

            context.Songs.AddRange(
                new SongModel { Name = "Shape Of You", Price = 5, Artist = "Ed Sheeran", Category = pop },
                new SongModel { Name = "I Don't Wanna Live Forever", Price = 3, Artist = "ZAYN, Taylor Swift", Category = pop },
                new SongModel { Name = "We Don't Talk Anymore", Price = 4, Artist = "Charlie Puth, Selena Gomez", Category = pop },
                new SongModel { Name = "Until You", Price = 3.5m, Artist = "Eagles", Category = rock },
                new SongModel { Name = "Hotel California", Price = 2.5m, Artist = "Ed Sheeran", Category = rock },
                new SongModel { Name = "Forever & One", Price = 4.5m, Artist = "Helloween", Category = rock },
                new SongModel { Name = "No Promises", Price = 7, Artist = "Shayne Ward", Category = rock },
                new SongModel { Name = "Starboy", Price = 2, Artist = "The Weeknd, Daft Punk", Category = rnb },
                new SongModel { Name = "Apologize", Price = 1.5m, Artist = "Timbaland, OneRepublic", Category = rnb },
                new SongModel { Name = "Freedom", Price = 4.25m, Artist = "Beyoncé, Kendrick Lamar", Category = rnb });
            context.SaveChanges();
        }
    }
}
