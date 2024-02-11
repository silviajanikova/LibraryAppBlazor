using System.ComponentModel.DataAnnotations;

namespace LibraryAppBlazor.Models
{
    public class Member
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Pole číslo OP je povinné!")]
        public string IdentityCardNumber { get; set; }
        [Required(ErrorMessage = "Pole meno je povinné!")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Pole priezvisko je povinné!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Pole dátum narodenia je povinné!")]
        public DateOnly DateOfBirth { get; set; }
    }
}
