using System.ComponentModel.DataAnnotations;

namespace MvcAjax.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [CustomEmailValidation]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Age")]
        [Required]
        public int? Age { get; set; }
    }
}