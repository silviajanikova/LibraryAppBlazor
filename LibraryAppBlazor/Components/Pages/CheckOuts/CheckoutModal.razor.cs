using LibraryAppBlazor.Data;
using LibraryAppBlazor.Models;
using LibraryAppBlazor.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LibraryAppBlazor.Components.Pages.CheckOuts
{
    public partial class CheckoutModal
    {
        [Inject]
        IRepository<Book> bookRepository { get; set; }

        [Inject]
        IRepository<Member> memberRepository { get; set; }

        [Inject]
        IDbContextFactory<LibraryAppContext> dbContextFactory { get; set; }

        public Book BookToCheckout { get; set; } = new();

        public Guid SelectedMemberId { get; set; } = Guid.Empty;

        public List<Member> MemberList { get; set; } = new();

        public CheckOutRecord CheckoutData { get; set; } = new();

        [Parameter]
        public EventCallback onBookCheckout { get; set; }

        protected override void OnInitialized()
        {
            MemberList = memberRepository.List();
            CheckoutData.CheckOutDate = DateTime.Now;
        }

        public void SelectBookToCheckout(Guid id) {
            BookToCheckout = bookRepository.GetById(id);
        }

        public void BookCheckout()
        {
            if (BookToCheckout.IsAvailable)
            {
                CreateCheckoutRecord();

                // update book
                BookToCheckout.IsAvailable = false;
                bookRepository.Edit(BookToCheckout);

                onBookCheckout.InvokeAsync();
            }
        }

        public void CreateCheckoutRecord()
        {
            var member = memberRepository.GetById(SelectedMemberId);
            CheckoutData.Id = Guid.NewGuid();

            using (var context = dbContextFactory.CreateDbContext())
            {
                context.Book.Attach(BookToCheckout);
                CheckoutData.Book = BookToCheckout;
                context.Member.Attach(member);
                CheckoutData.Member = member;

                context.CheckOutRecord.Add(CheckoutData);
                context.SaveChanges();
            }
        }
    }
}
