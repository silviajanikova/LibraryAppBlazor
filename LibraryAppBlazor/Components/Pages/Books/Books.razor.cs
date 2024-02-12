using LibraryAppBlazor.Data;
using LibraryAppBlazor.Models;
using LibraryAppBlazor.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Components.Pages.Books
{
    public partial class Books
    {
        [Inject]
        IRepository<Book> bookRepository { get; set; }

        [Inject]
        IRepository<Member> memberRepository { get; set; }

        [Inject]
        IRepository<CheckOutRecord> checkoutRepository { get; set; }

        [Inject]
        IDbContextFactory<LibraryAppContext> dbContextFactory { get; set; }

        public IEnumerable<Book> BookList { get; set; } = new List<Book>();

        public CheckOutRecord CheckoutData { get; set; } = new();

        public Guid SelectedMemberId { get; set; } = Guid.Empty;

        public Book BookToCheckout { get; set; } = new();

        public List<Member> MemberList { get; set; } = new();


        protected override void OnInitialized()
        {
            BookList = bookRepository.List();
            MemberList = memberRepository.List();
            CheckoutData.CheckOutDate = DateTime.Now;
        }

        public void DeleteBook(Guid id)
        {
            var book = bookRepository.GetById(id);
            var a = bookRepository.Delete(book);

            NavManager.NavigateTo("/Books");
            BookList = bookRepository.List();

        }

        public void OpenCheckoutModal(Guid id)
        {
            BookToCheckout = bookRepository.GetById(id);
        }

        public void BookCheckout()
        {
            if (BookToCheckout.IsAvailable)
            {
                CreateCheckoutrecord();

                // update book
                BookToCheckout.IsAvailable = false;
                bookRepository.Edit(BookToCheckout);

                BookList = bookRepository.List();
            }
        }

        public void CreateCheckoutrecord()
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
