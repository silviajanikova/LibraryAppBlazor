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

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<CheckOutRecord> CheckOutRecord { get; set; } = default!;

    }
}
