using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Database
{
    public class RentalContext : DbContext
    {

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E => 
            {
                E.Property("CreatedDate").CurrentValue = DateTime.Now;
            });

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedDate").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookRating> BooksRatings { get; set; }

        public DbSet<RentBook> RentBooks { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieRating> MovieRatings { get; set; }

        public DbSet<RentMovie> RentMovies { get; set; }

        public RentalContext(DbContextOptions<RentalContext> options) : base(options)
        {
        }
    }
}