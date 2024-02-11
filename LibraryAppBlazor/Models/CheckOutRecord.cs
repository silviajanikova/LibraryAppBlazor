using System.ComponentModel.DataAnnotations;

namespace LibraryAppBlazor.Models
{
    public class CheckOutRecord
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Pole čitateľ je povinné!")]
        public Member Member { get; set; }
        public Book Book { get; set; }
        [Required(ErrorMessage = "Pole dátum výpožičky je povinné!")]
        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
