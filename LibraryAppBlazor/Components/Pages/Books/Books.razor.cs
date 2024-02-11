using LibraryAppBlazor.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryAppBlazor.Components.Pages.Books
{
    public partial class Books
    {
        public IEnumerable<Book> BookList { get; set; } = new List<Book>();

        public Books()
        {
        }

        protected override void OnInitialized()
        {
            BookList = bookRepository.List();
        }

        public void DeleteBook(Guid id)
        {
            var book = bookRepository.GetById(id);
            var a = bookRepository.Delete(book);

            NavManager.NavigateTo("/Books");
            BookList = bookRepository.List();

        }

    }
}
