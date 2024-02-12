using LibraryAppBlazor.Data;
using LibraryAppBlazor.Models;
using LibraryAppBlazor.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Components.Pages.CheckOuts
{
    public partial class CheckoutDetailsModal
    {
        [Inject]
        IDbContextFactory<LibraryAppContext> dbContextFactory { get; set; }

        [Inject]
        protected IRepository<CheckOutRecord> checkoutRepository { get; set; }

        [Inject]
        IRepository<Book> bookRepository { get; set; }

        public CheckOutRecord CheckoutDetails { get; set; } = new();

        [Parameter]
        public EventCallback onReturnButtonClicked { get; set; }

        protected override void OnInitialized()
        {
            CheckoutDetails.Book = new Book();
            CheckoutDetails.Member = new Member();
        }

        public void GetCheckOutByBookId(Guid bookId)
        {
            var checkout = new CheckOutRecord();

            using (var context = dbContextFactory.CreateDbContext())
            {
                checkout = context.CheckOutRecord
                    .Include(ch => ch.Book)
                    .Include(ch => ch.Member)
                    .FirstOrDefault(ch => ch.Book.Id == bookId);
            }
            CheckoutDetails = checkout;
            StateHasChanged();
        }

        public void ReturnBook() { 
            CheckoutDetails.ReturnDate = DateTime.Now;
            checkoutRepository.Edit(CheckoutDetails);

            CheckoutDetails.Book.IsAvailable = true;
            bookRepository.Edit(CheckoutDetails.Book);

            onReturnButtonClicked.InvokeAsync();
        }
    }
}
