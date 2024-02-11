using LibraryAppBlazor.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var contextFactory = serviceProvider.GetService<IDbContextFactory<LibraryAppContext>> ();
            
            using(var context = contextFactory.CreateDbContext())
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null LibraryAppContext");
                }

                if (!context.Book.Any())
                {
                    context.Book.AddRange(CreateBookData());

                }
                if (!context.Member.Any())
                {
                    context.Member.AddRange(CreateReaderData());
                }


                context.SaveChanges();
            }

        }

        public static List<Book> CreateBookData()
        {
            var books = new List<Book>();
            books.AddRange(new[]
            {
                new Book
                {
                    Id = new Guid(),
                    Title = "Harry Potter a Kameň mudrcov",
                    Author = "J.K.Rowling",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = new Guid(),
                    Title = "Zabiť vtáča robáka",
                    Author = "Harper Lee",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = new Guid(),
                    Title = "1984",
                    Author = "George Orwell",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = new Guid(),
                    Title = "Veľký Gatsby",
                    Author = "F. Scott Fitzgerald",
                    IsAvailable = true,
                },

                new Book
                {
                    Id = new Guid(),
                    Title = "Pýcha a predsudok",
                    Author = "Jane Austen",
                    IsAvailable = true,
                }
            });

            return books;
        }

        public static List<Member> CreateReaderData()
        {
            var readers = new List<Member>();
            readers.AddRange(new[]
            {
                new Member
                {
                    Id = new Guid(),
                    IdentityCardNumber = "123456789",
                    Firstname = "Ján",
                    Surname = "Novák",
                    DateOfBirth = new DateOnly(1985,3,12)
                },
                new Member
                {
                    Id = new Guid(),
                    IdentityCardNumber = "987654321",
                    Firstname = "Eva",
                    Surname = "Svobodová",
                    DateOfBirth = new DateOnly(1990,6,25)
                },
                new Member
                {
                    Id = new Guid(),
                    IdentityCardNumber = "456123789",
                    Firstname = "Peter",
                    Surname = "Kováč",
                    DateOfBirth = new DateOnly(1982,8,8)
                },
                new Member
                {
                    Id = new Guid(),
                    IdentityCardNumber = "789456123",
                    Firstname = "Zuzana",
                    Surname = "Horváthová",
                    DateOfBirth = new DateOnly(1976,1,17)
                },
                new Member
                {
                    Id = new Guid(),
                    IdentityCardNumber = "321654987",
                    Firstname = "Marta",
                    Surname = "Ďuricová",
                    DateOfBirth = new DateOnly(1995,12,3)
                }
            });

            return readers;
        }
    }
}
