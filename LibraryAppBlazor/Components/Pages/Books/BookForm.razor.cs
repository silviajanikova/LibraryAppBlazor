using LibraryAppBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace LibraryAppBlazor.Components.Pages.Books
{
    public partial class BookForm
    {
        public Book BookData { get; set; } = new();

        [Parameter]
        public string BookId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

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
                BookData.Id = Guid.NewGuid();
                BookData.IsAvailable = true;
                bookRepository.Add(BookData);

                NavManager.NavigateTo("/Books");
            }

            // edit existing book
            else
            {
                bookRepository.Edit(BookData);

                NavManager.NavigateTo("/Books");
            }
        }
    }
}
