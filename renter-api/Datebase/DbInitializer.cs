using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DbContext dbContext;

        public DbInitializer(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<Role> roles;
        private DbSet<User> users;
        private DbSet<Category> categories;
        private DbSet<Director> directors;
        private DbSet<Movie> movies;

        public async Task Seed()
        {
            roles = dbContext.Set<Role>();
            users = dbContext.Set<User>();
            categories = dbContext.Set<Category>();
            directors = dbContext.Set<Director>();
            movies = dbContext.Set<Movie>();


            await SeedRoles();
            await SeedUsers();
            await SeedCategories();
            await SeedDirectors();
            await SeedMovies();
        }

        private async Task SeedRoles()
        {
            if (roles == null || await roles.AnyAsync())
            {
                return;
            }

            roles.Add(new Role { Name = "User" });
            roles.Add(new Role { Name = "Employee" });
            roles.Add(new Role { Name = "Administrator" });

            dbContext.SaveChanges();
        }

        private async Task SeedUsers()
        {
            if (users == null || await users.AnyAsync())
            {
                return;
            }

            var userRole = await roles.FirstOrDefaultAsync(x => x.Name == "User");
            var moderatorRole = await roles.FirstOrDefaultAsync(x => x.Name == "Employee");
            var administratorRole = await roles.FirstOrDefaultAsync(x => x.Name == "Administrator");

            users.Add(new User
            {
                Role = userRole,
                UserName = "testUser",
                Password = PasswordHasher.CalculateHashedPassword("testUser"),
                Email = "testUser@tt.tt",

            });

            users.Add(new User
            {
                Role = moderatorRole,
                UserName = "testEmployee",
                Password = PasswordHasher.CalculateHashedPassword("testEmployee"),
                Email = "testEmployee@tt.tt",

            });

            users.Add(new User
            {
                Role = administratorRole,
                UserName = "testAdministrator",
                Password = PasswordHasher.CalculateHashedPassword("testAdministrator"),
                Email = "testAdministrator@tt.tt",

            });

            dbContext.SaveChanges();
        }

        private async Task SeedCategories()
        {
            if (categories == null || await categories.AnyAsync())
            {
                return;
            }
            categories.Add(new Category { Name = "Dramat" });
            categories.Add(new Category { Name = "Biograficzny" });
            categories.Add(new Category { Name = "Horror" });
            categories.Add(new Category { Name = "Thriller" });
            categories.Add(new Category { Name = "Sci-fi" });
            categories.Add(new Category { Name = "Fantasy" });
            dbContext.SaveChanges();
        }

        private async Task SeedDirectors()
        {
            if(directors == null || await directors.AnyAsync())
            {
                return;
            }

            directors.Add(new Director {
                Name = "Alexandre",
                Surname = "Bustillo"
            });

            directors.Add(new Director
            {
                Name = "Ethan",
                Surname = "Warren"
            });

            dbContext.SaveChanges();
        }

        private async Task SeedMovies()
        {
            if (movies == null || await movies.AnyAsync() || !(await categories.AnyAsync()) || !(await directors.AnyAsync()))
            {
                return;
            }

            var horror = await categories.FirstOrDefaultAsync(x => x.Name == "Horror");
            var drama = await categories.FirstOrDefaultAsync(x => x.Name == "Dramat");

            movies.Add(new Movie
            {
                Category = horror,
                Title = "Leatherface",
                DirectorId = 1,
                Duration = new TimeSpan(1, 35, 0),
                Description = "W zapomnianym przez świat szpitalu psychiatrycznym na amerykańskiej prowincji przetrzymywane są dzieci o zaburzeniach tak niebezpiecznych, że medycyna nie widzi dla nich ratunku. Przemoc fizyczna i psychiczna, gwałty i terror są tutejszą codziennością. Pewnego dnia grupa pacjentów ucieka, porywając młodą pielęgniarkę. Na ich czele stoi ten, który wkrótce krwawo zapracuje sobie na przydomek Leatherface. Tropem uciekinierów rusza ambitny i bezlitosny szeryf, który okaże się przeciwnikiem godnym najgorszego psychopaty."
            });

            movies.Add(new Movie
            {
                Category = drama,
                Title = "West of Her",
                DirectorId = 2,
                Duration = new TimeSpan(1, 30, 0),
                Description = "Samotny i zagubiony w życiu Dan (Ryan Caraway) zapisuje się do tajemniczej organizacji, która wysyła go w misję. Razem z enigmatyczną dziewczyną o imieniu Jane (Kelsey Siepser) ma on nocami zostawiać na ulicach amerykańskich miast tabliczki z zaszyfrowanym przesłaniem. Podczas podróży między mężczyzną a dziewczyną rodzi się intensywne uczucie. Dan jest jak otwarta książka, Jane jednak nie chce zbyt dużo mówić o sobie… Kameralne kino drogi w reżyserii dobrze rokującego debiutanta Ethana Warrena. Na zachód od niej to poetycka i pełna tajemnic opowieść, która mówi o potrzebie przynależności oraz poszukiwaniu sensu życia."
            });

            dbContext.SaveChanges();
        }

    }
}
