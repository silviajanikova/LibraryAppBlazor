using LibraryAppBlazor.Data;
using LibraryAppBlazor.Models;
using LibraryAppBlazor.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Components.Pages.CheckOuts
{
    public partial class CheckOuts
    {
        [Inject]
        protected IRepository<CheckOutRecord> checkoutRepository { get; set; }

        [Inject]
        protected IDbContextFactory<LibraryAppContext> dbContextFactory { get; set; }

        public IEnumerable<CheckOutRecord> CheckoutList { get; set; } = new List<CheckOutRecord>();

        protected override void OnInitialized()
        {
            CheckoutList = GetCheckOuts();
        }

        public List<CheckOutRecord> GetCheckOuts()
        {
            var checkouts = new List<CheckOutRecord>();
            using (var context = dbContextFactory.CreateDbContext())
            {
                checkouts = context.CheckOutRecord
                    .Include(ch => ch.Book)
                    .Include(ch => ch.Member)
                    .AsNoTracking()
                    .ToList();
            }

            return checkouts;
        }
    }
}
