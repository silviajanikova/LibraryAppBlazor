using System.ComponentModel.DataAnnotations;

namespace LibraryAppBlazor.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Pole názov je povinné!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Pole autor je povinné!")]
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
    }
}
