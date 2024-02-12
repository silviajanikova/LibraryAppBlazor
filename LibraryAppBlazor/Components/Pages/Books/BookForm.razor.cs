using LibraryAppBlazor.Models;
using LibraryAppBlazor.Repository;
using Microsoft.AspNetCore.Components;

namespace LibraryAppBlazor.Components.Pages.Books
{
    public partial class BookForm
    {
        public Book BookData { get; set; } = new();

        [Inject]
        IRepository<Book> bookRepository { get; set; }

        [Parameter]
        public string BookId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // get data from existing book
            if (BookId != null)
            {
                BookData = bookRepository.GetById(Guid.Parse(BookId));

                if (BookData == null)
                {
                    NavManager.NavigateTo("/Error");
                }
            }

            await Task.CompletedTask;
        }

        private async Task Submit()
        {
            // add book
            if (BookId == null)
            {
                CreateBook();
            }

            // edit existing book
            else
            {
                bookRepository.Edit(BookData);
            }

            NavManager.NavigateTo("/Books");
        }

        public void CreateBook() {
            BookData.Id = Guid.NewGuid();
            BookData.IsAvailable = true;
            bookRepository.Add(BookData);
        }
    }
}
