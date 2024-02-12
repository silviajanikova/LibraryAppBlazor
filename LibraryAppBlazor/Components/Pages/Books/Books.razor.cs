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

        public IEnumerable<Book> BookList { get; set; } = new List<Book>();

        private CheckoutDetailsModal childDetailsModal { get; set; }

        private CheckoutModal childCheckoutModal { get; set; }

        protected override void OnInitialized()
        {
            BookList = bookRepository.List();
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
            childCheckoutModal.SelectBookToCheckout(id);
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
