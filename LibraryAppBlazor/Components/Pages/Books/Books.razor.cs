using LibraryAppBlazor.Components.Pages.CheckOuts;
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
        IDbContextFactory<LibraryAppContext> dbContextFactory { get; set; }

        public IEnumerable<Book> BookList { get; set; } = new List<Book>();

        public CheckOutRecord CheckoutData { get; set; } = new();

        public Guid SelectedMemberId { get; set; } = Guid.Empty;

        public Book BookToCheckout { get; set; } = new();

        private CheckoutDetailsModal childDetailsModal { get; set; }

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
            bookRepository.Delete(book);

            NavManager.NavigateTo("/Books");
            UpdateBookList();
        }

        // checkout modal
        public void OpenCheckoutModal(Guid id)
        {
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

                UpdateBookList();
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

        // detail modal
        public void OpenCheckoutDetailModal(Guid id) {
            childDetailsModal.GetCheckOutByBookId(id);
        }

        public void UpdateBookList() {
            BookList = bookRepository.List();
        }
    }
}
