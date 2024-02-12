using LibraryAppBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Data
{
    public class LibraryAppContext : DbContext
    {
        public IConfiguration _config { get; set; }

        public LibraryAppContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Harry Potter a Kameň mudrcov",
                    Author = "J.K.Rowling",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Zabiť vtáča robáka",
                    Author = "Harper Lee",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "1984",
                    Author = "George Orwell",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Veľký Gatsby",
                    Author = "F. Scott Fitzgerald",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Pýcha a predsudok",
                    Author = "Jane Austen",
                    IsAvailable = true,
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = Guid.NewGuid(),
                    IdentityCardNumber = "123456789",
                    Firstname = "Ján",
                    Surname = "Novák",
                    DateOfBirth = new DateOnly(1985, 3, 12)
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    IdentityCardNumber = "987654321",
                    Firstname = "Eva",
                    Surname = "Svobodová",
                    DateOfBirth = new DateOnly(1990, 6, 25)
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    IdentityCardNumber = "456123789",
                    Firstname = "Peter",
                    Surname = "Kováč",
                    DateOfBirth = new DateOnly(1982, 8, 8)
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    IdentityCardNumber = "789456123",
                    Firstname = "Zuzana",
                    Surname = "Horváthová",
                    DateOfBirth = new DateOnly(1976, 1, 17)
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    IdentityCardNumber = "321654987",
                    Firstname = "Marta",
                    Surname = "Ďuricová",
                    DateOfBirth = new DateOnly(1995, 12, 3)
                }
            );
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<CheckOutRecord> CheckOutRecord { get; set; } = default!;

    }
}
